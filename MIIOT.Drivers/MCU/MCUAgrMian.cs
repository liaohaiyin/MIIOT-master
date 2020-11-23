using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Threading.Tasks;
using MIIOT.Drivers.MCU;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MIIOT.Drivers
{

    /// <summary>
    /// 串口协议启动解析类，为静态或者单例模式类
    /// 所有的事件都采用委托，不采用事件操作
    /// 主要防止注册多个事件，导致数据重复
    /// </summary>
    public class MCUAgrMian
    {
        
        /// <summary>
        /// 此委托不受任何指令支配，他接受所有的协议上传，
        /// 到主界面用于记录日志，和显示状态栏。
        /// 表示所有的全局的委托。
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="args">协议返回参数</param>
        public EveryALLEventArgs OnEveryALLEventArgs;
        /// <summary>
        /// 串口打开时候 事件 是否打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="status"></param>
        public OpenSerialEventArgs OnOpenSerialEventArgs;

        /// <summary>
        /// 返回数据事件，交由Manager解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public ReturnDataEventArgs OnReturnDataEventArgs;


        /// <summary>
        /// 串口服务类
        /// </summary>
        private SerialPortService SerialPortService;
        private int baudRate = 115200;//默认值
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }
        private string portName = "COM9"; //默认值
        /// <summary>
        /// COM口
        /// </summary>
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }
        /// <summary>
        /// 接收到的流水号
        /// </summary>
        public byte ReceivSerSerNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 接收到的消息类型
        /// </summary>
        public byte ReceiveMsgType
        {
            get;
            set;
        }
        public static object Obj => obj;

        public MCUAgrMian(string portName, int baudRate)
        {
            PortName = portName;
            BaudRate = baudRate;
        }
        /// <summary>
        /// 串口开启
        /// </summary>
        public void Start()
        {
            SerialPortService = new SerialPortService
            {
                Agreement = new Agreement(6, 1, 0x7E), //如果此参数为null,则 数据为原始数据
                BaudRate = BaudRate,
                PortName = PortName,
                SerialType = "MCU"
            };
            SerialPortService.OnOpenClose += new SerialOpenClosedEvent(SerialPortService_OnOpenClose);
            SerialPortService.OnSerialRevice += new SerialReviceEventArgsEvent(SerialPortService_OnSerialRevice);
            SerialPortService.Start();
            
            //发送对列线程
            Thread thread = new Thread(QueueSendPro);
            thread.IsBackground = true;
            thread.Start();
        }
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Stop()
        {
            SerialPortService.Stop();
        }

        //命令流水号
        private byte cmdRecordOrder = 0;

        //命令流水号访问锁
        private static Object thisLock = new Object();
        bool isFirst = true;//首次使用标记
        protected byte GetCmdOrderNum() //命令流水号
        {
            lock (thisLock)
            {
                if (!isFirst)
                {
                    cmdRecordOrder++;
                }
                else
                {
                    isFirst = false;
                }
            }
            return cmdRecordOrder;
        }

        /// <summary>
        /// 发送对列
        /// </summary>
        private Queue SendProQueue = Queue.Synchronized(new Queue());

        /// <summary>
        /// 发送指令装入对列
        /// </summary>
        /// <param name="sendProtocol"></param>
        public void AddSendQueue(MInstructionType Instruction, int cabinet, int layer, int box, byte[] cmdBody, int cMDTag)
        {
            if (cmdBody == null) return;
            int length = cmdBody.Length + 5;
            byte[] cmdData = new byte[length + 5];
            cmdData[0] = (byte)CommandHead.CMD_HEAD_SIGN1;
            cmdData[1] = (byte)(cmdData.Length - 6);
            cmdData[2] = (byte)((cmdData.Length - 6) >> 8);
            cmdData[3] = (byte)Instruction;
            cmdData[4] = (byte)cabinet;
            cmdData[5] = (byte)layer;
            cmdData[6] = (byte)box;
            cmdData[cmdData.Length - 1] = (byte)CommandHead.CMD_HEAD_SIGN2;

            Array.Copy(cmdBody, 0, cmdData, 7, cmdBody.Length);

            byte[] CalcData = new byte[cmdData.Length - 4];
            Array.Copy(cmdData.ToArray(), 1, CalcData, 0, CalcData.Length);
            string crc = CRC.ToModbusCRC16(CalcData);
            Array.Copy(crc.StrToToHexByte(), 0, cmdData, cmdData.Length - 3, 2);

            SendProQueue.Enqueue(cmdData);
        }
        /// <summary>
        /// 秤发送指令
        /// </summary>
        /// <param name="instructionType"></param>
        /// <param name="cabinet"></param>
        /// <param name="layer"></param>
        /// <param name="box"></param>
        /// <param name="data"></param>
        public void SendMsgbyScales(ScalesInstructionType instructionType, int cabinet, int layer, int box, byte[] data)
        {
            byte[] buff;
            switch (instructionType)
            {
                case ScalesInstructionType.NumClear:
                case ScalesInstructionType.NumRead:
                    buff = new byte[] { (byte)instructionType };
                    break;
                case ScalesInstructionType.NumWrite:
                case ScalesInstructionType.ReadAll:
                    buff = new byte[data.Length + 1];
                    buff[0] = (byte)instructionType;
                    Array.Copy(data, 0, buff, 1, data.Length);
                    break;
                case ScalesInstructionType.ScalesStatus:
                    buff = data;
                    break;
                default:
                    buff = new byte[0];
                    break;
            }
            int CMDTag = (int)MInstructionType.Scale + (int)instructionType;
            AddSendQueue(MInstructionType.Scale, cabinet, layer, box, buff, CMDTag);
        }
        public void SendMsgbyFinger(FingerInstructionType instructionType, int cabinet, int layer, int box, byte[] data)
        {
            byte[] buff;
            switch (instructionType)
            {
                case FingerInstructionType.RegFinger:
                case FingerInstructionType.DeleteFinger:
                    buff = new byte[data.Length + 1];
                    buff[0] = (byte)instructionType;
                    Array.Copy(data, 0, buff, 1, data.Length);
                    break;
                case FingerInstructionType.StopFinger:
                case FingerInstructionType.DeleteAllFinger:
                    buff = new byte[] { (byte)instructionType };
                    break;
                default:
                    buff = data;
                    break;
            }
            int CMDTag = (int)MInstructionType.FingerCMD + (int)instructionType;
            AddSendQueue(MInstructionType.FingerCMD, cabinet, layer, box, buff, CMDTag);
        }
        public void SendMsgByMC(MInstructionType instructionType, int cabinet, int layer, int box, byte[] data)
        {
            byte[] buff;
            switch (instructionType)
            {
                case MInstructionType.LED:
                case MInstructionType.BackLight:
                case MInstructionType.POWER:
                    buff = data;
                    break;
                default:
                    buff = data;
                    break;
            }
            int CMDTag = (int)instructionType;
            AddSendQueue(instructionType, cabinet, layer, box, buff, CMDTag);
        }
        /// <summary>
        /// 自己拼接的数据类型和指令
        /// </summary>
        /// <param name="command"></param>
        public void AddSingleQueue(string command)
        {
            SendProQueue.Enqueue(command);
        }

        /// <summary>
        /// 线程执行
        /// </summary>
        private void QueueSendPro()
        {
            while (true)
            {
                try
                {
                    if (SendProQueue.Count > 0)
                    {
                        var CMDPkg =  (byte[])SendProQueue.Dequeue();
                        if (CMDPkg != null)
                        {
                            SerialPortService.Send(CMDPkg);
                            Log.Debug("MC>>" + CMDPkg.ByteToHexStr(" "));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("发送协议线程对列循环 QueueSendPro 函数异常!");
                    Log.Error("发送协议线程对列循环 QueueSendPro 函数异常!", ex);
                }
                //否则就发生死锁
                Thread.Sleep(20); //暂时定位200，担心下位机处理不过来。
            }
        }

        ResultFilter RFilter = new ResultFilter();
        private static readonly object obj = new object();
        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SerialPortService_OnSerialRevice(object sender, ReviceEventArgs args)
        {
            try
            {
                lock (Obj)
                {
                    #region 解析
                    //1、串口收到数据
                    if (args.EventType == EventType.SerialRevice)
                    {
                        if (args.ReviceBytes != null && args.ReviceBytes.Length > 0 && args.ReviceBytes[0] == 0x7E && args.ReviceBytes[args.ReviceBytes.Length - 1] == 0x7F)
                        {
                            string RecDatas = RFilter.GetResult(args.ReviceString);
                            if (RecDatas == string.Empty)
                                return;

                            byte[] CalcBuff = new byte[args.ReviceBytes.Length - 4];
                            Array.Copy(args.ReviceBytes, 1, CalcBuff, 0, CalcBuff.Length);
                            string crc = CRC.ToModbusCRC16(CalcBuff);
                            byte[] calcdata = new byte[2];
                            calcdata[0] = args.ReviceBytes[args.ReviceBytes.Length - 3];
                            calcdata[1] = args.ReviceBytes[args.ReviceBytes.Length - 2];
                            if (crc == calcdata.ByteToHexStr().Replace(" ", ""))
                            {
                                Log.Debug("MCU<<" + args.ReviceString);
                                var temp = args.ReviceBytes.ByteToStruct(typeof(MCHeader));
                                if (temp is MCHeader mcos)
                                {
                                    MCUProtocol MCU = new MCUProtocol();
                                    MCU.Header = mcos;
                                    MCU.DataBuff = args.ReviceBytes.Skip(6).Take(args.ReviceBytes.Length - 9).ToArray();
                                    ShuntInstructionType(MCU);
                                }
                            }
                        }
                    }
                    //2、串口事件抛出异常
                    else if (args.EventType == EventType.SerialError)
                    {
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this, new MCReviceEventArgs() { eventType = EventType.SerialError });
                        }
                        //记录日志即可
                    }
                    //3、串口中代码异常错误
                    else if (args.EventType == EventType.SerialServiceError)
                    {
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this, new MCReviceEventArgs() { eventType = EventType.SerialServiceError });
                        }
                        Log.Info("MC串口错误");
                        //记录日志即可
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.Error("MCUSerialPortService_OnSerialRevice", ex);
            }
        }



        /// <summary>
        /// 根据指令类型，发送返回相应的界面做相应的操作
        /// </summary>
        /// <param name="receivePro"></param>
        void ShuntInstructionType(MCUProtocol parsingData)
        {


            MInstructionType mInstruction = (MInstructionType)parsingData.Header.CMD;
            OnReturnDataEventArgs?.Invoke(this, new MCReviceEventArgs() { eventType = EventType.SerialRevice, mcuInstructionType = mInstruction, MsgBuff=parsingData.DataBuff});
           
        }
        public void SerialPortService_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            if (OnOpenSerialEventArgs != null)
                OnOpenSerialEventArgs(this, args.IsOpen);
        }

    }
    public class CMDPkg
    {
        public int CMDTag { get; set; }
        public byte[] Buff { get; set; }
    }
}

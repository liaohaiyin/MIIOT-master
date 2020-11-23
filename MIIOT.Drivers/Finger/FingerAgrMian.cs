using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Configuration;

namespace MIIOT.Drivers
{

    /// <summary>
    /// 串口协议启动解析类，为静态或者单例模式类
    /// 所有的事件都采用委托，不采用事件操作
    /// 主要防止注册多个事件，导致数据重复
    /// </summary>
    public class FingerAgrMian
    {
        /// <summary>
        /// 返回数据事件，交由Manager解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public FReturnDataEventArgs OnReturnDataEventArgs;

        /// <summary>
        /// 串口服务类
        /// </summary>
        private SerialPortService SerialPortService;
        public event SerialOpenClosedEvent OnOpenClose;
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
        public FingerAgrMian(string portName, int baudRate)
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
                Agreement = new Agreement(8, -1, 0x40), //如果此参数为null,则 数据为原始数据
                BaudRate = BaudRate,
                PortName = PortName,
                SerialType = "Finger"
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
        public void AddSendQueue(FInstructionType FType, byte[] lstBody, byte[] SubBody)
        {
            if (lstBody == null) return;
            List<byte> SendData = new List<byte>();
            byte[] cmdData = new byte[lstBody.Length + 5];
            cmdData[0] = 0x40;
            cmdData[1] = (byte)FType;
            cmdData[2] = 0xff;
            Array.Copy(lstBody, 0, cmdData, 3, lstBody.Length);
            cmdData[cmdData.Length - 2] = cmdData.CalcationCheckout();
            cmdData[cmdData.Length - 1] = 0x0D;
            if (SubBody.Length == 0)
            {
                SendProQueue.Enqueue(cmdData);
            }
            else
            {
                SendData.AddRange(cmdData);
                SendData.AddRange(AddSendSubQueue(FType, SubBody));
                SendProQueue.Enqueue(SendData.ToArray());
            }
        }
        /// <summary>
        /// 附加指令装入
        /// </summary>
        /// <param name="sendProtocol"></param>
        public byte[] AddSendSubQueue(FInstructionType FType, byte[] lstBody)
        {
            byte[] cmdData = new byte[lstBody.Length + 5];
            cmdData[0] = 0x3E;
            cmdData[1] = (byte)FType;
            cmdData[2] = 0xff;
            Array.Copy(lstBody, 0, cmdData, 3, lstBody.Length);
            cmdData[cmdData.Length - 2] = cmdData.CalcationCheckout();
            cmdData[cmdData.Length - 1] = 0x0D;
            return cmdData;
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
                    byte[] sendMsg = SendProQueue.Count > 0 ? (byte[])SendProQueue.Dequeue() : null;
                    if (sendMsg != null)
                    {
                        SerialPortService.Send(sendMsg);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("发送协议线程对列循环 QueueSendPro 函数异常!", ex);
                }
                //否则就发生死锁
                Thread.Sleep(200); //暂时定位200，担心下位机处理不过来。
            }
        }

        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SerialPortService_OnSerialRevice(object sender, ReviceEventArgs args)
        {
            try
            {
                #region 解析
                //1、串口收到数据
                if (args.EventType == EventType.SerialRevice)
                {
                    List<byte> buffer = args.ReviceBytes.ToList();
                    if (args.ReviceBytes != null && args.ReviceBytes.Length > 0 && args.ReviceBytes.Length == 8)
                    {
                        BuffAy(args.ReviceBytes, args.ReviceString);

                    }
                    else if (args.ReviceBytes != null && args.ReviceBytes.Length > 0 && buffer[0] == 0x3E && buffer[buffer.Count - 1] == 0x0D)
                    {
                        byte[] calcData = args.ReviceBytes.Take(args.ReviceBytes.Length - 2).ToArray();
                        //计算命令体异或值
                        byte byteCalcation = calcData.CalcationCheckout();
                        string temp = calcData.ByteToHexStr();
                        if (byteCalcation == args.ReviceBytes[args.ReviceBytes.Length - 2])
                        {
                            FReviceEventArgs msg = new FReviceEventArgs() { IsNormalCMD = false };
                            msg.MsgBuff = args.ReviceBytes;
                            msg.Msg = args.ReviceString;
                            msg.fInstructionType = ((int)msg.MsgBuff[1]).GetEnum<FInstructionType>();
                            msg.FingerBuff = args.ReviceBytes.Skip(3).Take(args.ReviceBytes.Length - 5).ToArray();
                            msg.eventType = EventType.SerialRevice;
                            if (OnReturnDataEventArgs != null)
                            {
                                OnReturnDataEventArgs(this, msg);
                            }

                        }
                    }
                }
                //2、串口事件抛出异常
                else if (args.EventType == EventType.SerialError)
                {
                    if (OnReturnDataEventArgs != null)
                    {
                        // OnReturnDataEventArgs(this, new MCBackMsg() { EventType = EventType.SerialError });
                    }
                    //记录日志即可
                }
                //3、串口中代码异常错误
                else if (args.EventType == EventType.SerialServiceError)
                {
                    if (OnReturnDataEventArgs != null)
                    {
                        // OnReturnDataEventArgs(this, new MCBackMsg() { EventType = EventType.SerialServiceError });
                    }
                    Log.Info("Finger串口错误");
                    //记录日志即可
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.Error("FingerSerialPortService_OnSerialRevice", ex);
            }


        }
        public void BuffAy(byte[] byterevice, string ReviceString)
        {
            //二进制协议已经解析
            //解析协议头部
            byte[] calcData = new byte[6];
            Array.Copy(byterevice, 0, calcData, 0, 6);
            //计算命令体异或值
            byte byteCalcation = calcData.CalcationCheckout();
            //当前异或的值与发送上来的异或值是否相等，如果不等，说明发送的指令有误。校验值不对

            if (byteCalcation == byterevice[6])
            {

                FReviceEventArgs msg = new FReviceEventArgs() { IsNormalCMD = true };
                msg.MsgBuff = byterevice;
                msg.Msg = ReviceString;
                msg.fInstructionType = ((int)msg.MsgBuff[1]).GetEnum<FInstructionType>();
                msg.fResponseCode = ((int)msg.MsgBuff[5]).GetEnum<FResponseCode>();
                msg.eventType = EventType.SerialRevice;
                if (OnReturnDataEventArgs != null)
                {
                    OnReturnDataEventArgs(this, msg);
                }
            }
        }

        public void SerialPortService_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            OnOpenClose?.Invoke(this, args);
        }

    }

}

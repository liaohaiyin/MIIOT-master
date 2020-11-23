using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Hnbc.Tools.SerialCode;
//using Hnbc.Protocol.SerialCode.Hcg;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Configuration;
using MIIOT.Drivers.MCU;

namespace MIIOT.Drivers
{

    /// <summary>
    /// 串口协议启动解析类，为静态或者单例模式类
    /// 所有的事件都采用委托，不采用事件操作
    /// 主要防止注册多个事件，导致数据重复
    /// </summary>
    public class SRFIDAgrMian
    {
        /// <summary>
        /// 返回数据事件，交由Manager解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
      public delegate void DelBuffBackArgs(object sender, MCBackMsg fBack);
        public DelBuffBackArgs OnReturnDataEventArgs;
        public OpenSerialEventArgs OnOpenSerialEventArgs;
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


        public SRFIDAgrMian(string portName, int baudRate)
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
                Agreement = new Agreement(0, 2, 0x5A), //如果此参数为null,则 数据为原始数据
                BaudRate = BaudRate,
                PortName = PortName,
                SerialType="RFID"
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
        public void AddSendQueue(HRFIDPCMD PCMD, HRFIDCMD Instruction, byte[] lstBody)
        {
            if (lstBody == null) return;

            byte[] cmdData = null;
            cmdData = new byte[9 + lstBody.Length];

            cmdData[0] = 0x5A;
            cmdData[2] = 0x01;//版本号
            cmdData[3] = (byte)PCMD;//消息类别号
            cmdData[4] = (byte)Instruction;//消息ID
            byte[] lenbuff = BitConverter.GetBytes((Int16)lstBody.Length);
            cmdData[5] = lenbuff[1];//高低在前转换
            cmdData[6] = lenbuff[0];
            Array.Copy(lstBody, 0, cmdData,7, lstBody.Length);
            byte[] crcbuff = cmdData.Skip(1).Take(cmdData.Length-3).ToArray();
            int a = crcbuff.CRC16_XMODEM();
            byte[] crc =BitConverter.GetBytes(a);
            Array.Reverse(crc);
            cmdData[cmdData.Length - 2] = crc[2];
            cmdData[cmdData.Length - 1] = crc[3];
            string str = cmdData.ByteToHexStr();
            SendProQueue.Enqueue(cmdData);
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
                    byte[] sendMsg = SendProQueue.Count > 0 ? (byte[])SendProQueue.Dequeue() : null;
                    if (sendMsg != null)
                    {
                        SerialPortService.Send(sendMsg);
                        Log.Debug("RFID COM>>" + sendMsg.ByteToHexStr(" "));
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


        List<string> RecDatas = new List<string>();
        /// <summary>
        /// 串口返回数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SerialPortService_OnSerialRevice(object sender, ReviceEventArgs args)
        {
            try
            {
                Log.Debug("RFID<<" + args.ReviceString);
                #region 解析
                //1、串口收到数据
                if (args.EventType == EventType.SerialRevice)
                {
                    //二进制协议已经解析
                    byte[] byterevice = args.ReviceBytes;
                    //解析协议头部
                    SRFIDCommandHead cmdHead = (SRFIDCommandHead)ByteToStruct(byterevice, typeof(SRFIDCommandHead));

                    //首先确定数据长度
                    byte[] lengthbuff = new byte[] { cmdHead.dataLen1, cmdHead.dataLen2 };
                    Array.Reverse(lengthbuff);
                    int length = BitConverter.ToInt16(lengthbuff,0);
                    byte[] calcData = new byte[length + 6];
                    Array.Copy(args.ReviceBytes, 1, calcData, 0, length+6 );
                    //计算命令体异或值
                    byte byteCalcation = calcData.CalcationCRCSum();
                    //当前异或的值与发送上来的异或值是否相等，如果不等，说明发送的指令有误。校验值不对
                    if (byteCalcation != byterevice[length + 3]) return;
                    if (cmdHead.cmdType == (byte)MInstructionType.RecCycRead)
                    {
                        byte[] resBuff = new byte[length - 5];
                        Array.Copy(args.ReviceBytes, 5, resBuff, 0, length - 5);
                        string recdata = resBuff.ByteToHexStr();
                        if (!RecDatas.Contains(recdata))
                        {
                            RecDatas.Add(recdata);
                            if (OnReturnDataEventArgs != null)
                            {
                                OnReturnDataEventArgs(this, new MCBackMsg() { EventType= EventType.SerialRevice, MsgBuff = resBuff, MCMDType = MInstructionType.RecCycRead, RegMsg = FingerRegMsg.SUCCESS, args = args });
                            }
                        }
                    }
                    if (cmdHead.cmdType == (byte)MInstructionType.RecReadStop)
                    {
                        RecDatas.Clear();
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this,new MCBackMsg() { EventType = EventType.SerialRevice,MCMDType=MInstructionType.RecReadStop, RegMsg = FingerRegMsg.SUCCESS, args = args });
                        }
                    }

                }
                //2、串口事件抛出异常
                if (args.EventType == EventType.SerialError)
                {
                    if (OnReturnDataEventArgs != null)
                    {
                        OnReturnDataEventArgs(this, new MCBackMsg() { EventType = EventType.SerialError });
                    }
                    //记录日志即可
                }
                //3、串口中代码异常错误
                if (args.EventType == EventType.SerialServiceError)
                {
                    if (OnReturnDataEventArgs != null)
                    {
                        OnReturnDataEventArgs(this, new MCBackMsg() {  EventType=EventType.SerialServiceError});
                    }
                    Log.Info("串口错误");
                    //记录日志即可
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.Error("SerialPortService_OnSerialRevice", ex);
            }

          
        }

     

        public void SerialPortService_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            if (OnOpenSerialEventArgs != null)
                OnOpenSerialEventArgs(this, args.IsOpen);
        }

        /// <summary>
        /// 将结构体转换为Byte数组
        /// </summary>
        /// <param name="structObj">结构体</param>
        /// <param name="size">数组长度</param>
        public static byte[] StructToBytes(object structObj, int size)
        {
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷贝到byte 数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }

        /// <summary>
        /// 将byte数组转换为结构体
        /// </summary>
        public static object ByteToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            if (size > bytes.Length)
            {
                return null;
            }
            //分配结构体内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷贝到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            return obj;
        }

        /// <summary>
        /// 计算命令体异或值
        /// </summary>
        /// <param name="bytes">命令体</param>
        /// <returns></returns>
        public static byte CalcationByte(byte[] bytes, int StartIndex)
        {
            byte byteValue = new byte();
            int index = StartIndex;
            while (index < bytes.Length)
            {
                if (index == StartIndex)
                {
                    byteValue = (byte)(bytes[index] ^ bytes[index + 1]);
                    index = index + 2;
                }
                else
                {
                    byteValue = (byte)(bytes[index] ^ byteValue);
                    index = index + 1;
                }
            }

            return byteValue;
        }
        #region "中文转GBK"
        /// <summary>
        /// 中文转GBK
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static byte[] CHConvertGBK(string ch)
        {
            byte[] buff = Encoding.GetEncoding("GBK").GetBytes(ch);
            return buff;
        }
        #endregion
    }

}

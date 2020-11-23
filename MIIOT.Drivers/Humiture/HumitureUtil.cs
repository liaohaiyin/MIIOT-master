using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class HumitureUtil
    {

        public event OpenSerialEventArgs OnOpenSerialEventArgs;
        public delegate void DelReturnDataArgs(object sender, HMBackMsg hMBack);
        public event DelReturnDataArgs OnReturnDataEventArgs;

        private SerialPortService SerialPortService;
        private Queue SendProQueue = Queue.Synchronized(new Queue());
        ResultFilter RFilter = new ResultFilter();
        private static HumitureUtil instance;
        private static object _lock = new object();
        /// <summary>
        /// 单例模式
        /// </summary>
        public static HumitureUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new HumitureUtil();
                        }
                    }
                }
                return instance;
            }
        }
        public HumitureUtil()
        {

        }
        public void Start(string PortName, int BaudRate)
        {

            SerialPortService = new SerialPortService
            {
                PortName = PortName,
                BaudRate = BaudRate,
                Agreement = new Agreement(11, -1, 0x01), //如果此参数为null,则 数据为原始数据
                SerialType = "MC"
            };
            SerialPortService.OnOpenClose += new SerialOpenClosedEvent(SerialPortService_OnOpenClose);
            SerialPortService.OnSerialRevice += new SerialReviceEventArgsEvent(SerialPortService_OnSerialRevice);
            SerialPortService.Start();

            //发送对列线程
            Thread thread = new Thread(QueueSendPro);
            thread.IsBackground = true;
            thread.Start();
        }
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
                        Log.Info("HM>>" + sendMsg.ByteToHexStr(" "));
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
        private static object obj = new object();
        public void SerialPortService_OnSerialRevice(object sender, ReviceEventArgs args)
        {
            try
            {
                lock (obj)
                {
                    #region 解析
                    //1、串口收到数据
                    if (args.EventType == EventType.SerialRevice)
                    {
                        string RecDatas = RFilter.GetResult(args.ReviceString);
                        if (RecDatas == string.Empty)
                            return;
                        if (OnReturnDataEventArgs != null)
                        {
                            if (args.ReviceBytes[2] == 0x02)
                            {
                                string msg = Encoding.ASCII.GetString(args.ReviceBytes.Skip(4).Take(5).ToArray());

                                if (msg.ToUpper().Contains("AL"))
                                    msg = "27";
                                if (msg.ToUpper().Contains("AH"))
                                    msg = "28";
                                if (msg.ToUpper().Contains("ALT"))
                                    msg = "29";
                                if (msg.ToUpper().Contains("AHT"))
                                    msg = "30";
                                if (msg.ToUpper().Contains("ADO"))
                                    msg = "31";
                                if (msg.ToUpper().Contains("AUF"))
                                    msg = "32";
                                if (msg.ToUpper().Contains("AHC"))
                                    msg = "33";
                                if (msg.ToUpper().Contains("ADF"))
                                    msg = "34";
                                else msg = "1";
                                OnReturnDataEventArgs(this, new HMBackMsg() { EventType = EventType.SerialError, Msg = msg, args = args, MsgBuff = args.ReviceBytes });
                            }
                            else if (args.ReviceBytes[2] == 0x01)
                            {
                                string msg = Encoding.ASCII.GetString(args.ReviceBytes.Skip(4).Take(5).ToArray());
                                OnReturnDataEventArgs(this, new HMBackMsg() { EventType = EventType.SerialRevice, HMType = 1, Msg = msg, args = args, MsgBuff = args.ReviceBytes });
                            }
                            else if (args.ReviceBytes[2] == 0x03)
                            {

                                string msg = Encoding.ASCII.GetString(args.ReviceBytes.Skip(4).Take(5).ToArray());
                                OnReturnDataEventArgs(this, new HMBackMsg() { EventType = EventType.SerialRevice, HMType = 2, Msg = msg, args = args, MsgBuff = args.ReviceBytes });
                            }

                        }
                    }
                    //2、串口事件抛出异常
                    else if (args.EventType == EventType.SerialError)
                    {
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this, new HMBackMsg() { EventType = EventType.SerialError });
                        }
                        //记录日志即可
                    }
                    //3、串口中代码异常错误
                    else if (args.EventType == EventType.SerialServiceError)
                    {
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this, new HMBackMsg() { EventType = EventType.SerialServiceError });
                        }
                        Log.Info("HumitrueMC串口错误");
                        //记录日志即可
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.Error("HumitureSerialPortService_OnSerialRevice", ex);
            }
        }
        public void SerialPortService_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            if (OnOpenSerialEventArgs != null)
                OnOpenSerialEventArgs(this, args.IsOpen);
        }
    }
    public class HMBackMsg
    {
        public ReviceEventArgs args { get; set; }
        public string Msg { get; set; }
        public byte[] MsgBuff { get; set; }
        public EventType EventType { set; get; }
        public int HMType { get; set; }//1:温度 2：湿度

    }
}

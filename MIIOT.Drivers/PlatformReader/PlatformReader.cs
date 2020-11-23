using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class PlatformReader
    {
        public delegate void DelReturnDataArgs(object sender, PFReaderBackMsg hMBack);
        public event DelReturnDataArgs OnReturnDataEventArgs;

        private static object obj = new object();
        ResultFilter RFilter = new ResultFilter();

        private List<byte> buffer = new List<byte>(4096);
        public PlatformReader()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var buff = CodeQueue.Count > 0 ? CodeQueue.Dequeue() : null;
                        if (buff is string ScanModeData)
                        {
                            OnReturnDataEventArgs?.Invoke(this, new PFReaderBackMsg() { EventType = EventType.SerialRevice, Msg = ScanModeData });
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("", ex);
                    }
                    Thread.Sleep(50);
                }
            });
          
        }
        
        private SerialPortService SerialPortService;
        private Queue SendProQueue = Queue.Synchronized(new Queue());
        public void Open(string PortName, int BaudRate=19200)
        {
            SerialPortService = new SerialPortService
            {
                PortName = PortName,
                BaudRate = BaudRate,
                //Agreement = new Agreement(11, -1, 0x01), //如果此参数为null,则 数据为原始数据
                SerialType = "PR"
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
                        Log.Info("PlatformReader>>" + sendMsg.ByteToHexStr(" "));
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
        public void SerialPortService_OnOpenClose(object sender, OpenCloseEventArgs args)
        {

        }
        HashSet<string> codesHash = new HashSet<string>();
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
                        CodeQueue.Enqueue(args.ReviceString);
                        return;
                        if (args.ReviceString == "040000525A")
                        {
                            return;
                        }
                        codesHash.Add(args.ReviceBytes.ByteToHexStr());
                        CodeQueue.Enqueue(args.ReviceString);
                        return;

                        string RecDatas = RFilter.GetResult(args.ReviceString);
                        if (RecDatas == string.Empty)
                            return;
                        if (args.ReviceBytes[2] == 0x03)
                        {

                            string msg = Encoding.ASCII.GetString(args.ReviceBytes.Skip(4).Take(5).ToArray());
                            OnReturnDataEventArgs(this, new PFReaderBackMsg() { EventType = EventType.SerialRevice, Msg = msg, args = args, MsgBuff = args.ReviceBytes });
                        }

                    }
                    //2、串口事件抛出异常
                    else if (args.EventType == EventType.SerialError)
                    {
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this, new PFReaderBackMsg() { EventType = EventType.SerialError });
                        }
                        //记录日志即可
                    }
                    //3、串口中代码异常错误
                    else if (args.EventType == EventType.SerialServiceError)
                    {
                        if (OnReturnDataEventArgs != null)
                        {
                            OnReturnDataEventArgs(this, new PFReaderBackMsg() { EventType = EventType.SerialServiceError });
                        }
                        Log.Info("PlatFormReader串口错误");
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
        List<CodeTime> codeTimes = new List<CodeTime>();//启动：10 00 0a f0 01 f2 00 01 01 01 01 00 01 00 00 00 4b   --04 00 00 52 5A 停止10 00 0a f0 00 f2 00 01 01 01 01 00 01 00 00 91 1e   --04 00 00 52 5A
        Queue CodeQueue = Queue.Synchronized(new Queue());
       
        public void Send(byte[] buff)
        {
            SendProQueue.Enqueue(buff);
        }

        public void Start()
        {
            Send("10 00 0a f0 01 f2 00 01 01 01 01 00 01 00 00 00 4b".StrToToHexByte());
        }

        public void Stop()
        {
            Send("10 00 0a f0 00 f2 00 01 01 01 01 00 01 00 00 91 1e".StrToToHexByte());
        }
    }
    public class CodeTime
    {
        public string code { get; set; }
        public DateTime time { get; set; }
    }
    public class PFReaderBackMsg
    {
        public ReviceEventArgs args { get; set; }
        public string Msg { get; set; }
        public byte[] MsgBuff { get; set; }
        public EventType EventType { set; get; }

    }
}

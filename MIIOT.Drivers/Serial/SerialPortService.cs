using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Collections.Concurrent;
using System.Collections;

namespace MIIOT.Drivers
{
    public class SerialPortService
    {
        /// <summary>
        /// C#串口服务对象
        /// </summary>
        private SerialPort comm = new SerialPort();

        private bool dtrEnable = false;
        //获取或设置一个值，该值在串行通信过程中启用数据终端就绪 (DTR) 信号。
        public bool DtrEnable
        {
            get { return dtrEnable; }
            set { dtrEnable = value; }
        }

        private bool isOpend = false;
        /// <summary>
        /// 串口是否打开,只能Get，不能Set
        /// </summary>
        public bool IsOpend { get { return isOpend; } }
        /// <summary>
        /// 串口号
        /// </summary>
        public string PortName { set; get; }
        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { set; get; }

        private int dataBits = 8;
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }
        public string SerialType { get; set; }

        /// <summary>
        /// 解析协议使用的，对象
        /// </summary>
        public Agreement Agreement { set; get; }

        /// <summary>
        /// 线程
        /// </summary>
        private Thread threadQueue = null;

        /// <summary>
        /// 串口返回事件，主要是参数
        /// </summary>
        public event SerialReviceEventArgsEvent OnSerialRevice;
        /// <summary>
        /// 事件+串口关闭或者打开时候
        /// </summary>
        public event SerialOpenClosedEvent OnOpenClose;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SerialPortService()
        {
            comm.DataReceived += new SerialDataReceivedEventHandler(comm_DataReceived);
            comm.ErrorReceived += new SerialErrorReceivedEventHandler(comm_ErrorReceived);

            threadQueue = new Thread(QueueByte);
            threadQueue.IsBackground = true;
            Timer = new Timer(new TimerCallback(TimerAction));
        }

        /// <summary>
        /// 开始启动
        /// </summary>
        public void Start()
        {
            //启动
            threadQueue.Start();
            OpenPort();
        }
        /// <summary>
        /// 停止串口
        /// </summary>
        public void Stop()
        {
            //关闭串口
            ClosePort();
            //清空集合
            ByteRest.Clear();
            //关闭线程
            threadQueue.Abort();
        }


        //定义的委托
        private void OpenClose(bool isOpen)
        {
            isOpend = isOpen;
            OpenCloseEventArgs args = new OpenCloseEventArgs() { IsOpen = isOpen };
            //OnOpenClose?.Invoke(this, args);
            if (OnOpenClose != null)
            {
                OnOpenClose(this, args);
            }
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        private void OpenPort()
        {


            comm.PortName = PortName;
            comm.BaudRate = BaudRate;
            //comm.DtrEnable = DtrEnable;

            comm.RtsEnable = true;
            comm.DataBits = DataBits;
            comm.Parity = Parity.None;
            comm.StopBits = StopBits.One;
            comm.DtrEnable = true;
            comm.WriteBufferSize = 4096;
            comm.ReceivedBytesThreshold = 1;
            try
            {
                comm.Open();
                Log.Debug("串口连接" + PortName + "---:" + BaudRate);
                OpenClose(true);
            }
            catch (Exception ex)
            {
                //打开失败了
                OpenClose(false);

                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。   
                comm = new SerialPort();
                //现实异常信息给客户。   
                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialServiceError;
                //是否开启
                args.IsOpend = false;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.Exception = ex;

                SerialReviceRT(this, args);
            }
        }

        private void ClosePort()
        {
            try
            {
                if (comm.IsOpen)
                    comm.Close();
                OpenClose(false);
            }
            catch (Exception ex)
            {
                //捕获到异常信息，创建一个新的comm对象，之前的不能用了。  
                comm = new SerialPort();

                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialServiceError;
                //是否开启
                args.IsOpend = false;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.Exception = ex;

                SerialReviceRT(this, args);
            }
        }
        void comm_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            if (OnSerialRevice != null)
            {
                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialError;
                //是否开启
                args.IsOpend = true;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.SerialErrorReceivedEventArgs = e;
                if (e.EventType == SerialError.RXOver)
                {
                    comm.DiscardInBuffer();
                    comm.DiscardOutBuffer();
                }
                SerialReviceRT(this, args);
            }
        }
        /// <summary>
        /// 线程安全队列
        /// </summary>
        private Queue ByteRest = Queue.Synchronized(new Queue());
        private List<byte> buffer = new List<byte>(4096);
        void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (SerialType == "Finger")
            {
                FingerReceived(sender, e);
            }
            else if (SerialType == "PR")
                PRReceived(sender, e);
            else if (SerialType == "MCU")
                MCUShortReceived(sender, e);
            else
                NormalReceived(sender, e);


        }
        Timer Timer;
        public void TimerAction(object state)
        {
            lock (codeset)
            {
                codeset.Clear();
            }
        }

        private void MCUShortReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int i = comm.BytesToRead;
                byte[] buf = new byte[i];
                comm.Read(buf, 0, i);
                if (buf.Length > 0)
                    Log.Debug("Serial Data:" + buf.ByteToHexStr(" "));
                if (buf.Length > 0)
                {
                    //1.缓存数据
                    buffer.AddRange(buf);
                }
                //如果没有设置，协议长度和协议头，则不做判断
                if (Agreement != null)
                {
                    int length = 0;
                    if (buffer[0] != Agreement.HeadSTR)//传输数据有帧头，用于清除导致丢失包头的情况
                    {
                        int index = buffer.IndexOf(Agreement.HeadSTR);
                        if (index > 0)
                        {
                            buffer.RemoveRange(0, index);
                        }
                    }
                    if (buffer.Count <= Agreement.LengthIndex + 1)
                        return;
                    if (Agreement.LengthIndex <= 0)
                        length = Agreement.AgreementLength;
                    else
                    {
                        byte[] lenbuff = new byte[4];
                        lenbuff[0] = buffer[Agreement.LengthIndex];
                        lenbuff[1] = buffer[Agreement.LengthIndex + 1];
                        lenbuff[2] = 0;
                        lenbuff[3] = 0;
                        length = BitConverter.ToInt32(lenbuff, 0);
                        length = length + Agreement.AgreementLength;

                    }

                    //2.完整性判断
                    while (buffer.Count >= length && buffer.Count > Agreement.LengthIndex) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                    {
                        //2.1 查找数据头
                        if (buffer[0] == Agreement.HeadSTR)//传输数据有帧头，用于判断
                        {
                            if (Agreement.LengthIndex <= 0)
                                length = Agreement.AgreementLength;
                            else
                            {
                                byte[] lenbuff = new byte[4];
                                lenbuff[0] = buffer[Agreement.LengthIndex];
                                lenbuff[1] = buffer[Agreement.LengthIndex + 1];
                                lenbuff[2] = 0;
                                lenbuff[3] = 0;
                                length = BitConverter.ToInt32(lenbuff, 0);
                                length = length + Agreement.AgreementLength;

                            }
                            //如果大于12个字节，说明连包在一起
                            if (buffer.Count > length)
                            {
                                //加入集合
                                ByteRest.Enqueue(buffer.GetRange(0, length).ToArray());
                                buffer.RemoveRange(0, length);
                            }
                            //说明包数据不完整，没有接受完毕.
                            else if (buffer.Count < length)
                            {
                                //返回去，等待下次进来数据
                                break;
                            }
                            else if (buffer.Count == length)
                            {
                                //如果刚好等于12，说明数据包完整，需要
                                ByteRest.Enqueue(buffer.ToArray());
                                buffer.RemoveRange(0, length);
                            }
                        }
                        else
                        {
                            //如果开头不是EE，则将此数据的前一部门删除，
                            //找到第一个EE开头的索引
                            int index = buffer.IndexOf(Agreement.HeadSTR);
                            if (index > 0)
                            {
                                buffer.RemoveRange(0, index);
                            }
                            //头验证失败，
                        }
                    }
                }
                //如果没有设置，协议长度和协议头，则不做判断
                else
                {
                    ByteRest.Enqueue(buffer.ToArray());
                    buffer.RemoveRange(0, buf.Length);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ComErr", ex);
                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialServiceError;
                //是否开启
                args.IsOpend = true;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.Exception = ex;

                SerialReviceRT(this, args);
            }
        }

        private void PRReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int i = comm.BytesToRead;
                byte[] buf = new byte[i];
                comm.Read(buf, 0, i);
                //如果没有设置，协议长度和协议头，则不做判断

                //1.缓存数据
                buffer.AddRange(buf);
                //2.完整性判断
                while (buffer.Count >= 10) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                {
                    if (buffer[8] == 0x0D && buffer[9] == 0x0A)
                    {
                        byte[] code = buffer.GetRange(0, 8).ToArray();
                        lock (codeset)
                        {
                            if (codeset.Count > 500)
                                codeset.Clear();
                            if (codeset.Add(code.ByteToHexStr()))
                            {
                                ByteRest.Enqueue(code);
                                Timer.Change(10000, 0);
                            }

                        }
                        buffer.RemoveRange(0, 10);
                    }
                    else
                    {
                        try
                        {
                            while (buffer[8] != 0x0D && buffer[9] != 0x0A)
                            {
                                buffer.RemoveRange(0, 1);
                            }
                        }
                        catch (Exception ex)
                        {
                            return;
                        }

                        byte[] code = buffer.GetRange(0, 8).ToArray();
                        lock (codeset)
                        {
                            if (codeset.Count > 500)
                                codeset.Clear();
                            if (codeset.Add(code.ByteToHexStr()))
                            {
                                ByteRest.Enqueue(code);
                                Timer.Change(10000, 0);
                            }
                        }
                        buffer.RemoveRange(0, 10);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("PRReceivedComErr", ex);
                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialServiceError;
                //是否开启
                args.IsOpend = true;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.Exception = ex;

                SerialReviceRT(this, args);
            }
        }
        private void NormalReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int i = comm.BytesToRead;
                byte[] buf = new byte[i];
                comm.Read(buf, 0, i);
                if (buf.Length > 0)
                    Log.Debug("Serial Data:" + buf.ByteToHexStr(" "));
                //如果没有设置，协议长度和协议头，则不做判断
                if (Agreement != null && buf.Length > Agreement.LengthIndex)
                {
                    //1.缓存数据
                    buffer.AddRange(buf);

                    int length = 0;
                    if (buffer[0] != Agreement.HeadSTR)//传输数据有帧头，用于清除导致丢失包头的情况
                    {
                        int index = buffer.IndexOf(Agreement.HeadSTR);
                        if (index > 0)
                        {
                            buffer.RemoveRange(0, index);
                        }
                    }
                    if (Agreement.LengthIndex <= 0)
                        length = Agreement.AgreementLength;
                    else
                        length = (int)buffer[Agreement.LengthIndex] + Agreement.AgreementLength;
                    //2.完整性判断
                    while (buffer.Count >= length && buffer.Count > Agreement.LengthIndex) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                    {
                        if (buffer.Count >= Agreement.AgreementLength)
                        {
                            ByteRest.Enqueue(buffer.GetRange(0, length).ToArray());
                            buffer.RemoveRange(0, length);
                            continue;
                        }
                        //2.1 查找数据头
                        if (buffer[0] == Agreement.HeadSTR)//传输数据有帧头，用于判断
                        {
                            if (Agreement.LengthIndex <= 0)
                                length = Agreement.AgreementLength;
                            else
                                length = (int)buffer[Agreement.LengthIndex] + Agreement.AgreementLength;
                            //如果大于12个字节，说明连包在一起
                            if (buffer.Count > length)
                            {
                                //加入集合
                                ByteRest.Enqueue(buffer.GetRange(0, length).ToArray());
                                buffer.RemoveRange(0, length);
                            }
                            //说明包数据不完整，没有接受完毕.
                            else if (buffer.Count < length)
                            {
                                //返回去，等待下次进来数据
                                break;
                            }
                            else if (buffer.Count == length)
                            {
                                //如果刚好等于12，说明数据包完整，需要
                                ByteRest.Enqueue(buffer.ToArray());
                                buffer.RemoveRange(0, length);
                            }
                        }
                        else
                        {
                            //如果开头不是EE，则将此数据的前一部门删除，
                            //找到第一个EE开头的索引
                            int index = buffer.IndexOf(Agreement.HeadSTR);
                            if (index > 0)
                            {
                                buffer.RemoveRange(0, index);
                            }
                            //头验证失败，
                        }
                    }
                }
                //如果没有设置，协议长度和协议头，则不做判断
                else
                {
                    buffer.AddRange(buf);
                    ByteRest.Enqueue(buffer.ToArray());
                    buffer.RemoveRange(0, buf.Length);
                }
            }
            catch (Exception ex)
            {
                Log.Error("ComErr", ex);
                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialServiceError;
                //是否开启
                args.IsOpend = true;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.Exception = ex;

                SerialReviceRT(this, args);
            }
        }
        int addLenght = 0;
        private void FingerReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Thread.Sleep(100);
                int i = comm.BytesToRead;
                byte[] buf = new byte[i];
                comm.Read(buf, 0, i);
                Log.Debug("Finger Serial Data:" + buf.ByteToHexStr(" "));
                //如果没有设置，协议长度和协议头，则不做判断
                if (Agreement != null && buf.Length > 3)
                {
                    //1.缓存数据
                    buffer.AddRange(buf);
                    int length = 0;

                    if (buffer[0] == 0x40)
                    {
                        length = 8;
                        addLenght = BitConverter.ToInt16(new byte[] { buffer[3], buffer[4] }, 0);
                    }
                    else if (buffer[0] == 0x3E)
                    {
                        length = addLenght + 5;
                    }
                    //2.完整性判断
                    while (buffer.Count >= length && buffer.Count > 3) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                    {
                        //2.1 查找数据头
                        if (buffer[0] == Agreement.HeadSTR || buffer[0] == 0x3E)//传输数据有帧头，用于判断
                        {
                            if (buffer[0] == 0x40)
                            {
                                length = 8;
                                addLenght = BitConverter.ToInt16(new byte[] { buffer[3], buffer[4] }, 0);
                            }
                            else if (buffer[0] == 0x3E)
                            {
                                length = addLenght + 5;
                            }
                            //如果大于12个字节，说明连包在一起
                            if (buffer.Count > length)
                            {
                                //加入集合
                                ByteRest.Enqueue(buffer.GetRange(0, length).ToArray());
                                buffer.RemoveRange(0, length);
                            }
                            //说明包数据不完整，没有接受完毕.
                            else if (buffer.Count < length)
                            {
                                //返回去，等待下次进来数据
                                break;
                            }
                            else if (buffer.Count == length)
                            {
                                //如果刚好等于12，说明数据包完整，需要
                                ByteRest.Enqueue(buffer.ToArray());
                                buffer.RemoveRange(0, length);
                            }
                        }
                        else
                        {
                            //如果开头不是EE，则将此数据的前一部门删除，
                            int index = 0;
                            int idx1 = buffer.IndexOf(0x40);
                            int idx2 = buffer.IndexOf(0x3E);
                            if (idx1 >= idx2)
                            {
                                index = idx2;
                            }
                            else
                                index = idx1;
                            if (index > 0 && index <= buffer.Count)
                            {
                                buffer.RemoveRange(0, index);
                            }
                            else
                            {
                                buffer.Clear();
                            }
                        }
                    }
                }
                //如果没有设置，协议长度和协议头，则不做判断
                else
                {
                    buffer.AddRange(buf);
                    ByteRest.Enqueue(buffer.ToArray());
                    buffer.RemoveRange(0, buf.Length);
                }
            }
            catch (Exception ex)
            {
                Log.Error("FingerComErr", ex);
                ReviceEventArgs args = new ReviceEventArgs();
                //串口服务类型
                args.EventType = EventType.SerialServiceError;
                //是否开启
                args.IsOpend = true;
                //异常类型，是否是串口错误
                args.ExceptionEventArgs.Exception = ex;

                SerialReviceRT(this, args);
            }
        }
        /// <summary>
        /// 线程执行
        /// </summary>
        private void QueueByte()
        {
            while (true)
            {
                Thread.Sleep(5);
                try
                {

                    byte[] cmdbyte = ByteRest.Count > 0 ? (byte[])ByteRest.Dequeue() : null;
                    if (cmdbyte != null)
                    {
                        if (SerialType == "PR")
                            Array.Reverse(cmdbyte);
                        string revice = cmdbyte.ByteToHexStr();// ConvertUtil.ByteToHexStr(cmdbyte);
                        //日志输出
                        ReviceEventArgs args = new ReviceEventArgs();
                        //串口服务类型
                        args.EventType = EventType.SerialRevice;
                        //是否开启
                        args.IsOpend = true;
                        //协议类容
                        args.ReviceBytes = cmdbyte;
                        //字符串协议类容
                        args.ReviceString = revice;
                        Log.Debug("串口返回：" + revice);
                        SerialReviceRT(this, args);


                    }
                }
                catch (Exception ex)
                {
                    Log.Error("QueueByte", ex);
                    ReviceEventArgs args = new ReviceEventArgs();
                    //串口服务类型
                    args.EventType = EventType.SerialServiceError;
                    //是否开启
                    args.IsOpend = true;
                    //异常类型，是否是串口错误
                    args.ExceptionEventArgs.Exception = ex;

                    SerialReviceRT(this, args);
                }
                //否则就发生死锁
                Thread.Sleep(10);
            }
        }
        private static HashSet<string> codeset = new HashSet<string>();
        /// <summary>
        /// 发送协议
        /// </summary>
        /// <param name="buffer">byte数组</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns>返回成功或失败</returns>
        public bool Send(byte[] buffer, int offset, int count)
        {
            if (IsOpen())
            {
                try
                {
                    comm.Write(buffer, offset, count);
                }
                catch (Exception ex)
                {
                    Log.Error("串口发送byte[]加offset异常", ex);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 发送协议
        /// </summary>
        /// <param name="buffer"></param>
        public bool Send(byte[] buffer)
        {
            if (IsOpen())
            {
                try
                {
                    comm.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    Log.Error("串口发送byte[]异常", ex);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 发送协议
        /// </summary>
        /// <param name="text">string文本</param>
        /// <returns></returns>
        public bool Send(string text)
        {
            if (IsOpen())
            {
                try
                {
                    comm.Write(text);
                }
                catch (Exception ex)
                {
                    Log.Error("串口发送文本异常", ex);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断是否关闭
        /// </summary>
        /// <returns></returns>
        private bool IsOpen()
        {
            if (!comm.IsOpen)
            {
                OpenPort();
            }
            return comm.IsOpen;
        }



        /// <summary>
        /// 虚方法，为了扩展
        /// </summary>
        /// <param name="sender">本类</param>
        /// <param name="args">串口参数</param>
        // public abstract void SerialRevice(object sender, ReviceEventArgs args);



        private void SerialReviceRT(object sender, ReviceEventArgs args)
        {
            //SerialRevice(sender, args);

            if (OnSerialRevice != null)
            {
                OnSerialRevice(sender, args);
            }
        }
    }
}

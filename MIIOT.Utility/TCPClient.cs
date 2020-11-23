using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT
{

    public class TCPClientUtil
    {
        public Socket socketClient;
        IPEndPoint point;
        public delegate void ReceiveBack(byte[] Buff);
        public event ReceiveBack DoReceiveCallback;
        public delegate void ConnectedBack(bool IsConnect);
        public event ConnectedBack DoConnected;
        private static TCPClientUtil tCPClient;
        public static object obj = new object();
        public static TCPClientUtil Ins
        {
            get
            {
                lock (obj)
                {
                    if (tCPClient == null)
                    {
                        tCPClient = new TCPClientUtil();
                    }
                }
                return tCPClient;
            }
        }
        public TCPClientUtil()
        {

        }
        public void SendByPack( byte[] sendBuff)
        {
            try
            {
                byte[] buff = new byte[sendBuff.Length + 8];
                buff[0] = 0xfa;
                buff[1] = 0xf5;
                buff[2] = 0x16;
                buff[3] = sendBuff.CalcationCheckout();
                byte[] lenbu = BitConverter.GetBytes(sendBuff.Length);
                buff[4] = lenbu[0];
                buff[5] = lenbu[1];
                buff[6] = lenbu[2];
                buff[7] = lenbu[3];
                Array.Copy(sendBuff, 0, buff, 8, sendBuff.Length);
                Send(buff);
            }
            catch (Exception ex)
            {
                Log.Error("SendToServer", ex);
            }
        }
        public void Send(byte[] buff)
        {
            try
            {

                socketClient.Send(buff);
            }
            catch (Exception ex)
            {
            }
        }
        public void Start(string IP, int Port)
        {
            socketClient = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(IP);
            point = new IPEndPoint(ip, Port);

            //不停的接收服务器端发送的消息
            Thread thread = new Thread(Receive);
            thread.IsBackground = true;
            thread.Start();

            Thread CThread = new Thread(Connected);
            CThread.IsBackground = true;
            CThread.Start();

        }
        public void DisConnect()
        {
        }
        public void Connected()
        {
            while (true)
            {
                if (!socketClient.Connected)
                {
                    try
                    {
                        //进行连接
                        socketClient = new Socket(SocketType.Stream, ProtocolType.Tcp);
                        socketClient.Connect(point);
                        if (socketClient.Connected)
                        {
                            if (DoConnected != null)
                            {
                                DoConnected(true);
                            }
                        }
                        Log.Debug("TCP Connected");
                    }
                    catch (Exception ex)
                    {
                        //Log.Error("TCP Connect", ex);
                    }
                }

                Thread.Sleep(800);
            }
        }
        void Receive()
        {
            //  为什么用telnet客户端可以，但这个就不行。
            while (true)
            {
                try
                {
                    Thread.Sleep(10);
                    //获取发送过来的消息
                    byte[] buffer = new byte[1024 * 1024 * 2];
                    if (socketClient.Connected == true)
                    {
                        var effective = socketClient.Receive(buffer);
                        if (effective == 0)
                        {
                            socketClient.Shutdown(SocketShutdown.Send);
                            if (socketClient != null)
                                socketClient.Dispose();
                            continue;
                        }
                        byte[] newBuffer = new byte[effective];
                        Buffer.BlockCopy(buffer, 0, newBuffer, 0, effective);
                        UnPack(newBuffer);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Receive", ex);
                }
            }
        }
        List<byte> buffer = new List<byte>();
        private void UnPack(byte[] buff)
        {
            try
            {
                Log.Debug("TCP Data>>" + buff.ByteToHexStr(" "));
                buffer.AddRange(buff);// = buff.ToList();
                if (buffer.Count < 4)
                    return;
                byte[] lenbuff = buff.Skip(4).Take(4).ToArray();
                int length = BitConverter.ToInt32(lenbuff, 0);
                length = length + 8;
                byte[] Buff = new byte[length];
                //2.完整性判断
                while (buffer.Count >= length) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                {
                    //2.1 查找数据头
                    if (buffer[0] == 0xfa)//传输数据有帧头，用于判断
                    {
                        lenbuff = buff.Skip(4).Take(4).ToArray();
                        length = BitConverter.ToInt32(lenbuff, 0);
                        length = length + 8;
                        //如果大于12个字节，说明连包在一起
                        if (buffer.Count > length)
                        {
                            //加入集合
                            Buff = buffer.GetRange(0, length).ToArray();
                            DoReceiveCallback?.Invoke(Buff);
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
                            Buff = buffer.ToArray();
                            DoReceiveCallback?.Invoke(Buff);
                            buffer.RemoveRange(0, length);
                        }
                    }
                    else
                    {
                        //如果开头不是EE，则将此数据的前一部门删除，
                        //找到第一个EE开头的索引
                        int index = buffer.IndexOf(0xfa);
                        if (index > 0)
                        {
                            buffer.RemoveRange(0, index);
                        }
                        //头验证失败，
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("UnPack", ex);
            }
        }
    }

}

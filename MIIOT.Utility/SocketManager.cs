using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Trical
{
    public class SocketManager
    {

        public Dictionary<string, SocketInfo> _listSocketInfo = null;
        Socket _socket = null;
        EndPoint _endPoint = null;
        bool _isListening = false;
        int BACKLOG = 10;

        public delegate void OnConnectedHandler(SocketInfo socketInfo);
        public event OnConnectedHandler OnConnected;
        public delegate void OnReceiveMsgHandler(SocketInfo socketInfo);
        public event OnReceiveMsgHandler OnReceiveMsg;
        public event OnReceiveMsgHandler OnDisConnected;

        public SocketManager(int port)
        {

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _endPoint = new IPEndPoint(System.Net.IPAddress.Any, port);
            _listSocketInfo = new Dictionary<string, SocketInfo>();
            Thread threadRec = new Thread(QueueSendMsgPro);
            threadRec.IsBackground = true;
            threadRec.Start();
        }

        public void Start()
        {
            _socket.Bind(_endPoint); //绑定端口
            _socket.Listen(BACKLOG); //开启监听
            _isListening = true;
            Thread acceptServer = new Thread(AcceptWork); //开启新线程处理监听
            acceptServer.IsBackground = true;
            acceptServer.Start();
        }

        public void AcceptWork()
        {
            try
            {
                while (_isListening)
                {
                    Socket acceptSocket = _socket.Accept();
                    if (acceptSocket != null && this.OnConnected != null)
                    {
                        SocketInfo sInfo = new SocketInfo();
                        sInfo.socket = acceptSocket;
                        string str = ((System.Net.IPEndPoint)acceptSocket.RemoteEndPoint).Address.ToString();
                        string ss = str + ":";
                        //IPAddress remote_ip = ((System.Net.IPEndPoint)acceptSocket.RemoteEndPoint).Address;//获取远程连接IP 
                        //包含指定的IP 和端口 
                        //必须遍历字典的key
                        List<string> listTemp = new List<string>();
                        foreach (var listkey in _listSocketInfo.Keys)
                        {
                            //如果有重复的IP
                            if (listkey.Contains(ss))
                            {
                                //将重复的先记录下来
                                listTemp.Add(listkey);

                            }
                        }
                        //再删除重复的IP
                        foreach (string liststr in listTemp)
                        {
                            //如果是空的直接删除
                            if (_listSocketInfo[liststr].socket == null)
                            {
                                //删除原有的
                                _listSocketInfo.Remove(liststr);
                            }
                            //如果不是空的先释放资源再删除
                            else
                            {
                                //shutDownClient(liststr); 
                                //_listSocketInfo[acceptSocket.RemoteEndPoint.ToString()].socket.Close();//就是这里报错  资源未完全释放
                                //_listSocketInfo[liststr].socket.Shutdown(SocketShutdown.Both);
                                //_listSocketInfo[liststr].socket.Disconnect(true);
                                //_listSocketInfo[liststr].isConnected = false;
                                //_listSocketInfo[liststr].socket.Close();
                                ////是否需要将socket值为空
                                //_listSocketInfo[liststr].socket = null;
                                ////删除原有的
                                //_listSocketInfo.Remove(liststr);

                            }
                        }
                        //最后将新的添加进来
                        _listSocketInfo.Add(acceptSocket.RemoteEndPoint.ToString(), sInfo);

                        OnConnected(sInfo);
                        Thread socketConnectedThread = new Thread(newSocketReceive);
                        socketConnectedThread.IsBackground = true;
                        socketConnectedThread.Start(acceptSocket);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception err)
            {
                Console.Write(err.Message + "异常5");
            }
        }

        public void newSocketReceive(object obj)
        {
            Socket socket = obj as Socket;
            SocketInfo sInfo = _listSocketInfo[socket.RemoteEndPoint.ToString()];
            sInfo.isConnected = true;
            while (sInfo.isConnected)
            {
                try
                {
                    if (sInfo.socket == null) return;
                    //这里向系统投递一个接收信息的请求，并为其指定ReceiveCallBack做为回调函数 
                    sInfo.socket.BeginReceive(sInfo.buffer, 0, sInfo.buffer.Length, SocketFlags.None, ReceiveCallBack, sInfo.socket.RemoteEndPoint);

                }
                catch (Exception ex)
                {
                    Log.Error("newSocketReceive", ex);
                    Console.WriteLine(ex.Message);
                    //return;
                }
                Thread.Sleep(20);
            }
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                EndPoint ep = ar.AsyncState as IPEndPoint;
                SocketInfo info = _listSocketInfo[ep.ToString()];
                int readCount = 0;
                try
                {
                    if (info.socket == null) return;
                    readCount = info.socket.EndReceive(ar);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //return;
                }
                if (readCount > 0)
                {
                    //byte[] buffer = new byte[readCount];
                    //Buffer.BlockCopy(info.buffer, 0, buffer, 0, readCount);
                    if (readCount < info.buffer.Length)
                    {
                        byte[] newBuffer = new byte[readCount];
                        Buffer.BlockCopy(info.buffer, 0, newBuffer, 0, readCount);
                        info.msgBuffer = newBuffer;
                    }
                    else
                    {
                        info.msgBuffer = info.buffer;
                    }
                    string msgTip = Encoding.ASCII.GetString(info.msgBuffer);
                    if (msgTip == "\0\0\0faild")
                    {
                        info.isConnected = false;
                        if (this.OnDisConnected != null) OnDisConnected(info);
                        _listSocketInfo.Remove(info.socket.RemoteEndPoint.ToString());
                        info.socket.Close();
                        return;
                    }

                    Log.Info("Receive:" + info.msgBuffer.ByteToHexStr(" "));
                    UnPack(info);
                }
                else
                {

                }
            }
            //新增的错误处理机制
            catch (Exception err)
            {
                Console.Write(err.Message);
            }
        }
        private void UnPack(SocketInfo info)
        {
            try
            {
                Log.Debug("TCP Data>>" + info.msgBuffer.ByteToHexStr(" "));
                info.PoolBuffer.AddRange(info.msgBuffer);// = buff.ToList();
                if (info.PoolBuffer.Count < 4)
                    return;
                byte[] lenbuff = info.PoolBuffer.Skip(4).Take(4).ToArray();
                int length = BitConverter.ToInt32(lenbuff, 0);
                length = length + 8;
                byte[] Buff = new byte[length];
                //2.完整性判断
                while (info.PoolBuffer.Count >= length) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
                {
                    //2.1 查找数据头
                    if (info.PoolBuffer[0] == 0xfa)//传输数据有帧头，用于判断
                    {
                        lenbuff = info.PoolBuffer.Skip(4).Take(4).ToArray();
                        length = BitConverter.ToInt32(lenbuff, 0);
                        length = length + 8;
                        //如果大于12个字节，说明连包在一起
                        if (info.PoolBuffer.Count > length)
                        {
                            //加入集合
                            Buff = info.PoolBuffer.GetRange(0, length).ToArray();
                            info.UnpackBuffer = Buff;
                            OnReceiveMsg?.Invoke(info);
                            info.PoolBuffer.RemoveRange(0, length);
                        }
                        //说明包数据不完整，没有接受完毕.
                        else if (info.PoolBuffer.Count < length)
                        {
                            //返回去，等待下次进来数据
                            break;
                        }
                        else if (info.PoolBuffer.Count == length)
                        {
                            Buff = info.PoolBuffer.ToArray();
                            info.UnpackBuffer = Buff;
                            OnReceiveMsg?.Invoke(info);
                            info.PoolBuffer.RemoveRange(0, length);
                        }
                    }
                    else
                    {
                        //如果开头不是EE，则将此数据的前一部门删除，
                        //找到第一个EE开头的索引
                        int index = info.PoolBuffer.IndexOf(0xfa);
                        if (index > 0)
                        {
                            info.PoolBuffer.RemoveRange(0, index);
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
        public void SendByJson(string Remote, string Target, string CMD, string SourceType, string dataMsg)
        {
            try
            {
                var Data = Newtonsoft.Json.JsonConvert.DeserializeObject(dataMsg);
                var data = new { Target, CMD, SourceType, Data };
                string datastr = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                SendByPack(Remote, Encoding.GetEncoding("GB2312").GetBytes(datastr));
            }
            catch (Exception ex)
            {
                Log.Error("SendByJson", ex);
            }
        }
        public void SendByPack(string remote, byte[] sendBuff)
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
                SendMsg(remote, buff);
            }
            catch (Exception ex)
            {
                Log.Error("SendToServer", ex);
            }
        }
        public void SendMsg(string remote, string text)
        {
            sendInfo info = new sendInfo() { socketIP = remote, sendData = Encoding.ASCII.GetBytes(text) };
            SendMsgQueue.Enqueue(info);
        }
        public int SendMsg(string remote, byte[] buff)
        {
          
            int i = 0;
            try
            {
                foreach (var item in _listSocketInfo.Keys)
                {
                    if (item.Contains(remote))
                    {
                        i = _listSocketInfo[item].socket.Send(buff);
                        Log.Info("send:" + buff.ByteToHexStr(" "));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(remote + "SendMsg:", ex);

                return 0;
            }
            return i;
            sendInfo info = new sendInfo() { socketIP = remote, sendData = buff };
            SendMsgQueue.Enqueue(info);
        }
        private Queue SendMsgQueue = Queue.Synchronized(new Queue());
        private void QueueSendMsgPro()
        {
            while (true)
            {
                try
                {
                    sendInfo info = SendMsgQueue.Count > 0 ? (sendInfo)SendMsgQueue.Dequeue() : null;
                    string remoteIP = "";
                    if (info != null)
                    {
                        Socket CurrSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        try
                        {
                            foreach (var item in _listSocketInfo.Keys)
                            {
                                if (item.Contains(info.socketIP))
                                {
                                    remoteIP = item;
                                    _listSocketInfo[item].socket.Send(info.sendData);
                                    CurrSocket = _listSocketInfo[item].socket;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error(info.socketIP + "SendMsg:::", ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("QueueSendMsgPro", ex);
                }
                Thread.Sleep(5);
            }
        }
        public void Stop()
        {
            // _isListening = false;
            foreach (SocketInfo s in _listSocketInfo.Values)
            {
                s.socket.Close();
            }
        }

        public void shutDownClient(string point)
        {
            try
            {
                if (_listSocketInfo.ContainsKey(point))
                {

                    lock (_listSocketInfo)
                    {
                        Socket socket = _listSocketInfo[point].socket;
                        _listSocketInfo[point].isConnected = false;
                        lock (_listSocketInfo)
                        {
                            if (this.OnDisConnected != null) OnDisConnected(_listSocketInfo[point]);
                            _listSocketInfo.Remove(point);
                            Log.Debug(point + "==========>>>主动断开连接");
                        }

                        socket.Shutdown(SocketShutdown.Both);

                        System.Threading.Thread.Sleep(10);
                        socket.Close();

                    }


                }
            }
            catch (Exception ex)
            {
                Log.Error("shutDownClientError:::" + point, ex);
            }

        }

        public class SocketInfo
        {
            public Socket socket = null;
            public byte[] buffer = null;
            public byte[] msgBuffer = null;
            public List<byte> PoolBuffer = new List<byte>();
            public byte[] UnpackBuffer = null;
            public bool isConnected = false;

            public SocketInfo()
            {
                buffer = new byte[1024 * 24];
            }
        }
        public class sendInfo
        {
            public string socketIP { get; set; }
            public byte[] sendData { get; set; }
        }
    }
    public class HeartB
    {
        public string IP { get; set; }
        public int Beat { get; set; }
        public HeartB(string ip, int beat)
        {
            IP = ip;
            Beat = beat;
        }
    }



}

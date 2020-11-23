using MIIOT.Model.TricalModel;
using Newtonsoft.Json;
using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MIIOT.Utility
{
    public class SuperSoccketMrg
    {
        public delegate void DelMsgCallback(object sender, string remote, string msg, string msgType = "");
        public event DelMsgCallback DoMsgCallback;
        public delegate void DelConnect(object sender, string Remote, bool isConnect);
        public event DelConnect onConnect;
        private SSServer appServer;
        private int port = 4000;
        public SuperSoccketMrg()
        {

        }
        public void Start(int _port)
        {

            Log.Debug("TCP Server is Start");
            port = _port;
            if (appServer != null && appServer.State == SuperSocket.SocketBase.ServerState.Running)
                return;
            var config = new SuperSocket.SocketBase.Config.ServerConfig()
            {
                Name = "SuperSocketServer",
                ServerTypeName = "SuperSocketServer",
                ClearIdleSession = false, //60秒执行一次清理90秒没数据传送的连接
                ClearIdleSessionInterval = 60,
                IdleSessionTimeOut = 90,
                MaxRequestLength = 100000, //最大包长度
                Ip = "Any",
                Port = port,
                MaxConnectionNumber = 10000,//最大允许的客户端连接数目
            };
            appServer = new SSServer(app_NewSessionConnected, app_SessionClosed);
            //移除请求处理方法的注册，因为它和命令不能同时被支持：
            appServer.NewRequestReceived -= App_NewRequestReceived;
            appServer.NewRequestReceived += App_NewRequestReceived;
            appServer.Setup(config);
            if (!appServer.Start())
            {
                //初始化服务失败
            }
        }
        //客户端断开
        void app_SessionClosed(SSSession session, CloseReason value)
        {
            onConnect?.Invoke(this, session.SocketSession.RemoteEndPoint.ToString(), false);
        }
        //客户端连接
        void app_NewSessionConnected(SSSession session)
        {
            onConnect?.Invoke(this, session.SocketSession.RemoteEndPoint.ToString(), true);
        }
        //接收客户端消息
        private void App_NewRequestReceived(SSSession session, SSRequestInfo requestInfo)
        {
            if (requestInfo == null) return;
            if (!requestInfo.IsHeart)
            {
                DoMsgCallback?.Invoke(this, session.RemoteEndPoint.ToString(), requestInfo.Body);
                Log.Fatal("WS Receive:" + session.RemoteEndPoint.ToString() + "<<" + requestInfo.Body, new Exception());
            }
            else
            {
                DoMsgCallback?.Invoke(this, session.RemoteEndPoint.ToString(), requestInfo.Body, "HB");
                //var msg = MsgBuilder.BuildMsgCmd(requestInfo.Body);
                //session.Send(msg,0,msg.Length);
            }
        }
        public void SendByJson(string Remote, string Target, string CMD, string SourceType, string dataMsg)
        {
            try
            {
                var Data = Newtonsoft.Json.JsonConvert.DeserializeObject(dataMsg);

                var data = new { Target, CMD, SourceType, Data };
                string datastr = JsonConvert.SerializeObject(data);

                Send(Remote, datastr);
                Log.Fatal("WS Send:" + Remote + ">>" + datastr, new Exception());
            }
            catch (Exception ex)
            {
                Log.Error("SendByJson", ex);
            }
        }
       
        //发送消息
        protected bool Send(string Remote, string message)
        {
            if (appServer != null && appServer.State == ServerState.Running && !string.IsNullOrEmpty(message))
            {
                var servers = appServer.GetAllSessions();
                foreach (var item in servers)
                {
                    if (item.Connected)
                    {
                        if (item.RemoteEndPoint.ToString() == Remote)
                        {
                            var msg = MsgBuilder.BuildMsgCmd(message);
                            item.Send(msg, 0, msg.Length);
                        }
                    }
                    else
                        Console.WriteLine("Connected" + Remote + ":" +item.Connected);

                }
                return true;
            }
            return false;
        }
    }
}

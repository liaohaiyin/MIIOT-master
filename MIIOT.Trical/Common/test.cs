using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;

namespace OilServer
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 从配置文件启动
            var bootstrap = BootstrapFactory.CreateBootstrap();
            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadKey();
                return;
            }
            var result = bootstrap.Start();

            Console.WriteLine("Start result: {0}!", result);

            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();

            //Stop the appServer
            bootstrap.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
            #endregion
            #region 编程启动
            //MyServer oilServer=new MyServer();
            //oilServer.Setup("127.0.0.1", 9200);
            //oilServer.Start();
            //Console.WriteLine("输入任意键结束...");
            //Console.ReadLine();
            //oilServer.Stop();
            #endregion
        }
    }

    /// <summary>
    /// 某个完整请求
    /// </summary>
    public class MyRequestInfo : RequestInfo<byte[]>
    {
        public MyRequestInfo(string key, byte[] body)
        : base(key, body)
        {

        }
    }

    /// <summary>
    /// 请求解析
    /// </summary>
    public class MyReceiveFilter : FixedHeaderReceiveFilter<MyRequestInfo>
    {
        public MyReceiveFilter() : base(4)//头部为4个字节
        {
        }

        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            int len =
            BitConverter.ToInt32(
            new byte[] { header[offset + 3], header[offset + 2], header[offset + 1], header[offset] }, 0);
            return len;
        }

        protected override MyRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            var requestInfo = new MyRequestInfo(Guid.NewGuid().ToString(), bodyBuffer.CloneRange(offset, length));
            return requestInfo;
        }
    }
    /// <summary>
    /// 请求解析工厂
    /// </summary>
    public class MyReceiveFilterFactory : IReceiveFilterFactory<MyRequestInfo>
    {
        public IReceiveFilter<MyRequestInfo> CreateFilter(IAppServer appServer, IAppSession appSession, System.Net.IPEndPoint remoteEndPoint)
        {
            return new MyReceiveFilter();
        }
    }

    /// <summary>
    /// 某个socket会话
    /// </summary>
    public class MyOilSession : AppSession<MyOilSession, MyRequestInfo>
    {
        public MyOilSession()
        {
            this.Charset = System.Text.UTF8Encoding.UTF8;
        }
        public MyOilClient Client { get; private set; }
        protected override void OnSessionStarted()
        {
            //this.Client.SessionRegiste(this);
            base.OnSessionStarted();
        }

        protected override void OnSessionClosed(CloseReason reason)
        {
            //add your business operations
            //this.Client.SessionUnRegiste(this);
        }

        protected override void HandleUnknownRequest(MyRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }

        protected override void HandleException(Exception e)
        {
            base.HandleException(e);
        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        public void NewRequestReceived(MyRequestInfo requestInfo)
        {
            int s = System.Threading.Thread.CurrentThread.GetHashCode();
            int k = s;
            var requestData = requestInfo.Body;
        }
    }
    public interface IDespatchServer
    {
        void DispatchMessage(IDespatchServer despatchServer, MyOilSession session, byte[] data);
    }
    /// <summary>
    /// 服务器端
    /// </summary>
    public class MyServer : AppServer<MyOilSession, MyRequestInfo>, IDespatchServer
    {
        public MyServer()
        : base(new MyReceiveFilterFactory())
        {
            base.NewSessionConnected += MyOilServer_NewSessionConnected;
            base.NewRequestReceived += MyOilServer_NewRequestReceived;
            base.SessionClosed += MyOilServer_SessionClosed;
            int s = System.Threading.Thread.CurrentThread.GetHashCode();
            int k = s;
        }

        void MyOilServer_SessionClosed(MyOilSession session, CloseReason value)
        {
            //throw new NotImplementedException();
            int s = System.Threading.Thread.CurrentThread.GetHashCode();
            int k = s;
        }

        void MyOilServer_NewRequestReceived(MyOilSession session, MyRequestInfo requestInfo)
        {
            //throw new NotImplementedException();
            session.NewRequestReceived(requestInfo);
            int s = System.Threading.Thread.CurrentThread.GetHashCode();
            int k = s;
            IDespatchServer iserver = null;
            switch (this.Name)
            {
                case "TelnetServerA":
                    iserver = this.Bootstrap.GetServerByName("TelnetServerB") as IDespatchServer;
                    break;
                case "TelnetServerB":
                    iserver = this.Bootstrap.GetServerByName("TelnetServerC") as IDespatchServer;
                    break;
                case "TelnetServerC":
                    iserver = this.Bootstrap.GetServerByName("TelnetServerA") as IDespatchServer;
                    break;
            }
            if (iserver != null)
                iserver.DispatchMessage(this, session, requestInfo.Body);
            else
            {

            }
        }

        void MyOilServer_NewSessionConnected(MyOilSession session)
        {
            //throw new NotImplementedException();
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }
        protected override void OnStopped()
        {
            base.OnStopped();
        }

        public List<MyOilSession> Find(string loginCode)
        {
            var sessions = this.GetSessions(a => a.Client != null && a.Client.LoginCode == loginCode);
            return sessions.ToList();
        }

        public void DispatchMessage(IDespatchServer despatchServer, MyOilSession session, byte[] data)
        {
            var sessions = this.GetAllSessions();
            foreach (var myOilSession in sessions)
            {
                myOilSession.Send(data, 0, data.Length);
            }
        }
    }

    public class MyOilClient
    {
        public string DeviceToken { get; set; }
        public string LoginCode { get; set; }
        public string OrgCode { get; set; }
        public Guid ClientID
        {
            get;
            private set;
        }
        public MyOilClientManager ClientManager { get; set; }
        public MyOilClient(Guid clientID)
        {
            this.ClientID = clientID;
        }
        /// <summary>
        /// 客户端的所有会话
        /// </summary>
        private List<MyOilSession> AllSessions = new List<MyOilSession>(1);

        public int SessionsCount
        {
            get
            {
                lock (AllSessions)
                {
                    return AllSessions.Count;
                }
            }
        }
        /// <summary>
        /// 添加会话
        /// </summary>
        /// <param name="session"></param>
        public void SessionRegiste(MyOilSession session)
        {
            lock (AllSessions)
            {
                if (!AllSessions.Contains(session))
                    AllSessions.Insert(0, session);
            }
        }

        public void SessionUnRegiste(MyOilSession session)
        {
            lock (AllSessions)
            {
                if (AllSessions.Contains(session))
                    AllSessions.Remove(session);
            }
        }

        public bool Send(byte[] data, int offset, int length)
        {
            var session = GetEnableSession();
            if (session != null)
            {
                session.Send(data, offset, length);
                return true;
            }
            else
            {
                return false;
            }
        }
        public MyOilSession GetEnableSession()
        {
            lock (AllSessions)
            {
                foreach (var myOilSession in AllSessions)
                {
                    if (myOilSession.Connected)
                    {
                        return myOilSession;
                    }
                }
            }
            return null;
        }
    }

    public class MyOilClientManager
    {
        private List<MyOilClient> AllClients = new List<MyOilClient>();

        public void OilClientRegiste(MyOilClient client)
        {
            lock (AllClients)
            {
                if (!AllClients.Contains(client))
                    AllClients.Add(client);
            }
        }

        public bool OilClientUnRegiste(MyOilClient client)
        {
            lock (AllClients)
            {
                if (AllClients.Contains(client))
                {
                    AllClients.Remove(client);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
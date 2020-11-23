using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    public class SSServer : AppServer<SSSession, SSRequestInfo>
    {
        public SSServer()
           : base(new DefaultReceiveFilterFactory<SSReceiveFilter, SSRequestInfo>())
        {
            this.NewSessionConnected += MyServer_NewSessionConnected;
            this.SessionClosed += MyServer_SessionClosed;
        }

        public SSServer(SessionHandler<SSSession> NewSessionConnected, SessionHandler<SSSession, CloseReason> SessionClosed)
            : base(new DefaultReceiveFilterFactory<SSReceiveFilter, SSRequestInfo>())
        {
            this.NewSessionConnected += NewSessionConnected;
            this.SessionClosed += SessionClosed;
        }

        protected override void OnStarted()
        {
            //启动成功
        }

        void MyServer_NewSessionConnected(SSSession session)
        {
            //连接成功
        }

        void MyServer_SessionClosed(SSSession session, CloseReason value)
        {

        }
    }
    public class SSSession : AppSession<SSSession, SSRequestInfo>
    {

    }
}

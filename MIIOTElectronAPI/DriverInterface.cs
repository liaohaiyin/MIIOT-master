using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOTElectronAPI
{
    public class DriverInterface
    {
        public DriverInterface()
        { 
        
        }
        SocketManager tcpserver = new SocketManager();

        public async Task<Object> GetVersion(object input)
        {
            return "DotNet 4.5";
        }
        public async Task<Object> ServerOpen(object input)
        {
            if (input == null) return "Parameter is Null";
            int port = 4000;
            if (!int.TryParse(input.ToString(), out port))
            {
                return "Parameter is invalid";
            }
            try
            {
                tcpserver.OnConnected += Tcpserver_OnConnected;
                tcpserver.OnDisConnected += Tcpserver_OnDisConnected;
                tcpserver.OnReceiveMsg += Tcpserver_OnReceiveMsg;
                tcpserver.Start(port);
                return "Succeed";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }

        private void Tcpserver_OnReceiveMsg(SocketManager.SocketInfo socketInfo)
        {

        }

        private void Tcpserver_OnDisConnected(SocketManager.SocketInfo socketInfo)
        {

        }

        private void Tcpserver_OnConnected(SocketManager.SocketInfo socketInfo)
        {

        }
    }
}

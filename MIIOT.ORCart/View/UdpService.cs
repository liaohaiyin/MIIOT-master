using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/3 14:46:41
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart.View
{
    public class UdpService
    {
        #region 内部变量

        string devIP = "127.0.0.1";
        int devPort = 60000;
        UdpClient mySocket;
        string meIP = "127.0.0.1";
        int mePort = 60000;
        IPEndPoint RemotePoint;
        bool isRunning = false;
        bool isOpen = false;
        List<Thread> listThread = new List<Thread>();
        string icId = "";
        string cardContent = "";
        byte[] cardContentBuf;
        #endregion

        public UdpService(string ServerIP, int ServerPort, string DevIP, int DevPort)
        {
            this.meIP = ServerIP;
            this.mePort = ServerPort;
            this.devIP = DevIP;
            this.devPort = DevPort;

        }

        #region   

        public void TurnOn()
        {
            try
            {
                if (isOpen) return;
                mySocket = new UdpClient(mePort);
                IPEndPoint ipLocalPoint = new IPEndPoint(IPAddress.Parse(meIP), mePort);

                RemotePoint = new IPEndPoint(IPAddress.Any, devPort);

                isRunning = true;
                Thread thread = new Thread(new ThreadStart(this.ReceiveHandle));
                thread.IsBackground = true;
                thread.Start();
                listThread.Add(thread);
                isOpen = true;

            }
            catch (Exception ex)
            {
                isOpen = false;
                throw new Exception(ex.Message);
            }
        }

        public void TurnOff()
        {
            try
            {
                isOpen = false;
                isRunning = false;

                for (int i = 0; i < listThread.Count; i++)
                {
                    try
                    {
                        listThread[i].Abort();
                    }
                    catch (Exception) { }
                }

                if (mySocket != null)
                {
                    mySocket.Close();
                }
            }
            catch (Exception)
            {
            }
        }
        public void Send(string message)
        {
            IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Parse(devIP), devPort);

            byte[] data = Encoding.Default.GetBytes(message);

            mySocket.Send(data, data.Length, ipendpoint);
        }
        public delegate void GetRecevice(byte[] ReceviceBuff);
        public event GetRecevice EvtGetValues;
        private void ReceiveHandle()
        {
            byte[] sendbuf = new byte[9];
            byte[] sendwritbuf = new byte[200];

            while (isRunning)
            {
                try
                {
                    if (mySocket == null || mySocket.Available < 1)
                    {
                        Thread.Sleep(300);
                        continue;
                    }
                    //接收UDP数据报，引用参数RemotePoint获得源地址  
                    byte[] buf = mySocket.Receive(ref RemotePoint);
                    if (devIP == null || devIP.Length < 1)
                    {
                        devIP = RemotePoint.Address.ToString();
                        devPort = RemotePoint.Port;
                    }
                    if (EvtGetValues != null)
                    {
                        EvtGetValues(buf);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion
    }
}

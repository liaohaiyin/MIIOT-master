using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers.Server
{
    
    public class NetServerManager
    {
        public delegate void DelMsgBack(object sender, string Msg);
        public event DelMsgBack OnMsgBack;

        public delegate void DelBufferBack(object sender, byte[] Buff);
        public event DelBufferBack OnBufferBack;

        public delegate void DelConnect(object sender, bool isConnect);
        public event DelConnect OnConnect;

        Queue SendQueue = Queue.Synchronized(new Queue());
        TCPClientUtil TCPServer = new TCPClientUtil();
        string IP = "";
        int Port = 4000;
        public string Cabinet { get; set; } 
        public string MacType { get; set; }
        public NetServerManager()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        byte[] buff = SendQueue.Count > 0 ? (byte[])SendQueue.Dequeue() : null;
                        if (buff != null)
                        {
                            if (TCPServer.socketClient.Connected)
                            {
                                TCPServer.socketClient.Send(buff);
                                OnMsgBack?.Invoke(this, ((NetType)buff[2]).GetEnumDescription() + ">>" + buff.ByteToHexStr(" "));
                            }
                            else if (buff[2] != 0x8f)
                            {
                                OnMsgBack?.Invoke(this, "TCP Server Lost" + buff.ByteToHexStr(" "));

                            }
                        }
                        Thread.Sleep(3);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("SendQueue", ex);
                    }
                }
            });
        }
        public void Start(string ip, int port)
        {
            IP = ip;
            Port = port;
            TCPServer.DoReceiveCallback += Controler_DoReceiveCallback;
            TCPServer.DoConnected += Controler_DoConnected;
            TCPServer.Start(IP, Port);
        }

        private void Controler_DoConnected(bool IsConnect)
        {
            OnConnect?.Invoke(this, IsConnect);
        }

        private void Controler_DoReceiveCallback(byte[] Buff)
        {
            OnBufferBack?.Invoke(this, Buff);
        }
        public void SendToServer(byte Bit, byte[] sendBuff)
        {
            try
            {
                byte[] buff = new byte[sendBuff.Length + 8];
                buff[0] = 0xfa;
                buff[1] = 0xf5;
                buff[2] = Bit;
                buff[3] = sendBuff.CalcationCheckout();
                byte[] lenbu = BitConverter.GetBytes(sendBuff.Length);
                buff[4] = lenbu[0];
                buff[5] = lenbu[1];
                buff[6] = lenbu[2];
                buff[7] = lenbu[3];
                Array.Copy(sendBuff, 0, buff, 8, sendBuff.Length);
                string aa = buff.ByteToHexStr(" ");
                SendQueue.Enqueue(buff);
            }
            catch (Exception ex)
            {
                Log.Error("SendToServer", ex);
            }
        }
        public void LEDComplete()
        {
            byte[] sendbuff = new byte[5];
            SendToServer((byte)NetCompleteType.LCD, sendbuff);
        }
        public void BLightComplete()
        {
            byte[] sendbuff = new byte[5];
            SendToServer((byte)NetCompleteType.BackLight, sendbuff);
        }
    }
}

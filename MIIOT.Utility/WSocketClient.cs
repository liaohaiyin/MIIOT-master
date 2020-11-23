using SuperSocket.ClientEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;

namespace MIIOT.Utility
{
    public class WSocketClient : IDisposable
    {
        //日志管理

        #region 向外传递数据事件
        public event Action<WSocketClient, WSMsgModel> MessageReceived;
        #endregion

        WebSocket4Net.WebSocket _webSocket;
        /// <summary>
        /// 检查重连线程
        /// </summary>
        Thread _thread;
        bool _isRunning = true;
        /// <summary>
        /// WebSocket连接地址
        /// </summary>
        public string ServerPath { get; set; }
        public string ClientName { get; set; }
        public Queue MsgQueue = Queue.Synchronized(new Queue());
        public WSocketClient(string url, string clientName)
        {
            ServerPath = url;
            ClientName = clientName;
            this._webSocket = new WebSocket4Net.WebSocket(url);
            this._webSocket.Opened += WebSocket_Opened;
            this._webSocket.Error += WebSocket_Error;
            this._webSocket.Closed += WebSocket_Closed;
            this._webSocket.MessageReceived += WebSocket_MessageReceived;

            Thread The = new Thread(new ThreadStart(() =>
            {
                try
                {

                    while (true)
                    {
                        if (MsgQueue.Count > 0)
                        {
                            WSMsgModel msg = MsgQueue.Dequeue() as WSMsgModel;
                            if (msg != null)
                            {
                                if (index == 65535)
                                    index = 0;
                                index++;
                                msg.SEQ = index;

                                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(msg);
                                if (msg.ECMD == "HB")
                                {
                                    var msgData = new { MacNo = msg.MacNo, ECMD = msg.ECMD, SEQ = index };
                                    dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(msgData);
                                }
                                if (_webSocket != null && _webSocket.State == WebSocket4Net.WebSocketState.Open)
                                {
                                    this._webSocket.Send(dataJson);
                                }
                            }
                        }
                        Thread.Sleep(20);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("", ex);
                }
            }));
            The.IsBackground = true;
            The.Start();
        }

        #region "web socket "
        /// <summary>
        /// 连接方法
        /// <returns></returns>
        public bool Start()
        {
            bool result = true;
            try
            {
                this._webSocket.Open();

                this._isRunning = true;
                this._thread = new Thread(new ThreadStart(CheckConnection));
                _thread.IsBackground = true;
                this._thread.Start();
            }
            catch (Exception ex)
            {
                Log.Error("WSStart", ex);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 消息收到事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var aaa = Newtonsoft.Json.JsonConvert.DeserializeObject<MIIOT.Utility.WSMsgModel>(e.Message);
            if (aaa.ECMD != "Reply")
            {
                SendMessage("", "Reply");
            }
            if (aaa.ECMD == "B")
            {
                MessageReceived?.Invoke(this, aaa);
            }

        }
        /// <summary>
        /// Socket关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Closed(object sender, EventArgs e)
        {
            Log.Info("websocket_Closed");
        }
        /// <summary>
        /// Socket报错事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Error(object sender, ErrorEventArgs e)
        {
            Log.Info("websocket_Error:" + e.Exception.ToString());
        }
        /// <summary>
        /// Socket打开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Opened(object sender, EventArgs e)
        {
            Log.Info(" websocket_Opened");
        }
        /// <summary>
        /// 检查重连线程
        /// </summary>
        private void CheckConnection()
        {
            do
            {
                try

                {
                    if (this._webSocket.State != WebSocket4Net.WebSocketState.Open && this._webSocket.State != WebSocket4Net.WebSocketState.Connecting)
                    {
                        Log.Info(" Reconnect websocket WebSocketState:" + this._webSocket.State);
                        this._webSocket.Close();
                        this._webSocket.Open();
                        Console.WriteLine("正在重连");
                    }
                    if (this._webSocket.State == WebSocket4Net.WebSocketState.Open)
                    {
                        SendMessage("", "HB");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("CheckConnection", ex);
                }
                System.Threading.Thread.Sleep(5000);
            } while (this._isRunning);
        }
        #endregion
        int index = 0;
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Message"></param>
        public void SendMessage(string Message, string ECMD = "B")
        {

            WSMsgModel msg = new WSMsgModel();
            msg.MacNo = ClientName;
            msg.ECMD = ECMD;
            msg.Data = Newtonsoft.Json.JsonConvert.DeserializeObject(Message);
            MsgQueue.Enqueue(msg);

        }

        public void Dispose()
        {
            this._isRunning = false;
            try
            {
                _thread.Abort();
            }
            catch
            {

            }
            this._webSocket.Close();
            this._webSocket.Dispose();
            this._webSocket = null;
        }
    }
    public class WSMsgModel
    {
        public string MacNo { get; set; }
        public string ECMD { get; set; }
        public int SEQ { get; set; }
        public object Data { get; set; }
        public override string ToString()
        {
            return MacNo + "|" + ECMD + "|" + SEQ + "|" + Data.ToString();
        }
    }
    public class WSMsgDtl
    {
        public string MacNo { get; set; }
        public string busType { get; set; }
        public List<string> datalist { get; set; }
    }
}

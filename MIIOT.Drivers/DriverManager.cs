using MIIOT.Drivers.Server;
using MIIOT.Model;
using MIIOT.Model.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class DriverManager : DriverObserver
    {
        //TCPClientUtil TCPServer = new TCPClientUtil();
        public NetServerManager NetM = new NetServerManager();
        public FingerManager FingerM = new FingerManager();
        public HIDScan hIDScan = new HIDScan();
        //Dictionary<string, RFIDManager> DicRFIDS = new Dictionary<string, RFIDManager>();
        public DriverManager() : base()
        {

        }
        public override void Start()
        {
            var macParas = LiteDBHelper.Ins.GetCollection<MacPara>();
            foreach (var item in macParas)
            {
                try
                {
                    MacTypeEnum macType = item.MacKey.ParseEnum<MacTypeEnum>();
                    switch (macType)
                    {
                        case MacTypeEnum.Server:
                            NetM.Cabinet = item.Cabinet;
                            NetM.MacType = item.MacKey;
                            NetM.OnMsgBack += Server_OnMsgBack;
                            NetM.OnConnect += Server_OnConnect;
                            NetM.OnBufferBack += Server_OnBufferBack;
                            NetM.Start(item.COM, int.Parse(item.BaudRate));
                            break;
                        case MacTypeEnum.PCB:
                            MCUManager.Instance.Cabinet = item.Cabinet;
                            MCUManager.Instance.MacType = item.MacKey;
                            MCUManager.Instance.OnReturnManagerDataEventArgs += MCUManager_DoReturnManagerAction;
                            MCUManager.Instance.OnOpenSerialEventArgs += MCUManager_OpenSerialEventArgs;
                            MCUManager.Instance.NetM = this.NetM;
                            MCUManager.Instance.Start(item.COM, int.Parse(item.BaudRate));
                            break;
                        case MacTypeEnum.FINGER:
                            FingerM.Cabinet = item.Cabinet;
                            FingerM.MacType = item.MacKey;
                            FingerM.DoMsgBack += Finger_DoMsgBack;
                            FingerM.OnOpenClose += FingerM_OnOpenClose;
                            FingerM.Start(item.COM, int.Parse(item.BaudRate));
                            break;
                        case MacTypeEnum.HID:
                            hIDScan.Cabinet = item.Cabinet;
                            hIDScan.MacType = item.MacKey;
                            hIDScan.DoBarcodeConnect += HIDScan_DoBarcodeConnect;
                            byte[] vID = item.COM.StrToToHexByte();
                            Array.Reverse(vID);
                            byte[] pID = item.BaudRate.StrToToHexByte();
                            Array.Reverse(pID);
                            hIDScan.Start(BitConverter.ToUInt16( vID, 0), BitConverter.ToUInt16(pID, 0));
                            break;
                        case MacTypeEnum.RFID:
                            //bool IsConnect = RFIDManager.Ins.Start(item.Cabinet, item.COM, item.BaudRate);
                            //if (NotifyEvent != null)
                            //{
                            //    NotifyEvent(this, MacTypeEnum.RFID.ToString(), item.Cabinet, IsConnect);
                            //}
                            break;
                        case MacTypeEnum.FRIDGE:
                            break;
                        case MacTypeEnum.POSPRINTER:
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(item + " Init:", ex);
                }
            }

        }

        private void FingerM_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            if (NotifyEvent != null)
            {
                NotifyEvent(this, MacTypeEnum.FINGER.ToString(), "1", args.IsOpen);
            }
        }

        private void HIDScan_DoBarcodeConnect(bool isConnected)
        {
            if (NotifyEvent != null)
            {
                NotifyEvent(this, MacTypeEnum.HID.ToString(), "1", isConnected);
            }
        }

        private void Server_OnBufferBack(object sender, byte[] Buff)
        {

        }

        private void Server_OnConnect(object sender, bool isConnect)
        {
            if (sender is NetServerManager magr)
            {
                if (NotifyEvent != null)
                {
                    NotifyEvent(this, magr.MacType, magr.Cabinet, isConnect);
                }
                // settingView.UpdateStatus(magr.Cabinet, magr.MacName, isConnect);
            }
        }

        private void Server_OnMsgBack(object sender, string Msg)
        {

        }
        private void MCUManager_OpenSerialEventArgs(object sender, bool status)
        {
            if (sender is MCUManager magr)
            {
                if (NotifyEvent != null)
                {
                    NotifyEvent(this, magr.MacType, magr.Cabinet, status);
                }
                // settingView.UpdateStatus(magr.Cabinet, magr.MacName, isConnect);
            }
        }
        private void MCUManager_DoReturnManagerAction(object sender, MCReviceEventArgs fBack)
        {

        }
        private void Finger_DoMsgBack(string msg)
        {

        }
    }
}

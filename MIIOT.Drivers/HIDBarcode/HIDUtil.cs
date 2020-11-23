using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class HIDUtil : object
    {
        #region parameter Define
        HIDInterface hid = new HIDInterface();


        struct connectStatusStruct
        {
            public bool? preStatus;
            public bool curStatus;
        }

        connectStatusStruct connectStatus = new connectStatusStruct();

        //推送连接状态信息
        public delegate void isConnectedDelegate(bool isConnected);
        public event isConnectedDelegate isConnectedFunc;


        //推送接收数据信息
        public delegate void PushReceiveDataDele(byte[] datas);
        public event PushReceiveDataDele pushReceiveData;

        #endregion
        public ushort VID { get; set; }
        public ushort PID { get; set; }
        public  HIDUtil(ushort vID,ushort pID)
        {
            VID = vID;
            PID = pID;
            Initial();
        }
        //第一步需要初始化，传入vid、pid，并开启自动连接
        public void Initial()
        {

            hid.StatusConnected = StatusConnected;
            hid.DataReceived = DataReceived;
            HIDInterface.HidDevice hidDevice = new HIDInterface.HidDevice();
            hidDevice.vID = VID;// 0x0c2e;//0c2e
            hidDevice.pID = PID;// 0x0e41;//0e41
            hidDevice.serial = "";
            hid.AutoConnect(hidDevice);

        }

        //不使用则关闭
        public void Close()
        {
            hid.StopAutoConnect();
        }

        //发送数据
        public bool SendBytes(byte[] data)
        {
            return hid.Send(data);

        }
        //接受到数据
        private void DataReceived(object sender, byte[] e)
        {
            if (pushReceiveData != null)
                pushReceiveData(e);
        }

        //状态改变接收
        private void StatusConnected(object sender, bool isConnect)
        {
            connectStatus.curStatus = isConnect;
            if (connectStatus.curStatus == connectStatus.preStatus)  //connect
                return;
            connectStatus.preStatus = connectStatus.curStatus;

            if (connectStatus.curStatus)
            {
                if(isConnectedFunc!=null)
                isConnectedFunc(true);
                //ReportMessage(MessagesType.Message, "连接成功");
            }
            else //disconnect
            {
                if (isConnectedFunc != null)
                    isConnectedFunc(false);
                //ReportMessage(MessagesType.Error, "无法连接");
            }
        }


    }
}

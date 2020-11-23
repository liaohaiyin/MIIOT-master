using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class HIDScan
    {
        public delegate void DelBarcodeBack(byte[] barcode);
        public event DelBarcodeBack DoBarcodeBack;
        public delegate void DelScanConnect(bool isConnected);
        public event DelScanConnect DoBarcodeConnect;
        HIDUtil hid;

        public string Cabinet { get; set; }
        public string MacType { get; set; }
        public HIDScan()
        {
           
        }
        public void Start(ushort VID, ushort PID)
        {
            hid = new HIDUtil(VID, PID);
            hid.pushReceiveData += Hid_pushReceiveData;
            hid.isConnectedFunc += Hid_isConnectedFunc;
        }
        private void Hid_isConnectedFunc(bool isConnected)
        {
            DoBarcodeConnect?.Invoke(isConnected);
        }

        #region HID扫码

        bool? IsTrgMode = null;
        private void Hid_pushReceiveData(byte[] datas)
        {
            string Result = datas.ByteToHexStr();
            if (datas[3] == 0x48 && datas[4] == 0x48 && datas[5] == 0x46)
            {
                IsTrgMode = true;
            }
            else if (datas[3] == 0x54 && datas[4] == 0x50 && datas[5] == 0x52)
            {
                IsTrgMode = false;
            }
            else
            {
                byte[] buff = new byte[datas.Length + 4];
                buff[0] = 0x01;
                buff[1] = 0xff;
                buff[2] = 0xff;
                buff[3] = 0x10;
                Array.Copy(datas, 0, buff, 4, datas.Length);
                // SendToServer(0x89, buff);
                DoBarcodeBack?.Invoke(buff);
            }
        }
        /// <summary>
        /// 亮灯，扫描成功则关闭补光灯
        /// </summary>
        public void TrgScan()
        {
            Task.Run(() =>
            {
                byte[] buff = new byte[64];
                if (IsTrgMode == false)
                {
                    buff[0] = 0xFD;
                    buff[1] = 0x0A;
                    buff[2] = 0x16;
                    buff[3] = 0x4D;
                    buff[4] = 0x0D;
                    buff[5] = 0x70;
                    buff[6] = 0x61;
                    buff[7] = 0x70;
                    buff[8] = 0x68;
                    buff[9] = 0x68;
                    buff[10] = 0x66;
                    buff[11] = 0x21;//开启手动触发模式
                    hid.SendBytes(buff);
                    Thread.Sleep(100);
                }
                buff = new byte[64];
                buff[0] = 0xFD;
                buff[1] = 0x03;
                buff[2] = 0x16;
                buff[3] = 0x54;
                buff[4] = 0x0D;
                hid.SendBytes(buff);//手动触发模式下开启扫描
                byte[] sendbuff = new byte[4];
                //SendToServer(0x89, sendbuff);
                DoBarcodeBack?.Invoke(buff);
            });
        }
        /// <summary>
        /// 一直亮灯，持续扫码
        /// </summary>
        public void Scaning()
        {
            if (IsTrgMode == false)
                return;
            byte[] buff = new byte[64];
            buff[0] = 0xFD;
            buff[1] = 0x0A;
            buff[2] = 0x16;
            buff[3] = 0x4D;
            buff[4] = 0x0D;
            buff[5] = 0x70;
            buff[6] = 0x61;
            buff[7] = 0x70;
            buff[8] = 0x74;
            buff[9] = 0x70;
            buff[10] = 0x72;
            buff[11] = 0x21;//关闭手动触发模式，既连续扫码模式
            hid.SendBytes(buff);
            byte[] sendbuff = new byte[4];
            //SendToServer(0x89, sendbuff);
            DoBarcodeBack?.Invoke(buff);
        }
        /// <summary>
        /// 
        /// </summary>
        public void ScanEnd()
        {

            byte[] buff = new byte[64];
            if (IsTrgMode == false)
            {
                buff = new byte[64];
                buff[0] = 0xFD;
                buff[1] = 0x0A;
                buff[2] = 0x16;
                buff[3] = 0x4D;
                buff[4] = 0x0D;
                buff[5] = 0x70;
                buff[6] = 0x61;
                buff[7] = 0x70;
                buff[8] = 0x68;
                buff[9] = 0x68;
                buff[10] = 0x66;
                buff[11] = 0x21;//开启手动触发模式
                hid.SendBytes(buff);
            }

            buff = new byte[64];
            buff[0] = 0xFD;
            buff[1] = 0x03;
            buff[2] = 0x16;
            buff[3] = 0x55;
            buff[4] = 0x0D;
            hid.SendBytes(buff);//手动触发模式下关闭扫描
            byte[] sendbuff = new byte[4];
            //SendToServer(0x89, sendbuff);
            DoBarcodeBack?.Invoke(buff);
        }
        #endregion
    }
}

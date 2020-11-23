using MIIOT.Drivers.RFID;
using MIIOT.Drivers.SRFID;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class HRFIDUtil : RFIDBase
    {
        int FrmHandle = -1;
        byte ComAddr = 255;
        int fCmdRet = 48;
        int NXPX2 = 1;//是否支持二代标签0不支持，1支持
        int ISAFI = 0;//0表示以AFI为0x00的方式询查标签,1表示以不带AFI的方式询查标签
        int IRSSI = 0;//0表示输出RSSI，1表示输出信噪比
        byte Option = 0;

        public HRFIDUtil()
        {

            Option |= (byte)NXPX2;
            Option |= (byte)(ISAFI << 1);
            Option |= (byte)(IRSSI << 2);
            for (int i = 0; i < 24; i++)
            {
                antList.Add((byte)i);
            }
        }

        public override bool Open(string COM, string BoudRate)
        {
            this.PortName = COM;
            this.BoudRate = BoudRate;
            try
            {
                byte[] VersionInfo = new byte[2];
                byte[] trType = new byte[2];
                byte ReaderType = 0;
                byte scanTime = 0;
                int port = int.Parse(PortName.Substring(3, PortName.Length - 3));
                int boudRate = int.Parse(BoudRate);
                byte rate = 0;
                switch (boudRate)
                {
                    case 19200:
                        rate = 0;
                        break;
                    case 38400:
                        rate = 1;
                        break;
                    case 57600:
                        rate = 2;
                        break;
                    case 115200:
                        rate = 3;
                        break;
                    default:
                        break;
                }
                fCmdRet = RWDev.OpenComPort(port, ref ComAddr, rate, ref FrmHandle);
                Log.Debug("Open:" + fCmdRet);
                fCmdRet = RWDev.GetReaderInformation(ref ComAddr, VersionInfo, ref ReaderType, trType, ref scanTime, FrmHandle);
                Log.Debug("GetReaderInformation:" + fCmdRet);
                if (fCmdRet == 0)
                {
                    IsConnected = true;

                }
            }
            catch (Exception ex)
            {

                Log.Error("Open", ex);
            }
            return IsConnected;
        }
        Thread ReadThread = null;

        List<byte> antList = new List<byte>();
        protected void RFIDRead()
        {
            if (IsReading)
            {
                if (OnReturnManagerDataEventArgs != null)
                {
                    RFIDReviceEventArgs backMsg = new RFIDReviceEventArgs();
                    backMsg.eventType = EventType.SerialRevice;
                    backMsg.sRFIDInstructionType = SRFIDInstructionType.IsBusy;
                    backMsg.sRFIDInstructionType = SRFIDInstructionType.RecCycRead;
                    OnReturnManagerDataEventArgs(this, backMsg);
                }
                return;
            }
            IsReading = true;
            fCmdRet = RWDev.OpenRf(ref ComAddr, FrmHandle);
            foreach (byte Ant in antList)
            {
                byte AntNum = Ant;
                fCmdRet = RWDev.SetActiveANT(ref ComAddr, ref AntNum, FrmHandle);
                byte state = 0x01;
                byte errorCode = 0;
                byte[] UID = new byte[8];
                fCmdRet = RWDev.ResetToReady(ref ComAddr, ref state, UID, ref errorCode, FrmHandle);
            }
            fCmdRet = RWDev.CloseRf(ref ComAddr, FrmHandle);

            byte AntNum1 = 0;
            fCmdRet = RWDev.SetActiveANT(ref ComAddr, ref AntNum1, FrmHandle);

            ReadThread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    GetCollectionInventory();

                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                }
            }));
            ReadThread.IsBackground = true;
            ReadThread.Start();
        }
        byte[] Ant_Target = new byte[4];
        List<string> uidlist_norsssi = new List<string>();
        List<TagInfo> uidlist_rssi = new List<TagInfo>();

        private void GetCollectionInventory()
        {
            uidlist_norsssi.Clear();
            uidlist_rssi.Clear();
            Ant_Target[0] = 0xff;
            Ant_Target[1] = 0xff;
            Ant_Target[2] = 0xff;
            Ant_Target[3] = 0xff;
            int cardNumber = 0;
            byte[] DSFIDAndUID = new byte[40000];
            string strDSFIDAndUID = "";
            fCmdRet = RWDev.Inventory_Collection(ref ComAddr, Option, Ant_Target, DSFIDAndUID, ref cardNumber, FrmHandle);
            if (cardNumber > 0)
            {
                if (IRSSI == 0)//RSSI查询
                {
                    Array.Resize(ref DSFIDAndUID, cardNumber * 11);
                    strDSFIDAndUID = DSFIDAndUID.ByteToHexStr();
                    for (int index = 0; index < cardNumber; index++)
                    {
                        TagInfo tag = new TagInfo();
                        string mytemp = strDSFIDAndUID.Substring(index * 22, 22);

                        tag.UID = mytemp.Substring(2, 16);
                        tag.dsfid = mytemp.Substring(0, 2);
                        //BackQueue.Enqueue(tag.UID);
                        byte rssivalue = Convert.ToByte(mytemp.Substring(18, 2), 16);

                        if ((rssivalue & 0x80) == 0)
                            tag.rssi = rssivalue.ToString();
                        else
                            tag.rssi = "-" + (256 - rssivalue).ToString();
                        tag.Antenna = (Convert.ToInt32(mytemp.Substring(20, 2), 16) + 1).ToString();
                        int uidIndex = uidlist_norsssi.IndexOf(tag.UID);
                        if (uidIndex == -1)
                        {
                            uidlist_norsssi.Add(tag.UID);
                            uidlist_rssi.Add(tag);
                            string[] arr = new string[3];
                            arr[0] = tag.UID;
                            arr[1] = tag.rssi;
                            arr[2] = tag.Antenna;
                        }
                        else
                        {
                            //卡号重复
                        }
                    }
                }
                else
                {
                    Array.Resize(ref DSFIDAndUID, cardNumber * 12);
                    strDSFIDAndUID = DSFIDAndUID.ByteToHexStr();
                    for (int index = 0; index < cardNumber; index++)
                    {
                        TagInfo tag = new TagInfo();
                        string mytemp = strDSFIDAndUID.Substring(index * 24, 24);

                        tag.UID = mytemp.Substring(2, 16);
                        tag.dsfid = mytemp.Substring(0, 2);
                        // BackQueue.Enqueue(tag.UID);
                        string SNR = mytemp.Substring(18, 2);
                        string NOISE = mytemp.Substring(20, 2);
                        byte noisevalue = Convert.ToByte(NOISE, 16);
                        if ((noisevalue & 0x80) == 0)
                            tag.noise = noisevalue.ToString();
                        else
                            tag.noise = "-" + (256 - noisevalue).ToString();

                        byte snrvalue = Convert.ToByte(SNR, 16);
                        if ((snrvalue & 0x80) == 0)
                            tag.snr = snrvalue.ToString();
                        else
                            tag.snr = "-" + (256 - snrvalue).ToString();

                        tag.Antenna = (Convert.ToInt32(mytemp.Substring(22, 2), 16) + 1).ToString();

                        int uidIndex = uidlist_norsssi.IndexOf(tag.UID);
                        if (uidIndex == -1)
                        {
                            uidlist_norsssi.Add(tag.UID);
                            uidlist_rssi.Add(tag);
                            string[] arr = new string[3];
                            arr[0] = tag.UID;
                            arr[1] = tag.snr;
                            arr[2] = tag.Antenna;
                        }
                        else
                        {
                            //卡号重复
                        }

                    }
                }

            }
            Log.Info("完成" + DateTime.Now.ToString("HH:mm:ss.fff"));
            foreach (var tag in uidlist_rssi)
            {
                if (OnReturnManagerDataEventArgs != null)
                {
                    string EPC = ChangeStr(tag.UID);
                    RFIDReviceEventArgs backMsg = new RFIDReviceEventArgs();
                    backMsg.eventType = EventType.SerialRevice;
                    backMsg.Msg = EPC;
                    backMsg.MsgBuff = EPC.StrToToHexByte();
                    backMsg.sRFIDInstructionType = SRFIDInstructionType.RecCycRead;
                    OnReturnManagerDataEventArgs(this, backMsg);
                }
                // BackQueue.Enqueue(ChangeStr(tag.UID));
            }
            Thread.Sleep(10);
            if (StopCallback != null)
            {
                StopCallback(BoxNo, uidlist_rssi.Count);
            }

            IsReading = false;
        }

        /// <summary>
        /// 交换RFID码的顺序
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string ChangeStr(string source)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < source.Length; i += 2)
            {
                result.Append(source[source.Length - i - 2]);
                result.Append(source[source.Length - i - 1]);
            }
            return result.ToString();

        }
        protected void RFIDstop()
        {

        }
        private string RR9000GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x01:
                    return "命令操作数长度错误";
                case 0x02:
                    return "操作命令不支持";
                case 0x03:
                    return "操作数范围不符";
                case 0x05:
                    return "感应场处于关闭状态";
                case 0x06:
                    return "EEPROM操作出错";
                case 0x0A:
                    return "指定的Inventory-Scan-Time溢出";
                case 0x0B:
                    return "在Inventory-Scan-Time时间内无得到所有电子标签的UID";
                case 0x0C:
                    return "ISO 错误";
                case 0x0E:
                    return "无电子标签可操作";
                case 0x0F:
                    return "操作出错";
                case 0x30:
                    return "通讯错误";
                case 0x31:
                    return "CRC校验错误";
                case 0x33:
                    return "通讯繁忙，设备正在执行其他指令";
                case 0x04:
                    return "操作命令当前无法执行";
                case 0x35:
                    return "端口已打开";
                case 0x36:
                    return "端口已关闭";
                case 0x37:
                    return "无效的句柄";
                case 0x38:
                    return "无效的端口 ";
                case 0x11:
                    return "天线异常进入保护状态 ";
                case 0x12:
                    return "温度过高进入保护状态 ";
                case 0x13:
                    return "电流过大进入保护状态 ";
                case 0x14:
                    return "电流过大进入保护状态，且失去自动检测功能，不能自动恢复到正常状态 ";
                case 0:
                    return "操作成功";
                default:
                    return "";
            }
        }
    }
    public class TagInfo
    {
        public string UID;
        public string dsfid;
        public string rssi;
        public string snr;
        public string noise;
        public string Antenna;
    }
}

using MIIOT.Drivers;
using MIIOT.Drivers.MCU;
using MIIOT.Drivers.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace MIIOT.Drivers
{
    public class MCUManager
    {
        MCUAgrMian MCUAgrMian;
        public ReturnDataEventArgs OnReturnManagerDataEventArgs;
        public OpenSerialEventArgs OnOpenSerialEventArgs;
        public MCUManager()
        {

        }
        public string Cabinet { get; set; }
        public string MacType { get; set; }
        public NetServerManager NetM{ get; set; }
        public void Start(string PortName, int BaudRate)
        {
            MCUAgrMian = new MCUAgrMian(PortName, BaudRate);
            MCUAgrMian.OnReturnDataEventArgs += ReturnDataEventArgsAction;
            MCUAgrMian.OnOpenSerialEventArgs += OpenSerialEventArgs;
            MCUAgrMian.Start();
        }
        private void OpenSerialEventArgs(object sender, bool status)
        {
            OnOpenSerialEventArgs?.Invoke(this, status);
        }
        private void ReturnDataEventArgsAction(object sender, MCReviceEventArgs byteData)
        {
            switch (byteData.mcuInstructionType)
            {
                case MInstructionType.LED:

                    break;
                case MInstructionType.GATE:
                    break;
                case MInstructionType.CARD:
                    break;
                case MInstructionType.POWER:
                    break;
                case MInstructionType.DoorStatus:
                    break;
                case MInstructionType.MacStatus:
                    break;
                case MInstructionType.BackLight:
                    break;
                case MInstructionType.LCDdisPlay:
                    break;
                case MInstructionType.LCDBackColor:
                    break;
                case MInstructionType.CloseLCD:
                    break;
                case MInstructionType.LCDSYNCTime:
                    break;
                case MInstructionType.HMData:
                    break;
                case MInstructionType.Scale:
                    break;
                case MInstructionType.BARCODESCAN:
                    break;
                case MInstructionType.FingerCMD:
                    break;
                case MInstructionType.CycRead:
                    break;
                case MInstructionType.ReadStop:
                    break;
                case MInstructionType.RecCycRead:
                    break;
                case MInstructionType.RecReadStop:
                    break;
                default:
                    break;
            }
        }

        ~MCUManager()
        {
            if (MCUAgrMian != null)
                MCUAgrMian.Stop();
        }
        private static MCUManager instance;
        private static object _lock = new object();
        /// <summary>
        /// 单例模式
        /// </summary>
        public static MCUManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new MCUManager();
                        }
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// LED操作
        /// </summary>
        /// <param name="chest"></param>
        /// <param name="layer"></param>
        /// <param name="box"></param>
        /// <param name="status">0：灭 1：红灯 2：黄灯 3：蓝灯 4：彩灯</param>
        public void LEDLight(int chest, int layer, int box, string status)
        {
            switch (status)
            {
                case "1":
                    MCUAgrMian.SendMsgByMC(MInstructionType.LED, chest, layer, box, new byte[] { 0x01, 0x00, 0x00 });
                    break;
                case "2":
                    MCUAgrMian.SendMsgByMC(MInstructionType.LED, chest, layer, box, new byte[] { 0x00, 0x01, 0x00 });
                    break;
                case "3":
                    MCUAgrMian.SendMsgByMC(MInstructionType.LED, chest, layer, box, new byte[] { 0x00, 0x00, 0x01 });
                    break;
                case "4":
                    MCUAgrMian.SendMsgByMC(MInstructionType.LED, chest, layer, box, new byte[] { 0x01, 0x01, 0x01 });
                    break;
                case "0":
                    MCUAgrMian.SendMsgByMC(MInstructionType.LED, chest, layer, box, new byte[] { 0x00, 0x00, 0x00 });
                    break;
                default:
                    break;
            }



        }
        public void backLight(int chect, byte LValue)
        {
            MCUAgrMian.SendMsgByMC(MInstructionType.BackLight, chect, 0, 0, new byte[] { LValue });
        }
        public void CardPowerOn(int chect)
        {
            MCUAgrMian.SendMsgByMC(MInstructionType.POWER, chect, 0, 0, new byte[] { 0x01 });
        }
        public void OpenGate(int chect)
        {
            MCUAgrMian.SendMsgByMC(MInstructionType.GATE, chect, 0, 0, new byte[] { 0x01 });
        }
        public void GateSatus(int chect)
        {
            MCUAgrMian.SendMsgByMC(MInstructionType.DoorStatus, chect, 0, 0, new byte[] { 0x00 });
        }
        public void LCD(int chect, int layer, int box, string name, string spec, string qty, string nickName, string factory, string code, string unit)
        {
            List<LCDFontFormat> datas = new List<LCDFontFormat>();
            byte[] color = new byte[3];
            datas.Add(new LCDFontFormat() { DataType = 0x01, Data = name, FSize = 20, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            color = new byte[3];
            datas.Add(new LCDFontFormat() { DataType = 0x02, Data = spec, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            color = new byte[3];
            datas.Add(new LCDFontFormat() { DataType = 0x03, Data = qty, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x04, Data = nickName, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x05, Data = factory, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x06, Data = code, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x07, Data = unit, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            int TotalLen = 0;
            byte[] DataBuff = new byte[4096];
            foreach (var item in datas)
            {
                byte[] temp = Encoding.GetEncoding("GB18030").GetBytes(item.Data);
                byte[] itemBuff = new byte[temp.Length + 2];
                itemBuff[0] = (byte)item.DataType;
                itemBuff[1] = (byte)temp.Length;
                //itemBuff[2] = (byte)item.FSize;
                //itemBuff[3] = (byte)item.ColorR;
                //itemBuff[4] = (byte)item.ColorG;
                //itemBuff[5] = (byte)item.ColorB;
                Array.Copy(temp, 0, itemBuff, 2, temp.Length);
                Array.Copy(itemBuff, 0, DataBuff, TotalLen, itemBuff.Length);
                TotalLen += itemBuff.Length;
            }

            MCUAgrMian.SendMsgByMC(MInstructionType.LCDdisPlay, chect, layer, box, DataBuff.Skip(0).Take(TotalLen).ToArray());
        }
        public void LCD(int chect, int layer, int box, byte[] Buff)
        {
            MCUAgrMian.SendMsgByMC(MInstructionType.LCDdisPlay, chect, layer, box, Buff);
        }
        public void CloseLCD(int chect, int layer, int box, byte[] Buff)
        {
            MCUAgrMian.SendMsgByMC(MInstructionType.LCDdisPlay, chect, layer, box, Buff);
        }
        #region 秤相关

        public void ScaleClear(int chest, int layer, int box)
        {
            MCUAgrMian.SendMsgbyScales(ScalesInstructionType.NumClear, chest, layer, box, new byte[0]);
        }
        public void ScaleCalibration(int chest, int layer, int box, byte[] QtyBuff)
        {
            MCUAgrMian.SendMsgbyScales(ScalesInstructionType.NumWrite, chest, layer, box, QtyBuff);
        }
        public void ScaleCount(int chest, int layer, int box)
        {
            MCUAgrMian.SendMsgbyScales(ScalesInstructionType.NumRead, chest, layer, box, new byte[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chest"></param>
        /// <param name="ReadType">01只读取有变化的，02读取所有</param>
        public void ScaleStart(int chest, byte ReadType)
        {
            MCUAgrMian.SendMsgbyScales(ScalesInstructionType.ReadAll, chest, 0, 0, new byte[] { ReadType });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chest"></param>
        /// <param name="ReadType">0关闭，1开启</param>
        public void AutoScale(int chest, byte open)
        {
            byte[] sendbuff = new byte[8];
            sendbuff[0] = (byte)chest;
            sendbuff[3] = 0x20;
            sendbuff[4] = open;

            //MCUAgrMian.AddSendQueue(MInstructionType.Scale, sendbuff.ToList());
        }

        #endregion
        #region 手指相关
        public void StopReg(int chest)
        {
            MCUAgrMian.SendMsgbyFinger(FingerInstructionType.StopFinger, chest, 0, 0, new byte[0]);
        }
        public void FingerReg(int chest, int fingerId)
        {
            byte[] buff = BitConverter.GetBytes((ushort)fingerId);
            MCUAgrMian.SendMsgbyFinger(FingerInstructionType.RegFinger, chest, 0, 0, new byte[] { buff[0], buff[1] });
        }

        public void FingerDel(int chest, int fingerId)
        {
            byte[] buff = BitConverter.GetBytes((ushort)fingerId);
            MCUAgrMian.SendMsgbyFinger(FingerInstructionType.DeleteFinger, chest, 0, 0, new byte[] { buff[0], buff[1] });
        }
        public void FingerDelAll(int chest)
        {
            MCUAgrMian.SendMsgbyFinger(FingerInstructionType.DeleteAllFinger, chest, 0, 0, new byte[0]);
        }
        #endregion
        public void Led(string para)
        {
            string[] boxs = para.Split(',');
            int a = int.Parse(boxs[0]);
            int b = int.Parse(boxs[1]);
            int c = int.Parse(boxs[2]);
            int d = int.Parse(boxs[3]);
            switch (d)
            {
                case 0://关灯
                    //MCUAgrMian.AddSendQueue(MInstructionType.LED, new List<byte>() { (byte)a, (byte)b, (byte)c, 0x00, 0x00, 0x00 });
                    break;
                case 1://亮红灯
                    //MCUAgrMian.AddSendQueue(MInstructionType.LED, new List<byte>() { (byte)a, (byte)b, (byte)c, 0x01, 0x00, 0x00 });
                    break;
                case 2://亮红灯
                    //MCUAgrMian.AddSendQueue(MInstructionType.LED, new List<byte>() { (byte)a, (byte)b, (byte)c, 0x00, 0x01, 0x00 });
                    break;
                case 3://亮红灯
                    //MCUAgrMian.AddSendQueue(MInstructionType.LED, new List<byte>() { (byte)a, (byte)b, (byte)c, 0x00, 0x00, 0x01 });
                    break;
                case 4://亮背光
                    //MCUAgrMian.AddSendQueue(MInstructionType.LED, new List<byte>() { (byte)a, 0x00, 0x00, 0x01, 0x00, 0x00 });
                    break;
                case 5://关背光
                    //MCUAgrMian.AddSendQueue(MInstructionType.LED, new List<byte>() { (byte)a, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    break;
                case 6://层板电开
                    //MCUAgrMian.AddSendQueue(MInstructionType.POWER, new List<byte>() { (byte)a, 0x00, 0x00, 0x01, 0x00, 0x00 });
                    break;
                case 7://层板电关
                    //MCUAgrMian.AddSendQueue(MInstructionType.POWER, new List<byte>() { (byte)a, 0x00, 0x00, 0x00, 0x00, 0x00 });
                    break;
                default:
                    break;
            }
        }
        public void FingerChk(int chest)
        {
            byte[] sendbuff = new byte[7];
            sendbuff[0] = (byte)chest;
            sendbuff[3] = 0x01;

            //  RFID.AddSendQueue(MInstructionType.FingerCMD, sendbuff.ToList());
        }



    }

}

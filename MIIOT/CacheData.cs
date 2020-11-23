using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT
{
    public class CacheData
    {
        private static CacheData _ins;
        public static object obj = new object();
        public static CacheData Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                    {
                        _ins = new CacheData();
                    }
                }
                return _ins;
            }
        }
        public CacheData()
        {
            JsonConfig = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath+"\\Config\\config.json");
            List<MacPara> list = LiteDBHelper.Ins.GetCollection<MacPara>();
            TcpServer = new SuperSoccketMrg();
            _sysParas = LiteDBHelper.Ins.GetCollection<SysPara>().FindAll(a=>a.ParaType.Contains("MacType"));
            foreach (var item in list)
            {
                switch (item.MacKey)
                {
                    case "IP":
                        IP = item.COM;
                        Port = int.Parse(item.BaudRate);
                        RFIDTimeout = int.Parse(item.Cabinet);
                        break;
                    case "PCB":
                        McCOM.COM = item.COM;
                        McCOM.BaudRate = item.BaudRate;
                        McCOM.Cabinet = item.Cabinet;
                        break;
                    case "Finger":
                        FingerCOM.COM = item.COM;
                        FingerCOM.BaudRate = item.BaudRate;
                        break;
                    case "Humiture":
                        HumitrueCOM.COM = item.COM;
                        HumitrueCOM.BaudRate = item.BaudRate;
                        break;
                    case "Printer":
                        PrinterCOM.COM = item.COM;
                        PrinterCOM.BaudRate = item.BaudRate;
                        break;
                    case "BarCode":
                        CabinetID = int.Parse(item.Cabinet);
                        byte[] buff = item.COM.StrToToHexByte();
                        VID = BitConverter.ToUInt16(new byte[] { buff[1], buff[0] }, 0);
                        buff = item.BaudRate.StrToToHexByte();
                        PID = BitConverter.ToUInt16(new byte[] { buff[1], buff[0] }, 0);
                        break;
                    default:
                        if (item.MacType.Contains("RFID"))
                        {
                            RFIDParas.Add(new MacPara() { COM = item.COM, BaudRate = item.BaudRate, Cabinet = item.Cabinet });
                        }
                        break;
                }

            }
        }

        public string IP { get; set; }
        public int Port { get; set; }
        public int RFIDTimeout { get; set; }
        public ushort VID { get; set; }
        public ushort PID { get; set; }
        public int CabinetID { get; set; }
        public int HBGap { get; set; } = 10;
        public MacPara McCOM { get; set; } = new MacPara();
        public MacPara FingerCOM { get; set; } = new MacPara();
        public MacPara HumitrueCOM { get; set; } = new MacPara();
        public MacPara PrinterCOM { get; set; } = new MacPara();
        public List<MacPara> RFIDParas { get; set; } = new List<MacPara>();
        public string _currTemperature = "0.0";
        public string _currHumidity = "0.0";
        public JsonConfigHelper JsonConfig { get; set; }
        public List<SysPara> _sysParas { get; set; }
        public SuperSoccketMrg TcpServer { get; set; }
    }
}

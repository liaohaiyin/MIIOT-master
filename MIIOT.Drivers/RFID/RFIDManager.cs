using MIIOT.Drivers.RFID;
using MIIOT.Drivers.SRFID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class RFIDManager
    {
        public delegate void ReturnDataEventArgs(object sender, RFIDReviceEventArgs args);
        public ReturnDataEventArgs OnReturnManagerDataEventArgs;
        public delegate void StopDel(string BoxNo, bool isEmpty);
        public StopDel StopCallback;
        public Dictionary<string, RFIDBase> dicMac = new Dictionary<string, RFIDBase>();
        public string MacType { get; set; }

        private static object obj = new object();
        private static RFIDManager _ins;

        public static RFIDManager Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                    {
                        _ins = new RFIDManager();
                    }
                }
                return _ins;
            }
        }

        public RFIDManager()
        {

        }
        public bool Start(string cabinet, string COM, string BaudRate)
        {
            bool isConnect = false;
            if (1 == 1)
            {
                SK_SRFIDUtil sRFIDUtil = new SK_SRFIDUtil();
                isConnect = sRFIDUtil.Open(COM, BaudRate);
                dicMac.Add(cabinet, sRFIDUtil);
            }
            return isConnect;
        }
        public void Read(int Chest)
        {
            dicMac[Chest.ToString()].Read();
        }
        public void Stop(int Chest)
        {
            dicMac[Chest.ToString()].Stop();

        }
    }
}

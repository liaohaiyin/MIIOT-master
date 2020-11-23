using MIIOT.Drivers.SRFID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers.RFID
{
    public class RFIDBase
    {
        public string BoxNo { get; set; }
        public delegate void ReturnDataEventArgs(object sender, RFIDReviceEventArgs args);
        public ReturnDataEventArgs OnReturnManagerDataEventArgs;
        public delegate void StopDel(string BoxNo, int RFIDCount);
        public StopDel StopCallback;
        public string PortName { get; set; }
        public string BoudRate { get; set; }
        public bool IsReading = false;
        public bool IsConnected = false;
        public virtual bool Open(string COM, string boudRate)
        {
            return false;
        }
        public virtual void Read(int timeOut=1)
        { 
        
        }
        public virtual void Stop()
        { 
        
        }
    }
}

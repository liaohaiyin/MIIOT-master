using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers.Serial
{
    public class SerialScaner
    {
        public void Scan()
        {
            string[] PortNames = System.IO.Ports.SerialPort.GetPortNames();
        }
    }
}


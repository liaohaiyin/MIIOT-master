using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class CECOMCardHelper
    {
        public delegate void DelMsgBack(object sender, byte[] buff);
        public event DelMsgBack DoMsgBack;
        public delegate void DelOpenOrClose(object sender, bool isOpen);
        public event DelOpenOrClose DoOpenOrClose;
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        private SerialPortService SerialPortService;
        public CECOMCardHelper(string portName, int baudRate)
        {
            PortName = portName;
            BaudRate = baudRate;
        }
        public void Start()
        {
            SerialPortService = new SerialPortService
            {
                Agreement = new Agreement(9, 0, 0x02), //如果此参数为null,则 数据为原始数据
                BaudRate = BaudRate,
                PortName = PortName,
            };
            SerialPortService.OnOpenClose += new SerialOpenClosedEvent(SerialPortService_OnOpenClose);
            SerialPortService.OnSerialRevice += new SerialReviceEventArgsEvent(SerialPortService_OnSerialRevice);
            SerialPortService.Start();


        }
        public void SerialPortService_OnSerialRevice(object sender, ReviceEventArgs args)
        {
            try
            {
                DoMsgBack?.Invoke(this, args.ReviceBytes);
            }
            catch (Exception ex)
            {
                Log.Error("CECOMCardHelperService_OnSerialRevice", ex);
            }
        }
        public void SerialPortService_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            DoOpenOrClose?.Invoke(this, args.IsOpen);
        }
    }
}

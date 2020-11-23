using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.Drivers
{
    public enum MacTypeEnum
    {
        [Description("服务端")]
        Server = 0,
        [Description("柜体串口")]
        PCB = 1,
        [Description("指静脉串口")]
        FINGER = 2,
        [Description("扫码仪HID")]
        HID = 3,
        [Description("RFID串口")]
        RFID = 4,
        [Description("冰箱温湿度串口")]
        FRIDGE = 5,
        [Description("冰箱打印机串口")]
        POSPRINTER=6
    }
}

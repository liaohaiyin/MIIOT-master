using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers.MCU
{

    public enum CommandHead
    {
        CMD_HEAD_SIGN1 = 0x7E,

        CMD_HEAD_SIGN2 = 0x7F
    }
    public class MCUProtocol
    {
        public MCHeader Header { get; set; }
        public byte[] DataBuff { get; set; }
    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct MCHeader
    {
        public byte headSign1;
        public short length;
        public byte CMD;
        public byte Cabinet;
        public byte Layer;
        public byte Box;
    }

   
    public class MCBackMsg
    {
        public MInstructionType MCMDType { get; set; }
        public ReviceEventArgs args { get; set; }
        public FingerRegMsg RegMsg { get; set; }
        public string Msg { get; set; }
        public byte[] MsgBuff { get; set; }
        public EventType EventType { set; get; }
        public int FingerID { get; set; }
        /// <summary>
        /// 是否已存在手指
        /// </summary>
        public bool ExistFinger { get; set; }
        /// <summary>
        /// 注册时检查手指
        /// </summary>
        public bool IsRegCheck { get; set; }
    }
}

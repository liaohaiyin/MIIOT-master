using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers.SRFID
{
    public class SRFIDDataEventArgs
    {

    }
    public class RFIDReviceEventArgs
    {
        public EventType eventType { get; set; }
        public SRFIDInstructionType sRFIDInstructionType { get; set; }
        public string Msg { get; set; }
        public byte[] MsgBuff { get; set; }
    }
    public enum SRFIDInstructionType
    {
        /// <summary>
        /// 读写器正忙
        /// </summary>
        [Description("读写器正忙")]
        IsBusy = 0x01,
        /// <summary>
        /// 循环查询标签数据
        /// </summary>
        [Description("循环查询标签数据")]
        CycRead = 0x17,

        /// <summary>
        /// 停止循环查询标签数据
        /// </summary>
        [Description("停止循环查询标签数据")]
        ReadStop = 0x18,
        /// <summary>
        /// 循环查询标签数据应答
        /// </summary>
        [Description("循环查询标签数据应答")]
        RecCycRead = 0x97,

        /// <summary>
        /// 停止循环查询标签数据应答
        /// </summary>
        [Description("停止循环查询标签数据应答")]
        RecReadStop = 0x98,
    }
}

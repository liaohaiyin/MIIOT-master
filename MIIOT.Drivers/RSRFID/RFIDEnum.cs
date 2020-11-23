using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MIIOT.Drivers
{
    public enum RCommandHead
    {
        CMD_HEAD = 0x5A,
        CMD_HEAD_SIGN1 = 0x0D,
        CMD_HEAD_SIGN2 = 0x0A,
    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class RSCommandHead  //命令头，跟协议保持一致
    {
        public byte head;
        public byte cmdType;
        public byte dataLen;		//命令体长度（）
        public RSCommandHead()
        {
        }
    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class SRFIDCommandHead  //命令头，跟协议保持一致
    {
        public byte head;
        public byte cmdNO;
        public byte cmdVer;
        public byte Pcmd;
        public byte cmdType;
        public byte dataLen1;
        public byte dataLen2;		//命令体长度（）
        public SRFIDCommandHead()
        {
        }
    }
    //public enum RInstructionType
    //{
    //    /// <summary>
    //    /// 循环查询标签数据
    //    /// </summary>
    //    [Description("循环查询标签数据")]
    //    CycRead = 0x17,

    //    /// <summary>
    //    /// 停止循环查询标签数据
    //    /// </summary>
    //    [Description("停止循环查询标签数据")]
    //    ReadStop = 0x18,
    //    /// <summary>
    //    /// 循环查询标签数据应答
    //    /// </summary>
    //    [Description("循环查询标签数据应答")]
    //    RecCycRead = 0x97,

    //    /// <summary>
    //    /// 停止循环查询标签数据应答
    //    /// </summary>
    //    [Description("停止循环查询标签数据应答")]
    //    RecReadStop = 0x98,
    //}

}

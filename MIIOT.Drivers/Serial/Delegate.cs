using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIIOT.Drivers
{
    /// <summary>
    /// 打开和关闭串口的委托
    /// </summary>
    /// <param name="sender">服务类</param>
    /// <param name="args">打开和关闭的参数</param>
    public delegate void SerialOpenClosedEvent(object sender, OpenCloseEventArgs args);

    /// <summary>
    /// 收到串口数据，或者串口错误数据
    /// </summary>
    /// <param name="sender">串口服务类</param>
    /// <param name="args">串口返回的参数</param>
    public delegate void SerialReviceEventArgsEvent(object sender, ReviceEventArgs args);
  

    /// <summary>
    /// 该类表示，解析协议的所需要的参数
    /// </summary>
    public class Agreement
    {
        /// <summary>
        /// 该类表示，
        /// </summary>
        /// <param name="agrtLength">协议长度</param>
        /// <param name="hSTR">协议头的二进制</param>
        public Agreement(int agrtLength,int lenIndex, byte hSTR,byte headSTR1=0x00,byte footSTR=0x00)
        {
            agreementLength = agrtLength;
            headSTR = hSTR;
            lengthIndex = lenIndex;
            HeadSTR1 = headSTR1;
            FootSTR = footSTR;
        }
        /// <summary>
        /// 协议长度，
        /// </summary>
        private int agreementLength;
        /// <summary>
        /// 协议长度，协议长度默认为0，如果没有设置，则会返回原始数据。
        /// </summary>
        public int AgreementLength
        {
            get { return agreementLength; }
            set { agreementLength = value; }
        }
        /// <summary>
        /// 协议长度，
        /// </summary>
        private int lengthIndex;
        /// <summary>
        /// 协议长度位置。
        /// </summary>
        public int LengthIndex
        {
            get { return lengthIndex; }
            set { lengthIndex = value; }
        }
        private byte headSTR;
        /// <summary>
        /// 协议头
        /// </summary>
        public byte HeadSTR { get { return headSTR; } }
        public byte FootSTR { get; set; }
        public byte HeadSTR1 { get; set; }
    }
}

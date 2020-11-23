using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace MIIOT.Drivers
{
    /// <summary>
    /// 串口信息(接收到的串口信息)
    /// </summary>
    public class ReviceEventArgs : EventArgs
    {
        /// <summary>
        /// 串口服务类型，用户判断数据干什么用的
        /// </summary>
        public EventType EventType { set; get; }
        /// <summary>
        /// 串口是否打开
        /// </summary>
        public bool IsOpend { set; get; }

        public ExceptionEventArgs exceptionEventArgs = new ExceptionEventArgs();
        /// <summary>
        /// 串口服务异常错误信息
        /// </summary>
        public ExceptionEventArgs ExceptionEventArgs
        {
            set
            {
                exceptionEventArgs = value;
            }
            get
            {
                return exceptionEventArgs;
            }
        }
        /// <summary>
        /// 串口数据 已经根据协议截断过的协议
        /// </summary>
        public Byte[] ReviceBytes { set; get; }
        /// <summary>
        /// 串口数据 字符串协议16进制
        /// </summary>
        public string ReviceString { set; get; }
    }
    /// <summary>
    /// 错误协议
    /// </summary>
    public class ExceptionEventArgs
    {
        /// <summary>
        /// 此参数是有 串口返回的，对解析没有意义
        /// </summary>
        public SerialErrorReceivedEventArgs SerialErrorReceivedEventArgs { set; get; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { set; get; }
    }
    /// <summary>
    /// 关闭与打开串口
    /// </summary>
    public class OpenCloseEventArgs
    {
        private bool isOpen;
        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; }
        }
    }
    /// <summary>
    /// 串口服务类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 串口发送数据
        /// </summary>
        SerialSended = 0,
        /// <summary>
        /// 串口收到数据
        /// </summary>
        SerialRevice = 1,
        /// <summary>
        /// 串口事件错误，主动上报的错误
        /// </summary>
        SerialError = 2,
        /// <summary>
        /// 串口服务类异常错误
        /// </summary>
        SerialServiceError = 3
    }
}

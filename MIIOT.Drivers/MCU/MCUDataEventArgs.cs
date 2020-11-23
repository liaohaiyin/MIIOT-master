using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class MCUDataEventArgs
    {

    }
    /// <summary>
    /// 此委托不受任何指令支配，他接受所有的协议上传，
    /// 到主界面用于记录日志，和显示状态栏。
    /// 表示所有的全局的委托。
    /// </summary>
    /// <param name="sender">this</param>
    /// <param name="args">协议返回参数</param>
    public delegate void EveryALLEventArgs(object sender, string Description);
    /// <summary>
    /// 串口打开时候 事件 是否打开
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="status"></param>
    public delegate void OpenSerialEventArgs(object sender, bool status);
    /// <summary>
    /// 串口返回值数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="msg"></param>
    /// <param name="byteData"></param>
    public delegate void ReturnDataEventArgs(object sender, MCReviceEventArgs fBack);
    public class MCReviceEventArgs
    {
        public EventType eventType { get; set; }
        public MInstructionType mcuInstructionType { get; set; }
        public string Msg { get; set; }
        public byte[] MsgBuff { get; set; }
    }

    public enum MInstructionType
    {
        /// <summary>
        /// LED控制
        /// </summary>
        [Description("LED控制")]
        LED = 0x02,

        /// <summary>
        /// 门锁
        /// </summary>
        [Description("门锁")]
        GATE = 0x03,
        /// <summary>
        /// 读卡器
        /// </summary>
        [Description("读卡器")]
        CARD = 0x04,
        /// <summary>
        /// 层板上电
        /// </summary>
        [Description("层板上电")]
        POWER = 0x05,
        /// <summary>
        /// 门状态
        /// </summary>
        [Description("门状态")]
        DoorStatus = 0x06,
        /// <summary>
        /// 设备状态
        /// </summary>
        [Description("设备状态")]
        MacStatus = 0x07,
        /// <summary>
        /// 背光
        /// </summary>
        [Description("背光")]
        BackLight = 0x08,
        /// <summary>
        /// LCD屏显示
        /// </summary>
        [Description("LCD屏显示")]
        LCDdisPlay = 0x09,
        /// <summary>
        /// LCD屏背景颜色
        /// </summary>
        [Description("LCD屏背景颜色")]
        LCDBackColor = 0x0A,
        /// <summary>
        /// LCD屏显示
        /// </summary>
        [Description("LCD屏显示")]
        CloseLCD = 0x0B,
        /// <summary>
        /// LCD屏显示
        /// </summary>
        [Description("LCD屏显示")]
        LCDSYNCTime = 0x0C,
        /// <summary>
        /// 温湿度数据
        /// </summary>
        [Description("温湿度数据")]
        HMData = 0x0D,
        /// <summary>
        /// 秤命令
        /// </summary>
        [Description("秤命令")]
        Scale = 0x0E,
        /// <summary>
        /// 扫码仪
        /// </summary>
        [Description("扫码仪")]
        BARCODESCAN = 0x0F,
        /// <summary>
        /// 指静脉
        /// </summary>
        [Description("指静脉")]
        FingerCMD = 0x10,
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
    public enum FingerStateType
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        SUCCESS = 0x00,
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        FALt = 0x01,
    }
    public enum ScalesInstructionType
    {
        /// <summary>
        /// 清零
        /// </summary>
        [Description("清零")]
        NumClear = 0x08,
        /// <summary>
        /// 标定
        /// </summary>
        [Description("标定")]
        NumWrite = 0x09,
        /// <summary>
        /// 计数
        /// </summary>
        [Description("计数")]
        NumRead = 0x0A,
        /// <summary>
        /// 状态
        /// </summary>
        [Description("计数状态")]
        ScalesStatus = 0x10,
        /// <summary>
        /// 全柜计数
        /// </summary>
        [Description("全柜计数")]
        ReadAll = 0x21,
    }
    public enum FingerInstructionType
    {
        /// <summary>
        /// 验证手指
        /// </summary>
        [Description("验证手指")]
        VerFinger = 0x00,
        /// <summary>
        /// 注册手指
        /// </summary>
        [Description("注册手指")]
        RegFinger = 0x03,
        /// <summary>
        /// 取消注册
        /// </summary>
        [Description("取消注册")]
        StopFinger = 0x04,
        /// <summary>
        /// 删除手指
        /// </summary>
        [Description("删除手指")]
        DeleteFinger = 0x05,
        /// <summary>
        /// 删除所有手指
        /// </summary>
        [Description("删除所有手指")]
        DeleteAllFinger = 0x06,
    }
    public enum FingerRegMsg
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        SUCCESS = 0x00,
        /// <summary>
        /// 验证失败
        /// </summary>
        [Description("操作失败")]
        ERR_FALT = 0x01,
        /// <summary>
        /// 注册超时
        /// </summary>
        [Description("操作超时")]
        ERR_TIMEOUT = 0x02,
        /// <summary>
        /// 与前一次采集静脉特征差异过大，请重放手指
        /// </summary>
        [Description("与前一次采集静脉特征差异过大，请重放手指")]
        ERR_TEMPLATE_OCC = 0x05,
        /// <summary>
        /// 与前一次采集手指下发信息不同
        /// </summary>
        [Description("与前一次采集手指下发信息不同")]
        ERR_FINGERID_OCC = 0x06,
        /// <summary>
        /// 存储空间为空
        /// </summary>
        [Description("存储空间为空")]
        ERR_FINGERID_NULL = 0x17,
        /// <summary>
        /// 传感器未检测到手指
        /// </summary>
        [Description("传感器未检测到手指")]
        ERR_NO_FINGER = 0x0e,
        /// <summary>
        /// 注册模板缓存区满
        /// </summary>
        [Description("注册模板缓存区满")]
        ERR_REG_BUFFFULL = 0x0f,
        /// <summary>
        /// 生成不合格模板
        /// </summary>
        [Description("生成不合格模板")]
        ERR_TEMPLATE = 0x10,
        /// <summary>
        /// 拍照超时
        /// </summary>
        [Description("拍照超时")]
        ERR_CAP = 0x11,
        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        NOUSER = 0x13,
        /// <summary>
        /// 静态模块内存出错
        /// </summary>
        [Description("静态模块内存出错")]
        FLASHERR = 0x14,
        /// <summary>
        /// 第1次读取成功，请抬起手指进行第2次读取
        /// </summary>
        [Description("第1次读取成功，请抬起手指进行第2次读取")]
        READ1ST = 0x07,
        /// <summary>
        /// 第2次读取成功，请抬起手指进行第3次读取
        /// </summary>
        [Description("第2次读取成功，请抬起手指进行第3次读取")]
        READ2ED = 0x08,
        /// <summary>
        /// 第3次读取成功，正在完成操作
        /// </summary>
        [Description("第3次读取成功，正在完成操作")]
        READ3ED = 0x09,
        /// <summary>
        /// 手指收录失败，请重新开始操作
        /// </summary>
        [Description("手指收录失败，请重新开始操作")]
        READERR = 0x0b,
        /// <summary>
        /// 与前一次采集静脉特征差异过大，请重放手指
        /// </summary>
        [Description("与前一次采集静脉特征差异过大，请重放手指")]
        READMIX = 0x05,
        /// <summary>
        /// 等待放置手指
        /// </summary>
        [Description("等待放置手指")]
        WAITFINGER = 0x1f,
        /// <summary>
        /// 手指ID重复
        /// </summary>
        [Description("手指ID重复")]
        SAMEFINGER = 0x15,
    }
    public class LCDFontFormat
    {
        public byte DataType { get; set; }
        public string Data { get; set; }
        public int FSize { get; set; }
        public int ColorR { get; set; }
        public int ColorG { get; set; }
        public int ColorB { get; set; }
    }
    public enum HRFIDPCMD
    {
        /// <summary>
        /// 读写器错误或告警消息
        /// </summary>
        [Description("读写器错误或告警消息")]
        RFIDERR = 0x00,
        /// <summary>
        /// 读写器配置与管理消息
        /// </summary>
        [Description("读写器配置与管理消息")]
        RFIDSET = 0x01,
        /// <summary>
        /// RFID配置与操作消息
        /// </summary>
        [Description("RFID配置与操作消息")]
        RFIDHANDLE = 0x02,
        /// <summary>
        /// 读写器日志消息
        /// </summary>
        [Description("读写器日志消息")]
        RFIDLOG = 0x03,
        /// <summary>
        /// 读写器应用处理器软件与基带软件升级消息
        /// </summary>
        [Description("读写器应用处理器软件与基带软件升级消息")]
        RFIDDRIVE = 0x04,
        /// <summary>
        /// 测试指令
        /// </summary>
        [Description("测试指令")]
        RFIDTEST = 0x05,
    }
    public enum HRFIDCMD
    {
        /// <summary>
        /// 读EPC标签
        /// </summary>
        [Description("读EPC标签")]
        RFIDREAD = 0x10,
        /// <summary>
        /// 停止指令
        /// </summary>
        [Description("停止指令")]
        RFIDSTOP = 0xFF,
    }
    public enum MCUInstructionType
    {
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

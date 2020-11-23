using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MIIOT.Drivers
{

    public enum FCommandHead
    {
        CMD_HEAD = 0x40,
        CMD_HEAD_SIGN1 = 0x0D,
        CMD_HEAD_SIGN2 = 0x0A,
    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class FSCommandHead  //命令头，跟协议保持一致
    {
        public byte head;
        public byte cmdType;
        public byte dataLen;		//命令体长度（）
        public FSCommandHead()
        {
        }
    }
    public delegate void FReturnDataEventArgs(object sender, FReviceEventArgs FReviceEventArgs);
    public class FReviceEventArgs
    {
        public bool IsNormalCMD { get; set; }
        public EventType eventType { get; set; }
        public FInstructionType fInstructionType { get; set; }
        public FResponseCode fResponseCode { get; set; }
        public string Msg { get; set; }
        public byte[] MsgBuff { get; set; }
        public byte[] FingerBuff { get; set; }
    }

    public enum FInstructionType
    {
        /// <summary>
        /// 验证手指
        /// </summary>
        [Description("验证手指")]
        ChkFinger = 0x00,
        /// <summary>
        /// 注册手指
        /// </summary>
        [Description("注册手指")]
        RegFinger = 0x03,
        /// <summary>
        /// 结束注册手指
        /// </summary>
        [Description("结束注册手指")]
        RegEnd = 0x04,
        /// <summary>
        /// 删除单个手指
        /// </summary>
        [Description("删除单个手指")]
        DeleteFinger = 0x05,
        /// <summary>
        /// 删除所有手指
        /// </summary>
        [Description("删除所有手指")]
        DeleteAllFinger = 0x06,
        /// <summary>
        /// 查询所有手指
        /// </summary>
        [Description("查询所有手指")]
        FindAllFinger = 0x07,
        /// <summary>
        /// 上传指定手指信息
        /// </summary>
        [Description("上传指定手指信息")]
        UPLOAD_INFOR = 0x08,
        /// <summary>
        /// 上传指定手指信息和模板
        /// </summary>
        [Description("上传指定手指信息和模板")]
        UPLOAD_INFOR_TEMPLATES = 0x0A,
        /// <summary>
        /// 下载指定手指信息和模板
        /// </summary>
        [Description("下载指定手指信息和模板")]
        DOWNLOAD_INFOR_TEMPLATES = 0x0C,
        /// <summary>
        /// 检测手指状态
        /// </summary>
        [Description("检测手指状态")]
        FindFinger = 0x0F,
    }
    public enum FResponseCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        SUCCESS = 0x00,
        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("操作失败")]
        ERR = 0x01,
        /// <summary>
        /// 操作超时
        /// </summary>
        [Description("操作超时")]
        TIMEOUT = 0x02,
        /// <summary>
        /// 信息头存储空间满
        /// </summary>
        [Description("信息头存储空间满")]
        ERR_INFOR_FULL= 0x03,
        /// <summary>
        /// 模板存储空间满
        /// </summary>
        [Description("模板存储空间满")]
        ERR_TEMPLATE_FULL = 0x04,
        /// <summary>
        /// 注册时两次采集静脉特征差异过大
        /// </summary>
        [Description("注册时两次采集静脉特征差异过大")]
        ERR_TEMPLATE_OCC = 0x05,
        /// <summary>
        /// 手指 FID 已存在
        /// </summary>
        [Description("手指 FID 已存在")]
        ERR_FINGERID_OCC = 0x06,
        /// <summary>
        /// 不存在该手指 FID
        /// </summary>
        [Description("不存在该手指 FID")]
        ERR_FINGERID_NULL = 0x07,
        /// <summary>
        /// 不存在该组号
        /// </summary>
        [Description("不存在该组号")]
        ERR_GROUPID_NULL = 0x08,
        /// <summary>
        /// Flash操作错误
        /// </summary>
        [Description("Flash操作错误")]
        ERR_FLASH = 0x09,
        /// <summary>
        /// 未检测到手指
        /// </summary>
        [Description("未检测到手指")]
        ERR_NO_FINGER = 0x0E,
        /// <summary>
        /// 注册模板缓存区满
        /// </summary>
        [Description("注册模板缓存区满")]
        ERR_REG_BUFFFULL = 0x0F,
        /// <summary>
        /// 生成的模板不合格
        /// </summary>
        [Description("生成的模板不合格")]
        ERR_TEMPLATE = 0x10,
        /// <summary>
        /// 拍照超时
        /// </summary>
        [Description("拍照超时")]
        ERR_CAP = 0x11,
    }
    public enum FPType
    {
        /// <summary>
        /// 取消本轮测试
        /// </summary>
        [Description("注册手指")]
        CancelReg = 0x00,
        /// <summary>
        /// 写入设备
        /// </summary>
        [Description("写入设备")]
        WriteReg = 0x01,
        /// <summary>
        /// 上传到上位机
        /// </summary>
        [Description("上传到上位机")]
        UploadReg = 0x02,
    }

}

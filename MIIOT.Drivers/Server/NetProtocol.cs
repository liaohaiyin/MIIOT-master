using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers.Server
{
    public class NetProtocol
    {

    }
    public enum NetType
    {
        /// <summary>
        /// 门锁
        /// </summary>
        [Description("门锁")]
        Lock = 0x01,
        /// <summary>
        /// 槽位控制
        /// </summary>
        [Description("槽位控制")]
        LCD = 0x02,
        /// <summary>
        /// 背光控制
        /// </summary>
        [Description("背光控制")]
        BackLight = 0x03,
        /// <summary>
        /// RFID开始读码
        /// </summary>
        [Description("RFID开始读码")]
        RFIDStart = 0x04,
        /// <summary>
        /// RFID结束读码
        /// </summary>
        [Description("RFID结束读码")]
        RFIDStop = 0x05,
        /// <summary>
        /// 注册手指
        /// </summary>
        [Description("注册手指")]
        FingerReg = 0x06,
        /// <summary>
        /// 验证手指
        /// </summary>
        [Description("验证手指")]
        FingerChk = 0x07,
        /// <summary>
        /// 删除单个手指
        /// </summary>
        [Description("删除单个手指")]
        FingerDelete = 0x08,
        /// <summary>
        /// 条码器控制
        /// </summary>
        [Description("条码器控制")]
        BarCodeOpt = 0x09,
        /// <summary>
        /// 门状态
        /// </summary>
        [Description("门状态")]
        DoorStatus = 0x0C,
        /// <summary>
        /// 关闭所有槽位显示
        /// </summary>
        [Description("关闭所有槽位显示")]
        CloseLCD = 0x0D,
        /// <summary>
        /// 查询所有手指
        /// </summary>
        [Description("查询所有手指")]
        FingerList = 0x0E,
        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print = 0x10,
        /// <summary>
        /// 秤归零
        /// </summary>
        [Description("秤归零")]
        ScaleClear = 0x12,
        /// <summary>
        /// 秤标定
        /// </summary>
        [Description("秤标定")]
        ScaleCalib = 0x13,
        /// <summary>
        /// 秤计数
        /// </summary>
        [Description("秤计数")]
        ScaleCount = 0x14,
    }
    public enum NetCompleteType
    {
        /// <summary>
        /// 门锁
        /// </summary>
        [Description("门锁")]
        Lock = 0x81,
        /// <summary>
        /// 槽位控制
        /// </summary>
        [Description("槽位控制")]
        LCD = 0x82,
        /// <summary>
        /// 背光控制
        /// </summary>
        [Description("背光控制")]
        BackLight = 0x83,
        /// <summary>
        /// RFID开始读码
        /// </summary>
        [Description("RFID开始读码")]
        RFIDStart = 0x84,
        /// <summary>
        /// RFID结束读码
        /// </summary>
        [Description("RFID结束读码")]
        RFIDStop = 0x85,
        /// <summary>
        /// 注册手指
        /// </summary>
        [Description("注册手指")]
        FingerReg = 0x86,
        /// <summary>
        /// 验证手指
        /// </summary>
        [Description("验证手指")]
        FingerChk = 0x87,
        /// <summary>
        /// 删除单个手指
        /// </summary>
        [Description("删除单个手指")]
        FingerDelete = 0x88,
        /// <summary>
        /// 条码器控制
        /// </summary>
        [Description("条码器控制")]
        BarCodeOpt = 0x89,
        /// <summary>
        /// 读卡器返回
        /// </summary>
        [Description("读卡器返回")]
        CardBack = 0x8A,
        /// <summary>
        /// 关门返回
        /// </summary>
        [Description("关门返回")]
        DoorCloseBack = 0x8A,
        /// <summary>
        /// 门状态
        /// </summary>
        [Description("门状态")]
        DoorStatus = 0x8C,
        /// <summary>
        /// 关闭所有槽位显示
        /// </summary>
        [Description("关闭所有槽位显示")]
        CloseLCD = 0x8D,
        /// <summary>
        /// 查询所有手指
        /// </summary>
        [Description("查询所有手指")]
        FingerList = 0x8E,
        /// <summary>
        /// 心跳
        /// </summary>
        [Description("心跳")]
        HB = 0x8F,
        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print = 0x90,
        /// <summary>
        /// 上传温湿度
        /// </summary>
        [Description("上传温湿度")]
        THUpload = 0x91,
        /// <summary>
        /// 秤归零
        /// </summary>
        [Description("秤归零")]
        ScaleClear = 0x92,
        /// <summary>
        /// 秤标定
        /// </summary>
        [Description("秤标定")]
        ScaleCalib = 0x93,
        /// <summary>
        /// 秤计数
        /// </summary>
        [Description("秤计数")]
        ScaleCount = 0x94,
        /// <summary>
        /// 上传设备状态
        /// </summary>
        [Description("上传设备状态")]
        MacStatus = 0x95,
    }
}

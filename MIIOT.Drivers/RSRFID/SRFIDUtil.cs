using MIIOT.Drivers;
using MIIOT.Drivers.MCU;
using MIIOT.Drivers.RFID;
using MIIOT.Drivers.SRFID;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UHF;

namespace MIIOT.Drivers
{
    public class SRFIDUtil : RFIDBase
    {
        Queue BackQueue = Queue.Synchronized(new Queue());
        public static object timeout = 3;
        Timer Timer;
        RFIDCallBack elegateRFIDCallBack;
        private byte fComAdr = 0xff; //当前操作的ComAdr
        private int fCmdRet = 30; //所有执行指令的返回值
        int FrmPortIndex = 0;

        Queue queue = Queue.Synchronized(new Queue());
        public SRFIDUtil(int _timeout)
        {

            timeout = _timeout;
            Timer = new Timer(new TimerCallback(TimerAction));
            Task.Run(()=> {
                while (true)
                {
                    if (queue.Count>0)
                    {
                        var uid = queue.Dequeue();
                        if (uid != null)
                        {
                            if (RFIDs.Add(uid.ToString()))
                            {
                                if (OnReturnManagerDataEventArgs != null)
                                {
                                    Timer.Change(int.Parse(timeout.ToString()) * 1000, 0);
                                    RFIDReviceEventArgs backMsg = new RFIDReviceEventArgs();
                                    backMsg.eventType = EventType.SerialRevice;
                                    backMsg.Msg = uid.ToString();
                                    backMsg.MsgBuff = uid.ToString().StrToToHexByte();
                                    backMsg.sRFIDInstructionType = SRFIDInstructionType.CycRead;
                                    OnReturnManagerDataEventArgs(this, backMsg);
                                }
                            }
                        }
                    }

                    Thread.Sleep(20);
                }
            });
        }
        public void GetUid(IntPtr p, Int32 nEvt)
        {
            RFIDTag ce = (RFIDTag)Marshal.PtrToStructure(p, typeof(RFIDTag));

            queue.Enqueue(ce.UID);
        }

        HashSet<string> RFIDs = new HashSet<string>();
        public void TimerAction(object state)
        {
            RFIDs.Clear();
        }
        public override bool Open(string COM, string boudRate)
        {
            PortName = COM;
            BoudRate = boudRate;
            try
            {

                int portNum = int.Parse(PortName.Substring(3));
                fComAdr = 255;//广播地址打开设备
                byte fBaud = 5;//默认57600
                fCmdRet = UHF.RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex);
                if (fCmdRet != 0)
                {
                    string strLog = "连接读写器失败，失败原因： " + GetReturnCodeDesc(fCmdRet);
                    Log.Debug(strLog);
                    IsConnected = false;
                    return false;
                }
                else
                {
                    IsConnected = true;
                    string strLog = "连接读写器 " + PortName + "@" + 57600;
                    Log.Debug(strLog);
                    elegateRFIDCallBack = new RFIDCallBack(GetUid);
                    if (FrmPortIndex > 0)
                        UHF.RWDev.InitRFIDCallBack(elegateRFIDCallBack, false, FrmPortIndex);
                    RFIDRead();
                }
            }
            catch (Exception ex)
            {
                Log.Error("RFID打开失败", ex);
            }
            return IsConnected;
        }


        CancellationTokenSource cts;
        private static object obj = new object();

        int timeOut = 2;
        public override void Read(int TimeOut = 2)
        {
            if (IsReading == true) return;
            IsReading = true;
            timeOut = TimeOut;
            Thread thr = new Thread(new ThreadStart(() =>
            {
                lock (timeout)
                {
                    timeout = TimeOut;
                    RFIDRead();
                }
            }));
            thr.IsBackground = true;
            thr.Start();
        }


        private void RFIDRead()
        {
            if (IsReading == true) return;
            IsReading = true;
            try
            {
                lock (obj)
                {
                    BackQueue.Clear();
                    RFIDs.Clear();
                    byte Qvalue = 8;//2的Q次方为标签数量最合适
                    byte Session = 255;
                    byte MaskMem = 0;
                    byte[] MaskAdr = new byte[2];
                    byte MaskLen = 0;
                    byte[] MaskData = new byte[100];
                    byte MaskFlag = 0;
                    byte tidAddr = 0;
                    byte tidLen = 0;
                    byte TIDFlag = 0;
                    byte Target = 0;
                    byte InAnt = 0x84;
                    byte Scantime = 0x14;
                    byte FastFlag = 0;
                    byte[] EPC = new byte[50000];
                    byte Ant = 0;
                    int TagNum = 0;
                    int Totallen = 0;
                    Task.Run(() =>
                    {
                        while (IsReading)
                        {
                            try
                            {
                                fCmdRet = UHF.RWDev.Inventory_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, tidAddr, tidLen,
                                    TIDFlag, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref TagNum, FrmPortIndex);
                                if (!IsReading)
                                {
                                    BackQueue.Clear();
                                    RFIDs.Clear();
                                    if (StopCallback != null)
                                    {
                                        StopCallback(BoxNo, RFIDs.Count);
                                    }
                                }
                                Thread.Sleep(20);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Inventory_G2", ex);
                            }
                        }
                    });
                    Log.Info("RFID>>开始扫码");
                }
            }
            catch (Exception ex)
            {
                Log.Error("RFIDRead", ex);
            }
        }
        public override void Stop()
        {
            RFIDstop();
        }
        bool isEmpty = false;
        private void RFIDstop()
        {
            try
            {
                lock (obj)
                {
                    IsReading = false;
                    //if (reDatas.Count == 0)
                    //{

                    //    DoErr?.Invoke(this);
                    //}
                    //BackQueue.Clear();
                    //RFIDs.Clear();
                }
            }
            catch (Exception ex)
            {

                Log.Error("RFIDstop", ex);
            }
        }
        DateTime Currtime = DateTime.Now;



        /// <summary>
        /// 错误代码
        /// </summary>
        /// <param name="cmdRet"></param>
        /// <returns></returns>
        #region 
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                case 0x26:
                    return "操作成功";
                case 0x01:
                    return "询查时间结束前返回";
                case 0x02:
                    return "指定的询查时间溢出";
                case 0x03:
                    return "本条消息之后，还有消息";
                case 0x04:
                    return "读写模块存储空间已满";
                case 0x05:
                    return "访问密码错误";
                case 0x09:
                    return "销毁密码错误";
                case 0x0a:
                    return "销毁密码不能为全0";
                case 0x0b:
                    return "电子标签不支持该命令";
                case 0x0c:
                    return "对该命令，访问密码不能为全0";
                case 0x0d:
                    return "电子标签已经被设置了读保护，不能再次设置";
                case 0x0e:
                    return "电子标签没有被设置读保护，不需要解锁";
                case 0x10:
                    return "有字节空间被锁定，写入失败";
                case 0x11:
                    return "不能锁定";
                case 0x12:
                    return "已经锁定，不能再次锁定";
                case 0x13:
                    return "参数保存失败,但设置的值在读写模块断电前有效";
                case 0x14:
                    return "无法调整";
                case 0x15:
                    return "询查时间结束前返回";
                case 0x16:
                    return "指定的询查时间溢出";
                case 0x17:
                    return "本条消息之后，还有消息";
                case 0x18:
                    return "读写模块存储空间已满";
                case 0x19:
                    return "电子不支持该命令或者访问密码不能为0";
                case 0x1A:
                    return "标签自定义功能执行错误";
                case 0xF8:
                    return "检测天线错误";
                case 0xF9:
                    return "命令执行出错";
                case 0xFA:
                    return "有电子标签，但通信不畅，无法操作";
                case 0xFB:
                    return "无电子标签可操作";
                case 0xFC:
                    return "电子标签返回错误代码";
                case 0xFD:
                    return "命令长度错误";
                case 0xFE:
                    return "不合法的命令";
                case 0xFF:
                    return "参数错误";
                case 0x30:
                    return "通讯错误";
                case 0x31:
                    return "CRC校验错误";
                case 0x32:
                    return "返回数据长度有错误";
                case 0x33:
                    return "通讯繁忙，设备正在执行其他指令";
                case 0x34:
                    return "繁忙，指令正在执行";
                case 0x35:
                    return "端口已打开";
                case 0x36:
                    return "端口已关闭";
                case 0x37:
                    return "无效句柄";
                case 0x38:
                    return "无效端口";
                case 0xEE:
                    return "命令代码错误";
                default:
                    return Convert.ToString(cmdRet, 16);
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "其它错误";
                case 0x03:
                    return "存储器超限或不被支持的PC值";
                case 0x04:
                    return "存储器锁定";
                case 0x0b:
                    return "电源不足";
                case 0x0f:
                    return "非特定错误";
                default:
                    return "";
            }
        }
        #endregion
    }
}

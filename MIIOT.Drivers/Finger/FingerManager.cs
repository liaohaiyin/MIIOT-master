using MIIOT.Drivers.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class FingerManager
    {
        FingerAgrMian FingerCom;
        public FingerManager()
        {

        }
        public delegate void DelBackMsg(string msg);
        public event DelBackMsg DoMsgBack;
        public event SerialOpenClosedEvent OnOpenClose;

        public string Cabinet { get; set; }
        public string MacType { get; set; }
        public void Start(string PortName, int BaudRate)
        {
            FingerCom = new FingerAgrMian(PortName, BaudRate);
            FingerCom.OnOpenClose += FingerCom_OnOpenClose;      
            FingerCom.OnReturnDataEventArgs += ReturnDataEventArgsAction;
            FingerCom.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    if (!IsRegist)
                    {
                        var isExsit = SendMsg(FInstructionType.FindFinger, new byte[3]);
                        if (isExsit.fResponseCode == FResponseCode.SUCCESS)
                        {
                            var chkArgs = SendMsg(FInstructionType.ChkFinger, new byte[3]);
                            if (chkArgs.fResponseCode == FResponseCode.SUCCESS)
                            {
                                DoMsgBack?.Invoke("成功" + chkArgs.Msg);
                            }
                            else
                            {
                                DoMsgBack?.Invoke("失败" + chkArgs.Msg);
                            }
                        }
                        else
                        {
                            DoMsgBack?.Invoke("未检测到手指" + isExsit.Msg);
                        }
                    }
                }
            });
        }

        private void FingerCom_OnOpenClose(object sender, OpenCloseEventArgs args)
        {
            OnOpenClose?.Invoke(this, args);
        }

        bool IsRegist = false;
        int FID = 234;
        Queue RegFingerQeueu = Queue.Synchronized(new Queue());
        FReviceEventArgs _Msg = new FReviceEventArgs();
        AutoResetEvent ARE = new AutoResetEvent(false);
        ResultBinding result = new ResultBinding(100, 4000);
        private static byte[] Fingerbuff = new byte[2048];
        private void ReturnDataEventArgsAction(object sender, FReviceEventArgs msg)
        {
            if (msg.IsNormalCMD)
            {
                if (msg.fInstructionType.ToString() == result.CheckCodeEx(msg.fInstructionType.ToString()))
                {
                    _Msg = msg;
                    ARE.Set();
                }

            }
            else if (msg.fInstructionType == FInstructionType.UPLOAD_INFOR_TEMPLATES)
            {
                lock (Fingerbuff)
                {
                    Fingerbuff = msg.FingerBuff;
                }
            }

        }


        private int RegFinger(int fingerID, int RegTime)
        {
            if (RegTime > 2) return RegTime;
            byte[] buff = BitConverter.GetBytes((ushort)fingerID);
            var temp = SendMsg(FInstructionType.RegFinger, new byte[] { buff[0], buff[1], 0x01 });//第一次注册手指
            if (temp.fResponseCode == FResponseCode.SUCCESS)
            {
                DoMsgBack?.Invoke($"第{RegTime + 1}次注册成功，请抬起放下手指进行第{RegTime + 2}次注册" + temp.Msg);
                while (SendMsg(FInstructionType.FindFinger, new byte[3]).fResponseCode == FResponseCode.SUCCESS)
                {
                    Thread.Sleep(200);
                }
                RegTime++;
                return RegFinger(fingerID, RegTime);
            }
            else
            {
                if (temp.fResponseCode == FResponseCode.ERR_TEMPLATE_OCC)
                {
                    DoMsgBack?.Invoke($"注册时两次采集静脉特征差异过大，请重新开始" + temp.Msg);
                    if (SendMsg(FInstructionType.RegEnd, new byte[3]).fResponseCode == FResponseCode.SUCCESS)//结束注册
                    {
                        return RegFinger(fingerID, 0);
                    }
                }
                DoMsgBack?.Invoke($"第{RegTime}次失败，请重试" + temp.Msg);
                return RegFinger(fingerID, RegTime);
            }
        }
        public FReviceEventArgs SendMsg(FInstructionType FType, byte[] lstBody)
        {
            FingerCom.AddSendQueue(FType, lstBody, new byte[0]);
            result.addSeqNo(FType.ToString());
            ARE.WaitOne();
            return _Msg;
        }
        #region 抛出数据事件
        /// <summary>
        /// 返回数据事件，交由Manager解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public ReturnDataEventArgs OnReturnManagerDataEventArgs;
        /// <summary>
        /// 返回数据事件，交由Manager解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public ReturnDataEventArgs OpenTheDoor;

        #endregion

        ~FingerManager()
        {
            if (FingerCom != null)
                FingerCom.Stop();
        }
        //private static FingerManager instance;
        //private static object _lock = new object();
        ///// <summary>
        ///// 单例模式
        ///// </summary>
        //public static FingerManager Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (_lock)
        //            {
        //                if (instance == null)
        //                {
        //                    instance = new FingerManager();
        //                }
        //            }
        //        }
        //        return instance;
        //    }
        //}

        public void RegistFinger(int fingerID)
        {
            IsRegist = true;
            byte[] buff = BitConverter.GetBytes((ushort)fingerID);
            if (SendMsg(FInstructionType.RegEnd, new byte[3]).fResponseCode == FResponseCode.SUCCESS)//结束注册
            {
                var temp = SendMsg(FInstructionType.UPLOAD_INFOR, new byte[] { buff[0], buff[1], 0x01 });//查询ID是否重复注册
                if (temp.fResponseCode != FResponseCode.SUCCESS)
                {
                    temp = SendMsg(FInstructionType.ChkFinger, new byte[3]);//查询手指是否已注册
                    if (temp.fResponseCode != FResponseCode.SUCCESS)
                    {
                        DoMsgBack?.Invoke("请放入手指，进行注册" + temp.Msg);
                        int time = RegFinger(fingerID, 0);
                        if (time > 2)
                        {
                            temp = SendMsg(FInstructionType.RegEnd, new byte[3] { 0x00, 0x00, 0x01 });//模板写入硬件
                            if (temp.fResponseCode == FResponseCode.SUCCESS)
                            {//注册成功
                                temp = SendMsg(FInstructionType.UPLOAD_INFOR_TEMPLATES, new byte[3] { buff[0], buff[1], 0x00 });
                                if (temp.fResponseCode == FResponseCode.SUCCESS)
                                {

                                }
                                DoMsgBack?.Invoke("注册成功" + temp.Msg);
                                IsRegist = false;
                            }
                            else
                            {
                                DoMsgBack?.Invoke("保存失败" + temp.fResponseCode + ">" + temp.Msg);
                                IsRegist = false;
                            }
                        }
                    }
                    else
                    {
                        DoMsgBack?.Invoke("手指已注册" + temp.fResponseCode + ">" + temp.Msg);
                        IsRegist = false;
                    }
                }
                else
                {
                    DoMsgBack?.Invoke("指纹ID已存在" + temp.fResponseCode + ">" + temp.Msg);
                    IsRegist = false;
                }
            }
        }
        /// <summary>
        /// 删除手指
        /// </summary>
        /// <param name="FingerID">手指ID:范围0~65535</param>
        public void DeleteFinger(int FingerID)
        {
            byte[] buff = BitConverter.GetBytes((ushort)FingerID);
            byte[] Buff = new byte[3];
            Array.Copy(buff, 0, Buff, 0, 2);
            FingerCom.AddSendQueue(FInstructionType.DeleteFinger, Buff, new byte[0]);
        }
        /// <summary>
        /// 删除所有手指
        /// </summary>
        public void DeleteAllFinger()
        {
            FingerCom.AddSendQueue(FInstructionType.DeleteAllFinger, new byte[3], new byte[0]);
        }
        /// <summary>
        /// 查询所有已注册的手指ID
        /// </summary>
        public void FingerList()
        {
            FingerCom.AddSendQueue(FInstructionType.FindAllFinger, new byte[3], new byte[0]);
        }
        public void UploadDatatemplates(int fingerID)
        {
            byte[] buff = BitConverter.GetBytes((ushort)fingerID);
            var temp = SendMsg(FInstructionType.UPLOAD_INFOR_TEMPLATES, new byte[3] { buff[0], buff[1], 0x00 });
            if (temp.fResponseCode == FResponseCode.SUCCESS)
            {

            }
        }
        /// <summary>
        /// 下载模板数据
        /// </summary>
        public void DownloadTempldates()
        {
            int len = 18 + 3 * 512;
            byte[] buffs = BitConverter.GetBytes((ushort)len);
            FingerCom.AddSendQueue(FInstructionType.DOWNLOAD_INFOR_TEMPLATES, new byte[3] { buffs[0], buffs[1], 0x00 }, Fingerbuff);
        }
    }

}

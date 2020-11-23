using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace MIIOT.Trical
{
    public class AlarmMsgSender
    {
        private static object obj = new object();
        private static AlarmMsgSender _ins;

        public static AlarmMsgSender Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                    {
                        _ins = new AlarmMsgSender();
                    }
                }
                return _ins;
            }
        }
        bool IsOpen = true;
        public AlarmMsgSender()
        {
            try
            {
                IsOpen = CacheData.Ins.JsonConfig["短信通知开关"] != "0";
            }
            catch (Exception ex)
            {


            }

            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        lock (ConnectMsgs)
                        {
                            if (ConnectMsgs.Count > 0)
                            {
                                string hospital = "";
                                string address = "";
                                foreach (var item in ConnectMsgs)
                                {
                                    if ((address + item.address).Length > 18 - HMMsgs.Count.ToString().Length)
                                        break;
                                    hospital = item.hospital;
                                    address += item.address;
                                }
                                if (address.Length > 18 - ConnectMsgs.Count.ToString().Length)
                                    address = address.Substring(0, 18 - ConnectMsgs.Count.ToString().Length);
                                address += $"等{ConnectMsgs.Count}台";
                                if (hospital.Length > 20)
                                    hospital = hospital.Substring(0, 20);
                                SendConnectMsg(hospital, address);
                                ConnectMsgs.Clear();
                            }
                        }
                        Thread.Sleep(1000 * 30);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("", ex);
                }
            });
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        lock (HMMsgs)
                        {
                            if (HMMsgs.Count > 0)
                            {
                                string hospital = "";
                                string address = "";
                                foreach (var item in HMMsgs)
                                {
                                    if ((address + item.address).Length > 18 - HMMsgs.Count.ToString().Length)
                                        break;
                                    hospital = item.hospital;
                                    address += item.address;

                                }

                                if (address.Length > 18 - HMMsgs.Count.ToString().Length)
                                    address = address.Substring(0, 18 - HMMsgs.Count.ToString().Length);
                                address += $"等{HMMsgs.Count}台";
                                if (hospital.Length > 20)
                                    hospital = hospital.Substring(0, 20);
                                SendHMMsg(hospital, address);
                                HMMsgs.Clear();
                            }
                        }
                        Thread.Sleep(1000 * 30);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("", ex);
                }
            });
        }
        List<WXSendInfo> infos = new List<WXSendInfo>();
        HashSet<WXSendInfo> ConnectMsgs = new HashSet<WXSendInfo>();
        HashSet<WXSendInfo> HMMsgs = new HashSet<WXSendInfo>();
        EbigWeChat.EbeitXmlWebServiceClient wsPacket = new EbigWeChat.EbeitXmlWebServiceClient();
        private void SendMsg(Wsid _wsid, string hospital, string address, string alarm, string phonenumbers)
        {
            try
            {
                string wsid = _wsid.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><datapacket errcode = \"0\" errmsg = \"\" wsid = \"{wsid}\"> " +
                    $" <rowdata> <row hospital = \"{hospital}\" address = \"{address}\" alarm = \"{alarm}\" " +
                    $"phonenumbers = \"{phonenumbers}\"/>  </rowdata></datapacket>");
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xmlDoc.WriteTo(tx);
                string xmlstr = sw.ToString();
                var tem = wsPacket.normalService("MIIOT_Trical", "admin", "koala01", xmlstr);
                
                if (!tem.Contains("errcode=\"0\""))
                {
                    Log.Debug(_wsid == Wsid.EIS_ALARM_SMS ? "短信" : "电话" + phonenumbers + hospital + address + alarm + tem);
                }
                Log.Info(_wsid == Wsid.EIS_ALARM_SMS ? "短信" : "电话" + phonenumbers + hospital + address + alarm + tem);
            }
            catch (Exception ex)
            {
                Log.Error("xmlSend", ex);
            }
        }
        public void SendWXMsg(Wsid _wsid, string equipmentno, string errormessage, string accountcode)
        {
            try
            {
                string info = equipmentno + errormessage;

                var temp = infos.FirstOrDefault(a => a.info == info);
                if (temp != null)
                {
                    if ((DateTime.Now - temp.time).Minutes < 1)
                    {
                        Log.Fatal("微信消息发送间隔小于1分钟" + info, new Exception(""));
                        return;
                    }
                    else
                    {
                        infos.Remove(temp);
                    }
                }
                WXSendInfo Info = new WXSendInfo() { info = info, time = DateTime.Now, sendCount = 1 };
                infos.Add(Info);
                if (!IsOpen)
                {
                    //CacheData.Ins.mainWindow.addHBlog(info);
                    Log.Fatal(_wsid.ToString() + info, new Exception());
                    return;
                }


                string wsid = _wsid.ToString();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml($"<?xml version=\"1.0\" encoding=\"UTF-8\"?><datapacket errcode = \"0\" errmsg = \"\" wsid = \"{wsid}\"> " +
                    $" <rowdata> <row equipmentno = \"{equipmentno}\" errormessage = \"{errormessage}\" accountcode = \"{accountcode}\" " +
                    $"/>  </rowdata></datapacket>");
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xmlDoc.WriteTo(tx);
                string xmlstr = sw.ToString();
                var tem = wsPacket.normalService("MIIOT_Trical", "admin", "koala01", xmlstr);
                if (!tem.Contains("errcode=\"0\""))
                {
                    Log.Debug("微信:" + equipmentno + errormessage + accountcode + tem);
                }
                Log.Info("微信:" + equipmentno + errormessage + accountcode + tem);
            }
            catch (Exception ex)
            {
                Log.Error("WX xmlSend", ex);
            }
        }

        public void SendSMSMsg(string hospital, string address, string alarm, string phonenumbers)
        {
            SendMsg(Wsid.EIS_ALARM_SMS, hospital, address, alarm, phonenumbers);
        }
        public void SendSMSMsg(string hospital, string address, string alarm, List<string> phonenumbers)
        {
            string info = Wsid.EIS_ALARM_SMS + hospital + address + alarm;

            var temp = infos.FirstOrDefault(a => a.info == info);
            if (temp != null)
            {
                if ((DateTime.Now - temp.time).Minutes < 1)
                {
                    Log.Fatal("信息发送间隔小于1分钟" + info, new Exception(""));
                    return;
                }
                else
                {
                    infos.Remove(temp);
                }
            }

            WXSendInfo Info = new WXSendInfo() { info = info, time = DateTime.Now, sendCount = 1 };
            infos.Add(Info);
            if (!IsOpen)
            {
                //CacheData.Ins.mainWindow.addHBlog(info);
                Log.Fatal("SendSMSMsg:" + info, new Exception());
                return;
            }
            var Macs = CacheData.Ins.JsonConfig["SMSPhones"];
            string[] PhoneList = Macs.Split(',');
            if (phonenumbers == null || phonenumbers.Count == 0)
                phonenumbers = PhoneList.ToList();
            foreach (var item in phonenumbers)
            {
                //SMSQeueu.Enqueue(new WXSendInfo() { hospital = hospital, department = department, address = address, alarm = alarm, phonenumbers = item, time = DateTime.Now });
                SendSMSMsg(hospital, address, alarm, item);
            }
        }
        public void SendMsg(string hospital, string address, string alarm, string phonenumbers)
        {

            SendMsg(Wsid.EIS_ALARM_MSG, hospital, address, alarm, phonenumbers);
        }
        public void SendMsg(string hospital, string address, string alarm, List<string> phonenumbers)
        {
            string info = Wsid.EIS_ALARM_MSG + hospital + address + alarm;

            var temp = infos.FirstOrDefault(a => a.info == info);
            if (temp != null)
            {
                if ((DateTime.Now - temp.time).Minutes < 1)
                {
                    Log.Fatal("语音消息发送间隔小于1分钟" + info, new Exception(""));
                    return;
                }
                else
                {
                    infos.Remove(temp);
                }
            }
            WXSendInfo Info = new WXSendInfo() { info = info, time = DateTime.Now, sendCount = 1 };
            infos.Add(Info);

            if (!IsOpen)
            {
               // CacheData.Ins.mainWindow.addHBlog(info);
                Log.Fatal("SendMsg:" + info, new Exception());
                return;
            }
            var Macs = CacheData.Ins.JsonConfig["Phones"];
            string[] PhoneList = Macs.Split(',');
            if (phonenumbers == null || phonenumbers.Count == 0)
                phonenumbers = PhoneList.ToList();
            foreach (var item in phonenumbers)
            {
                //MSGQeueu.Enqueue(new WXSendInfo() { hospital = hospital, department = department, address = address, alarm = alarm, phonenumbers = item, time = DateTime.Now });
                SendMsg(hospital, address, alarm, item);
            }
        }
        public void SendWXMsg(string equipmentno, string errormessage, List<string> accountcode)
        {
            var Macs = CacheData.Ins.JsonConfig["WeChats"];
            string[] PhoneList = Macs.Split(',');
            if (accountcode == null || accountcode.Count == 0)
                accountcode = PhoneList.ToList();
            foreach (var item in accountcode)
            {
                //WXQeueu.Enqueue(new WXSendInfo() { address = equipmentno, alarm = errormessage, phonenumbers = item, time = DateTime.Now });
                SendWXMsg(Wsid.EIS_ALARM_WX, equipmentno, errormessage, item);
            }
        }
        public void SendConnectMsg(string hospital, string MacName)
        {
            string SendMode = CacheData.Ins.JsonConfig["SendMode"];
            if (SendMode != null && SendMode.Length == 3)
            {
                if (SendMode[0] == '1')
                    SendWXMsg(hospital + MacName + "智能柜", "电源/网络连接", new List<string>());
                Thread.Sleep(100);
                if (SendMode[1] == '1')
                    SendSMSMsg(hospital, MacName, "电源/网络连接", new List<string>());
                Thread.Sleep(100);
                if (SendMode[2] == '1')
                    SendMsg(hospital, MacName, "电源/网络连接", new List<string>());
            }
            else
            {
                SendWXMsg(hospital + MacName + "智能柜", "电源/网络连接", new List<string>());
                Thread.Sleep(100);
                SendSMSMsg(hospital, MacName, "电源/网络连接", new List<string>());
                Thread.Sleep(100);
                SendMsg(hospital, MacName, "电源/网络连接", new List<string>());
                Log.Fatal("SendMode设置异常", new Exception(""));
            }
        }
        public void SendHMMsg(string hospital, string MacName)
        {
            string SendMode = CacheData.Ins.JsonConfig["SendMode"];
            if (SendMode != null && SendMode.Length == 3)
            {
                if (SendMode[0] == '1')
                    SendWXMsg(hospital + MacName + "智能柜", "温度异常", new List<string>());
                Thread.Sleep(100);
                if (SendMode[1] == '1')
                    SendSMSMsg(hospital, MacName, "温度", new List<string>());
                Thread.Sleep(100);
                if (SendMode[2] == '1')
                    SendMsg(hospital, MacName, "温度", new List<string>());
            }
            else
            {
                SendWXMsg(hospital + MacName + "智能柜", "温度异常", new List<string>());
                Thread.Sleep(100);
                SendSMSMsg(hospital, MacName, "温度", new List<string>());
                Thread.Sleep(100);
                SendMsg(hospital, MacName, "温度", new List<string>());
                Log.Fatal("SendMode设置异常", new Exception(""));
            }
        }
        public void SendMsg(string hospital, string MacName, string msgType)
        {
            if (msgType == "HM")
            {
                lock (HMMsgs)
                {
                    var temp = HMMsgs.FirstOrDefault(a => a.address == MacName);
                    if (temp == null)
                        HMMsgs.Add(new WXSendInfo() { hospital = hospital, address = MacName });
                }
            }
            else
            {
                lock (ConnectMsgs)
                {
                    var temp = ConnectMsgs.FirstOrDefault(a => a.address == MacName);
                    if (temp == null)
                        ConnectMsgs.Add(new WXSendInfo() { hospital = hospital, address = MacName });
                }
            }
        }
    }
    public class WXSendInfo
    {
        public string phonenumbers { get; set; }
        public string hospital { get; set; }
        public string address { get; set; }
        public string alarm { get; set; }
        public DateTime time { get; set; }
        public string info { get; set; }
        public int sendCount { get; set; }
    }
    public enum Wsid
    {
        EIS_ALARM_WX,
        EIS_ALARM_SMS,
        EIS_ALARM_MSG
    }
}

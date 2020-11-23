using MIIOT.Trical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.Model
{
    public class HeartbeatSign
    {
        public delegate void DelConnected(object sender, bool isConncet);
        public event DelConnected OnConnected;
        public HeartbeatSign(string macID, string macName, bool isConnect, int breadNum)
        {
            MacId = macID;
            MacName = macName;
            IsConnect = isConnect;
            BreakNum = breadNum;
             var hospital = CacheData.Ins.JsonConfig["HospitalName"];
            Task.Run(() =>
            {
                try
                {
                    while (true)
                    {
                        BreakNum++;
                        if (BreakNum >= 6)
                        {
                            if (IsConnect)
                            {
                                IsConnect = false;
                                OnConnected?.Invoke(this, false);
                                AlarmMsgSender.Ins.SendMsg(hospital, macName, "Connect");
                            }
                        }
                        else
                        {
                            OnConnected?.Invoke(this, true);
                            IsConnect = true;
                        }
                        Thread.Sleep(20000);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("HB", ex);
                }
            });
        }
        public string Remote { get; set; }
        public string MacId { get; set; }
        public string MacName { get; set; }
        public bool IsConnect { get; set; }
        public int BreakNum { get; set; }
    }
}

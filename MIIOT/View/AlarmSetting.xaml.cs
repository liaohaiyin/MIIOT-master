using MIIOT.Model;
using MIIOT.Trical;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Control
{
    /// <summary>
    /// AlarmSetting.xaml 的交互逻辑
    /// </summary>
    public partial class AlarmSetting : UserControl
    {
        Dictionary<string, HeartbeatSign> dicFridge = new Dictionary<string, HeartbeatSign>();
        string hospital = CacheData.Ins.JsonConfig["HospitalName"];
        public AlarmSetting()
        {
            InitializeComponent();

            try
            {
                var Macs = CacheData.Ins.JsonConfig["MacCodes"];
                dynamic mac = JsonConvert.DeserializeObject(Macs);
                foreach (var item in mac)
                {
                    string macID = item.MacID;
                    string macName = item.MacName;
                    HeartbeatSign HBS = new HeartbeatSign(macID, macName, false, 0);
                    HBS.OnConnected += HBS_OnConnected;
                    dicFridge.Add(macID, HBS);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Init FHB", ex);
            }

            try
            {
                CacheData.Ins.TcpServer.DoMsgCallback += TcpServer_DoMsgCallback;
                CacheData.Ins.TcpServer.onConnect += TcpServer_onConnect;
                CacheData.Ins.TcpServer.Start(int.Parse(CacheData.Ins.JsonConfig["TCPServerPort"]));
            }
            catch (Exception ex)
            {
                Log.Error("TCP Init:", ex);
            }
        }
        private void HBS_OnConnected(object sender, bool isConncet)
        {
            if (sender is HeartbeatSign sign)
            {
                //if (tHView != null)
                //    tHView.Refreshdata(sign.MacId, isConncet);
            }

        }
        private void TcpServer_onConnect(object sender, string remote, bool isConnect)
        {
            if (isConnect)
                CacheData.Ins.TcpServer.SendByJson(remote, "M1", "ADD", "aaa", "{\"s\":\"d\"}");
            Log.Debug(remote + ":" + isConnect);
            foreach (var item in dicFridge.Values)
            {
                if (item.Remote == remote)
                {
                    //if (tHView != null)
                    //    tHView.Refreshdata(item.MacId, false);
                }
            }
        }
        private void TcpServer_DoMsgCallback(object sender, string remote, string msg, string msgType = "")
        {
            try
            {

                Log.Debug(remote + ">>" + msg);

                dynamic dyData = JsonConvert.DeserializeObject(msg);
                string target = dyData.Target;
                string CMD = dyData.CMD;
                string SourceType = dyData.SourceType;
                List<string> codes = new List<string>();
                string code = dyData.Data == null ? "" : (dyData.Data.Ids ?? "");
                string card = dyData.Data == null ? "" : (dyData.Data.CARD ?? "");
                foreach (var item in dicFridge.Keys)
                {
                    if (target == item)
                    {
                        dicFridge[item].Remote = remote;
                        dicFridge[item].BreakNum = 0;
                        string aaa = dyData.Data.ToString();
                        dynamic HM = JsonConvert.DeserializeObject(aaa);
                        string T = HM.T;
                        string M = HM.H;
                        double Temp = 0;
                        double HUM = 0;
                        double.TryParse(T, out Temp);
                        double.TryParse(M, out HUM);
                        string BaseT = CacheData.Ins.JsonConfig["HMGap"];
                        string[] baseTs = BaseT.Split(',');
                        double miniT = 4;
                        double maxT = 11;
                        double.TryParse(baseTs[0], out miniT);
                        double.TryParse(baseTs[1], out maxT);
                        //if (tHView != null)
                        //    tHView.ReFreshTH(item, Temp, HUM);
                        if (Temp > maxT || Temp < miniT)
                        {
                            AlarmMsgSender.Ins.SendMsg(hospital, dicFridge[target].MacName, "HM");
                        }
                        return;
                    }
                }
                
                //if (Setting != null)
                //    Setting.Receive(msg);
            }
            catch (Exception ex)
            {
                Log.Error("TcpServer_OnReceiveMsg", ex);
            }

        }
    }
}

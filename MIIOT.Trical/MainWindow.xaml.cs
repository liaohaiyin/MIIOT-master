using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Trical.Common;
using MIIOT.Trical.Controls;
using MIIOT.Trical.View;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace MIIOT.Trical
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        #endregion
        #region 属性
        private bool _IsLoging;

        public bool IsLoging
        {
            get { return _IsLoging; }
            set
            {
                _IsLoging = value;
                OnPropertyChanged("IsLoging");
            }
        }

        #endregion
        public static Queue msgQueue = Queue.Synchronized(new Queue());
        List<Border> menus = new List<Border>();
        TrialBusiness trialBusiness = new TrialBusiness();
        public ContorllerItem[] Items { get; set; }
        SettingView Setting;
        public AcceptView acceptView;
        ApplyView applyView;
        ApplyBackView applyBackView;
        RefundView refundView;
        PrintLabelView printLabelView;
        ReceiveView receiveView;
        THView tHView;
        Dictionary<string, HeartbeatSign> dicFridge = new Dictionary<string, HeartbeatSign>();
        HashSet<string> ErrCode = new HashSet<string>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            CacheData.Ins.mainWindow = this;
            SystemSleep.PreventForCurrentThread();
            CacheData.Ins.solid = FindResource("PrimaryHueMidBrush") as SolidColorBrush;
            CacheData.Ins.ForegroundSolid = FindResource("PrimaryForegroudBrush") as SolidColorBrush;
            CacheData.Ins.WSClient = new Utility.WSocketClient(CacheData.Ins.JsonConfig["WSSocket"], CacheData.Ins.JsonConfig["WSSocketClientName"]);
            Task.Run(() =>
            {
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
                try
                {
                    CacheData.Ins.WSClient.MessageReceived += Clinet_MessageReceived;
                    CacheData.Ins.WSClient.Start();
                }
                catch (Exception ex)
                {
                    Log.Error("WSClient:", ex);
                }
                try
                {
                    CECOMCardHelper helper = new CECOMCardHelper(CacheData.Ins.JsonConfig["CardReaderPortName"], 9600);
                    helper.DoOpenOrClose += Helper_DoOpenOrClose;
                    helper.DoMsgBack += Helper_DoMsgBack;
                    helper.Start();
                }
                catch (Exception ex)
                {
                    Log.Error("CECOMCardHelper:", ex);
                }
                try
                {
                    PlatformReader reader = new PlatformReader();
                    reader.OnReturnDataEventArgs += Reader_OnReturnDataEventArgs;
                    reader.Open(CacheData.Ins.JsonConfig["PlatformReaderPortName"]);
                    reader.Start();
                }
                catch (Exception ex)
                {
                    Log.Error("PlatformReader:", ex);
                }
                try
                {
                    MCUManager.Instance.OnReturnManagerDataEventArgs += ReceiveMCO1EventArgs;
                    MCUManager.Instance.Start(CacheData.Ins.JsonConfig["RackCom"], 115200);
                }
                catch (Exception ex)
                {
                    Log.Error("MC01Manager", ex);
                }

            });
            Setting = new SettingView();
            acceptView = new AcceptView();
            applyView = new ApplyView();
            applyBackView = new ApplyBackView();
            refundView = new RefundView();
            receiveView = new ReceiveView();
            printLabelView = new PrintLabelView();
            tHView = new THView();
            Items = new[]{
                new ContorllerItem("验收",acceptView),
                new ContorllerItem("申领",applyView),
                new ContorllerItem("退库",applyBackView),
                new ContorllerItem("退货",refundView),
                new ContorllerItem("重打标签",printLabelView),
                new ContorllerItem("领用",receiveView),
                new ContorllerItem("测试",Setting),
                new ContorllerItem("温湿度监控",tHView),
                new ContorllerItem("温湿度监控",new UserMagView())
            };
            menus.Add(this.borAccept);
            menus.Add(this.borPull);
            menus.Add(this.borCancellingStock);
            menus.Add(this.borRefund);
            menus.Add(this.borPrintLabel);
            //menus.Add(this.borReceive);

            this.Loaded += MainWindow_Loaded;

            Task.Run(() =>
            {
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
                while (true)
                {
                    try
                    {
                        if (msgQueue.Count > 0)
                        {
                            var msg = msgQueue.Dequeue().ToString();
                            if (msg == null) continue;
                            this.Dispatcher.Invoke(() =>
                            {
                                try
                                {
                                    MainSnackbar.Message = new MaterialDesignThemes.Wpf.SnackbarMessage()
                                    {
                                        Content = msg,
                                        Foreground = new SolidColorBrush(Colors.White),
                                        FontSize = 20,
                                        HorizontalContentAlignment = HorizontalAlignment.Center
                                    };
                                    MainSnackbar.IsActive = true;
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("MainSnackbar", ex);
                                }
                            });
                            Thread.Sleep(5000);
                            this.Dispatcher.Invoke(() =>
                            {
                                MainSnackbar.IsActive = false;
                            });
                        }
                        Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("msgQueue", ex);
                    }
                }
            });
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    try
                    {


                        lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
                        GetLastInputInfo(ref lastInPut);

                        long noOperationTime = System.Environment.TickCount - lastInPut.dwTime;
                        int LoginTimeOut = 20;
                        int.TryParse(CacheData.Ins.JsonConfig["LoginTimeOut"], out LoginTimeOut);
                        if (noOperationTime > LoginTimeOut * 60 * 1000)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Logout();
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("auto logout", ex);
                    }
                }
            });

        }
        public void addHBlog(string log)
        {
            if (tHView != null)
                tHView.AddLog(log);
        }
        void ReceiveMCO1EventArgs(object sender, MCReviceEventArgs fBack)
        { 
        
        }

        private void TcpServer_onConnect(object sender, string remote, bool isConnect)
        {
            if (isConnect)
                CacheData.Ins.TcpServer.SendByJson(remote, "M1", "ADD", "aaa", "{\"s\":\"d\"}");
            Setting.Addline(remote + ":" + isConnect);
            foreach (var item in dicFridge.Values)
            {
                if (item.Remote == remote)
                {
                    if (tHView != null)
                        tHView.Refreshdata(item.MacId, false);
                }
            }
        }

        private void HBS_OnConnected(object sender, bool isConncet)
        {
            if (sender is HeartbeatSign sign)
            {
                if (tHView != null)
                    tHView.Refreshdata(sign.MacId, isConncet);
            }

        }

        private void Reader_OnReturnDataEventArgs(object sender, PFReaderBackMsg hMBack)
        {
            try
            {
                Log.Debug("RFID:" + hMBack.Msg);
                if (hMBack.EventType != EventType.SerialRevice) return;
                if (hMBack != null && hMBack.Msg != null)
                    Log.Debug("RFID:" + hMBack.Msg);
                else
                    Log.Debug("RFID is Error");
                this.Dispatcher.Invoke(() =>
                {
                    string rfid = hMBack.Msg;
                    switch (ItemListBox.SelectedIndex)
                    {
                        case 0:
                            acceptView.getSinglecodes(rfid);
                            break;
                        case 1:
                            applyView.getSinglecodes(rfid);
                            break;
                        case 2:
                            applyBackView.getSinglecodes(rfid);
                            break;
                        case 3:
                            refundView.getSinglecodes(rfid);
                            break;
                        case 4:
                            printLabelView.getSinglecodes(rfid);
                            break;
                        case 5:
                            receiveView.getSinglecodes(rfid);
                            break;
                        default:
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }

        }

        private async void Helper_DoMsgBack(object sender, byte[] buff)
        {
            try
            {
                if (buff == null) return;
                byte[] card = buff.Skip(3).Take(4).ToArray();
                Array.Reverse(card);
                HttpResultBase Result = await cardLogin(card.ByteToHexStr());
                if (Result != null && Result.data != null && Result.code == 0)
                {

                    dynamic record = JsonConvert.DeserializeObject(Result.data.ToString());
                    CacheData.Ins.Token = record.token ?? "";
                    CacheData.Ins._user_info.id = record.id ?? 0;
                    CacheData.Ins._user_info.displayName = record.displayName ?? "";


                    this.Dispatcher.Invoke(() =>
                    {
                        UserNameCache(txtUserName.Text);
                        bor.Visibility = Visibility.Collapsed;
                        txtErr.Visibility = Visibility.Collapsed;
                    });

                    var url = CacheData.Ins.JsonConfig.GetValue("取得用户信息_Get");
                    var result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
                    if (result != null && result.code == 0 && result.data != null)
                    {
                        CacheData.Ins._user_info = JsonConvert.DeserializeObject<user_info>(result.data.ToString());
                    }

                    RefreshDBData();

                }
                else
                {
                    msgQueue.Enqueue("卡片未授权");
                }

            }
            catch (Exception ex)
            {
                Log.Error("Helper_DoMsgBack", ex);
            }
        }

        private void Helper_DoOpenOrClose(object sender, bool isOpen)
        {
        }

        private void Clinet_MessageReceived(WSocketClient Sender, WSMsgModel Msg)
        {
            try
            {
                AddLog(Msg.Data.ToString());
                Log.Info("WSSRecive:" + Msg.Data.ToString());
                dynamic wsMsg = JsonConvert.DeserializeObject(Msg.Data.ToString());
                string CMD = wsMsg.Data.busType;
                List<string> codes = new List<string>();
                string aa = JsonConvert.SerializeObject(wsMsg.Data.datalist);

                codes = JsonConvert.DeserializeObject<List<string>>(aa);
                var cmd = Enum.Parse(typeof(TCPPushTagEnum), CMD);
                switch (cmd)
                {
                    case TCPPushTagEnum.DELIVERY:
                        var data = new { Ids = wsMsg.Data.datalist };
                        string dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            acceptView.GetDetail(item);
                        }
                        break;
                    case TCPPushTagEnum.ACCEPT:
                        data = new { Ids = wsMsg.Data.datalist };
                        dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            acceptView.GetDetail(item);
                        }
                        break;
                    case TCPPushTagEnum.APPLY_BACK:
                        data = new { Ids = wsMsg.Data.datalist };
                        dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            applyBackView.GetDetail(item);
                        }
                        break;
                    case TCPPushTagEnum.APPLY:
                        data = new { Ids = wsMsg.Data.datalist };
                        dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            applyView.GetDetail(item);
                        }
                        break;
                    case TCPPushTagEnum.REFUND:
                        data = new { Ids = wsMsg.Data.datalist };
                        dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            refundView.GetDetail(item);
                        }
                        break;
                    case TCPPushTagEnum.RACK:
                        data = new { Ids = wsMsg.Data.datalist };
                        dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            trialBusiness.RackList(item);
                        }
                        break;
                    case TCPPushTagEnum.PICK:
                        data = new { Ids = wsMsg.Data.datalist };
                        dataJson = JsonConvert.SerializeObject(data);
                        ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", CMD, dataJson);
                        foreach (var item in codes)
                        {
                            trialBusiness.PickList(item);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Clinet_MessageReceived", ex);
            }

        }
        string hospital = CacheData.Ins.JsonConfig["HospitalName"];
        private void TcpServer_DoMsgCallback(object sender, string remote, string msg, string msgType = "")
        {
            try
            {
                if (Setting != null)
                    Setting.Addline(remote + ">>" + msg);
                if (msgType == "HB")
                {
                    string macName = msg.Substring(4, msg.Length - 4);
                    if (macName == CacheData.Ins.MasterTCP)
                    {
                        if (CacheData.Ins.Screens[CacheData.Ins.MasterTCP] == "")
                        {
                            ScreenManager.Ins.init(remote, macName);
                        }
                        CacheData.Ins.Screens[CacheData.Ins.MasterTCP] = remote;
                    }
                    else if (macName == CacheData.Ins.SlaveTCP)
                    {
                        if (CacheData.Ins.Screens[CacheData.Ins.SlaveTCP] == "")
                        {
                            ScreenManager.Ins.init(remote, macName);
                        }
                        CacheData.Ins.Screens[CacheData.Ins.SlaveTCP] = remote;
                    }

                }
                else
                {
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
                            if (tHView != null)
                                tHView.ReFreshTH(item, Temp, HUM);
                            if (Temp > maxT || Temp < miniT)
                            {
                                AlarmMsgSender.Ins.SendMsg(hospital, dicFridge[target].MacName, "HM");
                            }
                            return;
                        }
                    }
                    if (target == CacheData.Ins.MasterTCP)
                    {

                    }
                    else if (target == CacheData.Ins.SlaveTCP)
                    {
                        if (CMD == TCPPushTagEnum.CARD.ToString())
                        {
                            ScreenCardLogin(remote, target, card);
                        }
                        if (CMD == "COM")//完成
                        {
                            if (SourceType == TCPPushTagEnum.RACK.ToString())
                            {
                                RackComplete(remote, target, SourceType, code);
                            }
                            if (SourceType == TCPPushTagEnum.PICK.ToString())
                            {
                                PickComplete(remote, target, SourceType, code);
                            }
                        }
                    }
                    if (Setting != null)
                        Setting.Receive(msg);
                }
            }
            catch (Exception ex)
            {
                Log.Error("TcpServer_OnReceiveMsg", ex);
            }

        }
        HttpResolver httpResolver = new HttpResolver();
        private async void RackComplete(string remote, string target, string SourceType, string code)
        {
            var data = new { sourceNo = code, status = 0 };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("上架确认_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (Result != null && Result.data != null && Result.code == 0)
            {

                var datas = new { Ids = code };
                var dataJson = JsonConvert.SerializeObject(datas);
                ScreenManager.Ins.SendMsg(remote, target, "REPLY", SourceType, dataJson);
            }
        }
        private async void PickComplete(string remote, string target, string SourceType, string code)
        {
            string url = CacheData.Ins.JsonConfig.GetValue("拣选完成_Post");
            url += "/" + code;
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                string pickCode = Result.data.ToString();
                var datas = new { Ids = code };
                var dataJson = JsonConvert.SerializeObject(datas);
                ScreenManager.Ins.SendMsg(remote, target, "REPLY", SourceType, dataJson);
            }
        }
        private async void ScreenCardLogin(string remote, string target, string card)
        {
            HttpResultBase Result = await cardLogin(card);
            if (Result != null && Result.data != null && Result.code == 0)
            {

                dynamic record = JsonConvert.DeserializeObject(Result.data.ToString());
                var datas = new { name = record.displayName ?? "工卡未授权" };
                var dataJson = JsonConvert.SerializeObject(datas);
                ScreenManager.Ins.SendMsg(remote, target, "ADD", "TASK", dataJson);
            }
            else
            {
                var datas = new { name = "工卡未授权" };
                var dataJson = JsonConvert.SerializeObject(datas);
                ScreenManager.Ins.SendMsg(remote, target, "ADD", "TASK", dataJson);
            }

        }
        private void TcpServer_OnDisConnected(SocketManager.SocketInfo socketInfo)
        {
            try
            {
                MessageBox.Show(socketInfo.socket.RemoteEndPoint.ToString() + "连接断开");
                if (socketInfo.socket.RemoteEndPoint.ToString().RegexIP() == CacheData.Ins.MasterTCP)
                {
                    CacheData.Ins.Screens[CacheData.Ins.MasterTCP] = "";
                }
                if (socketInfo.socket.RemoteEndPoint.ToString().RegexIP() == CacheData.Ins.SlaveTCP)
                {
                    CacheData.Ins.Screens[CacheData.Ins.SlaveTCP] = "";
                }
            }
            catch (Exception ex)
            {
                Log.Error("TcpServer_OnDisConnected", ex);
            }
        }

        private void TcpServer_OnConnected(SocketManager.SocketInfo socketInfo)
        {
            //MessageBox.Show(socketInfo.socket.RemoteEndPoint.ToString() + "连接成功");

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                (PresentationSource.FromVisual(this) as HwndSource).AddHook(new HwndSourceHook(this.WndProc));

                string liststr = CacheData.Ins.JsonUserCache["UserNameCache"];
                if (liststr.Length > 0)
                {
                    string[] list = liststr.Split(',');
                    foreach (var item in list)
                    {
                        txtUserName.Items.Add(item);
                        txtUserName.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("MainWindow_Loaded", ex);
            }
        }
        IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 0x004A)
            {
                COPYDATASTRUCT cds = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT)); // 接收封装的消息
                string rece = cds.lpData; // 获取消息内容


            }
            return hwnd;
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }

        private void ItemListBoxSelecte_changed(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        public static async Task<bool> ShowCheckWin(string Msg)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Msg = Msg
            };

            var CanClose = await DialogHost.Show(sampleMessageDialog, "RootDialog");
            return (bool)CanClose;
        }
        public void AddLog(string log)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss.fff>>") + log);
                txtLog.AppendText(Environment.NewLine);
                txtLog.ScrollToEnd();
            });
        }
        public static void SendMSg(object data)
        {

            string jsonstr = JsonConvert.SerializeObject(data);
            MessageHelper.Ins.SendMsg("YDHCG", jsonstr);
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            if (IsLoging) return;
            IsLoging = true;
            Login();
        }
        public async void Login()
        {
         

            try
            {
                if (txtUserName.Text == "")
                {
                    msgQueue.Enqueue("用户名为空");
                    return;
                }
                if (txtPassword.Password == "")
                {
                    msgQueue.Enqueue("密码为空");
                    return;
                }
                string url = CacheData.Ins.JsonConfig.GetValue("登录_Post");
                Dictionary<string, string> dic = new Dictionary<string, string>();
                this.Dispatcher.Invoke(() =>
                {
                    dic.Add("username", txtUserName.Text);
                    dic.Add("password", txtPassword.Password);
                    txtErr.Visibility = Visibility.Collapsed;
                });
                var usernameApassword = new { username = txtUserName.Text, password = txtPassword.Password };
                string datajson = JsonConvert.SerializeObject(usernameApassword);
                HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, "", null, datajson);
                if (Result != null && Result.data != null && Result.code == 0)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        UserNameCache(txtUserName.Text);
                        bor.Visibility = Visibility.Collapsed;
                        txtErr.Visibility = Visibility.Collapsed;
                    });
                    dynamic data = JsonConvert.DeserializeObject(Result.data.ToString());
                    if (data != null)
                    {

                        CacheData.Ins.Token = data.token ?? "";
                        url = CacheData.Ins.JsonConfig.GetValue("取得用户信息_Get");
                        var result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
                        if (result != null && result.code == 0 && result.data != null)
                        {
                            CacheData.Ins._user_info = JsonConvert.DeserializeObject<user_info>(result.data.ToString());
                        }

                        RefreshDBData();
                    }
                }
                else
                {
                    ShowErrMsg(Result.code.ToString());
                }
                IsLoging = false;
            }
            catch (Exception ex)
            {
                Log.Error("Login", ex);
            }
        }
        private void ShowErrMsg(string code)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtErr.Visibility = Visibility.Visible;
                msgQueue.Enqueue("请求失败：" + code);
            });
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(() =>
                {
                    txtErr.Visibility = Visibility.Collapsed;
                });
            });
        }
        private void UserNameCache(string userName)
        {
            try
            {
                string liststr = CacheData.Ins.JsonUserCache["UserNameCache"];
                if (liststr.Length > 0)
                {
                    var list = liststr.Split(',').ToList();
                    if (list.Contains(userName))
                    {
                        list.Remove(userName);
                    }
                    list.Insert(0, userName);
                    while (list.Count > 5)
                    {
                        list.RemoveAt(list.Count - 1);
                    }
                    liststr = string.Join(",", list.ToArray());
                    CacheData.Ins.JsonUserCache.SetValue("UserNameCache", liststr);
                }
                else
                {
                    CacheData.Ins.JsonUserCache.SetValue("UserNameCache", userName);
                }
            }
            catch (Exception ex)
            {
                Log.Error("UserNameCache", ex);
            }
        }
        public async Task<HttpResultBase> cardLogin(string cardNo)
        {
            var data = new { identity = cardNo, type = 0 };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig["刷卡登录_Post"];
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            return Result;

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

            Logout();
        }
        private void Logout()
        {
            try
            {
                bor.Visibility = Visibility.Visible;
                CacheData.Ins.Token = "";
                txtPassword.Password = "";
                txtUserName.Items.Clear();
                IsLoging = false;
                string liststr = CacheData.Ins.JsonConfig["UserNameCache"];
                if (liststr.Length > 0)
                {
                    string[] list = liststr.Split(',');
                    foreach (var item in list)
                    {
                        txtUserName.Items.Add(item);
                        txtUserName.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("btnLogout_Click", ex);
            }
        }
        public async void MessageTips(string message)
        {
            var sampleMessageDialog = new SampleMessageDialog
            {
                Msg = message
            };

            await DialogHost.Show(sampleMessageDialog, "RootDialog");

        }
        private async void RefreshDBData()
        {
            CacheData.Ins.Stocks = await trialBusiness.GetStockList();

            await trialBusiness.GeOrgan();

            acceptView.Organ = CacheData.Ins.Organ;
        }
        private void borMenu_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                StackPanel stk;
                if (sender is Border contro)
                {
                    SolidColorBrush brush = this.FindResource("PrimaryHueMidBrush") as SolidColorBrush;
                    for (int i = 0; i < menus.Count; i++)
                    {
                        if (menus[i] == contro)
                            continue;
                        stk = menus[i].Child as StackPanel;
                        foreach (var item in stk.Children)
                        {
                            if (item is Image img)
                            {
                                img.Source = new BitmapImage(new Uri($"/Images/{img.Name.Substring(3)}.png", UriKind.Relative));
                            }
                            if (item is TextBlock block)
                            {
                                block.Foreground = CacheData.Ins.ForegroundSolid;
                            }
                        }
                        menus[i].Background = new SolidColorBrush(Colors.White);

                    }
                    stk = contro.Child as StackPanel;
                    foreach (var item in stk.Children)
                    {
                        if (item is Image img)
                        {
                            img.Source = new BitmapImage(new Uri($"/Images/{img.Name.Substring(3)}_C.png", UriKind.Relative));
                        }
                        if (item is TextBlock block)
                        {
                            block.Foreground = new SolidColorBrush(Colors.White);
                        }
                    }
                    contro.Background = brush;
                    if (contro.Tag is string index)
                    {
                        ItemListBox.SelectedIndex = int.Parse(index);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("borMenu_Click", ex);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (IsLoging) return;
                IsLoging = true;
                Login();
            }
        }
        #region Aforge

        #endregion
        //private FilterInfoCollection collection;

        //private VideoCaptureDevice visDevice;
        //VideoSourcePlayer Player = new VideoSourcePlayer();
        //private bool InitWebcam()
        //{
        //    collection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        //    if (collection.Count == 0)
        //    {
        //        MessageBox.Show("未检测到摄像头");
        //        return false;
        //    }
        //    visDevice = new VideoCaptureDevice(collection[0].MonikerString);

        //    return true;
        //}
        //private bool CloseWebcam()
        //{
        //    if (Player.IsRunning)
        //        Player.SignalToStop();
        //    Player.WaitForStop();
        //    return true;
        //}
        //private void StartCamera()
        //{
        //    CloseWebcam();
        //    Player.VideoSource = visDevice;
        //    Player.Start();
        //    Player.NewFrame += Player_NewFrame;
        //}

        private void Player_NewFrame(object sender, ref System.Drawing.Bitmap image)
        {

        }

        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {

            if (await ShowCheckWin("是否确定关闭"))
            {
                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process process in processList)
                {
                    if (process.ProcessName == "MIIOT.Guarder")
                    {
                        process.Kill();
                    }
                }
                this.Close();
            }
            return;
            //applyView.getSinglecodes("E0040150D3508E76");
            string temp = "{\"macNoList\":[\"testname\"],\"busType\": \"ACCEPT\",\"datalist\": [\"PO2020042300002\"]},\"BCMD\": \"test\"}";
            Clinet_MessageReceived(null, new WSMsgModel() { Data = temp });

            return;

            new TrialBusiness().RackList("WA2020042000018");
            //new TrialBusiness().PickList("WA2020041600031");

            this.Close();

            //acceptView.getData("PO2020041700015");

            //applyView.GetDetail("WA2020041700013");

            //applyBackView.getData("");

            //refundView.GetDetail("");

            //new TrialBusiness().manualRefund();
            //new TrialBusiness().getSinglecodes();

            //KeyHelper.OnKeyPress(0x31);
        }
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        LASTINPUTINFO lastInPut = new LASTINPUTINFO();

        private async void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (await ShowCheckWin("是否确定关闭"))
                {
                    System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                    foreach (System.Diagnostics.Process process in processList)
                    {
                        if (process.ProcessName == "MIIOT.Guarder")
                        {
                            process.Kill();
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {

            }

        }
    }

}
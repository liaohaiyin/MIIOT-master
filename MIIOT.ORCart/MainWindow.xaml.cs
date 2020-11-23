using MaterialDesignThemes.Wpf;
using MIIOT.Controls;
using MIIOT.Controls.CommonModule;
using MIIOT.Drivers;
using MIIOT.Drivers.SRFID;
using MIIOT.Model;
using MIIOT.ORCart.Common;
using MIIOT.ORCart.MainView;
using MIIOT.ORCart.MainView.Replenish;
using MIIOT.ORCart.View;
using MIIOT.Trical;
using MIIOT.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using YDHCG.Common;
using Newtonsoft.Json;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Controls.Dialogs;
using System.Runtime.InteropServices;
using MIIOT.ORCart.MainView.Dialogs;
using MIIOT.ORCart.DataSync;

namespace MIIOT.ORCart
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
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }
        LASTINPUTINFO lastInPut = new LASTINPUTINFO();
        private ObservableCollection<ContorllerItem> _items = new ObservableCollection<ContorllerItem>();

        public ObservableCollection<ContorllerItem> MenuItems
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }
        private string _preSurgery;

        public string PreSurgery
        {
            get { return _preSurgery; }
            set
            {
                _preSurgery = value;
                OnPropertyChanged("PreSurgery");
            }
        }

        List<Border> menus = new List<Border>();
        SolidColorBrush solid;
        SolidColorBrush ForegroundSolid;
        BardCodeHooK BarCode = new BardCodeHooK();
        public bool IsLogin { get; set; }
        public static Queue msgQueue = Queue.Synchronized(new Queue());

        AccountView accountView;
        ApplyView applyView;
        ReplenishMain relenishmentView;
        ReplenishedView replenishedView;
        RefundView refundView;
        StockQueryView stockQueryView;
        AbStocksView abStocksView;
        StockTackingView stockTackingView;
        UserMagrView userMagrView;
        OCRView oCRView;
        SysSettingView sysSetting;
        UserBindingView userBindingView;
        Dictionary<string, IMenuMsgSend> dicMenu = new Dictionary<string, IMenuMsgSend>();
        public LoginWindow LoginWindow;


        IDFaceUtil iDFaceUtil = new IDFaceUtil();
        public MainWindow()
        {
            InitializeComponent();


            this.DataContext = this;
            SystemSleep.PreventForCurrentThread();
            solid = FindResource("PrimaryHueMidBrush") as SolidColorBrush;
            ForegroundSolid = FindResource("PrimaryForegroudBrush") as SolidColorBrush;
            BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);

            string DBStr = JsonConfig["DBStr"];
            bool DBsucc = new DapperUtil(DBStr).TestConnected();//数据库连接
            if (DBsucc)
                tab.SelectedIndex = 1;
            else
                tab.SelectedIndex = 0;
            iDFaceUtil.OnImageBack += IDFaceUtil_OnImageBack;
            iDFaceUtil.OnCheckComplete += IDFaceUtil_OnCheckComplete;
            iDFaceUtil.OnMsgBack += IDFaceUtil_OnMsgBack;
            txtspdHost.Text = JsonConfig["Host"];
            string[] dbParas = DBStr.Split(';');
            foreach (var item in dbParas)
            {
                if (item.Contains("Data Source"))
                {
                    txtDBHost.Text = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
                if (item.Contains("Initial Catalog"))
                {
                    txtDBName.Text = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
                if (item.Contains("User ID"))
                {
                    txtDBuserName.Text = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
                if (item.Contains("Password"))
                {
                    txtDBPassword.Password = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
            }
            LoginViewinit();



            Task.Run(() =>
            {
                while (true)
                {
                    bool isconnect = ConnectDetection.isConnet("127.0.0.1");
                    Dispatcher.Invoke(() =>
                    {
                        if (isconnect)
                        {
                            Servicer.Background = solid;
                        }
                        else
                            Servicer.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xEA, 0x5a, 0x6b));//red
                    });
                    Thread.Sleep(10000);
                }
            });
            try
            {
                SRFIDUtil rFIDUtil = new SRFIDUtil(2);
                rFIDUtil.OnReturnManagerDataEventArgs += ReceiveRfidEventArgs;
                bool res = rFIDUtil.Open(JsonConfig["PlatformReaderPortName"], "");
                if (res)
                {
                    RFIDReader.Background = solid;
                    Log.Debug(JsonConfig["PlatformReaderPortName"] + "连接成功");
                }
                else
                {
                    RFIDReader.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xEA, 0x5a, 0x6b));//red

                }
            }
            catch (Exception ex)
            {
                Log.Error("RFID init", ex);
            }
            int LoginTimeOut = 20;
            int.TryParse(JsonConfig["LoginTimeOut"], out LoginTimeOut);
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
            Task.Run(() =>
            {

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
#if Release

            Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    CacheData.Ins.faceUtil = new BaiduIDFaceUtil();
                });
            });
#endif
        }



        public void Init()
        {
            PreSurgery = CacheData.Ins.PreReceive;
            CacheData.Ins.mainWindow = this;
            this.Loaded += MainWindow_Loaded;
            try
            {
                accountView = new AccountView();
                applyView = new ApplyView();
                relenishmentView = new ReplenishMain();
                replenishedView = new ReplenishedView();
                refundView = new RefundView();
                stockQueryView = new StockQueryView();
                abStocksView = new AbStocksView();
                stockTackingView = new StockTackingView();
                userMagrView = new UserMagrView(CacheData.Ins.JsonConfig["DBStr"]);
                oCRView = new OCRView();
                sysSetting = new SysSettingView();
                userBindingView = new UserBindingView();
            }
            catch (Exception ex)
            {
                Log.Error("InitView", ex);
            }
            if (!dicMenu.ContainsKey("关联手术"))
                dicMenu.Add("关联手术", accountView);
            if (!dicMenu.ContainsKey("申领耗材"))
                dicMenu.Add("申领耗材", applyView);
            if (!dicMenu.ContainsKey("补货"))
                dicMenu.Add("补货", relenishmentView);
            if (!dicMenu.ContainsKey("已补货"))
                dicMenu.Add("已补货", replenishedView);
            if (!dicMenu.ContainsKey("退库"))
                dicMenu.Add("退库", refundView);
            if (!dicMenu.ContainsKey("库存查询"))
                dicMenu.Add("库存查询", stockQueryView);
            if (!dicMenu.ContainsKey("异常库存"))
                dicMenu.Add("异常库存", abStocksView);
            if (!dicMenu.ContainsKey("库存盘点"))
                dicMenu.Add("库存盘点", stockTackingView);
            if (!dicMenu.ContainsKey("用户权限"))
                dicMenu.Add("用户权限", userMagrView);
            if (!dicMenu.ContainsKey("证照识别"))
                dicMenu.Add("证照识别", oCRView);
            if (!dicMenu.ContainsKey("基础设置"))
                dicMenu.Add("基础设置", sysSetting);
            if (!dicMenu.ContainsKey("用户设置"))
            {
                dicMenu.Add("用户设置", userBindingView);
                // systemData.SyncDataByEditTime();
            }

        }

        void BarCode_BarCodeEvent(BardCodeHooK.BarCodes barCode)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (ItemListBox.SelectedIndex == 1)
                {
                    applyView.BarcodeScan(barCode.BarCode);
                }
            });
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PreFetch.Visibility = Visibility.Collapsed;
            FindProcess.IsRun();
            FindProcess.CheckProcess("MIIOT.Guarder");
        }

        private void MainWindow_Initialized(object sender, EventArgs e)
        {

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
                                block.Foreground = ForegroundSolid;
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
        public void PageChange(int index = -1)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (index == -1)
                    ItemListBox.SelectedIndex = MenuItems.Count - 1;// index;
                else
                    ItemListBox.SelectedIndex = index;

            });
        }
        private async void btnClose_Click(object sender, RoutedEventArgs e)
        {
            await DoClose();
        }
        private async Task<bool> DoClose()
        {
            if (CacheData.Ins.isReplenishEdit)
            {
                MessageTips("请先确认本次补货数量");
                return false;
            }
            if (await ShowCheckWin("是否确定退出"))
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
                return true;
            }
            return false;
        }
        private async void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            await DoClose();
        }


#if Net
        DataSync.SPDSystemData systemData = new DataSync.SPDSystemData();
#endif


        private async void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CacheData.Ins.User = new sys_user();
                txtBarcode.Text = "";
                txtPassword.Password = "";
                tab.Visibility = Visibility.Visible;
                picBox.Source = new BitmapImage(new Uri("Images/IDFace.png", UriKind.Relative));
                txtsuc.Text = "点击开始人脸认证登录";
            }
            catch (Exception ex)
            {

            }
        }
        private void Logout()
        {
            try
            {
                CacheData.Ins.User = new sys_user();
                txtBarcode.Text = "";
                tab.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {

            }
        }
        void ReceiveRfidEventArgs(object sender, RFIDReviceEventArgs msg)
        {
            Task.Run(() =>
            {
                Dispatcher.Invoke(() => { txtrfid.Text = msg.Msg; });
                Log.Debug("RFID:" + msg.Msg);
                Thread.Sleep(3000);
                Dispatcher.Invoke(() => { txtrfid.Text = ""; });
            });
            this.Dispatcher.Invoke(() =>
            {
                if (conControl.Content is IMenuMsgSend control)
                {
                    control.sendMsg(msg.Msg, "RFID");
                }

            });

        }
        public void Logined()
        {
            txtNickName.Text = "你好," + CacheData.Ins.User.display_name;
            List<sys_menu> menus = CacheData.Ins.CurrMenus = CacheData.Ins.dbUtil.Query<sys_menu>(@"SELECT d.id,d.menu_url, d.menu_name,d.menu_code,d.menu_parent_id from sys_role_menu c,sys_menu d 
                            where  c.menu_id=d.id and c.role_id=@id ORDER BY menu_code", new { id = CacheData.Ins.User.role });
            CreateTree(menus);
            RefreshMenu();
            IsLogin = true;
        }

        public async void Login()
        {
            try
            {
#if Net

                sys_user UserID = await NetLogin();
                if (UserID != null)
                {
                    UserNameCache(txtUserName.Text);
                    LoginComplete(UserID);
                    return;
                }

#endif
                var user = CacheData.Ins.dbUtil.Query<sys_user>("SELECT * from sys_user where user_code=@user_code and `password`=@password",
                    new { user_code = txtUserName.Text, password = txtPassword.Password.MD5Encrypt() });
                if (user != null && user.Count > 0)
                {
                    UserNameCache(txtUserName.Text);
                    LoginComplete(user[0]);
                    return;
                }
                else
                {
                    txtErr.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Login", ex);
            }
        }
        private void LoginComplete(sys_user user)
        {
            CacheData.Ins.User = user;
            txtErr.Visibility = Visibility.Collapsed;
            tab.Visibility = Visibility.Collapsed;
            Init();
            Logined();

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
        private async Task<sys_user> NetLogin()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["登录"];
                var data = new { username = txtUserName.Text, password = txtPassword.Password };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        spd_user User = JsonConvert.DeserializeObject<spd_user>(res.data.ToString());
                        sys_user dbUser = new sys_user()
                        {
                            id = User.accountid,
                            user_name = User.accountcode,
                            password = txtPassword.Password.MD5Encrypt(),
                            display_name = User.accountname,
                            user_code = User.accountcode,
                            user_department_id = User.departmentid,
                            user_source_id = User.accountid,
                            sotcks = string.Join(",", User.storehouseid),
                            role = User.roletype ?? 0
                        };
                        CacheData.Ins.dbUtil.Replace(dbUser);
                        return dbUser;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return null;
        }
        #region 菜单相关

        public void CreateTree(List<sys_menu> menus)
        {
            CacheData.Ins.RootMenu.ChildMenu.Clear();
            foreach (var item in menus)
            {
                if (item.menu_parent_id == 0)
                {
                    CacheData.Ins.RootMenu.ChildMenu.Add(item);
                    TreeGre(menus, item);
                }
            }
        }
        public void TreeGre(List<sys_menu> Menus, sys_menu menu)
        {
            var temp = Menus.FindAll(a => a.menu_parent_id == menu.id);
            if (temp == null)
            {
                return;
            }
            else
            {
                menu.ChildMenu.AddRange(temp);
                foreach (var item in temp)
                {
                    TreeGre(Menus, item);
                }

            }

        }
        private void AyncMenu(sys_menu Menu, ContorllerItem MenuItem)
        {
            foreach (var item in Menu.ChildMenu)
            {
                IMenuMsgSend msgSend;
                if (!dicMenu.ContainsKey(item.menu_name))
                    msgSend = dicMenu[Menu.menu_name];
                else
                    msgSend = dicMenu[item.menu_name];
                ContorllerItem contorllerItem = new ContorllerItem(item.menu_name, msgSend);
                contorllerItem.SysMenu = item;
                MenuItem.ChildItems.Add(contorllerItem);
                if (item.ChildMenu.Count > 0)
                    AyncMenu(item, contorllerItem);
            }
        }
        public void RefreshMenu()
        {
            try
            {
                MenuItems.Clear();
                Menu.Children.Clear();
                ContorllerItem contorllerItem = new ContorllerItem("", null);
                AyncMenu(CacheData.Ins.RootMenu, contorllerItem);
                MenuControl firstMenu = new MenuControl();
                for (int i = 0; i < contorllerItem.ChildItems.Count; i++)
                {
                    MenuItems.Add(contorllerItem.ChildItems[i]);
                    MenuControl menu = new MenuControl();
                    menu.Mode = 1;
                    //menu.InitImg("image/Accept.png");
                    menu.InitImg(contorllerItem.ChildItems[i].SysMenu.menu_url);
                    menu.Menu = contorllerItem.ChildItems[i].SysMenu;
                    menu.SelectedPage = contorllerItem.ChildItems[i].MenuMsgSend;
                    menu.OnSelected += Menu_OnSelected;
                    menu.MenuText = contorllerItem.ChildItems[i].SysMenu.menu_name;
                    Menu.Children.Add(menu);
                    if (i == 0)
                    {
                        menu.IsCheck = true;
                        conControl.Content = menu.SelectedPage;
                        firstMenu = menu;
                        CacheData.Ins.SelectedMenu = menu.MenuText;
                        if (menu.MenuText == "关联手术")
                            ChildMenuHide(true);
                        else

                            ChildMenuHide(false);
                    }
                    CreateChildMenu(menu, contorllerItem.ChildItems[i]);

                }

                secondMenu.Children.Clear();
                foreach (var item in firstMenu.ChildrenMenu)
                {
                    secondMenu.Children.Add(item);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Mainwindows Menu_OnSelected", ex);
            }
        }
        private List<MenuControl> CreateChildMenu(MenuControl Pmenu, object obj)
        {
            List<MenuControl> Menus = new List<MenuControl>();
            if (obj is ContorllerItem contorllerItem)
            {
                for (int j = 0; j < contorllerItem.ChildItems.Count; j++)
                {
                    MenuControl menu = new MenuControl()
                    {
                        Mode = 2,
                        Menu = contorllerItem.ChildItems[j].SysMenu,
                        MenuText = contorllerItem.ChildItems[j].SysMenu.menu_name
                    };
                    menu.OnSelected += Menu_OnSelected1;
                    if (j == 0)
                    {
                        menu.IsCheck = true;
                        menu.SelectedPage = contorllerItem.MenuMsgSend;
                        menu.SelectedPage.MenuSelected(menu.MenuText);
                    }
                    else
                    {
                        menu.SelectedPage = contorllerItem.ChildItems[j].MenuMsgSend;
                    }
                    Menus.Add(menu);
                }
                Pmenu.ChildrenMenu = Menus;

            }
            return Menus;
        }
        private void Menu_OnSelected1(object sender, string Text)
        {
            if (CacheData.Ins.isReplenishEdit)
            {
                CacheData.Ins.mainWindow.MessageTips("请先确认本次补货数量");
                return;
            }
            if (sender is MenuControl menu)
            {
                conControl.Content = menu.SelectedPage;
                foreach (var item in secondMenu.Children)
                {
                    if (item is MenuControl menus)
                    {
                        if (menus == menu)
                        {
                            CacheData.Ins.SelectedMenu = menu.MenuText;
                            menus.IsCheck = true;
                            menu.SelectedPage.MenuSelected(menus.MenuText);
                        }
                        else
                            menus.IsCheck = false;
                    }
                }
            }
            txtBarcode.Focus();
        }
        private void Menu_OnSelected(object sender, string Text)
        {
            try
            {
                if (PreFetch.Visibility == Visibility.Collapsed)
                    secondMenu.Visibility = Visibility.Visible;

                if (CacheData.Ins.isReplenishEdit)
                {
                    MessageTips("请先确认本次补货数量");
                    return;
                }
                for (int i = 0; i < Menu.Children.Count; i++)
                {
                    if (Menu.Children[i] is MenuControl menu)
                    {
                        if (sender is MenuControl SelectMenu)
                        {
                            if (menu == SelectMenu)
                            {
                                string SelectedMenu = MenuTreeMagr.GetMenuName(menu.Menu);
                                CacheData.Ins.SelectedMenu = SelectedMenu;
                                secondMenu.Children.Clear();
                                foreach (var item in menu.ChildrenMenu)
                                {
                                    secondMenu.Children.Add(item);
                                    if (item.IsCheck)
                                    {
                                        conControl.Content = item.SelectedPage;
                                    }
                                }
                                if (menu.ChildrenMenu.Count == 0)
                                    conControl.Content = SelectMenu.SelectedPage;
                                menu.IsCheck = true;
                                if (menu.MenuText == "关联手术")
                                    if (secondMenu.Visibility == Visibility.Collapsed)
                                        PreFetch.Visibility = Visibility.Visible;
                                    else
                                        PreFetch.Visibility = Visibility.Collapsed;
                                else
                                    ChildMenuHide(false);
                            }
                            else
                            {
                                menu.IsCheck = false;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Mainwindows Menu_OnSelected", ex);
            }
            txtBarcode.Focus();
        }
        public void ChildMenuHide(bool Hide)
        {
            if (Hide)
            {
                secondMenu.Visibility = Visibility.Collapsed;
                PreFetch.Visibility = Visibility.Visible;
            }
            else
            {
                secondMenu.Visibility = Visibility.Visible;
                PreFetch.Visibility = Visibility.Collapsed;

            }
        }

        #endregion

        public void MessageTips(string message)
        {
            this.Dispatcher.Invoke(async () =>
            {
                var sampleMessageDialog = new InfoView
                {
                    InfoText = message
                };
                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            });
        }
        public static async Task<bool> ShowCheckWin(string Msg)
        {
            var sampleMessageDialog = new YesOrNoView
            {
                InfoText = Msg
            };

            var CanClose = await DialogHost.Show(sampleMessageDialog, "RootDialog");
            return (bool)CanClose;
        }
        private void ItemListBoxSelecte_changed(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //if (IsLoging) return;
                //IsLoging = true;
                Login();
            }
        }


        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {

           // new SPDSystemData().GetPreSurgeryReceive(DateTime.Now.AddDays(-90),DateTime.Now);

          //await  new SPDSystemData().GetFollowInfo();

            //string jsonstr = "{\"data\":{\"log_id\":\"1601974216\",\"user_id_list\":[{\"user_id\":\"a13\"},{\"user_id\":\"a14\"}]},\"errno\":0,\"msg\":\"success\"}";
            //FaceListData res = JsonConvert.DeserializeObject<FaceListData>(jsonstr);
            //IDFaceView scaner = new IDFaceView();

            //var result = DialogHost.Show(scaner, "RootDialog", delegate (object senders, DialogOpenedEventArgs args)
            //{
            //    //Task.Run(() =>
            //    //{
            //    //    //Thread.Sleep(2000);
            //    //    this.Dispatcher.Invoke(() =>
            //    //    {
            //    //        //args.Session.Close(false);
            //    //    });
            //    //});
            //});
            //return;
            //if (IsLoging) return;
            //IsLoging = true;
            Login();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (conControl.Content is IMenuMsgSend control)
                {
                    control.sendMsg(txtBarcode.Text);
                }

                txtBarcode.SelectAll();
            }
        }

        JsonConfigHelper JsonUserCache = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\userCache.json");
        JsonConfigHelper JsonConfig = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\config.json");
        private void btnTestConnected_Click(object sender, RoutedEventArgs e)
        {
            string DBstr = $"Data Source={txtDBHost.Text};Initial Catalog={txtDBName.Text};Persist Security Info=True;User ID={txtDBuserName.Text};Password={txtDBPassword.Password}";
            JsonConfig.Update("DBStr", DBstr);
            JsonConfig.Update("Host", txtspdHost.Text);

            string dbstr = JsonConfig["DBStr"];
            bool DBsucc = new DapperUtil(dbstr).TestConnected();//数据库连接
            if (DBsucc)
            {
                ShowTip("数据库连接成功");

                tab.SelectedIndex = 1;
            }
            else
            {
                ShowTip("数据库连接失败，请检查参数");
                return;
            }
        }
        private void ShowTip(string text)
        {
            txtmsg.Text = text;
            txtmsg.Visibility = Visibility.Visible;
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(() =>
                {
                    txtmsg.Text = "";
                    txtmsg.Visibility = Visibility.Collapsed;
                });
            });
        }
        public void LoginViewinit()
        {
            //   txtPassword.Password = "";
            txtUserName.Items.Clear();
            string liststr = JsonUserCache["UserNameCache"];
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

        private void btnSetting_Click(object sender, MouseButtonEventArgs e)
        {
            tab.SelectedIndex = 0;
        }


        private void btnloginTypeChange_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton txt)
            {
                if (txt.Tag.ToString() == "0")
                {
                    iDFaceUtil.Close();
                    tabLoginType.SelectedIndex = 0;
                }
                else
                {
                    picBox.Source = null;
                    tabLoginType.SelectedIndex = 1;
                    iDFaceUtil.Check();
                }
            }
        }

        private void IDFaceUtil_OnImageBack(BitmapImage image)
        {
            this.Dispatcher.Invoke(() =>
            {
                picBox.Source = image;
            });
        }
        private void IDFaceUtil_OnMsgBack(string msg)
        {
            this.Dispatcher.Invoke(() => { txtsuc.Text = msg; });
        }

        private void IDFaceUtil_OnCheckComplete(sys_face face)
        {
            this.Dispatcher.Invoke(() =>
            {
                picBox.Source = null;
                var user = CacheData.Ins.dbUtil.Query<sys_user>("SELECT * from sys_user where id=@id",
           new { id = face.user_id });
                if (user != null && user.Count > 0)
                    LoginComplete(user[0]);
            });
        }

        private void btnFace_Click(object sender, MouseButtonEventArgs e)
        {
            iDFaceUtil.Check();
        }
    }
    public enum EnumNotiType
    {
        Info,
        Warn,
        Error,
        Success
    }
}

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Controls;
using MIIOT.Controls.Dialogs;
using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Model.ORCart.SPDPara;
using MIIOT.ORCart.Common;
using MIIOT.ORCart.Controls;
using MIIOT.ORCart.MainView.Dialogs;
using MIIOT.ORCart.View;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Recognition;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.ORCart.MainView
{
    /// <summary>
    /// AccountView.xaml 的交互逻辑
    /// </summary>
    public partial class AccountView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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


        private ObservableCollection<pub_room> _SurgeryRooms = new ObservableCollection<pub_room>();

        public ObservableCollection<pub_room> SurgeryRooms
        {
            get { return _SurgeryRooms; }
            set
            {
                _SurgeryRooms = value;
                OnPropertyChanged("SurgeryRooms");
            }
        }
        private bool _isClear;

        public bool IsClear
        {
            get { return _isClear; }
            set
            {
                _isClear = value;
                OnPropertyChanged("IsClear");
            }
        }

        private bool _Follow;

        public bool Follow
        {
            get { return _Follow; }
            set
            {
                _Follow = value;
                OnPropertyChanged("Follow");
            }
        }
        private bool _isAccount;

        public bool IsAccount
        {
            get { return _isAccount; }
            set
            {
                _isAccount = value;
                OnPropertyChanged("IsAccount");
            }
        }

        private pub_room _selectSurgeryRoom;

        public pub_room SelectSurgeryRoom
        {
            get { return _selectSurgeryRoom; }
            set
            {
                if (value != null)
                {
                    try
                    {
                        value.pub_Surgeries.Clear();
                        var surgerys = CacheData.Ins.dbUtil.Query<spd_surgeryinfo>("select * from  spd_surgeryinfo where origin_id=@origin_id ", new { origin_id = int.Parse(value.room_code) });
                        value.pub_Surgeries.AddRange(surgerys);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("SelectSurgeryRoom set", ex);
                    }
                }
                _selectSurgeryRoom = value;
                OnPropertyChanged("SelectSurgeryRoom");
            }
        }


        private spd_surgeryinfo _selectSurgeryInfo;

        public spd_surgeryinfo SelectPubSurgery
        {
            get { return _selectSurgeryInfo; }
            set
            {
                _selectSurgeryInfo = value;
                OnPropertyChanged("SelectPubSurgery");
            }
        }
        private List<cart_clear> _ClearList = new List<cart_clear>();

        public List<cart_clear> ClearList
        {
            get { return _ClearList; }
            set
            {
                _ClearList = value;
                OnPropertyChanged("ClearList");
            }
        }

        StockGoods StockGoods = new StockGoods();
        public AccountView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += AccountView_Loaded;
            StockGoods.OnBackSelect += StockGoods_OnBackSelect;
            StockGoods.OnClose += StockGoods_OnClose;
            borRight.Child = StockGoods;

        }

        private void AccountView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                StockGoods.Init(1);
                if (tab.SelectedIndex == 0)
                    CacheData.Ins.mainWindow.ChildMenuHide(true);
                else
                {
                    CacheData.Ins.mainWindow.ChildMenuHide(false);
                    Follow = false;
                }

                if (SurgeryRooms.Count == 0)
                {
                    var temo = CacheData.Ins.dbUtil.Query<pub_room>("select * from pub_room where status=1", null);
                    SurgeryRooms.AddRange(temo);
                }

            }
            catch (Exception ex)
            {
                Log.Error("AccountView_Loaded", ex);
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            tab.SelectedIndex = 0;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StockGoods.load();
            DtlTag.IsChecked = true;
            return;

        }
        //load
        private void surgery_DoSelect(spd_surgeryinfo pub_Surgery, int ClearStatus = 0)
        {
            try
            {
                tab.SelectedIndex = 1;
                ClearList.Clear();
                SelectPubSurgery = pub_Surgery;
                switch (CacheData.Ins.SelectedMenu)
                {
                    case "手术清台":
                        InitData(pub_Surgery, 0);
                        break;
                    case "跟台清台":
                        ClearList = CacheData.Ins.dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=0 and follow_tag = 1", new { origin_id = SelectPubSurgery.surgeryid });
                        break;
                    case "手术记账":
                        InitData(pub_Surgery, 2);
                        break;
                    default:
                        InitData(pub_Surgery, 0);
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error("SelectPubSurgery set", ex);
            }
        }
        private void InitData(spd_surgeryinfo pub_Surgery, int page)
        {
            ClearList = CacheData.Ins.dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=@status and follow_tag = 0", new { status = page == 0 ? 0 : 1, origin_id = pub_Surgery.surgeryid });
            if (page == 2)//记账标识
                ClearList.ForEach(a => a.IsHis = true);
            var temp = CacheData.Ins.dbUtil.Query<cart_clear>(@"SELECT  c.ischarge pay_flag,c.recyclableflag,c.singleflag, c.price, b.receivedtlid receiveddtl_id, a.sourceno,	b.goodsqty qty,	b.singlecode single_code,b.receivedtlstatus, c.goods_id,c.goods_no,
c.goods_name,c.goods_spec,c.goods_type,c.factory_name,c.unit FROM	spd_receiveinfo a
INNER JOIN spd_receiveinfodtl b ON a.sourceno = b.sourceno INNER JOIN cart_goods c ON b.goodsid = c.goods_id WHERE b.receivedtlstatus=@status AND	a.sourceno = @sourceno",
new { status = page == 2 ? 2 : 1, sourceno = pub_Surgery.surgeryno });

            List<cart_clear> NewReceive = new List<cart_clear>();
            foreach (var item in temp)
            {
                var newrecord = ClearList.FindAll(a => a.goods_id == item.goods_id && a.recevie_tag);
                if (newrecord != null && newrecord.Count > 0)
                {
                    continue;
                }
                cart_clear clear = new cart_clear()
                {
                    id = CacheData.Ins.SnowId.nextId(),
                    receiveddtl_id = item.receiveddtl_id,
                    origin_id = pub_Surgery.surgeryid,
                    goods_id = item.goods_id,
                    goods_no = item.goods_no,
                    goods_name = item.goods_name,
                    goods_spec = item.goods_spec,
                    goods_type = item.goods_type,
                    factory_name = item.factory_name,
                    recevie_tag = true,
                    unit = item.unit,
                    pay_flag = item.pay_flag,
                    single_code = item.single_code,
                    qty = item.qty,
                    price = item.price,
                    ExPrice = item.price,
                };
                if (page == 2)//记账标识
                {
                    clear.IsHis = true;
                    clear.status = 1;
                }
                ClearList.Add(clear);
                NewReceive.Add(clear);
            }
            if (NewReceive.Count > 0)
            {
                int i = CacheData.Ins.dbUtil.Insert<cart_clear>(NewReceive);
            }
        }
        //load
        public void MenuSelected(string menuName)
        {
            if (CacheData.Ins.SelectedMenu == "关联手术")
            {
                tab.SelectedIndex = 0;
                return;
            }

            sign.Visibility = Visibility.Collapsed;
            tab.SelectedIndex = 1;
            if (SelectPubSurgery == null) return;
            Follow = false; IsAccount = false;
            switch (CacheData.Ins.SelectedMenu)
            {

                case "手术清台":
                    IsClear = true;
                    Follow = false;
                    IsAccount = false;
                    surgery_DoSelect(SelectPubSurgery, 1);
                    //ClearList = dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=0 and follow_tag = 0", new { origin_id = SelectPubSurgery.surgeryid });
                    break;
                case "跟台清台":
                    IsClear = false;
                    Follow = true;
                    IsAccount = false;
                    ClearList = CacheData.Ins.dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=0 and follow_tag = 1", new { origin_id = SelectPubSurgery.surgeryid });
                    break;
                case "手术记账":
                    IsClear = false;
                    Follow = false;
                    IsAccount = true;

                    surgery_DoSelect(SelectPubSurgery, 2);
                    //ClearList = dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status =1 ", new { origin_id = SelectPubSurgery.surgeryid });
                    break;
                default:
                    break;
            }
            RefreshClears(SelectPubSurgery);
        }

        //刷新列表
        private void RefreshClears(spd_surgeryinfo surgery)
        {
            try
            {
                DateTime stiem = DateTime.Now;
                int count = signs.Children.Count;
                for (int i = 1; i < count; i++)
                {
                    signs.Children.RemoveAt(1);
                }
                Log.Debug(" signs.Children.RemoveAt：" + (DateTime.Now - stiem).TotalMilliseconds);
                surgery.ClearGroups.Clear();
                stkClear.Children.Clear();
                var groups = ClearList.GroupBy(a => new { a.goods_type });
                foreach (var item in groups)
                {
                    string groupName = ((GoodsTypeEnum)item.Key.goods_type).GetEnumDescription();
                    ClearGroup group = new ClearGroup() { groupName = groupName };
                    group.Clears.AddRange(item.ToList());
                    surgery.ClearGroups.Add(group);
                    GridControl grid = new GridControl();
                    grid.OnDelete += Grid_OnDelete;
                    grid.OnItemChecked += Grid_OnItemChecked;
                    grid.ClearList = ClearList;
                    grid.GroupName = groupName;
                    grid.Init(group.Clears);
                    stkClear.Children.Add(grid);
                    if (item.Key.goods_type == 2)
                    {
                        foreach (var clear in item.ToList())
                        {
                            string xaml = System.Windows.Markup.XamlWriter.Save(signgrid);
                            StackPanel stp = System.Windows.Markup.XamlReader.Parse(xaml) as StackPanel;
                            foreach (var con in stp.Children)
                            {
                                if (con is Border bor)
                                {
                                    if (bor.Child is TextBlock TB)
                                    {
                                        TB.FontSize = 15;
                                        TB.TextWrapping = TextWrapping.Wrap;
                                        switch (TB.Name)
                                        {
                                            case "txtname":
                                                TB.Text = clear.goods_name;
                                                break;
                                            case "txtfactory":
                                                TB.Text = clear.factory_name;
                                                break;
                                            case "txtspec":
                                                TB.Text = clear.goods_spec;
                                                break;
                                            case "txtprice":
                                                TB.Text = clear.price.ToString();
                                                break;
                                            case "txtqty":
                                                TB.Text = clear.qty.ToString();
                                                break;
                                            case "txtbatch":
                                                TB.Text = clear.remark;
                                                break;
                                            case "txtcode":
                                                TB.Text = clear.creater;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                            }
                            signs.Children.Add(stp);
                        }
                    }
                }
                NullTag.Visibility = ClearList.Count > 0 ? Visibility.Collapsed : Visibility.Visible;


            }
            catch (Exception ex)
            {
                Log.Error("RefreshClears", ex);
            }
        }
        //列表选中
        private void Grid_OnItemChecked(cart_clear clear)
        {
            foreach (var item in ClearList)
            {
                if (item == clear)
                {
                    item.Selected = clear.Selected;
                    break;
                }
            }
        }
        //删除
        private void Grid_OnDelete(GridControl sender, cart_clear clear)
        {
            clear.status = 4;
            var suc = CacheData.Ins.dbUtil.Update(clear);
            if (suc > 0)
            {
                sender.ClearList.Remove(clear);
                sender.Clears.Remove(clear);
                ClearList.Remove(clear);
                var temo = ClearList.FirstOrDefault(a => a.goods_type == clear.goods_type && a.recevie_tag == clear.recevie_tag);
                if (temo == null)
                    stkClear.Children.Remove(sender);
            }
        }
        //新增选择
        private async void StockGoods_OnBackSelect(object sender, IList<cart_goods> goods)
        {
            try
            {
                if (SelectPubSurgery == null) return;
                foreach (var item in goods)
                {
                    var clear = ClearList.FirstOrDefault(a => a.goods_no == item.goods_no && a.recevie_tag == false);

                    if (clear == null)
                    {
                        CacheData.Ins.mainWindow.PreSurgery = SelectPubSurgery.opsubtitle + "   " + SelectPubSurgery.customname;
                        if (item.goods_type == 3)
                            item.single_code = "";
                        cart_clear NewClear = new cart_clear()
                        {
                            id = CacheData.Ins.SnowId.nextId(),

                            origin_id = SelectPubSurgery.surgeryid,
                            goods_id = item.goods_id,
                            goods_no = item.goods_no,
                            goods_name = item.goods_name,
                            goods_spec = item.goods_spec,
                            goods_type = item.goods_type,
                            factory_name = item.factory_name,
                            plot_id = item.plot_id,
                            plot_no = item.plot_no,
                            single_code = item.single_code,
                            unit = item.unit,
                            price = item.price,
                            qty = 1,
                            pay_flag = item.ischarge == 1,
                            IsNew = true,
                            Selected = true
                        };

                        if (NewClear.pay_flag == false)
                        {
                            NewClear.ExPrice = 0;
                            NewClear.ExtotalPrice = 0;
                        }
                        if (item.goods_type == 1)
                        {
                            NewClear.price = 0;
                        }
                        if (((GoodsTypeEnum)item.goods_type) == GoodsTypeEnum.LOWFREE)
                        {
                            GoodsScan scaner = new GoodsScan();
                            scaner.goods = goods[0];
                            scaner.goods.qty = 0;
                            scaner.ShowType = 4;
                            scaner.IsMul = goods.Count > 1 ? Visibility.Visible : Visibility.Collapsed;
                            var temo = await DialogHost.Show(scaner, "RootDialog");
                            if ((bool)temo)
                            {
                                NewClear.qty = scaner.goods.qty;
                            }
                            else
                                return;
                        }
                        int count = CacheData.Ins.dbUtil.Insert<cart_clear>(NewClear);
                        ClearList.Add(NewClear);
                    }
                    else
                        CacheData.Ins.mainWindow.MessageTips($"[{item.goods_name}]已在列表中请勿重复添加");
                }
                if (goods.Count > 0)
                    RefreshClears(SelectPubSurgery);
                DtlTag.IsChecked = false;
            }
            catch (Exception ex)
            {
                Log.Error("StockGoods_OnBackSelect", ex);
            }
        }
        //刷卡
        public async void sendMsg(string code, string MsgType)
        {
            if (CacheData.Ins.SelectedMenu == "手术记账")
            {
                foreach (var item in stkClear.Children)
                {
                    if (item is GridControl grid)
                    {
                        grid.GridFilter(code);
                    }
                }
                return;
            }
            if (CacheData.Ins.SelectedMenu == "手术清台" || CacheData.Ins.SelectedMenu == "关联手术")
            {
                if (tab.SelectedIndex == 0)
                {
                    surgery.GridFilter(code);
                    return;
                }
                //var receive = dbUtil.Query<spd_receiveinfodtl>("SELECT * from spd_receiveinfodtl  a INNER JOIN spd_singlecode b on a.singlecode=b.singlecode WHERE b.rfid=@rfid", new { rfid = code });
                //if (receive != null && receive.Count > 0)
                //{
                //    CacheData.Ins.mainWindow.MessageTips($"单品码为[{receive[0].singlecode}]的商品已被术前领用，请勿重复添加");
                //    return;
                //}

                var goods = CacheData.Ins.dbUtil.Query<cart_goods>(@"SELECT  b.singlecode single_code,b.plotid plot_id,b.plotno plot_no,b.validto validity_date ,b.batchno  batch_no, a.* from cart_goods a left JOIN spd_singlecode b on a.goods_id=b.goodsid 
where b.rfid =@goods_no or a.goods_no=@goods_no or b.plotno=@goods_no  or b.batchno=@goods_no", new { goods_no = code });
                if (goods != null && goods.Count > 0)
                {
                    var clear = ClearList.FirstOrDefault(a => a.goods_no == goods[0].goods_no && a.recevie_tag == false);
                    if (!string.IsNullOrWhiteSpace(goods[0].single_code))
                        clear = ClearList.FirstOrDefault(a => a.single_code == goods[0].single_code);
                    if (clear == null)
                    {
                        cart_clear NewClear = new cart_clear()
                        {
                            id = CacheData.Ins.SnowId.nextId(),
                            origin_id = SelectPubSurgery.surgeryid,
                            goods_id = goods[0].goods_id,
                            goods_no = goods[0].goods_no,
                            goods_name = goods[0].goods_name,
                            goods_spec = goods[0].goods_spec,
                            goods_type = goods[0].goods_type,
                            factory_name = goods[0].factory_name,
                            plot_id = goods[0].plot_id,
                            plot_no = goods[0].plot_no,
                            single_code = goods[0].single_code,
                            unit = goods[0].unit,
                            price = goods[0].price,
                            qty = 1,
                            follow_tag = Follow,
                            pay_flag = goods[0].ischarge == 1,
                            IsNew = true,
                            Selected = true
                        };
                        if (((GoodsTypeEnum)goods[0].goods_type) != GoodsTypeEnum.HIGH)
                        {
                            GoodsScan scaner = new GoodsScan();
                            scaner.goods = goods[0];
                            scaner.ShowType = 4;
                            CacheData.Ins.mainWindow.PreSurgery = SelectPubSurgery.opsubtitle + "   " + SelectPubSurgery.customname;
                            var temo = await DialogHost.Show(scaner, "RootDialog");
                            if ((bool)temo)
                            {
                                NewClear.qty = scaner.goods.qty;
                            }
                            else
                                return;
                        }
                        int succ = CacheData.Ins.dbUtil.Insert(NewClear);
                        if (succ > 0)
                            ClearList.Add(NewClear);

                    }
                    else
                    {
                        if (goods[0].goods_type == 1 || goods[0].goods_type == 0)
                        {
                            clear.qty++;
                        }
                        else
                            CacheData.Ins.mainWindow.MessageTips($"[{goods[0].goods_name}]已在列表中请勿重复添加");
                    }
                }

                RefreshClears(SelectPubSurgery);
            }
        }
        private void StockGoods_OnClose()
        {
            DtlTag.IsChecked = false;
        }
        AccountUtil accountUtil = new AccountUtil();
        //记账
        private async void btnKeepAccount_click(object sender, RoutedEventArgs e)
        {

            var SelectClears = ClearList.FindAll(a => a.Selected);
            if (SelectClears.Count == 0)
            {
                CacheData.Ins.mainWindow.MessageTips($"未选中，无效操作");
                return;
            }
            foreach (var item in SelectClears)
            {
                if (item.qty == 0)
                {

                    CacheData.Ins.mainWindow.MessageTips($"[{item.goods_name}]数量为零");
                    return;
                }
            }

            List<cart_clear> clears = CacheData.Ins.dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status =1", new { origin_id = SelectPubSurgery.surgeryid });
            foreach (var item in clears)
            {
                var temp = SelectClears.FirstOrDefault(a => a.goods_type == 2 && a.single_code == item.single_code);
                if (temp != null)
                {
                    CacheData.Ins.mainWindow.MessageTips($"已记账,单品码为{item.single_code}的{item.goods_name}已被记账，请不要重复记账");
                    return;
                }
            }
#if Net

            string url = CacheData.Ins.JsonConfig["手术记账"];
            List<surgeryBillingPara> datas = new List<surgeryBillingPara>();
            foreach (var item in SelectClears)
            {
                surgeryBillingPara para = new surgeryBillingPara()
                {
                    storehouseId = CacheData.Ins.currStock.storehouseid,
                    recManId = CacheData.Ins.User.user_source_id,
                    recManNo = CacheData.Ins.User.user_code,
                    goodsName = item.goods_name,
                    goodsId = item.goods_id,
                    goodsNo = item.goods_no,
                    goodsQty = item.qty,
                    surgeryNo = SelectPubSurgery.surgeryno,
                    storehouseNo = CacheData.Ins.currStock.storehouseid,
                    plotId = item.plot_id,
                    plotNo = item.plot_no,
                    singleCode = item.single_code,
                    surgeryStatus = 2,
                    heelStageType = 0,
                    backFlag = 1,
                    categoryType = item.goods_type,
                    receiveDtlId = item.receiveddtl_id,
                    payFlag = item.pay_flag ? 1 : 0
                };
                datas.Add(para);
            }
            var jsondata = new { rows = datas };
            string dataJsonstr = JsonConvert.SerializeObject(jsondata);
            HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
            if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                if (res != null && res.code == 0)
                {
                    bool suc = accountUtil.KeepAccount(SelectClears, SelectPubSurgery);
                    if (suc)
                    {
                        CacheData.Ins.mainWindow.MessageTips("操作成功");
                        SelectClears.ForEach(a => ClearList.Remove(a));
                        RefreshClears(SelectPubSurgery);
                    }
                    else
                        CacheData.Ins.mainWindow.MessageTips("操作失败,请查看错误日志");
                }
                else
                    CacheData.Ins.mainWindow.MessageTips("请求错误：" + res.msg);

            }
            else
                CacheData.Ins.mainWindow.MessageTips("请求错误：" + result.StatusCode);


#else
            bool suc = accountUtil.KeepAccount(SelectClears, SelectPubSurgery);
            if (suc)
            {
                CacheData.Ins.mainWindow.MessageTips("操作成功");
                SelectClears.ForEach(a => ClearList.Remove(a));
                RefreshClears(SelectPubSurgery);
            }
            else
                CacheData.Ins.mainWindow.MessageTips("操作失败,请查看错误日志");
#endif
        }
        //销账
        private async void btnCancelAccount_Click(object sender, RoutedEventArgs e)
        {
            CancelAccountView view = new CancelAccountView();
            var temo = await DialogHost.Show(view, "RootDialog");
            return;

            var SelectClears = ClearList.FindAll(a => a.Selected);
            if (SelectClears.Count == 0)
            {
                CacheData.Ins.mainWindow.MessageTips($"未选中，无效操作");
                return;
            }
            var lowGoods = SelectClears.FindAll(a => a.recevie_tag == false && a.goods_type == 0 || a.goods_type == 1 || a.goods_type == 4);
            List<string> names = new List<string>();

            lowGoods.ForEach(a => { names.Add(a.goods_name); });
            if (lowGoods != null && lowGoods.Count > 0)
            {
                if (!await MainWindow.ShowCheckWin($"{string.Join(",", names)}已销账，是否归还实物"))
                {
                    lowGoods.ForEach(a => a.backFlag = false);
                }
            }
#if Net

            string url = CacheData.Ins.JsonConfig["手术记账"];
            List<surgeryBillingPara> datas = new List<surgeryBillingPara>();
            foreach (var item in SelectClears)
            {
                surgeryBillingPara para = new surgeryBillingPara()
                {
                    storehouseId = CacheData.Ins.currStock.storehouseid,
                    recManId = CacheData.Ins.User.user_source_id,
                    recManNo = CacheData.Ins.User.user_code,
                    goodsName = item.goods_name,
                    goodsId = item.goods_id,
                    goodsNo = item.goods_no,
                    goodsQty = item.qty,
                    surgeryNo = SelectPubSurgery.surgeryno,
                    storehouseNo = CacheData.Ins.currStock.storehouseid,
                    plotId = item.plot_id,
                    plotNo = item.plot_no,
                    singleCode = item.single_code,
                    surgeryStatus = 3,
                    heelStageType = 0,
                    categoryType = item.goods_type,
                    backFlag = item.backFlag ? 1 : 0,
                    receiveDtlId = item.receiveddtl_id
                };
                if (item.recevie_tag)
                    para.backFlag = 0;
                datas.Add(para);
            }
            var jsondata = new { rows = datas };
            string dataJsonstr = JsonConvert.SerializeObject(jsondata);
            HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
            if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                if (res != null && res.code == 0)
                {
                    bool suc = accountUtil.CancelAccount(ClearList, SelectPubSurgery);
                    if (suc)
                    {
                        CacheData.Ins.mainWindow.MessageTips("操作成功");
                        SelectClears.ForEach(a => ClearList.Remove(a));
                        RefreshClears(SelectPubSurgery);
                    }
                    else
                        CacheData.Ins.mainWindow.MessageTips("操作失败,请查看错误日志");
                }
                else
                    CacheData.Ins.mainWindow.MessageTips("请求错误：" + res.msg);
            }
            else
                CacheData.Ins.mainWindow.MessageTips("请求错误：" + result.StatusCode);

#else
            bool suc = accountUtil.CancelAccount(ClearList, SelectPubSurgery);
            if (suc)
            {
                CacheData.Ins.mainWindow.MessageTips("操作成功");
                SelectClears.ForEach(a => ClearList.Remove(a));
                RefreshClears(SelectPubSurgery);
            }
            else
                CacheData.Ins.mainWindow.MessageTips("操作失败,请查看错误日志");
#endif
        }


        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tab.SelectedIndex == 0)
                CacheData.Ins.mainWindow.ChildMenuHide(true);
            else
                CacheData.Ins.mainWindow.ChildMenuHide(false);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            CacheData.Ins.mainWindow.MessageTips("待开发");
            CacheData.Ins.mainWindow.MessageTips("待开发343534");
            CacheData.Ins.mainWindow.MessageTips("待开发1sdfgdgfd");
        }

        private void FollowAccountView_OnBackClick()
        {
            tab.SelectedIndex = 0;
        }

        private async void btnSign_Click(object sender, RoutedEventArgs e)
        {
            signcontent.Visibility = Visibility.Collapsed;
            sign.Visibility = Visibility.Visible;
            CameraSignView scaner = new CameraSignView();
            scaner.Open();
            scaner.OnBack += Scaner_OnBack;
            var temo = await DialogHost.Show(scaner, "RootDialog");
            signcontent.Visibility = Visibility.Visible;
        }

        private void Scaner_OnBack(CameraSignView scaner)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sign.Visibility = Visibility.Collapsed;
        }
    }
}

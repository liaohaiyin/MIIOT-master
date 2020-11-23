using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Model.ORCart.SPDPara;
using MIIOT.ORCart.Common;
using MIIOT.ORCart.MainView.Dialogs;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// ApplyView.xaml 的交互逻辑
    /// </summary>
    public partial class RefundView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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
        private ObservableCollection<cart_goods> _Refunds = new ObservableCollection<cart_goods>();

        public ObservableCollection<cart_goods> Refunds
        {
            get { return _Refunds; }
            set
            {
                _Refunds = value;
                OnPropertyChanged("Refunds");
            }
        }
        private ObservableCollection<spd_backreason> _Reasons = new ObservableCollection<spd_backreason>();

        public ObservableCollection<spd_backreason> Reasons
        {
            get { return _Reasons; }
            set
            {
                _Reasons = value;
                OnPropertyChanged("Reasons");
            }
        }
        private ObservableCollection<spd_backtargetstore> _BackStore = new ObservableCollection<spd_backtargetstore>();

        public ObservableCollection<spd_backtargetstore> BackStore
        {
            get { return _BackStore; }
            set
            {
                _BackStore = value;
                OnPropertyChanged("BackStore");
            }
        }
        private cart_goods _SelectGoods;

        public cart_goods SelectGoods
        {
            get { return _SelectGoods; }
            set
            {
                _SelectGoods = value;
                OnPropertyChanged("SelectGoods");
            }
        }

        public RefundView()
        {
            InitializeComponent();
            this.DataContext = this;
            var stores = dbUtil.Query<spd_backtargetstore>("SELECT * from spd_backtargetstore", null);
            txtstore.ItemsSource = stores;



            var reasons = dbUtil.Query<spd_backreason>("SELECT * from spd_backreason", null);
            Reasons.AddRange(reasons);
            this.Loaded += RefundView_Loaded;

        }
        private void StockGoods_OnBackSelect(object sender, IList<cart_goods> goods)
        {
            try
            {

                DtlTag.IsChecked = false;

                foreach (var cart_Goods in goods)
                {
                    cart_goods dtl = new cart_goods()
                    {
                        index = Refunds.Count + 1,
                        goods_id = cart_Goods.goods_id,
                        goods_no = cart_Goods.goods_no,
                        goods_name = cart_Goods.goods_name,
                        goods_spec = cart_Goods.goods_spec,
                        plot_no = cart_Goods.plot_no,
                        plot_id = cart_Goods.plot_id,
                        goods_type = cart_Goods.goods_type,

                        unit = cart_Goods.unit,
                        factory_name = cart_Goods.factory_name,
                        qty = 1,
                        IsNew = true,
                        Reasons = this.Reasons
                        // = ((pub_storehouse)txtstore.SelectedItem).storehouse_name
                    };
                    var app = Refunds.FirstOrDefault(a => a.goods_no == cart_Goods.goods_no);
                    if (app == null)
                        Refunds.Add(dtl);
                    else
                        app.qty++;
                }

            }
            catch (Exception ex)
            {
                Log.Error("StockGoods_OnBackSelect", ex);
            }
        }

        private void StockGoods_OnClose()
        {
            DtlTag.IsChecked = false;
        }
        private void RefundView_Loaded(object sender, RoutedEventArgs e)
        {

            StockGoods StockGoods = new StockGoods();
            StockGoods.OnBackSelect += StockGoods_OnBackSelect;
            StockGoods.OnClose += StockGoods_OnClose;
            borRight.Child = StockGoods;

            StockGoods.Init(3);
        }

        DapperUtil dbUtil = new DapperUtil(CacheData.Ins.JsonConfig["DBStr"]);
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
            }
        }

        private async void btnRefund_Click(object sender, RoutedEventArgs e)
        {
            if (Refunds.Count == 0) return;
            try
            {
                foreach (var item in Refunds)
                {
                    new StockUtil().Outstock(item.goods_id, item.qty, CacheData.Ins.currStock.storehouseid, "Refund");
                }
                Refunds.Clear();
                CacheData.Ins.mainWindow.MessageTips("退库成功");
            }
            catch (Exception ex)
            {
                CacheData.Ins.mainWindow.MessageTips("退库失败，请检查库存是否充足");
                Log.Error("", ex);
            }

            return;
#if Net
            try
            {
                string url = CacheData.Ins.JsonConfig["退库"];
                List<turnbackPara> datas = new List<turnbackPara>();
                foreach (var item in Refunds)
                {
                    turnbackPara para = new turnbackPara()
                    {
                        uOrganId = CacheData.Ins.currStock.uorganid,
                        operationType = "5",
                        goodsId = item.goods_id,
                        orgStorehouseId = CacheData.Ins.currStock.storehouseid,
                        destStorehouseId = ((spd_backtargetstore)txtstore.SelectedItem).deststorehouseid,
                        operateManId = CacheData.Ins.User.id,
                        backStoreTime = DateTime.Now.ToLongString(),
                        backStoreReason = 1,
                        plotId = item.plot_id,
                        plotNo = item.plot_no,
                        categoryType = item.goods_type.ToString(),
                        goodsQty = item.qty.ToString(),
                        locId = "1",
                        recManId = CacheData.Ins.User.id.ToString(),
                        recManNo = CacheData.Ins.User.user_code,
                        singleCode = "",
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
                        var temo = result.data.ToString();
                        List<spd_backreason> rfiddata = JsonConvert.DeserializeObject<List<spd_backreason>>(res.data.ToString());
                        int i = dbUtil.Replace<spd_backreason>(rfiddata);
                    }
                    else
                        CacheData.Ins.mainWindow.MessageTips("请求错误：" + res.msg);
                }
                else
                    CacheData.Ins.mainWindow.MessageTips("请求错误：" + result.StatusCode.GetEnumDescription());
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
#endif


            var Stocks = new StockUtil().GetAllStockGoods();
            List<DapperParams> UpdateStr = new List<DapperParams>();
            foreach (var item in Refunds)
            {
                int qty = 0;
                var temo = Stocks.FirstOrDefault(a => a.goods_id == item.goods_id);
                if (temo != null)
                {
                    qty = temo.qty - item.qty;
                    if (qty < 0)
                    {
                        CacheData.Ins.mainWindow.MessageTips("库存不足");
                        return;
                    }
                }
                else
                {
                    CacheData.Ins.mainWindow.MessageTips("非库存商品");
                    return;
                }
                UpdateStr.Add(new DapperParams($" UPDATE cart_stock set qty={qty} where goods_id='{item.goods_id}'; ", null));
                long No = CacheData.Ins.SnowId.nextId();
                cart_outstock outstock = new cart_outstock()
                {
                    cart_code = CacheData.Ins.CartNo,
                    goods_id = item.goods_id,
                    storehouse_name = ((spd_backtargetstore)txtstore.SelectedItem).deststorehousename,
                    outstock_qty = item.qty,
                    outstock_id = No,
                    outstock_no = No.ToString(),
                    creman_id = CacheData.Ins.User.id.ToString(),
                    creman_name = CacheData.Ins.User.user_name,
                    create_time = DateTime.Now

                };
                int res = dbUtil.Insert(outstock);
                if (res == 0)
                {
                    CacheData.Ins.mainWindow.MessageTips("退库失败");
                    return;
                }
            }
            bool succ = dbUtil.TransactionAction(UpdateStr);
            if (succ)
            {
                Refunds.Clear();

                CacheData.Ins.mainWindow.MessageTips("退库成功");
            }
        }

        public void sendMsg(string code, string MsgType)
        {
            this.Dispatcher.Invoke(async () =>
           {
               try
               {
                   Log.Debug(this.ToString() + code);
                   var temo = new StockUtil().GetStockGoodsByNo(code);
                   if (temo != null && temo.Count > 0)
                   {
                       if (temo[0].qty <= 0)
                       {

                           CacheData.Ins.mainWindow.MessageTips("没有库存");
                           return;
                       }
                       temo[0].Reasons = this.Reasons;
                       GoodsScan scaner = new GoodsScan();
                       scaner.goods = temo[0];
                       scaner.ShowType = 2;
                       var Result = await DialogHost.Show(scaner, "RootDialog");
                       if ((bool)Result == true)
                       {
                           temo[0].index = Refunds.Count + 1;
                           var refund = Refunds.FirstOrDefault(a => a.goods_id == temo[0].goods_id);
                           if (refund != null)
                           {
                               refund.qty += scaner.goods.qty;
                           }
                           else
                               Refunds.Add(temo[0]);
                       }


                   }
                   else
                   {
                       CacheData.Ins.mainWindow.MessageTips("当前商品非本库房商品，无法退库");
                       return;
                   }
               }
               catch (Exception ex)
               {
                   Log.Error("txtBarcode_KeyDown", ex);
               }
           });
        }
        public void MenuSelected(string menuName)
        {
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is cart_goods goods)
                {
                    Refunds.Remove(goods);
                }
            }
        }

        private void btnAddGoods_Click(object sender, RoutedEventArgs e)
        {
            DtlTag.IsChecked = true;
        }

        private async void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (txt.Tag is cart_goods goods)
                {
                    GoodsScan scaner = new GoodsScan();
                    scaner.goods = goods;
                    scaner.ShowType = 1;
                    DialogHost host = new DialogHost();

                    //var temo = await host.ShowDialog(scaner);
                    var temo = await DialogHost.Show(scaner, "RootDialog");
                    if ((bool)temo)
                    {
                        goods.qty = scaner.goods.qty;
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox com)
            {
                if (com.Tag is cart_goods item)
                {
                    if (com.SelectedItem is spd_backreason sItem)
                    {
                        item.ReasonCode = sItem.dictvalue;
                    }
                }
            }
        }

        private void grid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void grid_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                if (grid.Items != null)
                {
                    for (int i = 0; i < grid.Items.Count; i++)
                    {
                        try
                        {
                            DataGridRow row = grid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                            if (row != null)
                            {
                                row.Header = (i + 1).ToString();
                            }
                        }
                        catch { }
                    }
                }
            }
        }
    }
}

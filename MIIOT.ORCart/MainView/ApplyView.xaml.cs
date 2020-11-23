using MaterialDesignThemes.Wpf;
using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Model.ORCart.SPDPara;
using MIIOT.Model.Trical;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.ORCart.MainView
{
    /// <summary>
    /// ApplyView.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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
        private ObservableCollection<spd_replenishdtl> _ApplyViews = new ObservableCollection<spd_replenishdtl>();

        public ObservableCollection<spd_replenishdtl> ApplyViews
        {
            get { return _ApplyViews; }
            set
            {
                _ApplyViews = value;
                OnPropertyChanged("ApplyViews");
            }
        }
        private spd_replenishdtl _selectApply;

        public spd_replenishdtl selectApply
        {
            get { return _selectApply; }
            set
            {
                _selectApply = value;
                OnPropertyChanged("selectApply");
            }
        }

        StockGoods StockGoods = new StockGoods();

        public ApplyView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += ApplyView_Loaded;
            var stores =CacheData.Ins. dbUtil.Query<spd_stockhouse>("select * from spd_stockhouse where storehousestatus=1 and storehousetype=3", null);
            txtstore.ItemsSource = stores;
            StockGoods.OnBackSelect += StockGoods_OnBackSelect;
            StockGoods.OnClose += StockGoods_OnClose;
            borRight.Child = StockGoods;

        }

        private async void StockGoods_OnBackSelect(object sender, IList<cart_goods> goods)
        {
            try
            {
                if (goods.Count == 0) return;
                foreach (var item in goods)
                {
                    var app = ApplyViews.FirstOrDefault(a => a.goodsno == item.goods_no);
                    if (app != null)
                    {
                        CacheData.Ins.mainWindow.MessageTips($"[{item.goods_name}]已存在列表中");
                        return;
                    }
                }

                DtlTag.IsChecked = false;

                GoodsScan scaner = new GoodsScan();
                goods[0].qty = 0;
                scaner.goods = goods[0];
                scaner.ShowType = 5;
                scaner.IsMul = goods.Count > 1 ?Visibility.Visible:Visibility.Collapsed;
                DialogHost host = new DialogHost();
                var temo = await DialogHost.Show(scaner, "RootDialog");
                if ((bool)temo)
                {
                    foreach (var cart_Goods in goods)
                    {
                        spd_replenishdtl dtl = new spd_replenishdtl()
                        {
                            index = ApplyViews.Count + 1,
                            goodsid = cart_Goods.goods_id,
                            goodsno = cart_Goods.goods_no,
                            goodsname = cart_Goods.goods_name,
                            goodstype = cart_Goods.goods_spec,
                            goodsunitname = cart_Goods.unit,
                            factoryname = cart_Goods.factory_name,
                            new_replenish_qty = scaner.goods.qty,
                            IsNew = true,

                        };
                        var store= txtstore.SelectedItem;
                        if (store is spd_stockhouse stock)
                        {
                            dtl.out_storehouse_name = stock.storehousename;
                        }
                        
                            ApplyViews.Add(dtl);
                    }
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

        private void ApplyView_Loaded(object sender, RoutedEventArgs e)
        {
            StockGoods.Init(2);
        }
        public void BarcodeScan(string barcode)
        {
        }

        public async void sendMsg(string code,string MsgType)
        {
            try
            {
                List<cart_goods> list = new StockUtil().GetStrategyGoods();
                var cart_Goods = list.FirstOrDefault(a => a.goods_no == code);
                if (cart_Goods == null)
                {
                     CacheData.Ins.mainWindow.MessageTips("非库房策略商品，不能申领");
                    return;
                }
                //cart_Goods.goods_no = temp.goods_no;
                //cart_Goods.goods_name = temp.goods_name;
                //cart_Goods.goods_spec = temp.goods_spec;
                //cart_Goods.factory_name = temp.factory_name;
                //cart_Goods.validity_date = temp.validity_date;
                //cart_Goods.qty = temp.new_replenish_qty;
                //cart_Goods.need_qty = temp.need_replenish_qty;
                cart_Goods.qty = 0;
                GoodsScan scaner = new GoodsScan();
                scaner.goods = cart_Goods;
                scaner.ShowType = 5;
                DialogHost host = new DialogHost();
                var temo = await DialogHost.Show(scaner, "RootDialog");
                if ((bool)temo)
                {
                    var good = ApplyViews.FirstOrDefault(a=>a.goodsno==code);
                    if (good != null)
                    {
                        good.new_replenish_qty += cart_Goods.qty;
                        return;
                    }
                    spd_replenishdtl dtl = new spd_replenishdtl()
                    {
                        index = ApplyViews.Count + 1,
                        goodsid = cart_Goods.goods_id,
                        goodsno = cart_Goods.goods_no,
                        goodsname = cart_Goods.goods_name,
                        goodstype = cart_Goods.goods_spec,
                        goodsunitname = cart_Goods.unit,
                        factoryname = cart_Goods.factory_name,
                        new_replenish_qty = cart_Goods.qty,
                        IsNew = true,

                    };
                    ApplyViews.Add(dtl);

                }
            }
            catch (Exception ex)
            {
                Log.Error("BarcodeIn", ex);
            }

        }

        public void MenuSelected(string menuName)
        {
        }

        private void btnAddGoods_Click(object sender, RoutedEventArgs e)
        {
            DtlTag.IsChecked = true;
        }

        private async void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (ApplyViews.Count==0)
            {
                CacheData.Ins.mainWindow.MessageTips("无效操作");
                return;
            }
#if Net

            string url = CacheData.Ins.JsonConfig["申领"];
            List<applyOutStockPara> datas = new List<applyOutStockPara>();
            foreach (var item in ApplyViews)
            {
                if (item.new_replenish_qty == 0) continue;
                applyOutStockPara para = new applyOutStockPara()
                {
                    orgStorehouseId = txtstore.SelectedValue.ToString(),
                    destStorehouseId = CacheData.Ins.currStock.storehouseid,
                    categoryType = 2,
                    goodsId = item.goodsid,
                    goodsNo = item.goodsno,
                    goodsQty = item.new_replenish_qty,
                    operateTime = DateTime.Now.ToLongString(),
                    operateManId = CacheData.Ins.User.user_source_id,
                    operateManNo = CacheData.Ins.User.user_code,
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
                    CacheData.Ins.mainWindow.MessageTips("申领成功");
                    ApplyViews.Clear();
                }
                else
                    CacheData.Ins.mainWindow.MessageTips("请求错误：" + res.msg);
            }
            else
                CacheData.Ins.mainWindow.MessageTips("请求错误：" + result.StatusCode.GetEnumDescription());

#else
  CacheData.Ins.mainWindow.MessageTips("申领成功");
                    ApplyViews.Clear();

#endif
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyViews.Remove(selectApply);
        }

        private async void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is TextBox txt)
            {
                if (txt.Tag is spd_replenishdtl goods)
                {

                    cart_goods cart_Goods = new cart_goods()
                    {
                        goods_id = goods.goodsid,
                        goods_name = goods.goodsname,
                        goods_spec = goods.goodstype,
                        factory_name = goods.factoryname,
                        unit = goods.goodsunitname,
                        qty = goods.new_replenish_qty
                    };

                    GoodsScan scaner = new GoodsScan();
                    scaner.goods = cart_Goods;
                    scaner.ShowType = 5;
                    DialogHost host = new DialogHost();

                    //var temo = await host.ShowDialog(scaner);
                    var temo = await DialogHost.Show(scaner, "RootDialog");
                    if ((bool)temo)
                    {
                        goods.new_replenish_qty = scaner.goods.qty;
                    }
                }
            }
        }

        private void gridApply_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void gridApply_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            //gridApply_LoadingRow(sender, e);
            if (gridApply.Items != null)
            {
                for (int i = 0; i < gridApply.Items.Count; i++)
                {
                    try
                    {
                        DataGridRow row = gridApply.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
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

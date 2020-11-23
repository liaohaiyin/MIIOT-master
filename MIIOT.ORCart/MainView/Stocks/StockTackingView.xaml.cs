using Dragablz;
using MaterialDesignThemes.Wpf;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Model.ORCart.SPDPara;
using MIIOT.ORCart.DataSync;
using MIIOT.ORCart.MainView.Dialogs;
using MIIOT.Utility;
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
    /// StockTackingView.xaml 的交互逻辑
    /// </summary>
    public partial class StockTackingView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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
        private ObservableCollection<StockTack> _Stocks = new ObservableCollection<StockTack>();

        public ObservableCollection<StockTack> Stocks
        {
            get { return _Stocks; }
            set
            {
                _Stocks = value;
                OnPropertyChanged("Stocks");
            }
        }
        public StockTackingView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += StockTackingView_Loaded;
        }
        SPDSystemData sPDSystemData = new SPDSystemData();

        private  void StockTackingView_Loaded(object sender, RoutedEventArgs e)
        {

            InitStockData();
        }
        private async void InitStockData()
        {
            try
            {
                Stocks.Clear();
                var temp = new StockUtil().GetAllStock();
                List<spd_stock> spd_stocks = await sPDSystemData.GetStock(true);
                foreach (var item in temp)
                {
                    StockTack ST = new StockTack();
                    var spd = spd_stocks.FirstOrDefault(a => a.goodsid == item.goods_id);
                    if (spd != null)
                        ST.SpdQty = (int)(spd.stockqty ?? 0);
                    ST.goods_id = item.goods_id;
                    ST.goods_name = item.goods_name;
                    ST.plot_id = item.plot_id;
                    ST.plot_no = item.plot_no;
                    ST.goods_no = item.goods_no;
                    ST.goods_spec = item.goods_spec;
                    ST.Qty = item.qty;
                    ST.TackQty = item.qty;
                    ST.unit = item.unit;
                    ST.factory_name = item.factory_name;

                    SetTackStatus(ST);
                    Stocks.Add(ST);
                }
            }
            catch (Exception ex)
            {
                Log.Error("StockTackingView_Loaded", ex);
            }
        }
        private void SetTackStatus(StockTack ST)
        {
            if (ST.Qty == ST.TackQty)
            {
                ST.TackStatus = "正常";
                if (ST.Qty != ST.SpdQty)
                    ST.TackStatus = "SPD库存异常";
            }
            else if (ST.Qty > ST.TackQty)
                ST.TackStatus = "缺少";
            else if (ST.Qty < ST.TackQty)
                ST.TackStatus = "多出";
        }
        private void gridStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridStock.SelectedIndex == -1) return;
            var temo = gridStock.SelectedItems;
            for (int i = 0; i < temo.Count; i++)
            {

                if (temo[i] is StockTack store_In)
                {
                    store_In.Selected = !((StockTack)temo[temo.Count - 1]).Selected;
                }
            }


        }


        private async void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender is TextBox txt)
                {
                    if (txt.Tag is StockTack ST)
                    {
                        GoodsScan scaner = new GoodsScan();
                        scaner.ShowType = 3;
                        scaner.goods = ST;
                        scaner.goods.qty = ST.TackQty;
                        var result = await DialogHost.Show(scaner, "RootDialog");
                        if ((bool)result == true)
                        {
                            ST.TackQty = scaner.goods.qty;
                            SetTackStatus(ST);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("TextBox_PreviewMouseDown", ex);
            }
        }

        private void btnTack_Click(object sender, RoutedEventArgs e)
        {
            InitStockData();

          
        }
        private void btnTackOK_Click(object sender, RoutedEventArgs e)
        {
            //List<cart_stock_take> cart_Stock_Takes = new List<cart_stock_take>();
            //List<DapperParams> dic = new List<DapperParams>();
            //foreach (var item in Stocks)
            //{
            //    if (item.TackQty != 0)
            //    {
            //        string no = CacheData.Ins.SnowId.nextId().ToString();
            //        cart_stock_take tack = new cart_stock_take()
            //        {
            //            cretime = DateTime.Now,
            //            goods_id = item.goods_id,
            //            stock_id = no,
            //            qty = item.TackQty,
            //            repair_status = item.TackQty > item.Qty ? "盘盈" : "盘亏"
            //        };
            //        int res = CacheData.Ins.dbUtil.Insert(tack);
            //        dic.Add(new DapperParams($"update cart_stock set qty={item.TackQty} where goods_id={item.goods_id}", null));

            //        item.Qty = item.TackQty;
            //        item.TackQty = 0;
            //    }
            //}
            bool succ = true;// CacheData.Ins.dbUtil.TransactionAction(dic);
            if (succ)
            {
                List<stockTackPara> datas = new List<stockTackPara>();
                foreach (var item in Stocks)
                {
                    if (item.SpdQty == item.TackQty) continue;
                    stockTackPara data = new stockTackPara();
                    data.goodsId = item.goods_id;
                    data.goodsNo = item.goods_no;
                    data.goodsName = item.goods_name;
                    data.categoryType = item.goods_type;
                    data.factQty = item.TackQty;
                    data.plotId = item.plot_id;
                    data.plotNo = item.plot_no;
                    data.operateManId = CacheData.Ins.User.id;
                    data.operateManNo = CacheData.Ins.User.user_code;
                    data.operateTime = DateTime.Now.ToLongString();
                    data.storehouseId = CacheData.Ins.currStock.storehouseid;
                    data.storehouseName = CacheData.Ins.currStock.storehousename;
                    data.stockQty = item.SpdQty;
                    data.singleList = "";
                    datas.Add(data);
                }

                sPDSystemData. StockTackSPD(datas);
            }
            //Stocks.ForEach(a => { if (a.TackQty == 0) return; a.Qty = a.TackQty; a.TackQty = 0; });

        }

        public void sendMsg(string code, string MsgType)
        {
            this.Dispatcher.Invoke(() =>
            {
                Log.Debug(this.ToString() + code);
                StockUtil.GridFilter(gridStock, code);
            });
        }
        public void MenuSelected(string menuName)
        {

        }

        private void gridStock_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void gridStock_UnloadingRow(object sender, DataGridRowEventArgs e)
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

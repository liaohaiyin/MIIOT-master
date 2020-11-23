using MIIOT.Model;
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
    /// AbStocksView.xaml 的交互逻辑
    /// </summary>
    public partial class AbStocksView : UserControl, IMenuMsgSend, INotifyPropertyChanged
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
        private ObservableCollection<cart_goods> _stocks = new ObservableCollection<cart_goods>();

        public ObservableCollection<cart_goods> Stocks
        {
            get { return _stocks; }
            set
            {
                _stocks = value;
                OnPropertyChanged("Stocks");
            }
        }
        private void CheckBoxHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                for (int i = 0; i < girdAbStock.Items.Count; i++)
                {
                    DataGridRow neddrow = (DataGridRow)girdAbStock.ItemContainerGenerator.ContainerFromIndex(i);
                    var cb = girdAbStock.Columns[0].GetCellContent(neddrow);
                    CheckBoxUtil.GetVisualChild(cb, chk.IsChecked);
                }
                foreach (var item in girdAbStock.Items)
                {
                    if(item is BaseRecord record)
                        record.Selected = chk.IsChecked == true;
                }
            }
        }

        private void CheckBoxCell_Click(object sender, RoutedEventArgs e)
        {
            //if (sender is CheckBox chk)
            //{
            //    if (chk.Tag is cart_goods goods)
            //    {
            //        goods.Selected = chk.IsChecked == true;
            //    }
            //}
        }
        public AbStocksView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += AbStocksView_Loaded;
        }

        private void AbStocksView_Loaded(object sender, RoutedEventArgs e)
        {
            Stocks.Clear();
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        List<cart_goods> goods = new List<cart_goods>();
                        goods =CacheData.Ins. dbUtil.Query<cart_goods>("SELECT  a.stock_id SoucrceCode,b.goods_id, b.goods_no,b.goods_name,b.goods_spec,b.factory_name,b.unit,a.qty,a.repair_status use_status,a.creman_name,a.cretime from cart_stock_err a, cart_goods b where a.goods_id=b.goods_id and repair_status=0 and qty>0", null);
                        if (goods == null) return;
                        for (int i = 0; i < goods.Count; i++)
                        {
                            goods[i].index = i + 1;
                        }
                        Stocks.AddRange(goods);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("StockQueryView_Loaded disp", ex);
                    }
                });
            });
        }

        public void MenuSelected(string menuName)
        {
        }

        public void sendMsg(string code,string MsgType)
        {
            StockUtil.GridFilter(girdAbStock, code);
        }

        private void btnAddStock_Click(object sender, RoutedEventArgs e)
        {
            List<DapperParams> sqlstr = new List<DapperParams>();
            foreach (var item in Stocks)
            {
                if (item.Selected == false) continue;
                sqlstr.Add(new DapperParams(new StockUtil().StockBulder(new cart_stock() { goods_id = item.goods_id, qty = item.qty }), null));
                sqlstr.Add(new DapperParams($"UPDATE cart_stock_err SET repair_status=1 where stock_id={item.SoucrceCode}", null));
            }
            if (sqlstr.Count == 0)
            {
                CacheData.Ins.mainWindow.MessageTips("未操作");
                return;
            }
            bool succ =CacheData.Ins. dbUtil.TransactionAction(sqlstr);
            if (succ)
            {
                CacheData.Ins.mainWindow.MessageTips("报溢成功");
            }
        }

        private void btnRmStock_Click(object sender, RoutedEventArgs e)
        {
            CacheData.Ins.mainWindow.MessageTips("暂无负库存，待开发");
        }

        private void girdAbStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (girdAbStock.SelectedIndex != -1)
            {
                if (girdAbStock.SelectedItem is cart_goods goods)
                {
                    goods.Selected = !goods.Selected;
                }
                girdAbStock.SelectedIndex = -1;
            }
        }

        private void girdAbStock_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void girdAbStock_Unloaded(object sender, RoutedEventArgs e)
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

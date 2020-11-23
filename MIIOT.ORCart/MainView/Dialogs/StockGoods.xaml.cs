using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Model.Trical;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace MIIOT.ORCart.MainView.Dialogs
{
    /// <summary>
    /// StockGoods.xaml 的交互逻辑
    /// </summary>
    public partial class StockGoods : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<cart_goods> _StockGoodslist = new ObservableCollection<cart_goods>();

        public ObservableCollection<cart_goods> StockGoodslist
        {
            get { return _StockGoodslist; }
            set
            {
                _StockGoodslist = value;
                OnPropertyChanged("StockGoodslist");
            }
        }
        private ObservableCollection<cart_goods> _ReuseGoodslist = new ObservableCollection<cart_goods>();

        public ObservableCollection<cart_goods> ReuseGoodslist
        {
            get { return _ReuseGoodslist; }
            set
            {
                _ReuseGoodslist = value;
                OnPropertyChanged("ReuseGoodslist");
            }
        }
        public delegate void DelBackSelect(object sender, IList<cart_goods> goods);
        public event DelBackSelect OnBackSelect;
        public delegate void DelClose();
        public event DelClose OnClose;
        public StockGoods()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += StockGoods_Loaded;

        }
        private int _initType;

        public int InitType
        {
            get { return _initType; }
            set
            {
                _initType = value;
                OnPropertyChanged("ReuseGoodslist");
            }
        }

        private void StockGoods_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void Init(int initType)
        {
            try
            {
                InitType = initType;
                if (initType == 1)
                {
                    tabitem1.Visibility = Visibility.Visible;
                    tabitem2.Visibility = Visibility.Visible;
                }
                else
                {
                    tabitem1.Visibility = Visibility.Collapsed;
                    tabitem2.Visibility = Visibility.Collapsed;

                }
                switch (initType)
                {
                    case 1:
                    case 3:
                        var temp = new StockUtil().GetAllStockGoods();
                        foreach (var item in temp)
                        {
                            item.InitType = initType;

                        }
                        StockGoodslist.Clear();
                        ReuseGoodslist.Clear();
                        StockGoodslist.AddRange(temp.FindAll(a => ((GoodsTypeEnum)a.goods_type) != GoodsTypeEnum.REUSE && a.qty > 0));
                        var reuseDate = CacheData.Ins.dbUtil.Query<cart_goods>(@"SELECT b.singlecode single_code,IFNULL(b.usetime,0) usetime,b.plotid plot_id,b.plotno plot_no,b.batchno batch_no,b.validto validity_date,
 a.* from cart_goods a INNER JOIN spd_singlecode b on a.goods_id=b.goodsid where a.goods_type=3  GROUP BY b.goodsid", null);
                        ReuseGoodslist.AddRange(reuseDate);
                        break;
                    case 2:
                        temp = new StockUtil().GetStrategyGoods();
                        temp.ForEach(a => a.InitType = initType);
                        StockGoodslist.Clear();
                        StockGoodslist.AddRange(temp.FindAll(a => ((GoodsTypeEnum)a.goods_type) != GoodsTypeEnum.REUSE));
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Log.Error("StockGoods init ", ex);
            }
        }
        public void load()
        {
            if (chkAll != null) chkAll.IsChecked = false;
            StockGoodslist.ForEach(a => a.Selected = false);
            ReuseGoodslist.ForEach(a => a.Selected = false);
            gridStock.Items.Refresh();
            gridReuseGoods.Items.Refresh();
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnClose?.Invoke();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {

            List<cart_goods> list = new List<cart_goods>();
            foreach (var item in StockGoodslist)
            {
                if (item.Selected)
                    list.Add(item);
            }
            foreach (var item in ReuseGoodslist)
            {
                if (item.Selected)
                    list.Add(item);
            }
            OnBackSelect?.Invoke(this, list);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            StockGoodslist.ForEach(a => a.Selected = false);
            ReuseGoodslist.ForEach(a => a.Selected = false);
            OnClose?.Invoke();
        }

        private void CheckBox_1_Click(object sender, RoutedEventArgs e)
        {
            CheckBox headercb = (CheckBox)sender;

            for (int i = 0; i < gridStock.Items.Count; i++)
            {
                //获取行
                DataGridRow neddrow = (DataGridRow)gridStock.ItemContainerGenerator.ContainerFromIndex(i);

                //获取该行的某列
                CheckBox cb = (CheckBox)gridStock.Columns[0].GetCellContent(neddrow);

                cb.IsChecked = headercb.IsChecked;

            }
            foreach (var item in StockGoodslist)
            {
                item.Selected = headercb.IsChecked == true;
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                if (chk.Tag is cart_goods goods)
                {
                    goods.Selected = !goods.Selected;
                }
            }
        }
        private void CheckBox1_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                if (chk.Tag is cart_goods goods)
                {
                    goods.Selected = !goods.Selected;
                }
            }
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GridFilter(gridStock, ((TextBox)sender).Text);
        }
        private void GridFilter(DataGrid dataGrid, string text)
        {
            var cvs = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = new Predicate<object>(a =>
                {
                    if (a is cart_goods)
                    {
                        return (a as cart_goods).goods_no.ToLower().Contains(text.ToLower())
                            || (a as cart_goods).goods_name.ToLower().Contains(text.ToLower());
                    }
                    return false;
                });

            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            GridFilter(gridReuseGoods, ((TextBox)sender).Text);
        }
        CheckBox chkAll = null;
        private void CheckBoxHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                chkAll = chk;
                for (int i = 0; i < gridStock.Items.Count; i++)
                {
                    DataGridRow neddrow = (DataGridRow)gridStock.ItemContainerGenerator.ContainerFromIndex(i);
                    if (neddrow == null) continue;
                    var cb = gridStock.Columns[0].GetCellContent(neddrow);
                    CheckBoxUtil.GetVisualChild(cb, chk.IsChecked);
                }
                foreach (var item in gridStock.Items)
                {
                    if (item is BaseRecord record)
                        record.Selected = chk.IsChecked == true;
                }
            }
        }





        private void CheckBoxHeader1_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                for (int i = 0; i < gridReuseGoods.Items.Count; i++)
                {
                    DataGridRow neddrow = (DataGridRow)gridReuseGoods.ItemContainerGenerator.ContainerFromIndex(i);
                    var cb = gridReuseGoods.Columns[0].GetCellContent(neddrow);
                    CheckBoxUtil.GetVisualChild(cb, chk.IsChecked);
                }
                foreach (var item in gridReuseGoods.Items)
                {
                    if (item is BaseRecord record)
                        record.Selected = chk.IsChecked == true;
                }
            }
        }

        private void gridStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridStock.SelectedIndex != -1)
            {
                if (gridStock.SelectedItem is cart_goods goods)
                {
                    goods.Selected = !goods.Selected;
                }
                gridStock.SelectedIndex = -1;
            }
        }

        private void gridReuseGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridReuseGoods.SelectedIndex != -1)
            {
                if (gridReuseGoods.SelectedItem is cart_goods goods)
                {
                    goods.Selected = !goods.Selected;
                }
                gridReuseGoods.SelectedIndex = -1;
            }
        }
    }

}

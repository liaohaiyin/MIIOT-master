using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.ORCart.MainView.Dialogs;
using MIIOT.Utility;
using System;
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

namespace MIIOT.ORCart.Controls
{
    /// <summary>
    /// GridControl.xaml 的交互逻辑
    /// </summary>
    public partial class GridControl : UserControl, INotifyPropertyChanged
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
        private string _GroupName;

        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                _GroupName = value;
                OnPropertyChanged("GroupName");
            }
        }
        private ObservableCollection<cart_clear> _Clears = new ObservableCollection<cart_clear>();

        public ObservableCollection<cart_clear> Clears
        {
            get
            {
                return _Clears;
            }
            set
            {
                _Clears = value;
                OnPropertyChanged("Clears");
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
        public delegate void delDelete(GridControl sender, cart_clear clear);
        public event delDelete OnDelete;
        public delegate void DelItemCheck(cart_clear clear);
        public event DelItemCheck OnItemChecked;
        public GridControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void Init(IList<cart_clear> cart_Clears)
        {
            var group = cart_Clears.GroupBy(a=>a.goods_id);
            foreach (var item in group)
            {
                cart_clear clear = item.First();
                clear.qty = item.Sum(a=>a.qty);
                Clears.Add(clear);
            }
        }
            
        //删除
        private void btndelClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    if (btn.Tag is cart_clear clear)
                    {

                        OnDelete?.Invoke(this, clear);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("btndelClear_Click", ex);
            }
        }
        //拆单
        private void btnSplit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    if (btn.Tag is cart_clear clear)
                    {
                        if (clear.qty < 2)
                        {

                            CacheData.Ins.mainWindow.MessageTips($"数量不够，不可再拆单");
                            return; }
                        cart_clear Newclear = clear.DeepClone();
                        Newclear.id = CacheData.Ins.SnowId.nextId();
                        Newclear.qty = 1;
                        Newclear.IsNew = true;
                        clear.qty--;
                        CacheData.Ins.dbUtil.Update(clear);
                        CacheData.Ins.dbUtil.Insert(Newclear);
                        Clears.Add(Newclear);
                        ClearList.Add(Newclear);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("btnSplit_Click", ex);
            }
        }
        //筛选
        public void GridFilter(string ParaStr)
        {

            var cvs = CollectionViewSource.GetDefaultView(girdClear.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = new Predicate<object>(a =>
                {
                    if (a is cart_clear clear)
                    {
                        var text = ParaStr.ToLower();
                        return clear.goods_no.ToLower().Contains(text)
                            || clear.goods_name.ToLower().Contains(text);
                    }
                    return false;
                });

            }
        }
        private void girdOrder_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            RetryUtil.TimeOutAction(200, () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.IsHitTestVisible = false;
                });
            },
            () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.IsHitTestVisible = true;
                });

            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (txt.Tag is cart_clear clear)
                {
                    clear.qty = int.Parse(txt.Text);
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox headercb = (CheckBox)sender;

            for (int i = 0; i < girdClear.Items.Count; i++)
            {
                //获取行
                DataGridRow neddrow = (DataGridRow)girdClear.ItemContainerGenerator.ContainerFromIndex(i);

                //获取该行的某列
                CheckBox cb = (CheckBox)girdClear.Columns[0].GetCellContent(neddrow);

                cb.IsChecked = headercb.IsChecked;

            }
        }

        private void CheckBoxHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                for (int i = 0; i < girdClear.Items.Count; i++)
                {
                    DataGridRow neddrow = (DataGridRow)girdClear.ItemContainerGenerator.ContainerFromIndex(i);
                    var cb = girdClear.Columns[0].GetCellContent(neddrow);
                    CheckBoxUtil.GetVisualChild(cb, chk.IsChecked);
                }
                foreach (var item in girdClear.Items)
                {
                    if (item is BaseRecord record)
                        record.Selected = chk.IsChecked == true;
                }
            }
        }

        private void CheckBoxCell_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                if (chk.Tag is cart_clear goods)
                {
                    goods.Selected = chk.IsChecked == true;
                    OnItemChecked?.Invoke(goods);
                }
            }
        }


        private async void TextBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox txt)
            {
                if (txt.Tag is cart_clear clear)
                {
                    cart_goods goods = new cart_goods()
                    {
                        goods_id = clear.goods_id,
                        goods_name = clear.goods_name,
                        goods_no = clear.goods_no,
                        goods_spec = clear.goods_spec,
                        factory_name = clear.factory_name,
                        unit = clear.unit,
                        price = clear.price,
                        qty = 0
                    };

                    GoodsScan scaner = new GoodsScan();
                    scaner.goods = goods;
                    scaner.ShowType = 4;
                    var temo = await DialogHost.Show(scaner, "RootDialog");
                    if ((bool)temo)
                    {
                        clear.qty = scaner.goods.qty;
                        clear.Selected = true;
                        int i = CacheData.Ins.dbUtil.Update(clear);
                    }
                }
            }
        }

        private async void btndetail_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is cart_clear goods)
                {
                    var logs = CacheData.Ins.dbUtil.Query<cart_clear_log>("SELECT * from cart_clear_log where source_id=@code and goods_id=@goods_id", new { code=goods.origin_id,goods_id=goods.goods_id});

                    ClearLogView scaner = new ClearLogView();
                    scaner.Init(goods, logs);
                    var temo = await DialogHost.Show(scaner, "RootDialog");
                    if ((bool)temo)
                    {

                    }
                }
            }

        }

        private void girdClear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (girdClear.SelectedIndex != -1)
            {
                if (girdClear.SelectedItem is cart_clear goods)
                {
                    goods.Selected = !goods.Selected;
                }
                girdClear.SelectedIndex = -1;
            }
        }

        private void ToggleButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToggleButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Primitives.ToggleButton Tbtn)
            {
                if (Tbtn.Tag is cart_clear clear)
                {
                    if (Tbtn.IsChecked == false)
                    {
                        clear.pay_flag = false;
                        clear.ExPrice = 0;
                        clear.ExtotalPrice = 0;
                    }
                    else
                    {
                        clear.pay_flag = true;
                        clear.ExPrice = clear.price;
                        clear.ExtotalPrice = clear.price * clear.qty;
                    }
                }
            }
        }
    }
}

using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.Trical;
using MIIOT.ORCart.Controls;
using MIIOT.Utility;
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

namespace MIIOT.ORCart.MainView.Dialogs
{
    /// <summary>
    /// NewGoods.xaml 的交互逻辑
    /// </summary>
    public partial class NewGoods : UserControl
    {
        public delegate void DelClose();
        public event DelClose OnClose;
        public delegate void DelQtyChange(cart_goods goods);
        public event DelQtyChange OnQtyChange;
        private List<cart_goods> _cartGoods = new List<cart_goods>();

        public List<cart_goods> CartGoods
        {
            get { return _cartGoods; }
            set
            {
                _cartGoods = value;
            }
        }
        public NewGoods()
        {
            InitializeComponent();
            myStyle = (Style)this.FindResource("TabItemCardStyle");
            this.Loaded += NewGoods_Loaded;

        }

        private void NewGoods_Loaded(object sender, RoutedEventArgs e)
        {
            InitGoods();
        }

        Style myStyle;
        DapperUtil dbUtil = new DapperUtil();
        List<cart_goods> _Currgoods = new List<cart_goods>();
        public void InitGoods()
        {
            _Currgoods = new StockUtil().GetAllStockGoods();

            var temp = _Currgoods.GroupBy(a => a.goods_type);
            if (temp.Count() > 0)
                tabControl.Items.Clear();
            foreach (var item in temp)
            {
                TabItem tabItem = new TabItem() { Header = item.Key, Style = myStyle };
                ScrollViewer scrollViewer = new ScrollViewer();
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                tabItem.Content = scrollViewer;
                WrapPanel panel = new WrapPanel();
                scrollViewer.Content = panel;
                foreach (var good in item)
                {
                    GoodsControl goodsControl = new GoodsControl() { Goods = good };
                    goodsControl.OnClick += GoodsControl_OnClick;
                    panel.Children.Add(goodsControl);
                }
                tabControl.Items.Add(tabItem);
            }
            tabControl.SelectedIndex = 0;
        }
        public void RefreshGoods(IList<cart_clear> clears)
        {
            foreach (var item in tabControl.Items)
            {
                if (item is TabItem tabItem)
                {
                    if (tabItem.Content is ScrollViewer scroll)
                    {
                        if (scroll.Content is WrapPanel panel)
                        {
                            foreach (var con in panel.Children)
                            {
                                if (con is GoodsControl goods)
                                {
                                    var clear = clears.FirstOrDefault(a => a.goods_no == goods.Goods.goods_no);
                                    if (clear != null)
                                        goods.Goods.qty = clear.qty;
                                    else
                                        goods.Goods.qty = 0;
                                }

                            }
                        }
                    }
                }
            }
        }
        private void GoodsControl_OnClick(cart_goods goods)
        {
            OnQtyChange?.Invoke(goods);

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OnClose?.Invoke();
        }
    }
}

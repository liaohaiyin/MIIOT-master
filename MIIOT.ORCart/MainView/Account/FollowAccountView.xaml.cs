using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.Common;
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
    /// FollowAccountView.xaml 的交互逻辑
    /// </summary>
    public partial class FollowAccountView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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
        public delegate void DelBack();
        public event DelBack OnBackClick;
        public FollowAccountView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += FollowAccountView_Loaded;
        }

        private void FollowAccountView_Loaded(object sender, RoutedEventArgs e)
        {
            if (SelectPubSurgery == null) return;
            Clears.Clear();
            var ClearList = new MIIOT.Utility.DapperUtil().Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=0 and follow_tag=1 ", new { origin_id = SelectPubSurgery.surgeryid });
            Clears.AddRange(ClearList);
        }

        public void sendMsg(string code)
        {
            var goods = new StockUtil().GetStockGoodsByNo(code);
            if (goods != null && goods.Count > 0)
            {
                cart_clear clear = new cart_clear()
                {
                    id = CacheData.Ins.SnowId.nextId(),
                    origin_id = SelectPubSurgery.surgeryid,
                    goods_no = goods[0].goods_no,
                    goods_name = goods[0].goods_name,
                    factory_name = goods[0].factory_name,
                    goods_spec = goods[0].goods_spec,
                    goods_type = goods[0].goods_type,
                    unit = goods[0].unit,
                    follow_tag = true,
                    qty = 1,
                    price = 10,
                    IsNew = true,
                    unfree = true,Selected=true
                };
                var temp = Clears.FirstOrDefault(a => a.goods_no == clear.goods_no);
                if (temp == null)
                {
                    int succ = new MIIOT.Utility.DapperUtil().Insert(clear);
                    if (succ > 0)
                        Clears.Add(clear);
                }
                else
                {
                    CacheData.Ins.mainWindow.MessageTips($"[{clear.goods_name}]商品已在列表中");
                }

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OnBackClick?.Invoke();
        }

        private void SurgeryView_DoSelect(spd_surgeryinfo pub_Surgery)
        {

        }

        public void MenuSelected(string menuName)
        {
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
                foreach (var item in Clears)
                {
                    item.Selected = chk.IsChecked == true;
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
                }
            }
        }

        private void btnSplit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btndelClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button btn)
                {
                    if (btn.Tag is cart_clear clear)
                    {
                        if (clear.IsNew)
                        {
                            Clears.Remove(clear);
                            CacheData.Ins.mainWindow.MessageTips("删除成功");
                            return;
                        }
                        if (new MIIOT.Utility.DapperUtil().Delete(clear))
                        {
                            Clears.Remove(clear);
                            CacheData.Ins.mainWindow.MessageTips("删除成功");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("btndelClear_Click", ex);
            }
        }

        private void btnKeep_Click(object sender, RoutedEventArgs e)
        {

            var SelectClears = Clears.ToList().FindAll(a => a.Selected);
            if (new AccountUtil().KeepAccount(SelectClears, SelectPubSurgery))
            {
                Clears.Clear();
                var ClearList = new MIIOT.Utility.DapperUtil().Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=0 and follow_tag=1 ", new { origin_id = SelectPubSurgery.surgeryid });
                Clears.AddRange(ClearList);
                
            }
        }

        private void btnUnKeep_Click(object sender, RoutedEventArgs e)
        {
            var SelectClears = Clears.ToList().FindAll(a => a.Selected);
            if (new AccountUtil().CancelAccount(SelectClears, SelectPubSurgery))
            {
                Clears.Clear();
                var ClearList = new MIIOT.Utility.DapperUtil().Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status=0 and follow_tag=1 ", new { origin_id = SelectPubSurgery.surgeryid });
                Clears.AddRange(ClearList);
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
    }
}

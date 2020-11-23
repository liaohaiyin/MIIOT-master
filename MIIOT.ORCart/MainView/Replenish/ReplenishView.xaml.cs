using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.DataSync;
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

namespace MIIOT.ORCart.MainView.Replenish
{
    /// <summary>
    /// ReplenishView.xaml 的交互逻辑
    /// </summary>
    public partial class ReplenishView : UserControl, INotifyPropertyChanged
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

        private ObservableCollection<spd_replenish> _Replenishes = new ObservableCollection<spd_replenish>();

        public ObservableCollection<spd_replenish> Replenishes
        {
            get { return _Replenishes; }
            set
            {
                _Replenishes = value;
                OnPropertyChanged("Replenishes");
            }
        }
        public ReplenishmentView Ment { get; set; }
        public ReplenishView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += ReplenishView_Loaded;

        }

        private void Ment_DoBack()
        {
            InstockOrder();
        }

        private void ReplenishView_Loaded(object sender, RoutedEventArgs e)
        {
            if (Ment != null)
            {

                Ment.DoBack -= Ment_DoBack;
                Ment.DoBack += Ment_DoBack;
                Ment.DoNext -= Ment_DoNext;
                Ment.DoNext += Ment_DoNext;
            }
            InstockOrder();
        }

        private void Ment_DoNext(spd_replenish replenish)
        {
            int index = Replenishes.IndexOf(replenish);
            if (Replenishes.Count > index + 1)
            {
                if (Ment != null)
                    Ment.DoReplenish(new List<spd_replenish>() { Replenishes[index + 1] });
            }
            else
            {
                if (Ment != null)
                    Ment.DoReplenish(new List<spd_replenish>() { Replenishes[0] });
            }
        }

        public void InstockOrder()
        {

            try
            {
                Replenishes.Clear();
                List<spd_replenish> replenishs = CacheData.Ins.dbUtil.Query<spd_replenish>("SELECT * from spd_replenish order by replenishno desc", null);
                List<spd_replenishdtl> replenishdtls = CacheData.Ins.dbUtil.Query<spd_replenishdtl>("SELECT * from spd_replenishdtl", null);

                foreach (var item in replenishs)
                {
                    item.replenishstatus = 0;
                    var temp = replenishdtls.FindAll(a => a.replenishid == item.replenishid);
                    if (temp != null)
                    {
                        if (temp.FirstOrDefault(a => a.rackstatus == 2) != null)
                        {
                            var group = temp.GroupBy(a => a.rackstatus);
                            if (group.Count() > 1)
                            {
                                item.replenishstatus = 3;
                                Replenishes.Add(item);
                            }
                            else if (group.Count() == 1)
                            {
                                foreach (var dtls in temp)
                                {
                                    if (dtls.old_replenish_qty > 0 && dtls.old_replenish_qty < dtls.replenishqty)
                                        item.replenishstatus = 3;
                                    if (dtls.old_replenish_qty > 0 && dtls.old_replenish_qty == dtls.replenishqty)
                                        item.replenishstatus = 1;
                                }

                                Replenishes.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("InstockOrder", ex);
            }

        }
        private void girdGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (girdGoods.SelectedIndex != -1)
            {
                if (girdGoods.SelectedItem is spd_replenish goods)
                {
                    goods.Selected = !goods.Selected;
                }
                girdGoods.SelectedIndex = -1;
            }

        }
        private void btnRelenishAll_Click(object sender, RoutedEventArgs e)
        {
            var temo = Replenishes.ToList().FindAll(a => a.Selected == true);
            if (temo.Count == 0)
            {
                CacheData.Ins.mainWindow.MessageTips("未选择记录");
                return;
            }
            if (Ment != null)
                Ment.DoReplenish(temo);
            Transitioner.MoveNextCommand.Execute("", this);
        }

        private void btnisHandOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRepenish_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is spd_replenish replenish)
                {
                    if (Ment != null)
                        Ment.DoReplenish(new List<spd_replenish>() { replenish });
                    Transitioner.MoveNextCommand.Execute("", this);
                }
            }

        }

        public void SendMsg(string code)
        {
            GridFilter(girdGoods, code);
        }


        public void GridFilter(DataGrid dataGrid, string ParaStr)
        {
            var cvs = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = new Predicate<object>(a =>
                {
                    if (a is spd_replenish cart_Replenish)
                    {
                        var text = ParaStr.ToLower();
                        return cart_Replenish.replenishno.ToLower().Contains(text);
                    }
                    return false;
                });

            }
        }

        private void CheckBoxHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                for (int i = 0; i < girdGoods.Items.Count; i++)
                {
                    DataGridRow neddrow = (DataGridRow)girdGoods.ItemContainerGenerator.ContainerFromIndex(i);
                    if (neddrow == null) continue;
                    var cb = girdGoods.Columns[0].GetCellContent(neddrow);
                    CheckBoxUtil.GetVisualChild(cb, chk.IsChecked);
                }
                foreach (var item in girdGoods.Items)
                {
                    if (item is BaseRecord record)
                    {
                        record.Selected = chk.IsChecked == true;

                    }
                }
            }
        }

        private void CheckBoxCell_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                if (chk.Tag is spd_replenish goods)
                {
                    goods.Selected = chk.IsChecked == true;
                }
            }
        }

        private void btnGetSPDData_Click(object sender, RoutedEventArgs e)
        {
#if Net
            new SPDSystemData().GetRepnelish(DateTime.Now.AddDays(-7), DateTime.Now);
#endif
        }
    }
}

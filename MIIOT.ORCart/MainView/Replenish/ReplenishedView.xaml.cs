using MaterialDesignThemes.Wpf;
using MIIOT.Controls;
using MIIOT.Controls.Dialogs;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
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

namespace MIIOT.ORCart.MainView.Replenish
{
    /// <summary>
    /// ReplenishedView.xaml 的交互逻辑
    /// </summary>
    public partial class ReplenishedView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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
        private ObservableCollection<spd_replenishdtl> _ReplenishedDtls = new ObservableCollection<spd_replenishdtl>();

        public ObservableCollection<spd_replenishdtl> ReplenishedDtls
        {
            get { return _ReplenishedDtls; }
            set
            {
                _ReplenishedDtls = value;
                OnPropertyChanged("ReplenishedDtls");
            }
        }
        private ObservableCollection<cart_replenish> _Replenished = new ObservableCollection<cart_replenish>();

        public ObservableCollection<cart_replenish> Replenished
        {
            get { return _Replenished; }
            set
            {
                _Replenished = value;
                OnPropertyChanged("Replenished");
            }
        }
        private cart_replenish _selectDtlData;

        public cart_replenish SelectDtlData
        {
            get { return _selectDtlData; }
            set
            {
                _selectDtlData = value;
                OnPropertyChanged("SelectDtlData");
            }
        }
        public ReplenishedView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += ReplenishedView_Loaded;
            txtSdate.SelectedDate = DateTime.Now.Date.AddDays(-7);
            txtEdate.SelectedDate = DateTime.Now;
        }

        private void ReplenishedView_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Dispatcher.Invoke(() =>
                {
                    RefreshReplenished(DateTime.Now.Date.AddDays(-7), DateTime.Now.Date.AddDays(1));
                });
            });

        }

        private void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtSdate.SelectedDate > txtEdate.SelectedDate)
                {
                     CacheData.Ins.mainWindow.MessageTips("日期区间选择错误");
                    return;
                }

                RefreshReplenished(txtSdate.SelectedDate.Value, txtEdate.SelectedDate.Value.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second));

            }
            catch (Exception ex)
            {
                Log.Error("btnQuery_Click", ex);
            }
        }

        private void btnRelenishDtl_Click(object sender, RoutedEventArgs e)
        {
            ReplenishedDtls.Clear();
            var temp =CacheData.Ins. dbUtil.Query<spd_replenishdtl>("SELECT * from spd_replenishdtl WHERE replenishid=@id and old_replenish_qty>0; ", new { id = SelectDtlData.replenish_id });
            foreach (var item in temp)
            {
                item.index = ReplenishedDtls.Count + 1;
                ReplenishedDtls.Add(item);
            }

            DtlTag.IsChecked = true;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DtlTag.IsChecked = false;
        }
        public void RefreshReplenished(DateTime sdate, DateTime edate)
        {
            try
            {
                Replenished.Clear();

                var result = DialogHost.Show(new LoadingDialog(), "RootDialog", delegate (object senders, DialogOpenedEventArgs args)
                {
                    Task.Run(() =>
                    {
                        string stime = sdate.ToLongString();
                        string etime = edate.ToLongString();
                        var temp =CacheData.Ins. dbUtil.Query<cart_replenish>("SELECT * from cart_replenish where replenish_time >@stime and replenish_time <@etime", new { stime, etime });
                        Dispatcher.Invoke(() =>
                        {
                            Replenished.AddRange(temp);
                            args.Session.Close(false);
                        });
                    });
                });
            }
            catch (Exception ex)
            {
                Log.Error("RefreshReplenished", ex);
            }
        }

        public void sendMsg(string code,string MsgType)
        {
            GridFilter(girdReplenished, code);
        }
        public void MenuSelected(string menuName)
        {
        }
        public void GridFilter(DataGrid dataGrid, string ParaStr)
        {
            var cvs = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = new Predicate<object>(a =>
                {
                    if (a is cart_replenishdtl cart_Replenish)
                    {
                        var text = ParaStr.ToLower();
                        return cart_Replenish.goods_no.ToLower().Contains(text)
                            || cart_Replenish.goods_name.ToLower().Contains(text)
                            || cart_Replenish.replenish_no.ToLower().Contains(text);
                    }
                    return false;
                });

            }
        }
        private void girdGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnQueryWeek_Click(object sender, RoutedEventArgs e)
        {
            RefreshReplenished(DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek), DateTime.Now.Date.AddDays(1));
        }

        private void btnQueryMonth_Click(object sender, RoutedEventArgs e)
        {
            RefreshReplenished(DateTime.Now.Date.AddDays(-7), DateTime.Now.Date.AddDays(1));
        }
    }
}

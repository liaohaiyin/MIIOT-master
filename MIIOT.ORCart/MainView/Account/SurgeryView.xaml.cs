using MIIOT.Controls;
using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.DataSync;
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
    /// SurgeryView.xaml 的交互逻辑
    /// </summary>
    public partial class SurgeryView : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<spd_room> _SurgeryRooms = new ObservableCollection<spd_room>();

        public ObservableCollection<spd_room> SurgeryRooms
        {
            get { return _SurgeryRooms; }
            set
            {
                _SurgeryRooms = value;
                OnPropertyChanged("SurgeryRooms");
            }
        }
        private spd_room _selectSurgeryRoom;

        public spd_room SelectSurgeryRoom
        {
            get { return _selectSurgeryRoom; }
            set
            {
                if (value != null)
                {
                    try
                    {
                        InitSurgery(SelectedDate, value);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("SelectSurgeryRoom set", ex);
                    }
                }
                _selectSurgeryRoom = value;
                OnPropertyChanged("SelectSurgeryRoom");
            }
        }
        public DateTime SelectedDate { get; set; } = DateTime.Now.Date;

        private spd_surgeryinfo _selectSurgeryInfo;

        public spd_surgeryinfo SelectPubSurgery
        {
            get { return _selectSurgeryInfo; }
            set
            {
                if (value != null)
                {
                    CacheData.Ins.mainWindow.ChildMenuHide(false);
                    DoSelect?.Invoke(value, 1);
                }
                _selectSurgeryInfo = value;
                OnPropertyChanged("SelectPubSurgery");
            }
        }
        public delegate void DelSelect(spd_surgeryinfo pub_Surgery, int ClearStatus);
        public event DelSelect DoSelect;

        public SurgeryView()
        {
            InitializeComponent();
            this.DataContext = this;

            this.Loaded += SurgeryView_Loaded;
        }
        private void SurgeryView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                InitDateList();
                gridSurgery.SelectedIndex = -1;
                var temo = CacheData.Ins.dbUtil.Query<spd_room>("select * from spd_room where status=1", null);
                foreach (var item in temo)
                {
                    var room = SurgeryRooms.FirstOrDefault(a => a.roomid == item.roomid);
                    if (room == null)
                        SurgeryRooms.Add(item);
                }
                List<spd_room> RmList = new List<spd_room>();
                foreach (var item in SurgeryRooms)
                {
                    var room = temo.FirstOrDefault(a => a.roomid == item.roomid);
                    if (room == null)
                        RmList.Add(item);
                }
                RmList.ForEach(a => SurgeryRooms.Remove(a));

                if (SurgeryRooms.Count > 0)
                    SelectSurgeryRoom = SurgeryRooms[0];
                GridFilter("");
            }
            catch (Exception ex)
            {
                Log.Error("AccountView_Loaded", ex);
            }
        }
        private void InitDateList()
        {

            if (stkdate.Children.Count > 0)
            {
                if (stkdate.Children[stkdate.Children.Count - 1] is RadioButton btn)
                {
                    if (btn.Tag is DateTime date)
                    {
                        if (date == DateTime.Now.Date)
                        {

                        }
                        else
                        {
                            stkdate.Children.Clear();
                            for (int i = 6; i >= 0; i--)
                            {
                                RadioButton radioButton = new RadioButton();
                                radioButton.Cursor = Cursors.Hand;
                                radioButton.Content = $"{DateTime.Now.Date.AddDays(-i).Month}月{DateTime.Now.Date.AddDays(-i).Day}日";
                                if (i == 1)
                                    radioButton.Content = "昨天";
                                if (i == 0)
                                {
                                    radioButton.Content = "今天";
                                    SelectedDate = DateTime.Now.Date;
                                    radioButton.IsChecked = true;
                                }
                                radioButton.Tag = DateTime.Now.Date.AddDays(-i);
                                radioButton.Click += RadioButton_Click;
                                stkdate.Children.Add(radioButton);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 6; i >= 0; i--)
                {
                    RadioButton radioButton = new RadioButton();
                    radioButton.Content = $"{DateTime.Now.Date.AddDays(-i).Month}月{DateTime.Now.Date.AddDays(-i).Day}日";
                    if (i == 1)
                        radioButton.Content = "昨天";
                    if (i == 0)
                    {
                        radioButton.Content = "今天";
                        SelectedDate = DateTime.Now.Date;
                        radioButton.IsChecked = true;
                    }
                    radioButton.Tag = DateTime.Now.Date.AddDays(-i);
                    radioButton.Click += RadioButton_Click;
                    stkdate.Children.Add(radioButton);
                }
            }
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton btn)
            {
                if (btn.Tag is DateTime date)
                {
                    SelectedDate = date;
                    InitSurgery(date);
                }
            }
        }
        private void InitSurgery(DateTime date, spd_room room = null)
        {
            if (SelectSurgeryRoom == null || SelectSurgeryRoom.pub_Surgeries == null) return;
            if (room == null)
                room = SelectSurgeryRoom;
            room.pub_Surgeries.Clear();
            List<spd_surgeryinfo> surgerys = CacheData.Ins.dbUtil.Query<spd_surgeryinfo>("select * from  spd_surgeryinfo where roomid=@origin_id  and surgerytime >=@sdate  and surgerytime <@edate ",
                new { origin_id = room.roomno, sdate = date, edate = date.AddDays(1) });
            Console.WriteLine($"room:{room.roomno}，{date}--{date.AddDays(1)}");
            room.pub_Surgeries.AddRange(surgerys);
        }
        public void ClearSelect()
        {
            gridSurgery.SelectedIndex = -1;
        }
        bool isInit = true;
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInit)
            {
                isInit = false;
                return;
            }

        }
        public void GridFilter(string code)
        {
            GridFilter(gridSurgery, code);
        }
        private void GridFilter(DataGrid dataGrid, string text)
        {
            var cvs = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = new Predicate<object>(a =>
                {
                    if (a is spd_surgeryinfo)
                    {
                        return (a as spd_surgeryinfo).surgeryno.ToLower().Contains(text.ToLower())
                            || (a as spd_surgeryinfo).opsubtitle.ToLower().Contains(text.ToLower())
                            || (a as spd_surgeryinfo).customname.ToLower().Contains(text.ToLower())
                            || (a as spd_surgeryinfo).customno.ToLower().Contains(text.ToLower());
                    }
                    return false;
                });

            }
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (SelectPubSurgery != null)
                CacheData.Ins.mainWindow.PageChange();
        }

        private void btnRefreshSPDSurgery_click(object sender, RoutedEventArgs e)
        {
#if Net
            new SPDSystemData().GetSurgery(DateTime.Now.AddDays(-7), DateTime.Now);
#endif
        }
    }

}

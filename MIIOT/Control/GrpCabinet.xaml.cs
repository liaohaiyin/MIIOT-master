using MaterialDesignThemes.Wpf;
using MIIOT.Model;
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

namespace MIIOT.Control
{
    /// <summary>
    /// GrpCabinet.xaml 的交互逻辑
    /// </summary>
    public partial class GrpCabinet : UserControl, INotifyPropertyChanged
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
        private string _CabinetName = "主柜";

        public string CabinetName
        {
            get { return _CabinetName; }
            set
            {
                _CabinetName = value;
                OnPropertyChanged("CabinetName");
            }
        }
        private int _CabinetID = 1;

        public int CabinetID
        {
            get { return _CabinetID; }
            set
            {
                _CabinetID = value;
                OnPropertyChanged("CabinetID");
            }
        }

        private ObservableCollection<SysPara> _MacTypes = new ObservableCollection<SysPara>();

        public ObservableCollection<SysPara> MacTypes
        {
            get { return _MacTypes; }
            set
            {
                _MacTypes = value;
                OnPropertyChanged("MacTypes");
            }
        }
        private ObservableCollection<MacPara> _MacParas = new ObservableCollection<MacPara>();

        public ObservableCollection<MacPara> MacParas
        {
            get { return _MacParas; }
            set
            {
                _MacParas = value;
                OnPropertyChanged("MacParas");
            }
        }
        private MacPara _CurrMacPara;

        public MacPara CurrMacPara
        {
            get { return _CurrMacPara; }
            set
            {
                _CurrMacPara = value;
                OnPropertyChanged("CurrMacPara");
            }
        }
        public delegate void DelGrpButton(GrpCabinet send, int CabinetID);
        public event DelGrpButton DoGrpBtnBack;
        public GrpCabinet()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += GrpCabinet_Loaded;
        }

        private void GrpCabinet_Loaded(object sender, RoutedEventArgs e)
        {
            liboxMacType.Children.Clear();
            foreach (var item in MacTypes)
            {
                Button btn = new Button() { Tag = item, Background = new SolidColorBrush(Colors.Red), Margin = new Thickness(5, 3, 5, 3), Content = item.ParaValue };
                btn.Click += Btn_Click1;
                liboxMacType.Children.Add(btn);
            }
        }

        private void Btn_Click1(object sender, RoutedEventArgs e)
        {
            if (sender is Button text)
            {
                if (text.Tag is SysPara sysPara)
                {
                    MacPara macPara = new MacPara() { MacType = sysPara.ParaType, MacKey = sysPara.ParaKey, Description = sysPara.ParaValue, COM = "COM1", TimeOut = 200, BaudRate = "9600", Active = true, Cabinet = CabinetID.ToString() };
                    NewControl(macPara);
                }
            }
        }

        private void Control_OnDelete(BaseConnectControl send)
        {
            staMacs.Children.Remove(send);
            MacParas.Remove(send.MacPara);
            MIIOT.Drivers.LiteDBHelper.Ins.Delete<MacPara>(send.MacPara.Id);
        }

        public BaseConnectControl NewControl(MacPara macPara)
        {
            if (MacParas.FirstOrDefault(a => a.Cabinet == macPara.Cabinet && a.MacKey == macPara.MacKey) != null)
            {
                return null;
            }
            CurrMacPara = macPara;// 
            BaseConnectControl control = new SerialCard() { MacPara = macPara };
            MIIOT.Model.Drivers.MacTypeEnum macType = macPara.MacKey.ParseEnum<Model.Drivers.MacTypeEnum>();
            switch (macType)
            {
                case Model.Drivers.MacTypeEnum.Server:
                    control = new TCPServerCard() { MacPara = macPara };
                    break;
                case Model.Drivers.MacTypeEnum.PCB:
                    control = new SerialCardLen() { MacPara = macPara };
                    break;
                case Model.Drivers.MacTypeEnum.RFID:
                case Model.Drivers.MacTypeEnum.FRIDGE:
                case Model.Drivers.MacTypeEnum.HID:
                    control = new SerialCardTO() { MacPara = macPara };
                    break;
                default:
                    break;
            }
            control.MacPara = CurrMacPara;
            control.OnDelete += Control_OnDelete;
            staMacs.Children.Add(control);

            MacParas.Add(CurrMacPara);
            return control;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is StackPanel stk)
                {
                    staMacs.Children.Remove(stk);
                }
            }
        }

        private void btnDelete_click(object sender, RoutedEventArgs e)
        {
            DoGrpBtnBack?.Invoke(this, CabinetID);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }


        private void liboxMacType_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

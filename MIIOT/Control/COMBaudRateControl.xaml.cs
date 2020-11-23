using MIIOT.Drivers;
using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
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

namespace MIIOT
{
    /// <summary>
    /// COMBaudRateControl.xaml 的交互逻辑
    /// </summary>
    public partial class COMBaudRateControl : UserControl, INotifyPropertyChanged
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

        public delegate void DelDelete(COMBaudRateControl control);
        public event DelDelete OnDelete;
        private bool _isExReadOnly;

        public bool IsExReadOnly
        {
            get { return _isExReadOnly; }
            set { _isExReadOnly = value;    
                OnPropertyChanged("IsExReadOnly");
            }
        }
        private MacPara macPara =new MacPara() ;

        public MacPara MacPara
        {
            get { return macPara; }
            set { macPara = value;
                OnPropertyChanged("MacPara");
            }
        }
        private ObservableCollection<SysPara> _sysParas=new ObservableCollection<SysPara>();

        public ObservableCollection<SysPara> sysParas
        {
            get { return _sysParas; }
            set {
                _sysParas = value;
                OnPropertyChanged("sysParas");
            }
        }
        private string _ParaTitle1="COM口：";

        public string ParaTitle1
        {
            get { return _ParaTitle1; }
            set { _ParaTitle1 = value;
                OnPropertyChanged("ParaTitle1");
            }
        }
        private string _ParaTitle2 = "波特率：";

        public string ParaTitle2
        {
            get { return _ParaTitle2 ; }
            set
            {
                _ParaTitle2 = value;
                OnPropertyChanged("ParaTitle2");
            }
        }
        public COMBaudRateControl()
        {
            InitializeComponent();
            this.DataContext = this;
            string[] ArryPort = SerialPort.GetPortNames();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            OnDelete?.Invoke(this);
        }
    }
}

using MaterialDesignThemes.Wpf.Transitions;
using Newtonsoft.Json;
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

namespace MIIOT.Trical.View
{
    /// <summary>
    /// THView.xaml 的交互逻辑
    /// </summary>
    public partial class THView : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<HMInfo> _HMInfos = new ObservableCollection<HMInfo>();

        public ObservableCollection<HMInfo> HMInfos
        {
            get { return _HMInfos; }
            set
            {
                _HMInfos = value;
                OnPropertyChanged("HMInfos");
            }
        }
        public THView()
        {
            InitializeComponent();
            this.DataContext = this;
            try
            {
                var Macs = CacheData.Ins.JsonConfig["MacCodes"];
                dynamic mac = JsonConvert.DeserializeObject(Macs);
                foreach (var item in mac)
                {
                    string macID = item.MacID;
                    string macName = item.MacName;
                    HMInfos.Add(new HMInfo() { MacId = macID, Name = macName, Humi = 0, Temp = 0, IsConnect = "等待连接" });
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
          
        }
        public void Refreshdata(string MacId, bool isConnect)
        {
            foreach (var item in HMInfos)
            {
                if (item.MacId == MacId)
                {
                    if (isConnect && item.IsConnect == "等待连接")
                    {
                        return;
                    }
                    if (!isConnect)
                    {
                        item.IsConnect = "未连接";
                        item.Temp = 0;
                        item.Humi = 0;
                    }
                    item.IsConnect = isConnect ? "连接成功" : "未连接";
                }
            }
        }
        public void AddLog(string log)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtlog.AppendText(DateTime.Now.ToString("HH:mm:ss.fff>>")+ log);
                txtlog.AppendText(Environment.NewLine);
                txtlog.ScrollToEnd();
            });
        }
        public void ReFreshTH(string MacId, double temp, double humi)
        {
            foreach (var item in HMInfos)
            {
                if (item.MacId == MacId)
                {
                    item.IsConnect = "连接成功";
                    item.Temp = temp;
                    item.Humi = humi;
                }
            }
        }
    }
    public class HMInfo : INotifyPropertyChanged
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
        private string _macId;

        public string MacId
        {
            get { return _macId; }
            set
            {
                _macId = value;
                OnPropertyChanged("MacId");
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private double _temp;

        public double Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChanged("Temp");
            }
        }
        private double _Humi;

        public double Humi
        {
            get { return _Humi; }
            set
            {
                _Humi = value;
                OnPropertyChanged("Humi");
            }
        }
        private string _isConnect;

        public string IsConnect
        {
            get { return _isConnect; }
            set
            {
                _isConnect = value;
                OnPropertyChanged("IsConnect");
            }
        }


    }
}

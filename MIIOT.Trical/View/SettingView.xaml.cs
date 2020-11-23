using MIIOT.Model;
using MIIOT.Trical.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace MIIOT.Trical.View
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<RefundInfo> _RefundItem = new ObservableCollection<RefundInfo>();

        public ObservableCollection<RefundInfo> RefundItem
        {
            get { return _RefundItem; }
            set
            {
                _RefundItem = value;
                OnPropertyChanged("RefundItem");
            }
        }
        public SettingView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string target = CacheData.Ins.MasterTCP;
            if (Screen.SelectedIndex == 1)
            {
                target = CacheData.Ins.SlaveTCP;
            }
            string Operation = "ADD";
            if (add2rm.SelectedIndex == 1)
            {
                Operation = "RM";
            }
            var types = msgtype.SelectedItem.ToString();
            string MsgType = types.Substring(types.IndexOf("_") + 1, types.Length - 1 - types.IndexOf("_"));
            var Data = Newtonsoft.Json.JsonConvert.DeserializeObject(msgcontent.Text);
            if (CacheData.Ins.Screens[target].Length == 0)
            {
                MainWindow.msgQueue.Enqueue(target + "未连接");
                return;
            }
            ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[target], target, Operation, MsgType, Data.ToString());
        }



        public void Receive(string msg)
        {
            Dispatcher.Invoke(() =>
            {
                reccontent.Text = msg;
            });
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            CacheData.Ins.WSClient.SendMessage(msgcontent.Text);

        }
        public void Addline(string text)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtlog.AppendText(DateTime.Now.ToString("HH:mm:ss.fff>>")+ text);
                txtlog.AppendText(Environment.NewLine);
            });
        }
    }
}

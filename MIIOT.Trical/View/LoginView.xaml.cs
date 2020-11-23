using MaterialDesignThemes.Wpf;
using MIIOT.Model.Trical;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
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
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl, INotifyPropertyChanged
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
        private bool _CanClosed = false;

        public bool CanClosed
        {
            get { return _CanClosed; }
            set
            {
                _CanClosed = value;
                OnPropertyChanged("CanClosed");
            }
        }
        public LoginView(bool canClosed)
        {
            InitializeComponent();
            this.DataContext = this;
            CanClosed = canClosed;
            this.Loaded += LoginView_Loaded;
        }

        private void LoginView_Loaded(object sender, RoutedEventArgs e)
        {
            var sysPara = Drivers.LiteDBHelper.Ins.GetCollection<Model.SysPara>().FirstOrDefault(a => a.ParaKey == "UserNameCache");
            if (sysPara != null)
            {
                string[] list = sysPara.ParaValue.Split(',');
                foreach (var item in list)
                {
                    txtUserName.Items.Add(item);
                    txtUserName.SelectedIndex = 0;
                }
            }
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string url = CacheData.Ins.JsonConfig.GetValue("Login");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("username", txtUserName.Text);
            dic.Add("password", txtPassword.Password);
            var temop = await httpClientHelper.PostRequest(url, dic, "", "");
            HttpResultBase result = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpResultBase>(temop.data);
            if (result != null && result.code == 0)
            {
                UserNameCache(txtUserName.Text);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(result.data.ToString());
                if (data != null)
                {
                    CacheData.Ins.Token = data.token;
                    url = CacheData.Ins.JsonConfig.GetValue("UserInfo");
                    temop = await httpClientHelper.GetRequest(url, CacheData.Ins.Token);
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpResultBase>(temop.data);
                    if (result != null && result.code == 0)
                    {
                        CacheData.Ins._user_info = Newtonsoft.Json.JsonConvert.DeserializeObject<user_info>(result.data.ToString());
                    }
                    DialogHost.CloseDialogCommand.Execute("", this);
                }
            }

        }
        HttpClientHelper httpClientHelper = new HttpClientHelper();
        private void UserNameCache(string userName)
        {
            var liststr = Drivers.LiteDBHelper.Ins.GetCollection<Model.SysPara>().FirstOrDefault(a => a.ParaKey == "UserNameCache");
            if (liststr != null)
            {
                var list = liststr.ParaValue.Split(',').ToList();
                if (list.Contains(userName))
                {
                    list.Remove(userName);
                }
                list.Insert(0, userName);
                while (list.Count > 5)
                {
                    list.RemoveAt(list.Count - 1);
                }
                liststr.ParaValue = string.Join(",", list.ToArray());
                MIIOT.Drivers.LiteDBHelper.Ins.Update<MIIOT.Model.SysPara>(liststr);
            }
            else
            {
                Model.SysPara sysPara = new Model.SysPara() { ParaType = "Para", ParaKey = "UserNameCache", ParaValue = userName };
                MIIOT.Drivers.LiteDBHelper.Ins.Insert<MIIOT.Model.SysPara>(sysPara);
            }
        }
    }
}

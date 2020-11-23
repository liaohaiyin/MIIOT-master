using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.DataSync;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace MIIOT.ORCart
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow
    {

        JsonConfigHelper JsonUserCache = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\userCache.json");
        JsonConfigHelper JsonConfig = new JsonConfigHelper(System.Windows.Forms.Application.StartupPath + "\\Config\\config.json");
        public LoginWindow()
        {
            InitializeComponent();

            string DBStr = JsonConfig["DBStr"];
            bool DBsucc = new DapperUtil(DBStr).TestConnected();//数据库连接
            if (DBsucc)
                tab.SelectedIndex = 1;
            else
                tab.SelectedIndex = 0;

            txtspdHost.Text = JsonConfig["Host"];
            string[] dbParas = DBStr.Split(';');
            foreach (var item in dbParas)
            {
                if (item.Contains("Data Source"))
                {
                    txtDBHost.Text = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
                if (item.Contains("Initial Catalog"))
                {
                    txtDBName.Text = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
                if (item.Contains("User ID"))
                {
                    txtDBuserName.Text = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
                if (item.Contains("Password"))
                {
                    txtDBPassword.Password = item.Substring(item.IndexOf("=") + 1, item.Length - item.IndexOf("=") - 1);
                }
            }
            this.Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoginViewinit();
        }

        private void btnClose_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnSetting_Click(object sender, MouseButtonEventArgs e)
        {

            tab.SelectedIndex = 0;

            return;

        }
        private async Task<sys_user> NetLogin()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["登录"];
                var data = new { username = txtUserName.Text, password = txtPassword.Password };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        spd_user User = JsonConvert.DeserializeObject<spd_user>(res.data.ToString());
                        sys_user dbUser = new sys_user()
                        {
                            id = User.accountid,
                            user_name = User.accountcode,
                            password = txtPassword.Password.MD5Encrypt(),
                            display_name = User.accountname,
                            user_code = User.accountcode,
                            user_department_id = User.departmentid,
                            user_source_id = User.accountid,
                            sotcks = string.Join(",", User.storehouseid),
                            role = User.roletype ?? 0
                        };
                        CacheData.Ins.dbUtil.Replace(dbUser);
                        return dbUser;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return null;
        }
        public void LoginViewinit()
        {
            txtPassword.Password = "";
            txtUserName.Items.Clear();
            string liststr = JsonUserCache["UserNameCache"];
            if (liststr.Length > 0)
            {
                string[] list = liststr.Split(',');
                foreach (var item in list)
                {
                    txtUserName.Items.Add(item);
                    txtUserName.SelectedIndex = 0;
                }
            }
        }
        private async Task<bool> DoLogin()
        {
            try
            {
#if Net
                sys_user UserID = await NetLogin();
                if (UserID != null)
                {
                    UserNameCache(txtUserName.Text);
                    CacheData.Ins.User = UserID;
                    txtErr.Visibility = Visibility.Collapsed;
                    return true;
                }
#endif

                var user = CacheData.Ins.dbUtil.Query<sys_user>("SELECT * from sys_user where user_code=@user_code and `password`=@password",
                    new { user_code = txtUserName.Text, password = txtPassword.Password.MD5Encrypt() });
                if (user != null && user.Count > 0)
                {
                    UserNameCache(txtUserName.Text);

                    CacheData.Ins.User = user[0];
                    txtErr.Visibility = Visibility.Collapsed;
                    return true;
                }
                else
                {
                    txtErr.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Login", ex);
            }
            return false;
        }
        private void UserNameCache(string userName)
        {
            try
            {
                string liststr = CacheData.Ins.JsonUserCache["UserNameCache"];
                if (liststr.Length > 0)
                {
                    var list = liststr.Split(',').ToList();
                    if (list.Contains(userName))
                    {
                        list.Remove(userName);
                    }
                    list.Insert(0, userName);
                    while (list.Count > 5)
                    {
                        list.RemoveAt(list.Count - 1);
                    }
                    liststr = string.Join(",", list.ToArray());
                    CacheData.Ins.JsonUserCache.SetValue("UserNameCache", liststr);
                }
                else
                {
                    CacheData.Ins.JsonUserCache.SetValue("UserNameCache", userName);
                }
            }
            catch (Exception ex)
            {
                Log.Error("UserNameCache", ex);
            }
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (await DoLogin())
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.LoginWindow = this;
                mainWindow.ShowDialog();
            }
        }

        private async void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (await DoLogin())
                {
                    this.Hide();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.LoginWindow = this;
                    mainWindow.ShowDialog();
                }
            }

        }

        private void btnTestConnected_Click(object sender, RoutedEventArgs e)
        {
            string DBstr = $"Data Source={txtDBHost.Text};Initial Catalog={txtDBName.Text};Persist Security Info=True;User ID={txtDBuserName.Text};Password={txtDBPassword.Password}";
            JsonConfig.Update("DBStr", DBstr);
            JsonConfig.Update("Host", txtspdHost.Text);

            string dbstr = JsonConfig["DBStr"];
            bool DBsucc = new DapperUtil(dbstr).TestConnected();//数据库连接
            if (DBsucc)
            {
                ShowTip("数据库连接成功");

                tab.SelectedIndex = 1;
            }
            else
            {
                ShowTip("数据库连接失败，请检查参数");
                return;
            }
        }
        private void ShowTip(string text)
        {
            txtmsg.Text = text;
            txtmsg.Visibility = Visibility.Visible;
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(() =>
                {
                    txtmsg.Text = "";
                    txtmsg.Visibility = Visibility.Collapsed;
                });
            });
        }
    }
}

using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.ViewModels;
using MIIOT.UI.ControlEx;
using MIIOT.UI.Controls;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.DiagManager
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : NbWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Waiting();
                Thread.Sleep(new Random().Next(500, 1000));
                var result = AppRuntime.Login(GetLoginName(), GetPassword());
                if (result.IsSuccess)
                {
                    SaveLoginRecord();
                    LoginSuccess();
                }
                else
                {
                    NbMessageBox.Error(result.Message);
                }
                UnWaiting();
            });
        }

        private void Waiting()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.LoginButton.Content = string.Empty;
                this.LoginButton.IsEnabled = false;
                this.LoginNameBox.IsEnabled = false;
                this.PasswordBox.IsEnabled = false;
                this.RememberPassword.IsEnabled = false;
            });
        }
        private void UnWaiting()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.LoginButton.Content = "登录";
                this.LoginButton.IsEnabled = true;
                this.LoginNameBox.IsEnabled = true;
                this.PasswordBox.IsEnabled = true;
                this.RememberPassword.IsEnabled = true;
            });
        }

        private void LoginSuccess()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.DialogResult = true;
                this.Close();
            });
        }

        private void SaveLoginRecord()
        {
            this.Dispatcher.Invoke(() =>
            {
                (this.DataContext as LoginVm)?.SaveRecord();
            });
        }

        private string GetLoginName()
        {
            var ret = string.Empty;
            this.Dispatcher.Invoke(() =>
            {
                ret = this.LoginNameBox.Text ?? string.Empty;
            });
            return ret;
        }
        private string GetPassword()
        {
            var ret = string.Empty;
            this.Dispatcher.Invoke(() =>
            {
                ret = this.PasswordBox.Password ?? string.Empty;
            });
            return ret;
        }
        private bool GetRemember()
        {
            var ret = false;
            this.Dispatcher.Invoke(() =>
            {
                ret = this.RememberPassword.IsChecked.HasValue ? this.RememberPassword.IsChecked.Value : false;
            });
            return ret;
        }
    }
}

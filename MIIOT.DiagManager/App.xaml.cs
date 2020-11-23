using MIIOT.DiagManager.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.DiagManager
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            while (true)
            {
                AppRuntime.IsLogout = false;
                LoginWindow lw = new LoginWindow();
                if (lw.ShowDialog() == true)
                {
                    Application.Current.MainWindow = new MainWindow();
                    Application.Current.MainWindow.ShowDialog();
                }
                if (AppRuntime.IsLogout == false)
                    break;
            }

            Application.Current.Shutdown();
        }
    }
}

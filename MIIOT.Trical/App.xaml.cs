using MIIOT.Drivers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.Trical
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            LiteDBHelper.Ins.InitDB("Trical", System.Windows.Forms.Application.StartupPath);
            Startup += App_Startup;
            IsRun();
            CheckProcess("MIIOT.Guarder");
        }
        private void SyncConfig()
        {
            try
            {
                string configFileSource = "";
                string path = System.Windows.Forms.Application.StartupPath;
                string[] urls = path.Split('\\');
                for (int i = 0; i < urls.Length - 3; i++)
                {
                    configFileSource += urls[i] + "\\";
                }
                configFileSource += "\\MIIOT.Model\\Config\\config.json";
                string configFileTarget = System.Windows.Forms.Application.StartupPath + "\\config.json";
                if (File.Exists(configFileSource))
                    File.Copy(configFileSource, configFileTarget, true);
            }
            catch (Exception ex)
            {
                Log.Error("SyncConfig", ex);
            }
        }
        /// <summary>
        /// 判断是否已经启动一个程序
        /// </summary>
        private void IsRun()
        {
            //string configFileTarget = "";
            //string configFileSource = System.Windows.Forms.Application.StartupPath+ "/appsettings.json";
            //if (File.Exists(configFileSource))
            //    File.Copy(configFileSource, configFileTarget);


            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            ////获取版本号 
            //CommonData.VersionNumber = Application.ProductVersion; 
            //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                MessageBox.Show("系统已经运行！", "消息", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }
        private void CheckProcess(string prName)
        {
            try
            {
                bool isRun = false;
                string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                //string prName = Path.Substring(Path.LastIndexOf('\\') + 1, Path.Length - Path.LastIndexOf('\\') - 1);

                foreach (System.Diagnostics.Process process in processList)
                {
                    if (process.ProcessName == prName)
                    {
                        isRun = true;
                    }
                }
                if (!isRun)
                {
                    string path = Environment.CurrentDirectory + @"\" + prName + ".exe";
                    System.Diagnostics.Process MainProcess = System.Diagnostics.Process.Start(path);
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 程序启动时，注册以下事件 捕获系统异常，导致灾难性错误的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题.." + e.Exception,
            //    "MIIOT.Trical:意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作
            e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。

            //记录日志
            Log.Error("系统全局异常：", e.Exception);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题.请联系管理员." + e.ExceptionObject, "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);

            //记录日志,MessageShow 发布版本时可以先取消掉
            Log.Error("系统全局异常：", (Exception)e.ExceptionObject);
        }
    }
}

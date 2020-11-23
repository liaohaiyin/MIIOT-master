using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using YDHCG.Common;

namespace MIIOT.Guarder
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Task.Run(() =>
            {
                try
                {
                    string path = Environment.CurrentDirectory;
                    while (true)
                    {
                        string[] dirstr = Directory.GetFiles(path);
                        foreach (var item in dirstr)
                        {
                            if (item.Substring(item.Length - 4, 4) == ".exe")
                            {
                                CheckProcess(item);
                            }
                        }
                        Thread.Sleep(10000);
                    }
                }
                catch (Exception ex)
                {


                }
            });
            IsRun();
        }
        private void IsRun()
        {
            //string configFileTarget = "";
            //string configFileSource = System.Windows.Forms.Application.StartupPath+ "/appsettings.json";
            //if (File.Exists(configFileSource))
            //File.Copy(configFileSource, configFileTarget);


            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;

            ////获取版本号 
            //CommonData.VersionNumber = Application.ProductVersion; 
            //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
            {
                //MessageBox.Show("系统已经运行！", "消息", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }
        private void CheckProcess(string Path)
        {
            try
            {
                bool isRun = false;
                System.Diagnostics.Process Pro = System.Diagnostics.Process.GetCurrentProcess();
                string strProcessName = Pro.ProcessName;
                System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
                string prName = Path.Substring(Path.LastIndexOf('\\') + 1, Path.Length - Path.LastIndexOf('\\') - 1);

                foreach (System.Diagnostics.Process process in processList)
                {
                    if (process.ProcessName == prName.Substring(0, prName.LastIndexOf('.')))
                    {
                        if (process.Id != Pro.Id && string.IsNullOrWhiteSpace(process.MainWindowTitle))
                        {
                            process.Kill();
                        }
                        else
                            isRun = true;
                    }
                }
                if (!isRun)
                {
                    string path = Environment.CurrentDirectory;
                    System.Diagnostics.Process MainProcess = System.Diagnostics.Process.Start(Path);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}







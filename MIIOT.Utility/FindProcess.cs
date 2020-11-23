using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YDHCG.Common
{
    public static class FindProcess
    {
        #region 显示程序
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        /// <summary> 
        /// 该函数设置由不同线程产生的窗口的显示状态。 
        /// </summary> 
        /// <param name="hWnd">窗口句柄</param> 
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分。</param> 
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary> 
        /// 该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary> 
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄。</param> 
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零。</returns> 
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;

        static Process process = null;
        static IntPtr appWin;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);


        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        #endregion
        private static bool FindForeProcess(string winName)
        {
            try
            {
                if (!string.IsNullOrEmpty(winName))
                {
                    int hWnd = FindWindow(null, winName);
                    if (hWnd == 0)
                    {
                        return false;
                    }
                    else
                    {
                        IntPtr p = new IntPtr(hWnd);
                        //存在
                        SetForegroundWindow(p);

                        ShowWindowAsync(p, WS_SHOWNORMAL); //显示 
                        SetForegroundWindow(p);            //放到前端 
                        return true;
                    }
                }
                else
                {
                    Log.Debug("winName is null");
                }
            }
            catch (Exception ex)
            {
                Log.Error("FindForeProcess", ex);
            }
            return false;
        }
        public static void IsRun()
        {
            Process process = System.Diagnostics.Process.GetCurrentProcess();
            string strProcessName = process.ProcessName;

            ////获取版本号 
            //CommonData.VersionNumber = Application.ProductVersion; 
            //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
            var processs = Process.GetProcessesByName(strProcessName);
            if (processs.Length > 1)
            {
                foreach (var item in processs)
                {
                    if (string.IsNullOrWhiteSpace(item.MainWindowTitle) && item.Id != process.Id)
                    {
                        item.Kill();
                    }
                    else if (!string.IsNullOrWhiteSpace(item.MainWindowTitle))
                    {
                        bool runFore = FindForeProcess(item.MainWindowTitle);
                        Environment.Exit(0);
                    }
                }
            }
        }

        public static void CheckProcess(string prName)
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
    }
}

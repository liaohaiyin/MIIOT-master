using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Trical
{
    public class MessageHelper
    {
        private static object obj = new object();
        private static MessageHelper _ins;

        public static MessageHelper Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                    {
                        _ins = new MessageHelper();
                    }

                }
                return _ins;
            }
        }

      
        public void SendMsg(string ProcessName, string Data)
        {
            // 枚举进程
            Process[] procs = Process.GetProcesses();
            foreach (Process p in procs)
            {              
                if (p.ProcessName.Equals(ProcessName))
                {                  
                    // 获取目标进程句柄
                    IntPtr hWnd = p.MainWindowHandle;
                    // 封装消息
                    byte[] sarr = System.Text.Encoding.Default.GetBytes(Data);
                    int len = sarr.Length;
                    COPYDATASTRUCT cds2;
                    cds2.dwData = (IntPtr)0;
                    cds2.cbData = len + 1;
                    cds2.lpData = Data;
                    // 发送消息
                    SendMessage(hWnd, 0x004A, IntPtr.Zero, ref cds2);
                }
            }
        }

        public void SendMsg(IntPtr hWnd, string Data)
        {
            // 封装消息
            byte[] sarr = System.Text.Encoding.Default.GetBytes(Data);
            int len = sarr.Length;
            COPYDATASTRUCT cds2;
            cds2.dwData = (IntPtr)0;
            cds2.cbData = len + 1;
            cds2.lpData = Data;
            // 发送消息
            SendMessage(hWnd, 0x004A, IntPtr.Zero, ref cds2);
        }
        // 导出SendMessage函数
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref COPYDATASTRUCT lParam);
        // 导出FindWindow函数，用于找到目标窗口所在进程
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr wnd, int msg, IntPtr wP, IntPtr lP);
    }
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData; // 任意值
        public int cbData;    // 指定lpData内存区域的字节数
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData; // 发送给目标窗口所在进程的数据
    }
}

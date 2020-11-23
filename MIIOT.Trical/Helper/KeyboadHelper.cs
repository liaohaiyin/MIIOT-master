using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Trical
{
    public class KeyboadHelper
    {
        public static int ShowInputPanel()
        {
            try
            {
                var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                            select x.GetPropertyValue("Caption")).FirstOrDefault();
                string file = @"C:\Windows\System32\osk.exe";
                if (name.ToString().Contains("Windows 10"))
                {
                    file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                }
                if (!System.IO.File.Exists(file))
                    return -1;
                System.Diagnostics.Process.Start(file);
                //return SetUnDock(); //不知SetUnDock()是什么，所以直接注释返回1
                return 1;
            }
            catch (Exception ex)
            {
                return 255;
            }
        }
    }
}

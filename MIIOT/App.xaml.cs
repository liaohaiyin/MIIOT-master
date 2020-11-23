using MIIOT.Drivers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            LiteDBHelper.Ins.InitDB("FaceDB", System.Windows.Forms.Application.StartupPath);
        }
    }
}

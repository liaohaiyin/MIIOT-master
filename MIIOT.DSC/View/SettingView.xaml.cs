using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MIIOT.Clinical.View
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
            this.Loaded += SettingView_Loaded;
            var web = new ChromiumWebBrowser("http://120.24.255.231:8080/app");
            grid.Children.Add(web);
        }

        private void SettingView_Loaded(object sender, RoutedEventArgs e)
        {

         
        }
        public static int Add(int pNum1, int pNum2)
        {
            return pNum1 + pNum2;
        }
        test _te = new test();
        public int puls(int te)
        {
            //switch (te)
            //{
            //    case 1:
            //        return 11;
            //    case 2:
            //        return 21;
            //    case 3:
            //        return 4;
            //    default:
            //        break;
            //}
            return 0;
        }
    }
    public class test
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}

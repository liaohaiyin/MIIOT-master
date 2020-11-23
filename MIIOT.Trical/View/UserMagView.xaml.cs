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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.View
{
    /// <summary>
    /// UserMagView.xaml 的交互逻辑
    /// </summary>
    public partial class UserMagView : UserControl
    {
        public UserMagView()
        {
            InitializeComponent();
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        this.Dispatcher.Invoke(() => { 
            //            pb1.Value = pb1.Value + 1 > pb1.Maximum ? 0 : pb1.Value + 1;
            //        });
            //        Thread.Sleep(100);
            //    }
            //});
            this.Loaded += UserMagView_Loaded;
        }

        private void UserMagView_Loaded(object sender, RoutedEventArgs e)
        {
            Common.CamanerHelper camaner = new Common.CamanerHelper();
            var handle = RealPlayWnd.Handle;
            camaner.Start((int)handle);

        }
    }
}

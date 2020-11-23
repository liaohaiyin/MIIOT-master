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

namespace MIIOT.Trical.Controls
{
    /// <summary>
    /// AskView.xaml 的交互逻辑
    /// </summary>
    public partial class AskView : UserControl
    {
        public delegate void delCallBack(object sender,bool Succeed);
        public event delCallBack DoCallBack;
        public AskView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoCallBack?.Invoke(this, true);
        }
    }
}

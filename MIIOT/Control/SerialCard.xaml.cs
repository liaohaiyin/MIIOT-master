using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MIIOT.Control
{
    /// <summary>
    /// SerialCard.xaml 的交互逻辑
    /// </summary>
    public partial class SerialCard : BaseConnectControl
    {

        public SerialCard()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            base.DoDelete();
        }
        public override void UpdateStatus(bool isConnect)
        {
            base.UpdateStatus(isConnect);
            if (isConnect)
            {
                this.card.Background = new SolidColorBrush(Colors.LightGreen);
            }
            else
                this.card.Background = new SolidColorBrush(Color.FromRgb(245, 183, 177));
        }
    }
}

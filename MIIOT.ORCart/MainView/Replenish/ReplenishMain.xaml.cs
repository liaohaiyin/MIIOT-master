using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Controls;
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

namespace MIIOT.ORCart.MainView.Replenish
{
    /// <summary>
    /// ReplenishMain.xaml 的交互逻辑
    /// </summary>
    public partial class ReplenishMain : UserControl, IMenuMsgSend
    {
        ReplenishmentView replenishmentView = new ReplenishmentView();
        ReplenishView replenishView = new ReplenishView();
        public ReplenishMain()
        {
            InitializeComponent();
            this.Loaded += ReplenishMain_Loaded;

         
            replenishView.Ment = replenishmentView;
            tra.Content = replenishView;

            tras.Content = replenishmentView;

        }

        private void ReplenishMain_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void sendMsg(string code,string MsgType)
        {
            if (trar.SelectedValue is  TransitionerSlide Slide)
            {
                if (Slide.Name == "tra")
                {
                    replenishView.SendMsg(code);
                }
                else
                {
                    replenishmentView.SendMsg(code);
                }
            }
        }
        public void MenuSelected(string menuName)
        {
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}

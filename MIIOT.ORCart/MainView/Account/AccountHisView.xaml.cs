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

namespace MIIOT.ORCart.MainView
{
    /// <summary>
    /// AccountHisView.xaml 的交互逻辑
    /// </summary>
    public partial class AccountHisView : UserControl,IMenuMsgSend
    {
        private SurgeryView _surgery;

        public SurgeryView surgery
        {
            get { return _surgery; }
            set
            {
                if (value != null)
                {
                    SurgeryView surgery = new SurgeryView();
                    grid.Children.Add(surgery);
                    surgery.DoSelect += SurgeryView_DoSelect;
                }
                _surgery = value;
            }
        }
        public delegate void DelBack();
        public event DelBack OnBackClick;
        public AccountHisView()
        {
            InitializeComponent();
            this.Loaded += AccountHisView_Loaded;
        }

        private void AccountHisView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void sendMsg(string code)
        {

        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            OnBackClick?.Invoke();
        }

        private void SurgeryView_DoSelect(Model.pub_surgery pub_Surgery)
        {

        }

        public void MenuSelected(string menuName)
        {
        }
    }
}

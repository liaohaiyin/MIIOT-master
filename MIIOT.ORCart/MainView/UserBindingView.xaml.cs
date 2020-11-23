using MIIOT.Model;
using MIIOT.ORCart.Controls;
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
    /// UserBindingView.xaml 的交互逻辑
    /// </summary>
    public partial class UserBindingView : UserControl, IMenuMsgSend
    {
        public UserBindingView()
        {
            InitializeComponent();
            this.Loaded += UserBindingView_Loaded;
        }

        private void UserBindingView_Loaded(object sender, RoutedEventArgs e)
        {
            panelUsers.Children.Clear();
            List<sys_user> Users = CacheData.Ins.dbUtil.Query<sys_user>("select * from sys_user", null);
            foreach (var item in Users)
            {
                UserCard UC = new UserCard() { Margin=new Thickness(10)};
                UC.User = item;
                panelUsers.Children.Add(UC);
            }

        }

        public void MenuSelected(string menuName)
        {
        }

        public void sendMsg(string code, string MsgType = "")
        {


        }
        public void GridFilter(string ParaStr)
        {

        }

    }
}

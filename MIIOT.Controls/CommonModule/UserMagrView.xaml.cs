using MaterialDesignThemes.Wpf;
using MIIOT.Utility;
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
using MIIOT.Model;
using MIIOT.ORCart;

namespace MIIOT.Controls.CommonModule
{
    /// <summary>
    /// UserMagrView.xaml 的交互逻辑
    /// </summary>
    public partial class UserMagrView : UserControl, INotifyPropertyChanged,IMenuMsgSend
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        #endregion
        private ObservableCollection<sys_user> _Users = new ObservableCollection<sys_user>();

        public ObservableCollection<sys_user> Users
        {
            get
            {
                return _Users;
            }
            set
            {
                _Users = value;
                OnPropertyChanged("Users");
            }
        }
        private sys_user _SelectUser;

        public sys_user SelectUser
        {
            get { return _SelectUser; }
            set
            {
                _SelectUser = value;
                OnPropertyChanged("SelectUser");
            }
        }
        private ObservableCollection<sys_menu> _menu = new ObservableCollection<sys_menu>();

        public ObservableCollection<sys_menu> Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                OnPropertyChanged("Menu");
            }
        }


        private ObservableCollection<sys_role> _Roles = new ObservableCollection<sys_role>();

        public ObservableCollection<sys_role> Roles
        {
            get
            {
                return _Roles;
            }
            set
            {
                _Roles = value;
                OnPropertyChanged("Roles");
            }
        }
        private sys_role _SelectedRole;

        public sys_role SelectedRole
        {
            get { return _SelectedRole; }
            set
            {



                _SelectedRole = value;
                OnPropertyChanged("SelectedRole");
                if (value != null)
                {
                    RefreshTree();
                }
            }
        }
        DapperUtil DBUtil ;
        string _dbstr;
        public UserMagrView(string dbstr)
        {
            _dbstr = dbstr;
            DBUtil = new DapperUtil(dbstr);
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += UserMagrView_Loaded;
        }

        private void UserMagrView_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Users.Clear();
                    Roles.Clear();
                    Menu.Clear();
                    Users.AddRange(DBUtil.Query<sys_user>("select * from sys_user", null));
                    Roles.AddRange(DBUtil.Query<sys_role>("select * from sys_role", null));
                });
            });


        }
        public void RefreshTree()
        {
            Menu.Clear();
            var menus = DBUtil.Query<sys_menu>("select * from sys_menu", null);
            var RoleMenu = DBUtil.Query<sys_role_menu>("select * from sys_role_menu", null);

            foreach (var item in menus)
            {
                if (SelectedRole != null)
                {
                    var temp = RoleMenu.FirstOrDefault(a => a.role_id == SelectedRole.role_id && a.menu_id == item.id);
                    if (temp != null)
                        item.IsChecked = true;
                }
                if (item.menu_parent_id == 0)
                {
                    Menu.Add(item);
                    TreeGre(menus, item);
                }
            }
            // tvProperties.ItemsSource = Menu;
        }

        public void TreeGre(List<sys_menu> Menus, sys_menu menu)
        {
            var temp = Menus.FindAll(a => a.menu_parent_id == menu.id);
            if (temp == null)
            {
                return;
            }
            else
            {
                menu.ChildMenu.AddRange(temp);
                foreach (var item in temp)
                {
                    TreeGre(Menus, item);
                }

            }

        }
        private void btnUserDelDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void btnUserDelEdit_Click(object sender, RoutedEventArgs e)
        {
            var supply = new UserBindingRoleView(_dbstr);
            supply.rolesAll = this.Roles.ToList();
            supply.userid = SelectUser.id;
            var CanClose = await DialogHost.Show(supply, "RootDialog");
            if ((bool)CanClose == false)
            {

            }
        }

        private void mygird_DragLeave(object sender, DragEventArgs e)
        {

        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Tag is sys_menu menu)
                {
                    DBUPdate(menu, checkBox.IsChecked == true);
                    foreach (var item in menu.ChildMenu)
                    {
                        item.IsChecked = checkBox.IsChecked == true;
                        DBUPdate(item, checkBox.IsChecked == true);
                    }
                }
            }
        }
        public void DBUPdate(sys_menu menu, bool isChecked)
        {
            if (isChecked)
            {
                var roleMenu = new sys_role_menu() { role_id = SelectedRole.role_id, menu_id = menu.id };
                DBUtil.Insert(roleMenu);
            }
            else
            {
                DBUtil.Delete("DELETE from sys_role_menu where role_id=@role_id and menu_id=@menu_id", new { SelectedRole.role_id, menu_id = menu.id });
            }
        }

        public void sendMsg(string code,string MsgType)
        {

        }

        public void MenuSelected(string menuName)
        {
        }
    }
}

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

namespace MIIOT.Controls.CommonModule
{
    /// <summary>
    /// UserBindingRoleView.xaml 的交互逻辑
    /// </summary>
    public partial class UserBindingRoleView : UserControl, INotifyPropertyChanged
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
        DapperUtil dbUtil;
        private ObservableCollection<sys_role> _roles = new ObservableCollection<sys_role>();
        public string userid { get; set; }
        public ObservableCollection<sys_role> Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
                OnPropertyChanged("Roles");
            }
        }
        private ObservableCollection<sys_role> _SelectRoles = new ObservableCollection<sys_role>();

        public ObservableCollection<sys_role> SelectRoles
        {
            get { return _SelectRoles; }
            set
            {
                _SelectRoles = value;
                OnPropertyChanged("SelectRoles");
            }
        }
        private sys_role _Role;

        public sys_role Sys_Role
        {
            get { return _Role; }
            set
            {
                _Role = value;
                OnPropertyChanged("Sys_Role");
            }
        }
        private sys_role _chkRole;

        public sys_role ChkRole
        {
            get { return _chkRole; }
            set { _chkRole = value;
                OnPropertyChanged("ChkRole");
            }
        }
        public List<sys_role> rolesAll { get; set; } = new List<sys_role>();

        List<sys_user_role> user_Roles = new List<sys_user_role>();
        public UserBindingRoleView(string dbstr)
        {
            dbUtil = new DapperUtil(dbstr);
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += UserBindingRoleView_Loaded;
        }

        private void UserBindingRoleView_Loaded(object sender, RoutedEventArgs e)
        {
            user_Roles = dbUtil.Query<sys_user_role>("select * from sys_user a, sys_user_role b where a.id=b.user_id and a.id=@id", new { id = userid });
       
            foreach (var item in rolesAll)
            {
                var temp = user_Roles.FirstOrDefault(a=>a.role_id==item.role_id);
                if (temp==null)
                {
                    Roles.Add(item);
                }
                else
                    SelectRoles.Add(item);
            }

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Sys_Role == null) return;
            sys_user_role userRole = new sys_user_role();
            userRole.user_id = userid;
            userRole.role_id = Sys_Role.role_id;
            dbUtil.Insert(userRole);
            SelectRoles.Add(Sys_Role);
            Roles.Remove(Sys_Role);
        }

        private void btnRM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ChkRole == null) return;
                sys_user_role userRole = new sys_user_role();
                userRole.user_id = userid;
                userRole.role_id = ChkRole.role_id;
                dbUtil.Delete<sys_user_role>("DELETE from sys_user_role where user_id =@user_id and role_id=@role_id", userRole);

                Roles.Add(ChkRole);
                SelectRoles.Remove(ChkRole);
            }
            catch (Exception ex)
            {
                 
            }
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

       
    }
}

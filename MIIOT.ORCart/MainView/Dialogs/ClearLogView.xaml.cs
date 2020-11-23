using MIIOT.Model;
using MIIOT.Model.ORCart;
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

namespace MIIOT.ORCart.MainView.Dialogs
{
    /// <summary>
    /// ClearLogView.xaml 的交互逻辑
    /// </summary>
    public partial class ClearLogView : UserControl,INotifyPropertyChanged
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
        private ObservableCollection<cart_clear_log> _Clear_Logs = new ObservableCollection<cart_clear_log>();

        public ObservableCollection<cart_clear_log> Clear_Logs
        {
            get { return _Clear_Logs; }
            set
            {
                _Clear_Logs = value;
                OnPropertyChanged("Clear_Logs");
            }
        }
        public ClearLogView()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public void Init(cart_clear goods, List<cart_clear_log> log)
        {
            txtname.Text = goods.goods_name;
            txtno.Text = goods.goods_no;
            txtfac.Text = goods.factory_name;
            txtspec.Text = goods.goods_spec;
            Clear_Logs.AddRange(log);
        }
    }
}

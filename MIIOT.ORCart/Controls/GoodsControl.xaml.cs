using MIIOT.Model;
using System;
using System.Collections.Generic;
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

namespace MIIOT.ORCart.Controls
{
    /// <summary>
    /// GoodsControl.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsControl : UserControl, INotifyPropertyChanged
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
        private cart_goods _goods;

        public cart_goods Goods
        {
            get { return _goods; }
            set
            {
                _goods = value;
                OnPropertyChanged("Goods");
            }
        }
       


        public delegate void DelClick(cart_goods goods);
        public event DelClick OnClick;


        public GoodsControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Goods.qty++;
            OnClick?.Invoke(Goods);
        }

        private void Card_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Goods.qty+=5;
            OnClick?.Invoke(Goods);
        }
    }
}

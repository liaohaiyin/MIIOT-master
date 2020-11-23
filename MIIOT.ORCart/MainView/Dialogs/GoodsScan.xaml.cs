using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    /// GoodsScan.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsScan : UserControl, INotifyPropertyChanged
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

        public cart_goods goods
        {
            get { return _goods; }
            set
            {
                _goods = value;
                OnPropertyChanged("goods");
            }
        }
        private Visibility _isMul=Visibility.Collapsed;

        public Visibility IsMul
        {
            get { return _isMul; }
            set
            {
                _isMul = value;
                OnPropertyChanged("goods");
            }
        }

        private int _showType;

        public int ShowType
        {
            get { return _showType; }
            set
            {
                _showType = value;
                OnPropertyChanged("ShowType");
            }
        }


        public GoodsScan()
        {
            InitializeComponent();
            this.DataContext = this;
            goods = new cart_goods();
            this.Loaded += GoodsScan_Loaded;
        }

        private void GoodsScan_Loaded(object sender, RoutedEventArgs e)
        {
            txtqty.SelectAll();
        }

        private void btnNum_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button bor)
                {
                    if (bor.Tag is string tag)
                    {
                        if (tag == "back")
                        {
                            if (goods.qty.ToString().Length == 1)
                                goods.qty = 0;
                            else
                                goods.qty = int.Parse(goods.qty.ToString().Substring(0, goods.qty.ToString().Length - 1));

                        }
                        else if (tag == "clear")
                        {
                            goods.qty = 0;

                        }
                        else
                        {
                            int qty = 0;
                            int.TryParse(goods.qty.ToString() + tag, out qty);
                            goods.qty = qty;
                        }
                        txtqty.SelectionStart = txtqty.Text.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("btnNum_Click", ex);
            }
        }

        private void txtqty_TextChanged(object sender, TextChangedEventArgs e)
        {
            int a = 0;
            if (!int.TryParse(txtqty.Text, out a))
            {
                txtqty.Text = "0";
            }
            else
            {
                if (a < 0)
                    txtqty.Text = "0";
            }
        }
    }
}

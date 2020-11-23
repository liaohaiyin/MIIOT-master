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
    /// SurgeryCard.xaml 的交互逻辑
    /// </summary>
    public partial class SurgeryCard : UserControl, INotifyPropertyChanged
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
        //private int _SeqNo;

        //public int SeqNo
        //{
        //    get { return _SeqNo; }
        //    set
        //    {
        //        _SeqNo = value;
        //        OnPropertyChanged("SeqNo");
        //    }
        //}
        //private string _SName;

        //public string SName
        //{
        //    get { return _SName; }
        //    set
        //    {
        //        _SName = value;
        //        OnPropertyChanged("SName");
        //    }
        //}




        public string SeqNo
        {
            get { return (string)GetValue(SeqNoProperty); }
            set { SetValue(SeqNoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeqNo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeqNoProperty =
            DependencyProperty.Register("SeqNo", typeof(string), typeof(SurgeryCard), new PropertyMetadata("01"));



        public string SName
        {
            get { return (string)GetValue(SNameProperty); }
            set { SetValue(SNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SNameProperty =
            DependencyProperty.Register("SName", typeof(string), typeof(SurgeryCard), new PropertyMetadata("手术间"));



        public SurgeryCard()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var temp = this.Tag;
        }
    }
}

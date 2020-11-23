using MIIOT.ORCart;
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

namespace MIIOT.Controls
{
    /// <summary>
    /// MenuControl.xaml 的交互逻辑
    /// </summary>
    public partial class MenuControl : UserControl, INotifyPropertyChanged
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
        SolidColorBrush solid;
        SolidColorBrush ForegroundSolid;
        public delegate void DelSelected(object sender, string Text);
        public event DelSelected OnSelected;
        public MenuControl()
        {
            InitializeComponent();
            this.DataContext = this;
            solid = FindResource("PrimaryHueMidBrush") as SolidColorBrush;
            ForegroundSolid = FindResource("PrimaryForegroudBrush") as SolidColorBrush;
        }
        private string _menuText;

        public string MenuText
        {
            get { return _menuText; }
            set
            {
                _menuText = value;
                OnPropertyChanged("MenuText");
            }
        }
        public sys_menu Menu { get; set; }
        public IMenuMsgSend SelectedPage { get; set; }
        private int _mode;

        public int Mode
        {
            get { return _mode; }
            set
            {
                if (value == 1)
                {
                    bor.BorderThickness = new Thickness(0, 0, 0, 1);
                    stk.Margin = new Thickness(30, 60, 30, 60);

                }
                else
                {
                    stk.Margin = new Thickness(30, 0, 30, 0);
                    //if(!IsCheck)
                    //bor.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xf5, 0xf5, 0xf5));
                    this.Margin = new Thickness(15, 0, 5, 0);
                    txt.Margin = new Thickness(-20, 0, 10, 0);
                }
                _mode = value;
            }
        }

        private bool _isCheck;

        public bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                if (value)
                {

                    bor.Background = solid;
                    txt.Foreground = new SolidColorBrush(Colors.White);
                    if (!string.IsNullOrWhiteSpace(imgUrl))
                        img.Source = new BitmapImage(new Uri(imgUrl.Substring(0,imgUrl.LastIndexOf('.'))+"_C.png", UriKind.Relative));
                }
                else
                {

                    bor.Background = new SolidColorBrush(Colors.White);
                    txt.Foreground = ForegroundSolid;
                    if (!string.IsNullOrWhiteSpace(imgUrl))
                        img.Source = new BitmapImage(new Uri(imgUrl, UriKind.Relative));
                }
                _isCheck = value;
                OnPropertyChanged("IsCheck");
            }
        }
        public List<MenuControl> ChildrenMenu { get; set; } = new List<MenuControl>();
        string imgUrl;
        public void InitImg(string url)
        {
            imgUrl = url;
            img.Source = new BitmapImage(new Uri(url, UriKind.Relative));
        }
        public void OnChecked()
        {
            var pare = this.Parent;
            if (pare is StackPanel SP)
            {
                foreach (var item in SP.Children)
                {
                    if (item is MenuControl MC)
                    {
                        if (MC == this)
                        {

                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        private void borMenu_Click(object sender, MouseButtonEventArgs e)
        {
            OnSelected?.Invoke(this, this.MenuText);
        }

        private void bor_Click(object sender, RoutedEventArgs e)
        {
            OnSelected?.Invoke(this, this.MenuText);
        }
    }
}

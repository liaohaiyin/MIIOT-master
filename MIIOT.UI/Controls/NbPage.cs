using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MIIOT.UI.Controls
{
    public class NbPage : Page
    {
        private System.Timers.Timer _timer;
        private Border _outerBorder;
        private Border _innerBorder;
        private LoadingBox _loadingBox;
        private TextBlock _textLabel;


        public NbPage()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += _timer_Elapsed;
            _timer.Interval = 3000;
        }

        public bool IsWaiting
        {
            get
            {
                return (bool)GetValue(IsWaitingProperty);
            }
            set
            {
                SetValue(IsWaitingProperty, value);
                Waiting();
            }
        }
        public static readonly DependencyProperty IsWaitingProperty =
            DependencyProperty.Register("IsWaiting", typeof(bool), typeof(NbPage), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IsWaitingPropertyCallBack));
        public static void IsWaitingPropertyCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            property.SetValue(IsWaitingProperty, args.NewValue);
            (property as NbPage).Waiting();
        }



        public string TipText
        {
            get { return (string)GetValue(TipTextProperty); }
            set { SetValue(TipTextProperty, value); ShowTipText(value); }
        }

        // Using a DependencyProperty as the backing store for LoadText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipTextProperty =
            DependencyProperty.Register("TipText", typeof(string), typeof(NbPage), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TipTextPropertyCallBack));
        public static void TipTextPropertyCallBack(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            property.SetValue(TipTextProperty, args.NewValue);
            (property as NbPage).ShowTipText(args.NewValue?.ToString());
        }

        private void WaitingBoxInit()
        {
            if (this._outerBorder == null)
            {
                var pageContent = this.Content as FrameworkElement;
                var grid = new Grid();
                this._outerBorder = new Border();

                //if (this.GetType().Name == "HomePage")//首页没有内边距6
                //    this._outerBorder.Margin = pageContent.Margin;
                //else//二级菜单有内边距
                //    this._outerBorder.Margin = new Thickness(pageContent.Margin.Left - 12, pageContent.Margin.Top - 12, pageContent.Margin.Right - 12, pageContent.Margin.Bottom - 12);
                
                this._outerBorder.CornerRadius = new CornerRadius(3);
                this._outerBorder.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this._outerBorder.Opacity = 0.1;
                this._outerBorder.MouseDown += _outerBorder_MouseDown;
                this._innerBorder = new Border();
                this._innerBorder.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this._innerBorder.Opacity = 0.6;
                this._innerBorder.MinHeight = 100;
                this._innerBorder.MinWidth = 100;
                this._innerBorder.MaxWidth = 300;
                this._innerBorder.HorizontalAlignment = HorizontalAlignment.Center;
                this._innerBorder.VerticalAlignment = VerticalAlignment.Center;
                this._innerBorder.Padding = new Thickness(6);
                this._innerBorder.CornerRadius = new System.Windows.CornerRadius(5);

                _textLabel = new TextBlock();
                _textLabel.TextAlignment = TextAlignment.Center;
                _textLabel.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                _textLabel.Foreground = new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF));
                _textLabel.TextWrapping = TextWrapping.Wrap;
                _textLabel.Margin = new Thickness(3);

                _loadingBox = new LoadingBox();
                _loadingBox.Height = 64;
                _loadingBox.Width = 64;
                _loadingBox.RadiusX = 4.5;
                _loadingBox.RadiusY = 4.5;

                this.Content = null;
                grid.Children.Add(pageContent);
                grid.Children.Add(this._outerBorder);
                grid.Children.Add(this._innerBorder);
                this.Content = grid;
            }
        }

        public void Waiting()
        {
            WaitingBoxInit();

            if (this.IsWaiting)
            {
                _innerBorder.Child = _loadingBox;
                this._outerBorder.Visibility = Visibility.Visible;
                this._innerBorder.Visibility = Visibility.Visible;
            }
            else
            {
                this._outerBorder.Visibility = Visibility.Collapsed;
                this._innerBorder.Visibility = Visibility.Collapsed;
            }
        }

        public void ShowTipText(string text)
        {
            WaitingBoxInit();


            _textLabel.Text = text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                if (this.IsWaiting)
                    this.IsWaiting = false;

                _timer.Enabled = true;
                this._innerBorder.Child = _textLabel;
                this._outerBorder.Visibility = Visibility.Visible;
                this._innerBorder.Visibility = Visibility.Visible;
            }
            else if(!this.IsWaiting)
            {
                this._outerBorder.Visibility = Visibility.Collapsed;
                this._innerBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrWhiteSpace(TipText))
                {
                    TipText = string.Empty;
                }
            });
        }

        private void _outerBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TipText))
            {
                TipText = string.Empty;
            }
        }

        public void Init()
        {
            var vm = this.DataContext as NbPageVm;
            if (vm == null)
                return;
            vm.Init();
        }
    }
}

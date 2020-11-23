using MIIOT.UI.Controls;
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
using System.Windows.Shapes;

namespace MIIOT.UI.ControlEx
{
    /// <summary>
    /// NbFormWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NbFormWindow : NbWindow
    {
        private System.Timers.Timer _timer = new System.Timers.Timer(5000);
        private bool _isWaiting = false;

        public string WaitingText { get; set; } = "正在保存";
        public bool IsWaiting
        {
            get
            {
                return this._isWaiting;
            }
            set
            {
                this._isWaiting = value;
                if (value)
                {
                    this.ErrorTipBack.Visibility = Visibility.Visible;
                    this.ErrorTip.Visibility = Visibility.Visible;
                    this.ErrorTipText.Text = this.WaitingText;
                }
                else
                {
                    this.ErrorTipBack.Visibility = Visibility.Collapsed;
                    this.ErrorTip.Visibility = Visibility.Collapsed;
                    this.ErrorTipText.Text = string.Empty;
                }
            }
        }

        public NbDialogResult NbDialogResult { get; set; } = NbDialogResult.Unknown;

        public NbFormWindow()
        {
            InitializeComponent();
            this._timer.Elapsed += _timer_Elapsed;
            this.Loaded += NbFormWindow_Loaded;
        }

        private void NbFormWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is NbPageVm)
            {
                var vm = this.DataContext as NbPageVm;
                vm.Init();
            }
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this._timer.Enabled = false;
            if (this.IsWaiting)
                return;
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.ErrorTipBack.Visibility = Visibility.Collapsed;
                this.ErrorTip.Visibility = Visibility.Collapsed;
                this.ErrorTipText.Text = string.Empty;
            });
        }

        private async void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsWaiting = true;
            var form = (this.WinContent.Content as Frame).Content as NbFormPage;
            var ret = await form.ValidateAsync();
            this.IsWaiting = false;
            if (!ret.CheckAccess)
            {
                this.ErrorTipBack.Visibility = Visibility.Visible;
                this.ErrorTip.Visibility = Visibility.Visible;
                this.ErrorTipText.Text = ret.Message;
                this._timer.Enabled = true;
                return;
            }
            this.NbDialogResult = NbDialogResult.OK;
            this.Close();
        }

        private async void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsWaiting = true;
            var form = this.WinContent.Content as NbFormPage;
            var ret = await form.ValidateAsync();
            this.IsWaiting = false;
            if (!ret.CheckAccess)
            {
                this.ErrorTipBack.Visibility = Visibility.Visible;
                this.ErrorTip.Visibility = Visibility.Visible;
                this.ErrorTipText.Text = ret.Message;
                this._timer.Enabled = true;
                return;
            }
            this.NbDialogResult = NbDialogResult.Yes;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.NbDialogResult = NbDialogResult.Cancel;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.NbDialogResult = NbDialogResult.No;
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.NbDialogResult = NbDialogResult.Cancel;
            this.Close();
        }



        public static NbDialogResult Show(NbFormPage content)
        {
            return Show(content, string.Empty, NbMessageBoxButtons.OkCancel);
        }



        public static NbDialogResult Show(NbFormPage content, string title)
        {
            return Show(content, title, NbMessageBoxButtons.OkCancel);
        }

        public static NbDialogResult Show(NbFormPage content, string title, string waitingText)
        {
            return Show(content, title, NbMessageBoxButtons.OkCancel, waitingText);
        }

        public static NbDialogResult Show(NbFormPage content, string title, NbMessageBoxButtons buttons, string waitingText = "")
        {
            var result = NbDialogResult.Unknown;
            Application.Current.Dispatcher.Invoke(() =>
            {
                var form = new NbFormWindow();
                form.OkButton.Visibility = (buttons == NbMessageBoxButtons.OK || buttons == NbMessageBoxButtons.OkCancel) ? Visibility.Visible : Visibility.Collapsed;
                form.YesButton.Visibility = (buttons == NbMessageBoxButtons.YesNo || buttons == NbMessageBoxButtons.YesNoCancel) ? Visibility.Visible : Visibility.Collapsed;
                form.NoButton.Visibility = (buttons == NbMessageBoxButtons.YesNo || buttons == NbMessageBoxButtons.YesNoCancel) ? Visibility.Visible : Visibility.Collapsed;
                form.CancelButton.Visibility = (buttons == NbMessageBoxButtons.YesNoCancel || buttons == NbMessageBoxButtons.OkCancel) ? Visibility.Visible : Visibility.Collapsed;
                form.CloseButton.Visibility = (buttons == NbMessageBoxButtons.Close) ? Visibility.Visible : Visibility.Collapsed;
                form.WinTitle.Content = title;
                if (string.IsNullOrWhiteSpace(waitingText))
                    form.WaitingText = "正在保存";
                else
                    form.WaitingText = waitingText;
                form.Width = content.Width;
                form.Height = content.Height + 48 + 32;

                var frame = new Frame();
                frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                frame.Content = content;
                form.WinContent.Content = frame;
                form.YesButton.Content = "是";
                form.NoButton.Content = "否";
                form.OkButton.Content = "确定";
                form.CancelButton.Content = "取消";
                form.CloseButton.Content = "关闭";

                form.Owner = Application.Current.MainWindow;
                content.Init();
                form.ShowDialog();
                result = form.NbDialogResult;
            });

            return result;
        }

        private void ErrorTipBack_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsWaiting)
                return;
            this._timer.Enabled = false;
            this.ErrorTipBack.Visibility = Visibility.Collapsed;
            this.ErrorTip.Visibility = Visibility.Collapsed;
            this.ErrorTipText.Text = string.Empty;
        }
    }
}

using MIIOT.UI.Controls;
using System.Windows;

namespace MIIOT.UI.ControlEx
{
    /// <summary>
    /// NbMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class NbMessageBox : NbWindow
    {
        public NbDialogResult NbDialogResult { get; set; } = NbDialogResult.Unknown;
        public NbMessageBox()
        {
            InitializeComponent();
        }


        public static NbDialogResult Show(string text)
        {
            return Show(text, string.Empty, NbMessageBoxButtons.OK, NbMessageBoxIcon.Notity);
        }


        public static NbDialogResult Show(string text, string title)
        {
            return Show(text, title, NbMessageBoxButtons.OK, NbMessageBoxIcon.Notity);
        }
        public static NbDialogResult Show(string text, string title, NbMessageBoxIcon icon)
        {
            return Show(text, title, NbMessageBoxButtons.OK, icon);
        }
        public static NbDialogResult Show(string text, string title, NbMessageBoxButtons buttons)
        {
            return Show(text, title, buttons, NbMessageBoxIcon.Notity);
        }



        public static NbDialogResult Show(string text, string title, NbMessageBoxButtons buttons, NbMessageBoxIcon icon)
        {
            var result = NbDialogResult.Unknown;
            Application.Current.Dispatcher.Invoke(() =>
            {
                var msgBox = new NbMessageBox();
                msgBox.OkButton.Visibility = (buttons == NbMessageBoxButtons.OK || buttons == NbMessageBoxButtons.OkCancel) ? Visibility.Visible : Visibility.Collapsed;
                msgBox.YesButton.Visibility = (buttons == NbMessageBoxButtons.YesNo || buttons == NbMessageBoxButtons.YesNoCancel) ? Visibility.Visible : Visibility.Collapsed;
                msgBox.NoButton.Visibility = (buttons == NbMessageBoxButtons.YesNo || buttons == NbMessageBoxButtons.YesNoCancel) ? Visibility.Visible : Visibility.Collapsed;
                msgBox.CancelButton.Visibility = (buttons == NbMessageBoxButtons.YesNoCancel || buttons == NbMessageBoxButtons.OkCancel) ? Visibility.Visible : Visibility.Collapsed;

                if (icon == NbMessageBoxIcon.None)
                {
                    //msgBox.MsgBoxBarIcon.Data = msgBox.MsgBoxIcon.Data = null;
                    //msgBox.MsgBoxIconGrid.Width = new GridLength(0);
                    msgBox.MsgBoxViewbox.Visibility = Visibility.Collapsed;
                    msgBox.MsgBoxMessage.HorizontalAlignment = HorizontalAlignment.Center;
                }
                //else
                //{
                //    //msgBox.MsgBoxBarIcon.Data = msgBox.MsgBoxIcon.Data = UIResource.GetIcon($"Icon-{icon.ToString()}");
                //    if (icon == NbMessageBoxIcon.Error || icon == NbMessageBoxIcon.Warn)
                //    {
                //        msgBox.MsgBoxIcon.Fill = UIResource.GetBrush("Nb.TransWarnBtn.Foreground");
                //    }
                //    else
                //    {
                //        msgBox.MsgBoxIcon.Fill = UIResource.GetBrush("Nb.DefaultBtn.Background");
                //    }
                //}


                msgBox.MsgBoxTitle.Content = title;
                msgBox.MsgBoxMessage.Text = text;
                msgBox.YesButton.Content = "是";
                msgBox.NoButton.Content = "否";
                msgBox.OkButton.Content = "确定";
                msgBox.CancelButton.Content = "取消";
                msgBox.Owner = Application.Current.MainWindow;

                if (msgBox.NoButton.Visibility == Visibility.Visible)
                    msgBox.NoButton.Focus();
                else if (msgBox.CancelButton.Visibility == Visibility.Visible)
                    msgBox.CancelButton.Focus();
                else if (msgBox.YesButton.Visibility == Visibility.Visible)
                    msgBox.YesButton.Focus();
                else if (msgBox.OkButton.Visibility == Visibility.Visible)
                    msgBox.OkButton.Focus();
                msgBox.ShowDialog();
                result = msgBox.NbDialogResult;
            });
            return result;
        }


        public static NbDialogResult Ok(string text)
        {
            return Show(text, string.Empty, NbMessageBoxButtons.OK, NbMessageBoxIcon.Notity);
        }
        public static NbDialogResult Notity(string text)
        {
            return Show(text, string.Empty, NbMessageBoxButtons.OkCancel, NbMessageBoxIcon.Notity);
        }
        /// <summary>
        /// yes & no 警告通知
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static NbDialogResult Warn(string text)
        {
            return Show(text, string.Empty, NbMessageBoxButtons.YesNo, NbMessageBoxIcon.Warn);
        }
        //public static void Warn(string text, Action yesCallback)
        //{
        //    if(Show(text, string.Empty, NbMessageBoxButtons.YesNo, NbMessageBoxIcon.Warn) == NbDialogResult.Yes)
        //    {
        //        yesCallback?.Invoke();
        //    }
        //}
        public static NbDialogResult Error(string text)
        {
            return Show(text, string.Empty, NbMessageBoxButtons.OK, NbMessageBoxIcon.Error);
        }


        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.NbDialogResult = NbDialogResult.OK;
            this.Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
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
    }


    public enum NbMessageBoxButtons
    {
        OK = 0,
        YesNo = 1,
        YesNoCancel = 2,
        OkCancel = 3,
        Close = 4,
    }
    public enum NbMessageBoxIcon
    {
        None = 0,
        Notity = 1,
        Warn = 2,
        Error = 3
    }
    public enum NbDialogResult
    {
        Unknown = 0,
        OK = 1,
        Yes = 2,
        No = 3,
        Cancel = 4,
    }
}

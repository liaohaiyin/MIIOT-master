using AForge.Controls;
using AForge.Video.DirectShow;
using MaterialDesignThemes.Wpf;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.ORCart.View
{
    /// <summary>
    /// CameraSignView.xaml 的交互逻辑
    /// </summary>
    public partial class CameraSignView : UserControl
    {
        public CameraSignView()
        {
            InitializeComponent();
            this.Loaded += CameraSignView_Loaded;
            InitWebcam();

        }
        public delegate void delback(CameraSignView sender);
        public event delback OnBack;
        private void CameraSignView_Loaded(object sender, RoutedEventArgs e)
        {

           

           

        }
        public void Open()
        {
            Task.Run(() => {
                Dispatcher.Invoke(() =>
                {
                    StartCamera();
                });
                System.Threading.Thread.Sleep(3000);
                Dispatcher.Invoke(() =>
                {
                    CloseWebcam();
                    DialogHost.CloseDialogCommand.Execute("", this);
                });
            });
        }
        private FilterInfoCollection collection;
        private VideoCaptureDevice visDevice;
        VideoSourcePlayer Player = new VideoSourcePlayer();
        private bool InitWebcam()
        {
            collection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (collection.Count == 0)
            {
                MessageBox.Show("未检测到摄像头");
                return false;
            }
            visDevice = new VideoCaptureDevice(collection[0].MonikerString);

            return true;
        }
        private void StartCamera()
        {
            try
            {
                CloseWebcam();
                Player.VideoSource = visDevice;
                Player.Start();
                Player.NewFrame += this.Player_NewFrame;
            }
            catch (Exception ex)
            {


            }

        }
        private bool CloseWebcam()
        {
            try
            {
                if (Player.IsRunning)
                    Player.SignalToStop();
                // Player.WaitForStop();
            }
            catch (Exception ex)
            {
            }
            return true;
        }
        private void Player_NewFrame(object sender, ref Bitmap image)
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);

            bi.StreamSource = ms;
            bi.EndInit();

            bi.Freeze();

            this.Dispatcher.Invoke(() =>
            {
                OnBack?.Invoke(this);
                picBox.Source = bi;
            });
        }
    }
}

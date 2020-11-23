using MaterialDesignThemes.Wpf;
using MIIOT.Model;
using MIIOT.ORCart.MainView.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
    /// UserCart.xaml 的交互逻辑
    /// </summary>
    public partial class UserCard : UserControl, INotifyPropertyChanged
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
        private sys_user _user;

        public sys_user User
        {
            get { return _user; }
            set
            {
                _user = value;
                idcardChange(value);
                idfaceChange(value);
                OnPropertyChanged("User");
            }
        }

        #endregion
        public UserCard()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void idcardChange(sys_user User)
        {
            if (string.IsNullOrWhiteSpace(User.idcard))
            {
                imgidcard.Source = new BitmapImage(new Uri("/MIIOT.ORCart;component/Images/idcard_null.png", UriKind.Relative));
                txtidcardnull.Visibility = Visibility.Visible;
                txtidcard.Visibility = Visibility.Collapsed;
            }
            else
            {
                imgidcard.Source = new BitmapImage(new Uri("/MIIOT.ORCart;component/Images/idcard.png", UriKind.Relative));
                txtidcardnull.Visibility = Visibility.Collapsed;
                txtidcard.Visibility = Visibility.Visible;
            }
        }
        private void idfaceChange(sys_user User)
        {
            if (string.IsNullOrWhiteSpace(User.idface))
            {
                imgidface.Stretch = Stretch.None;
                imgidface.Source = new BitmapImage(new Uri("/MIIOT.ORCart;component/Images/iface_null.png", UriKind.Relative));
                txtidfacenull.Visibility = Visibility.Visible;
                txtidface.Visibility = Visibility.Collapsed;
            }
            else
            {
                var faces = CacheData.Ins.dbUtil.Query<sys_face>("select * from sys_face where id=@id", new { id = User.idface });
                if (faces != null && faces.Count > 0)
                {
                    System.Drawing.Image img = IDFaceUtil.byte2img(faces[0].img, "");
                    imgidface.Source = Image2Bitmap(img);
                    imgidface.Stretch = Stretch.UniformToFill;
                }
                else
                {
                    imgidface.Stretch = Stretch.None;
                    imgidface.Source = new BitmapImage(new Uri("/MIIOT.ORCart;component/Images/iface.png", UriKind.Relative));

                }
                txtidfacenull.Visibility = Visibility.Collapsed;
                txtidface.Visibility = Visibility.Visible;
            }
        }
        private BitmapImage Image2Bitmap(System.Drawing.Image bmp)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);// 格式自处理,这里du用 bitmap
                                                                 // 下行,初始一个zhi ImageSource 作为 myImage 的Source
            System.Windows.Media.Imaging.BitmapImage bi = new System.Windows.Media.Imaging.BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(ms.ToArray()); // 不要直接使用 ms
            bi.EndInit();
            ms.Close();
            return bi;
        }

        private async void btnidface_click(object sender, MouseButtonEventArgs e)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(User.idface))
                {
                    IDFaceView scaner = new IDFaceView();
                    string faceid = CacheData.Ins.SnowId.nextId().ToString();
                    scaner.Regist(faceid);
                    await DialogHost.Show(scaner, "RootDialog");
                    if (scaner.isRegistSucc)
                    {
                        User.idface = faceid;
                        CacheData.Ins.dbUtil.Update(User);
                        idfaceChange(User);
                        imgidface.Source = BitmapConverter(scaner.Succbitmap);
                        imgidface.Stretch = Stretch.UniformToFill;
                        System.Drawing.Image img = scaner.Succbitmap;
                        byte[] faceImg = IDFaceUtil.img2byte(img);
                        sys_face face = new sys_face()
                        {
                            id = faceid,
                            user_id = User.id,
                            img = faceImg
                        };
                        CacheData.Ins.dbUtil.Insert(face);
                    }
                    else
                    {
                      //  CacheData.Ins.mainWindow.MessageTips($"登记失败，超时或人工取消，请重试");
                    }
                }
                else
                {
#if Release

                    CacheData.Ins.faceUtil.user_delete(User.idface, "1");
#endif
                    sys_face face = new sys_face()
                    {
                        id = User.idface,
                        user_id = User.id,
                    };
                    CacheData.Ins.dbUtil.Delete(face);
                    User.idface = "";
                    CacheData.Ins.dbUtil.Update(User);
                    idfaceChange(User);
                }
            }
            catch (Exception ex)
            {
                Log.Error("btnidface_click", ex);
            }
        }
        private BitmapImage BitmapConverter(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
        private void btnidcard_click(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(User.idcard))
            {
                CardReaderView scaner = new CardReaderView();
                var result = DialogHost.Show(scaner, "RootDialog", delegate (object senders, DialogOpenedEventArgs args)
                {
                    Task.Run(() =>
                    {
                        Thread.Sleep(2000);
                        this.Dispatcher.Invoke(() =>
                        {
                            User.idcard = "123456";
                            args.Session.Close(false);
                            CacheData.Ins.dbUtil.Update(User);
                            idcardChange(User);
                        });
                    });

                });
            }
            else
            {
                User.idcard = "";
                CacheData.Ins.dbUtil.Update(User);
                idcardChange(User);
            }
        }



        public string ReadImg(string img)
        {
            return Convert.ToBase64String(File.ReadAllBytes(img));
        }

    }
}

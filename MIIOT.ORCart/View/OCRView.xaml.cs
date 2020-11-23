using Dragablz;
using MaterialDesignThemes.Wpf;
using MIIOT.Drivers.OpenCV;
using MIIOT.ORCart.Common;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ZXing;
using Image = System.Drawing.Image;
using Rect = OpenCvSharp.Rect;

namespace MIIOT.ORCart.View
{
    /// <summary>
    /// OCRView.xaml 的交互逻辑
    /// </summary>
    public partial class OCRView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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
        private ObservableCollection<PicModel> _pics = new ObservableCollection<PicModel>();

        public ObservableCollection<PicModel> Pics
        {
            get
            {
                return _pics;
            }
            set
            {
                _pics = value;
                OnPropertyChanged("Pics");
            }
        }
        private PicModel _picModel;

        public PicModel PicInfo
        {
            get { return _picModel; }
            set
            {
                _picModel = value;
                OnPropertyChanged("PicInfo");
            }
        }
        public OCRView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += OCRView_Loaded;

        }

        private void OCRView_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            aa = true;
            Start();
            return;


        }

        public UdpService udp;

        bool aa = false;
        int i = 0;

        bool isScaning = false;
        public void Start()
        {
            DateTime date = DateTime.Now;
            txtBarCode.Text = "";
            isScaning = true;
            Task.Run(() =>
            {

                using (VideoCapture cap = VideoCapture.FromCamera(1))
                {
                    cap.FrameHeight = 1080;
                    cap.FrameWidth = 1920;
                    if (!cap.IsOpened())
                    {
                        Console.WriteLine("open camera error");
                        return;
                    }


                    // Frame image buffer
                    Mat image = new Mat();

                    int change = 0;
                    // When the movie playback reaches end, Mat.data becomes NULL.
                    while (isScaning)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            txtBarCode.Text = "";
                        });
                        cap.Read(image); // same as cvQueryFrame


                        if (!image.Empty())
                        {
                            Mat grayImage = new Mat();
                            OpencvHelper.CvGrayImage(image, grayImage);
                            change++;
                            if (change > 13)
                                change = 1;
                            OpencvHelper.RotateImage(grayImage, grayImage, 30 * change, 1);
                            this.Dispatcher.Invoke(() =>
                            {
                                Bitmap _bitmap;
                                BitmapImage map = MatToBitmapImage(grayImage, out _bitmap);

                                picBox.Source = map;
                                BarcodeReader reader = new BarcodeReader();
                                reader.Options.CharacterSet = "UTF-8";
                                Result result = reader.Decode(_bitmap);
                                if (result != null)
                                {
                                    txtBarCode.Text = string.Join("\r\n", result);
                                    MessageBox.Show(result + ":" + (DateTime.Now-date  ).TotalMilliseconds);
                                    date = DateTime.Now;
                                }
                            });


                            //Bitmap _bitmap;
                            //BitmapImage map = MatToBitmapImage(image, out _bitmap);

                            //this.Dispatcher.Invoke(() =>
                            //{
                            //    picBox.Source = map;
                            //});
                            Cv2.WaitKey(1);
                            Thread.Sleep(10);
                        }
                        else
                        {
                            Console.WriteLine("mat is empty");
                        }
                    }
                }

            });
        }
        public void OCR(Bitmap img)
        {
            //var ocr = new TesseractEngine("./tessdata", "eng", EngineMode.TesseractAndCube);
            //var page = ocr.Process(img);

        }

        private void Magr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            bormar.Visibility = Visibility.Visible;
            borscan.Visibility = Visibility.Collapsed;
        }

        private void Scan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bormar.Visibility = Visibility.Collapsed;
            borscan.Visibility = Visibility.Visible;
        }

        private void btnStopScan_Click(object sender, RoutedEventArgs e)
        {
            isScaning = false;
        }

        public void sendMsg(string code, string MsgType)
        {
        }

        public void MenuSelected(string menuName)
        {
        }

        public BitmapImage MatToBitmapImage(Mat image, out Bitmap bitmap)
        {
            bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
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



        #region barcode
        private void OpenCV(Mat srcImage)
        {
            if (srcImage.Empty()) { return; }

            //图像转换为灰度图像
            Mat grayImage = new Mat();
            OpencvHelper.CvGrayImage(srcImage, grayImage);
            ShowImage("灰度图像", grayImage);


            OpencvHelper.RotateImage(grayImage, grayImage, 50, 1);
            OpencvHelper.Imshow("旋转", grayImage);

            //建立图像的梯度幅值
            Mat gradientImage = new Mat();
            OpencvHelper.CvConvertScaleAbs(grayImage, gradientImage);
            ShowImage("梯度幅值", gradientImage);

            //对图片进行相应的模糊化,使一些噪点消除
            Mat blurImage = new Mat();
            Mat thresholdImage = new Mat();
            OpencvHelper.BlurImage(gradientImage, blurImage, thresholdImage);
            ShowImage("二值化", blurImage);

            //二值化以后的图像,条形码之间的黑白没有连接起来,就要进行形态学运算,消除缝隙,相当于小型的黑洞,选择闭运算
            //因为是长条之间的缝隙,所以需要选择宽度大于长度
            Mat morphImage = new Mat();
            OpencvHelper.MorphImage(thresholdImage, morphImage);
            ShowImage("闭运算", morphImage);

            //现在要让条形码区域连接在一起,所以选择膨胀腐蚀,而且为了保持图形大小基本不变,应该使用相同次数的膨胀腐蚀
            //先腐蚀,让其他区域的亮的地方变少最好是消除,然后膨胀回来,消除干扰,迭代次数根据实际情况选择
            OpencvHelper.DilationErosionImage(morphImage);
            ShowImage("膨胀腐蚀", morphImage);


            Mat[] contours = new Mat[10000];
            List<double> OutArray = new List<double>();
            //接下来对目标轮廓进行查找,目标是为了计算图像面积
            Cv2.FindContours(morphImage, out contours, OutputArray.Create(OutArray), RetrievalModes.External, ContourApproximationModes.ApproxSimple);
            //看看轮廓图像
            //Cv2.DrawContours(srcImage, contours, -1, Scalar.Yellow);
            //OpencvHelper.Imshow("目标轮廓", srcImage);

            //计算轮廓的面积并且存放
            for (int i = 0; i < OutArray.Count; i++)
            {
                OutArray[i] = contours[i].ContourArea(false);
            }

            List<string> codes = new List<string>();
            int num = 0;
            while (num < 10) //找出10个面积最大的矩形
            {
                //找出面积最大的轮廓
                double minValue, maxValue;
                OpenCvSharp.Point minLoc, maxLoc;
                Cv2.MinMaxLoc(InputArray.Create(OutArray), out minValue, out maxValue, out minLoc, out maxLoc);
                //计算面积最大的轮廓的最小的外包矩形
                RotatedRect minRect = Cv2.MinAreaRect(contours[maxLoc.Y]);
                //找到了矩形的角度,但是这是一个旋转矩形,所以还要重新获得一个外包最小矩形
                OpenCvSharp.Rect myRect = Cv2.BoundingRect(contours[maxLoc.Y]);
                //将扫描的图像裁剪下来,并保存为相应的结果,保留一些X方向的边界,所以对rect进行一定的扩张
                myRect.X = myRect.X - (myRect.Width / 20);
                myRect.Width = (int)(myRect.Width * 1.1);

                //TermCriteria termc = new TermCriteria(CriteriaType.MaxIter, 1, 1);
                //Cv2.CamShift(srcImage, myRect, termc);

                //一次最大面积的
                var a = contours.ToList();
                a.Remove(contours[maxLoc.Y]);
                contours = a.ToArray();
                OutArray.Remove(OutArray[maxLoc.Y]);

                string code = DiscernBarCode(srcImage, myRect);
                if (!string.IsNullOrEmpty(code))
                {
                    //Cv2.Rectangle(srcImage, myRect, new Scalar(0, 255, 255), 3, LineTypes.AntiAlias);
                    codes.Add(code);
                }
                Cv2.Rectangle(srcImage, myRect, new Scalar(0, 255, 255), 3, LineTypes.AntiAlias);
                num++;
                if (contours.Count() <= 0)
                    break;
            }
            // Image img2 = CreateImage(srcImage);
            // picBox.Source = img2;
            this.Dispatcher.Invoke(() =>
            {
                Bitmap _bitmap;
                BitmapImage map = MatToBitmapImage(srcImage, out _bitmap);

                picBox.Source = map;

                txtBarCode.Text = string.Join("\r\n", codes);
            });
            ////找出面积最大的轮廓
            //double minValue, maxValue;
            //OpenCvSharp.Point minLoc, maxLoc;
            //Cv2.MinMaxLoc(InputArray.Create(OutArray), out minValue, out maxValue, out minLoc, out maxLoc);
            ////计算面积最大的轮廓的最小的外包矩形
            //RotatedRect minRect = Cv2.MinAreaRect(contours[maxLoc.Y]);
            ////为了防止找错,要检查这个矩形的偏斜角度不能超标
            ////如果超标,那就是没找到
            //if (minRect.Angle < 2.0)
            //{
            //    //找到了矩形的角度,但是这是一个旋转矩形,所以还要重新获得一个外包最小矩形
            //    Rect myRect = Cv2.BoundingRect(contours[maxLoc.Y]);
            //    //把这个矩形在源图像中画出来
            //    //Cv2.Rectangle(srcImage, myRect, new Scalar(0, 255, 255), 3, LineTypes.AntiAlias);
            //    //看看显示效果,找的对不对
            //    //Imshow("裁剪图片", srcImage);
            //    //将扫描的图像裁剪下来,并保存为相应的结果,保留一些X方向的边界,所以对rect进行一定的扩张
            //    myRect.X = myRect.X - (myRect.Width / 20);
            //    myRect.Width = (int)(myRect.Width * 1.1);
            //    Mat resultImage = new Mat(srcImage, myRect);
            //    //OpencvHelper.Imshow("结果图片", resultImage);
            //    Image img = CreateImage(resultImage);
            //    picCode.Image = img;
            //    DiscernBarcode(img);
            //    //看看轮廓图像
            //    Cv2.DrawContours(srcImage, contours, -1, Scalar.Red);
            //    //把这个矩形在源图像中画出来
            //    Cv2.Rectangle(srcImage, myRect, new Scalar(0, 255, 255), 3, LineTypes.AntiAlias);
            //    Image img2 = CreateImage(srcImage);
            //    picFindContours.Image = img2;

            //    //string path = Path.GetDirectoryName(@g_sFilePath) + "\\Ok.png";
            //    //if (File.Exists(@path)) File.Delete(@path);//如果文件存在 则删除
            //    //if (!Cv2.ImWrite(@path, resultImage))
            //}
            srcImage.Dispose();
        }
        private void HandelCode(Mat srcImage, Rect myRect, Mat[] contours)
        {
            Mat resultImage = new Mat(srcImage, myRect);
            Image img = CreateImage(resultImage);
            //picCode.Image = img;
            DiscernBarcode(img);
            //看看轮廓图像
            Cv2.DrawContours(srcImage, contours, -1, Scalar.Red);
            //把这个矩形在源图像中画出来
            Cv2.Rectangle(srcImage, myRect, new Scalar(0, 255, 255), 3, LineTypes.AntiAlias);
            //Image img2 = CreateImage(srcImage);
            //picFindContours.Image = img2;
        }

        private Image CreateImage(Mat resultImage)
        {
            byte[] bytes = resultImage.ToBytes();
            MemoryStream ms = new MemoryStream(bytes);
            return Bitmap.FromStream(ms, true);
        }

        private void ShowImage(string name, Mat resultImage)
        {
            this.Dispatcher.Invoke(() =>
            {
                Bitmap _bitmap;
                BitmapImage map = MatToBitmapImage(resultImage, out _bitmap);

                picBox.Source = map;
                Thread.Sleep(1000);
            });

            //Image img = CreateImage(resultImage);
            //frmShowImage frm = new frmShowImage(name, img);
            //frm.ShowDialog();
        }

        /// <summary>
        /// 解析条形码图片
        /// </summary>
        private string DiscernBarCode(Mat srcImage, Rect myRect)
        {
            try
            {
                Mat resultImage = new Mat(srcImage, myRect);
                Image img = CreateImage(resultImage);
                Bitmap pImg = MakeGrayscale3((Bitmap)img);
                BarcodeReader reader = new BarcodeReader();
                reader.Options.CharacterSet = "UTF-8";
                Result result = reader.Decode(new Bitmap(pImg));
                Console.Write(result);
                if (result != null)
                    return result.ToString();
                else
                    return "";
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return "";
            }
        }

        /// <summary>
        /// 解析条形码图片
        /// </summary>
        private void DiscernBarcode(Image primaryImage)
        {
            //Bitmap pImg = MakeGrayscale3((Bitmap)primaryImage);
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Result result = reader.Decode(new Bitmap(primaryImage));//Image.FromFile(path)
            Console.Write(result);
            if (result != null)
                txtBarCode.Text = result.ToString();
            else
                txtBarCode.Text = "";

            //watch.Start();
            //watch.Stop();
            //TimeSpan timeSpan = watch.Elapsed;
            //MessageBox.Show("扫描执行时间：" + timeSpan.TotalMilliseconds.ToString());


            //using (ZBar.ImageScanner scanner = new ZBar.ImageScanner())
            //{
            //    scanner.SetConfiguration(ZBar.SymbolType.None, ZBar.Config.Enable, 0);
            //    scanner.SetConfiguration(ZBar.SymbolType.CODE39, ZBar.Config.Enable, 1);
            //    scanner.SetConfiguration(ZBar.SymbolType.CODE128, ZBar.Config.Enable, 1);

            //    List<ZBar.Symbol> symbols = new List<ZBar.Symbol>();
            //    symbols = scanner.Scan((Image)pImg);
            //    if (symbols != null && symbols.Count > 0)
            //    {
            //        //string result = string.Empty;
            //        //symbols.ForEach(s => result += "条码内容:" + s.Data + " 条码质量:" + s.Type + Environment.NewLine);
            //        txtBarCode.Text = symbols.FirstOrDefault().Data;
            //    }
            //    else
            //    {
            //        txtBarCode.Text = "";
            //    }
            //}
        }

        /// <summary>
        /// 处理图片灰度
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);
            //create the grayscale ColorMatrix
            System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(
               new float[][]
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });
            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();
            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);
            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }
        #endregion
    }
}

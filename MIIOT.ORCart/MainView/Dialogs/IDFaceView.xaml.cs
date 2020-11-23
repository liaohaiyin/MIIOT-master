using AForge.Controls;
using AForge.Video.DirectShow;
using MaterialDesignThemes.Wpf;
using MIIOT.Model;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace MIIOT.ORCart.MainView.Dialogs
{
    /// <summary>
    /// IDFaceView.xaml 的交互逻辑
    /// </summary>
    public partial class IDFaceView : UserControl
    {

        public IDFaceView()
        {
            InitializeComponent();
            this.Loaded += IDFaceView_Loaded;
            iDFaceUtil.OnMsgBack += IDFaceUtil_OnMsgBack;
            iDFaceUtil.OnImageBack += IDFaceUtil_OnImageBack;
            iDFaceUtil.OnRegistComplete += IDFaceUtil_OnRegistComplete;
        }

        private void IDFaceUtil_OnRegistComplete(Bitmap image)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (image != null)
                {
                    isRegistSucc = true;
                    Console.WriteLine("登记成功");
                    Succbitmap = image;
                }
            });
        }

        private void IDFaceUtil_OnImageBack(BitmapImage image)
        {
            this.Dispatcher.Invoke(() =>
            {
                picBox.Source = image;
            });
        }

        private void IDFaceUtil_OnMsgBack(string msg)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtsuc.Text = msg;
            });
        }

        private void IDFaceView_Loaded(object sender, RoutedEventArgs e)
        {

        }
        IDFaceUtil iDFaceUtil = new IDFaceUtil();
        public bool isRegistSucc = false;
        public Bitmap Succbitmap;
        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            iDFaceUtil.Delete("");
        }
        int face = 12;
        private void btnRegist_Click(object sender, RoutedEventArgs e)
        {
            iDFaceUtil.Regist("a" + face);
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            iDFaceUtil.Check();
        }
        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            iDFaceUtil.List();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            iDFaceUtil.Close();
            picBox.Source = null;
        }

        public void Regist(string face_id)
        {
            Console.WriteLine("开始识别");
            iDFaceUtil.Regist(face_id);
        }


    }

    public class IDFaceUtil
    {
        public delegate void DelMsgBack(string msg);
        public event DelMsgBack OnMsgBack;
        public delegate void DelImgBack(BitmapImage image);
        public event DelImgBack OnImageBack;
        public delegate void DelCheckResult(sys_face face);
        public event DelCheckResult OnCheckComplete;

        public delegate void DelRegistBack(Bitmap image);
        public event DelRegistBack OnRegistComplete;


        public bool isRegistSucc = false;
        public string Msg = "";
        bool isScaning = false;
        bool isRegist = false;
        bool isCheck = false;
        sys_face sys_face;

        Bitmap bitmap;
        BitmapImage img;
        public Bitmap Succbitmap;
        bool isBusy = false;
        public void Regist(string face_id)
        {
            isScaning = true;
#if Release
            var faces = CacheData.Ins.dbUtil.Query<sys_face>("select * from sys_face", null);
            DevFace.set_occlu_thr(0.9f);
#endif
            Task.Run(() =>
            {
                using (VideoCapture cap = VideoCapture.FromCamera(0))
                {
                    if (!cap.IsOpened())
                    {
                        Console.WriteLine("open camera error");
                        return;
                    }
                    // Frame image buffer
                    Mat image = new Mat();
                    // When the movie playback reaches end, Mat.data becomes NULL.
                    while (isScaning)
                    {
                        cap.Read(image); // same as cvQueryFrame
                        if (!image.Empty())
                        {
#if Release

                            int ilen = 2;//传入的人脸数
                            TrackFaceInfo[] track_info = new TrackFaceInfo[ilen];
                            for (int i = 0; i < ilen; i++)
                            {
                                track_info[i] = new TrackFaceInfo();
                                track_info[i].landmarks = new int[144];
                                track_info[i].headPose = new float[3];
                                track_info[i].face_id = 0;
                                track_info[i].score = 0;
                            }
                            int sizeTrack = Marshal.SizeOf(typeof(TrackFaceInfo));
                            IntPtr ptT = Marshal.AllocHGlobal(sizeTrack * ilen);
                            int faceSize = ilen;//返回人脸数  分配人脸数和检测到人脸数的最小值
                            int curSize = ilen;//当前人脸数 输入分配的人脸数，输出实际检测到的人脸数
                            faceSize = CacheData.Ins.faceUtil.FaceTrack(ptT, image.CvPtr, ref curSize);
                            for (int index = 0; index < faceSize; index++)
                            {
                                IntPtr ptr = new IntPtr();
                                if (8 == IntPtr.Size)
                                {
                                    ptr = (IntPtr)(ptT.ToInt64() + sizeTrack * index);
                                }
                                else if (4 == IntPtr.Size)
                                {
                                    ptr = (IntPtr)(ptT.ToInt32() + sizeTrack * index);
                                }

                                track_info[index] = (TrackFaceInfo)Marshal.PtrToStructure(ptr, typeof(TrackFaceInfo));

                                //// 画人脸框
                                drawbox(track_info[index], ref image);
                            }
                            Marshal.FreeHGlobal(ptT);

#endif
                            Bitmap _bitmap;
                            img = MatToBitmapImage(image, out _bitmap);
                            bitmap = _bitmap.DeepClone();
                            OnImageBack?.Invoke(img);
#if Release

                            DevFace.clear_tracked_faces();
                            DevFace.set_detect_in_video_interval(20);
                            DevFace.set_track_by_detection_interval(20);
                            byte[] bitmapbuff = img2byte(bitmap);

                            Thread.Sleep(20);
                            bool isLive = CacheData.Ins.faceUtil.rgb_liveness_check_by_buf(bitmapbuff);
                            if (isLive)
                            {
                                for (int j = 0; j < faces.Count; j++)
                                {
                                    Thread.Sleep(20);
                                    if (CacheData.Ins.faceUtil.match_by_buf(bitmapbuff, faces[j].img))
                                    {
                                        OnMsgBack?.Invoke("此特征已登记过，请直接前往进行登录");
                                        bitmap = null;
                                        isRegist = false;
                                        isScaning = false;
                                        isRegistSucc = true;
                                        return;
                                    }
                                }

                                Thread.Sleep(20);
                                bool succ = CacheData.Ins.faceUtil.RegisterFace(face_id, "1", bitmapbuff);
                                if (succ)
                                {
                                    Succbitmap = bitmap.DeepClone();
                                    bitmap = null;
                                    isRegist = false;
                                    isScaning = false;
                                    isRegistSucc = true;
                                    OnRegistComplete?.Invoke(Succbitmap);
                                    OnMsgBack?.Invoke("登记成功");

                                    break;
                                }
                            }
#endif
                            Cv2.WaitKey(1);
                        }
                        else
                        {
                            Console.WriteLine("mat is empty");
                        }
                    }
                }

            });

           
        }


        public void Check()
        {
#if Release
            DevFace.set_occlu_thr(0.2f);
            var faces = CacheData.Ins.dbUtil.Query<sys_face>("select * from sys_face", null);
#endif
            isScaning = true;
            Task.Run(() =>
            {
                using (VideoCapture cap = VideoCapture.FromCamera(0))
                {
                    if (!cap.IsOpened())
                    {
                        Console.WriteLine("open camera error");
                        return;
                    }
                    // Frame image buffer
                    Mat image = new Mat();
                    // When the movie playback reaches end, Mat.data becomes NULL.
                    while (isScaning)
                    {
                        cap.Read(image); // same as cvQueryFrame
                        if (!image.Empty())
                        {
#if Release

                            int ilen = 2;//传入的人脸数
                            TrackFaceInfo[] track_info = new TrackFaceInfo[ilen];
                            for (int i = 0; i < ilen; i++)
                            {
                                track_info[i] = new TrackFaceInfo();
                                track_info[i].landmarks = new int[144];
                                track_info[i].headPose = new float[3];
                                track_info[i].face_id = 0;
                                track_info[i].score = 0;
                            }
                            int sizeTrack = Marshal.SizeOf(typeof(TrackFaceInfo));
                            IntPtr ptT = Marshal.AllocHGlobal(sizeTrack * ilen);
                            int faceSize = ilen;//返回人脸数  分配人脸数和检测到人脸数的最小值
                            int curSize = ilen;//当前人脸数 输入分配的人脸数，输出实际检测到的人脸数
                            faceSize = CacheData.Ins.faceUtil.FaceTrack(ptT, image.CvPtr, ref curSize);
                            for (int index = 0; index < faceSize; index++)
                            {
                                IntPtr ptr = new IntPtr();
                                if (8 == IntPtr.Size)
                                {
                                    ptr = (IntPtr)(ptT.ToInt64() + sizeTrack * index);
                                }
                                else if (4 == IntPtr.Size)
                                {
                                    ptr = (IntPtr)(ptT.ToInt32() + sizeTrack * index);
                                }

                                track_info[index] = (TrackFaceInfo)Marshal.PtrToStructure(ptr, typeof(TrackFaceInfo));

                                //// 画人脸框
                                drawbox(track_info[index], ref image);
                            }
                            Marshal.FreeHGlobal(ptT);

#endif
                            Bitmap _bitmap;
                            img = MatToBitmapImage(image, out _bitmap);
                            bitmap = _bitmap.DeepClone();
                            OnImageBack?.Invoke(img);
#if Release

                            DevFace.clear_tracked_faces();
                            DevFace.set_detect_in_video_interval(20);
                            DevFace.set_track_by_detection_interval(20);
                            byte[] bitmapbuff = img2byte(bitmap);
                            Thread.Sleep(20);
                            byte[] facebuff = img2byte(bitmap);

                            bool isLive = CacheData.Ins.faceUtil.rgb_liveness_check_by_buf(facebuff);
                            if (isLive)
                            {

                                Console.WriteLine("databases:"+faces.Count);
                                for (int j = 0; j < faces.Count; j++)
                                {
                                    Thread.Sleep(20);
                                    if (CacheData.Ins.faceUtil.match_by_buf(facebuff, faces[j].img))
                                    {
                                        sys_face = faces[j];
                                        isCheck = false;
                                        isScaning = false;
                                        bitmap = null;
                                        OnCheckComplete?.Invoke(sys_face);
                                        OnMsgBack?.Invoke("识别成功");
                                        break;
                                    }
                                }
                            }
#endif
                            Cv2.WaitKey(1);
                        }
                        else
                        {
                            Console.WriteLine("mat is empty");
                        }
                    }
                }

            });

        }

        public void Delete(string face_id)
        {
#if Release
            var faces = CacheData.Ins.dbUtil.Query<sys_face>("select * from sys_face", null);

            CacheData.Ins.faceUtil.get_face_feature_by_buf(faces[0].img);

            CacheData.Ins.faceUtil.match_by_buf(faces[0].img, faces[1].img);

            return;
            var temp = CacheData.Ins.faceUtil.user_delete(face_id, "1");
#endif
        }
        public void List()
        {
#if Release
            //  CacheData.Ins.faceUtil.test_get_group_list();
            CacheData.Ins.faceUtil.get_user_list();
#endif
        }
        public void Close()
        {
            bitmap = null;
            isCheck = false;
            isScaning = false;
        }

        private void Scan()
        {
            isScaning = true;
            using (VideoCapture cap = VideoCapture.FromCamera(0))
            {
                if (!cap.IsOpened())
                {
                    Console.WriteLine("open camera error");
                    return;
                }
                // Frame image buffer
                Mat image = new Mat();
                // When the movie playback reaches end, Mat.data becomes NULL.
                while (isScaning)
                {
                    cap.Read(image); // same as cvQueryFrame
                    if (!image.Empty())
                    {
#if Release
                        int ilen = 2;//传入的人脸数
                        TrackFaceInfo[] track_info = new TrackFaceInfo[ilen];
                        for (int i = 0; i < ilen; i++)
                        {
                            track_info[i] = new TrackFaceInfo();
                            track_info[i].landmarks = new int[144];
                            track_info[i].headPose = new float[3];
                            track_info[i].face_id = 0;
                            track_info[i].score = 0;
                        }
                        int sizeTrack = Marshal.SizeOf(typeof(TrackFaceInfo));
                        IntPtr ptT = Marshal.AllocHGlobal(sizeTrack * ilen);
                        int faceSize = ilen;//返回人脸数  分配人脸数和检测到人脸数的最小值
                        int curSize = ilen;//当前人脸数 输入分配的人脸数，输出实际检测到的人脸数
                        faceSize = CacheData.Ins.faceUtil.FaceTrack(ptT, image.CvPtr, ref curSize);
                        for (int index = 0; index < faceSize; index++)
                        {
                            IntPtr ptr = new IntPtr();
                            if (8 == IntPtr.Size)
                            {
                                ptr = (IntPtr)(ptT.ToInt64() + sizeTrack * index);
                            }
                            else if (4 == IntPtr.Size)
                            {
                                ptr = (IntPtr)(ptT.ToInt32() + sizeTrack * index);
                            }

                            track_info[index] = (TrackFaceInfo)Marshal.PtrToStructure(ptr, typeof(TrackFaceInfo));

                            //// 画人脸框
                            drawbox(track_info[index], ref image);
                        }
                        Marshal.FreeHGlobal(ptT);

#endif
                        if (isScaning)
                        {
                            Bitmap _bitmap;
                            img = MatToBitmapImage(image, out _bitmap);
                            bitmap = _bitmap.DeepClone();
                            OnImageBack?.Invoke(img);
                            // picBox.Source = img;
                        }
                        Cv2.WaitKey(1);
                    }
                    else
                    {
                        Console.WriteLine("mat is empty");
                    }
                }
            }
        }
        public void drawbox(TrackFaceInfo faceinfo, ref Mat image)
        {
            RotatedRect box;
            box = bounding_box(faceinfo.landmarks, faceinfo.landmarks.Length);
            draw_rotated_box(ref image, ref box, new Scalar(0, 255, 0));
        }
        public RotatedRect bounding_box(int[] landmarks, int size)
        {
            int min_x = 1000000;
            int min_y = 1000000;
            int max_x = -1000000;
            int max_y = -1000000;
            for (int i = 0; i < size / 2; ++i)
            {
                min_x = (min_x < landmarks[2 * i] ? min_x : landmarks[2 * i]);
                min_y = (min_y < landmarks[2 * i + 1] ? min_y : landmarks[2 * i + 1]);
                max_x = (max_x > landmarks[2 * i] ? max_x : landmarks[2 * i]);
                max_y = (max_y > landmarks[2 * i + 1] ? max_y : landmarks[2 * i + 1]);
            }
            int width = ((max_x - min_x) + (max_y - min_y)) / 2;
            float angle = 0;
            Point2f center = new Point2f((min_x + max_x) / 2, (min_y + max_y) / 2);
            return new RotatedRect(center, new Size2f(width, width), angle);
        }
        public void draw_rotated_box(ref Mat img, ref RotatedRect box, Scalar color)
        {
            Point2f[] vertices = new Point2f[4];
            vertices = box.Points();
            for (int j = 0; j < 4; j++)
            {
                Cv2.Line(img, (OpenCvSharp.Point)vertices[j], (OpenCvSharp.Point)vertices[(j + 1) % 4], color);
            }
        }
#region 图片格式相关
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        // 图片文件转bytes
        public static byte[] img2byte(System.Drawing.Image img)
        {
            //将Image转换成流数据，并保存为byte[]
            MemoryStream mstream = new MemoryStream();
            img.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length);
            mstream.Close();
            return byData;
        }
        // bytes转图片文件
        public static System.Drawing.Image byte2img(byte[] b, string file_name)
        {
            MemoryStream ms = new MemoryStream(b);
            ms.Position = 0;
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
            //img.Save(file_name);
            //ms.Close();
        }

        public Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }
        public BitmapImage MatToBitmapImage(Mat image, out Bitmap bitmap)
        {
            bitmap = MatToBitmap(image);
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
#endregion
    }
}

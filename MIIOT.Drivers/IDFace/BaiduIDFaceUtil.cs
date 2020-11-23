using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace MIIOT
{
    public static class DevFace
    {
        private const string DLLNAME = @".\IDFace\BaiduFaceApi.dll";
        // sdk初始化
        [DllImport(DLLNAME, EntryPoint = "sdk_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int sdk_init(string model_path, bool id_card);
        // 是否授权
        [DllImport(DLLNAME, EntryPoint = "is_auth", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool is_auth();
        // 获取设备指纹
        [DllImport(DLLNAME, EntryPoint = "get_device_id", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr get_device_id();

        [DllImport(DLLNAME, EntryPoint = "clear_tracked_faces", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void clear_tracked_faces();

        [DllImport(DLLNAME, EntryPoint = "set_track_by_detection_interval", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void set_track_by_detection_interval(int interval_in_ms = 300);

        [DllImport(DLLNAME, EntryPoint = "set_detect_in_video_interval", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void set_detect_in_video_interval(int interval_in_ms = 300);
        // sdk销毁
        [DllImport(DLLNAME, EntryPoint = "sdk_destroy", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sdk_destroy();

        [DllImport(DLLNAME, EntryPoint = "set_occlu_thr", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void set_occlu_thr(float thr);

        // 提取人脸特征值(传二进制图片buffer）
        [DllImport(DLLNAME, EntryPoint = "get_face_feature_by_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr get_face_feature_by_buf(byte[] buf, int size, ref int length);

        // 人脸注册(传入图片二进制buffer)
        [DllImport(DLLNAME, EntryPoint = "user_add_by_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr user_add_by_buf(string user_id, string group_id, byte[] image, int size, string user_info = "");

        // 人脸注册(传入特征值)
        [DllImport(DLLNAME, EntryPoint = "user_add_by_feature", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr user_add_by_feature(string user_id, string group_id, byte[] fea, int fea_len, string user_info = "");

        // 1:N人脸识别（传图片二进制文件buffer和库里的比对）
        [DllImport(DLLNAME, EntryPoint = "identify_by_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr identify_by_buf(byte[] buf, int size, string group_id_list, string user_id, int user_top_num = 1);

        // 提前加载库里所有数据到内存中
        [DllImport(DLLNAME, EntryPoint = "load_db_face", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool load_db_face();

        // 1:N人脸识别（传人脸图片文件和内存已加载的整个库数据比对）
        [DllImport(DLLNAME, EntryPoint = "identify_by_buf_with_all", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr identify_by_buf_with_all(byte[] image, int size, int user_top_num = 1);
        // 用户删除
        [DllImport(DLLNAME, EntryPoint = "user_delete", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr user_delete(string user_id, string group_id);

        // 用户组列表查询
        [DllImport(DLLNAME, EntryPoint = "get_user_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr get_user_list(string group_id, int start = 0, int length = 100);
        // 组列表查询
        [DllImport(DLLNAME, EntryPoint = "get_group_list", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr get_group_list(int start = 0, int length = 100);
        /*  trackMat
         *  传入参数: maxTrackObjNum:检测到的最大人脸数，传入外部分配人脸数，需要分配对应的内存大小。
         *            传出检测到的最大人脸数
         *    返回值: 传入的人脸数 和 检测到的人脸数 中的最小值。
         ****/
        [DllImport(DLLNAME, EntryPoint = "track_mat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int track_mat(IntPtr ptr, IntPtr mat, ref int max_track_num);
        // 单目RGB静默活体检测（传入图片文件二进制buffer)
        [DllImport(DLLNAME, EntryPoint = "rgb_liveness_check_by_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rgb_liveness_check_by_buf(byte[] buf, int size);

        // 人脸1:1比对（传二进制图片buffer）
        [DllImport(DLLNAME, EntryPoint = "match_by_buf", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr match_by_buf(byte[] buf1, int size1, byte[] buf2, int size2);
    }
    public class BaiduIDFaceUtil
    {
        public BaiduIDFaceUtil()
        {

            // id_card=false表示采用生活照模式（通常采用的模式）
            // id_card=true表示采用证件照模式（如识别身份证等小图）和生活照模式不能混用
            bool id_card = false;
            // model_path为模型文件夹路径，即face-resource文件夹（里面存的是人脸识别的模型文件）
            // 传空为采用默认路径，若想定置化路径，请填写全局路径如：d:\\model
            // 若模型文件夹采用定置化路径，则激活文件(license.ini, license.key)也需要放置到该目录，否则激活文件会找不到路径
            string model_path = null;
            // string model_path="d:\\model";
            int n = DevFace.sdk_init(model_path, id_card);
            if (n != 0)
            {
                Console.WriteLine("auth result is {0:D}", n);
                Console.ReadLine();
            }

            // 测试是否授权
            bool authed = DevFace.is_auth();

            Console.WriteLine("authed res is:" + authed);
        }
        // 测试获取人脸特征值(512个byte）
        public byte[] get_face_feature_by_buf(byte[] img_bytes, bool id_card = false)
        {
            try
            {
                int dim_count = 512;
                if (id_card)
                {
                    dim_count = 2048;
                }

                Console.WriteLine("开始取特征值:" + img_bytes.GetHashCode());
                // 证件照模式特征值长度为2048，生活照模式为512
                byte[] fea = new byte[dim_count];
                int len = 0;
                IntPtr ptr = DevFace.get_face_feature_by_buf(img_bytes, img_bytes.Length, ref len);
                if (ptr == IntPtr.Zero)
                {
                    return null;
                }
                else
                {
                    if (len > 0)
                    {
                        Marshal.Copy(ptr, fea, 0, len);
                        // 可保存特征值512个字节的fea到文件中
                        //  FileUtil.byte2file("d:\\fea2.txt",fea, len);
                        Console.WriteLine("取得特征值——————" + fea.GetHashCode());

                        return fea;
                    }
                    else
                    {
                        Console.WriteLine("get face feature cout为0");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("identify_by_buf_with_all", ex);
                return null;
            }
        }
        // 测试人脸注册(传入特征值)
        public bool user_add_by_feature(string user_id, string group_id, byte[] feature)
        {
            Console.WriteLine("特征值——" + user_id + "————" + feature.Length + "+" + feature.GetHashCode());
            // 人脸注册
            // 传入人脸特征值，提取特征值demo，可参考FaceCompare文件中
            //byte[] feature = new byte[512];
            string user_info = "user_info";
            IntPtr ptr = DevFace.user_add_by_feature(user_id, group_id, feature, feature.Length, user_info);
            string buf = Marshal.PtrToStringAnsi(ptr);
            dynamic res = JsonConvert.DeserializeObject(buf);
            if (res.msg == "success")
            {
                Console.WriteLine("user_add_by_feature res is:" + buf);
                return true;
            }
            Console.WriteLine("user_add_by_feature res is:" + buf);
            return false;
        }
        // 测试1:1比较，传入图片文件二进制buffer
        public bool match_by_buf(byte[] img_bytes1, byte[] img_bytes2)
        {
            mathData data = null;
            try
            {
                DevFace.clear_tracked_faces();
                DevFace.set_detect_in_video_interval(100);
                DevFace.set_track_by_detection_interval(100);
                IntPtr ptr = DevFace.match_by_buf(img_bytes1, img_bytes1.Length, img_bytes2, img_bytes2.Length);
                string buf = Marshal.PtrToStringAnsi(ptr);
                Console.WriteLine("对比结果：" + buf);
                data = JsonConvert.DeserializeObject<mathData>(buf);
                if (data != null && data.msg == "success")
                {
                    double score = 0;
                    double.TryParse(data.data.score, out score);
                    if (score > 90)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (data != null)
                    Log.Error("match_by_buf" + data.data, ex);
                else
                    Log.Error("match_by_buf", ex);
            }
            return false;
        }

        // 测试人脸注册(传入二进制图片buffer)
        public bool RegisterFace(string user_id, string group_id, byte[] img_bytes)
        {
            try
            {
                byte[] feature = new byte[512];
                // 人脸注册
                //Image img = System.Drawing.Image.FromFile("d:\\4.jpg");
                //byte[] img_bytes = ImageUtil.img2byte(img);
                string user_info = "user_info"; //用户资料，256个字符以内
                IntPtr ptr = DevFace.user_add_by_buf(user_id, group_id, img_bytes, img_bytes.Length, user_info);
                string buf = Marshal.PtrToStringAnsi(ptr);
                dynamic res = JsonConvert.DeserializeObject(buf);
                if (res.msg == "success")
                {
                    Console.WriteLine("add 成功:" + buf);
                    return true;
                    // return get_face_feature_by_buf(img_bytes);
                }
            }
            catch (Exception ex)
            {
                Log.Error("identify_by_buf_with_all", ex);
            }
            return false;
        }

        // 测试单目RGB静默活体检测（传入图片文件二进制buffer)
        public bool rgb_liveness_check_by_buf(byte[] img_bytes)
        {
            try
            {
                IntPtr ptr = DevFace.rgb_liveness_check_by_buf(img_bytes, img_bytes.Length);
                string buf = Marshal.PtrToStringAnsi(ptr);
                // Console.WriteLine("识别真人："+ img_bytes.GetHashCode() + buf.Trim());
                mathData res = JsonConvert.DeserializeObject<mathData>(buf);
                if (res != null && res.msg == "success")
                {
                    double score = 0;
                    double.TryParse(res.data.score, out score);
                    if (score > 0.3)
                    {
                        Console.WriteLine("是真人活体" + res.data.score);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("rgb_liveness_check_by_buf", ex);
            }
            return false;
        }
        // 测试1:N比较，传入图片文件二进制buffer
        public bool identify_by_buf(byte[] img_bytes)
        {
            try
            {
                string user_group = "1";
                string user_id = "123";
                IntPtr ptr = DevFace.identify_by_buf(img_bytes, img_bytes.Length, user_group, user_id);
                string buf = Marshal.PtrToStringAnsi(ptr);
                dynamic res = JsonConvert.DeserializeObject(buf);
                if (res.msg == "success")
                {
                    Console.WriteLine("identify_by_buf res is:" + res.msg);
                    return true;
                }
                Console.WriteLine("identify_by_buf res is:" + buf);
            }
            catch (Exception ex)
            {
                Log.Error("identify_by_buf_with_all", ex);
            }
            return false;
        }

        public bool user_delete(string user_id, string group_id)
        {
            try
            {
                IntPtr ptr = DevFace.user_delete(user_id, group_id);
                string buf = Marshal.PtrToStringAnsi(ptr);
                Console.WriteLine("user_delete res is:" + buf);
            }
            catch (Exception ex)
            {
                Log.Error("user_delete", ex);
            }
            return true;
        }
        // 用户组列表查询
        public void get_user_list()
        {
            try
            {
                string group_id = "1";
                IntPtr ptr = DevFace.get_user_list(group_id);
                string buf = Marshal.PtrToStringAnsi(ptr);
                Console.WriteLine("get_user_list res is:" + buf);
            }
            catch (Exception ex)
            {
                Log.Error("get_user_list", ex);
            }
        }
        // 组列表查询
        public FaceListData test_get_group_list()
        {
            try
            {


                IntPtr ptr = DevFace.get_group_list();
                string buf = Marshal.PtrToStringAnsi(ptr);
                Console.WriteLine("get_group_list res is:" + buf);
                FaceListData res = JsonConvert.DeserializeObject<FaceListData>(buf);
                if (res != null && res.msg == "success")
                {
                    return res;
                }
            }
            catch (Exception ex)
            {
                Log.Error("test_get_group_list", ex);
            }
            return null;


        }
        // 测试1:N比较，传入图片文件二进制buffer和已加载的内存中整个库比较
        public FaceData identify_by_buf_with_all(byte[] img_bytes, bool ChkLive = false)
        {
            try
            {



                //if (ChkLive)
                //    if (!rgb_liveness_check_by_buf(img_bytes)) return null;
                IntPtr ptr = DevFace.identify_by_buf_with_all(img_bytes, img_bytes.Length);
                string buf = Marshal.PtrToStringAnsi(ptr);
                Console.WriteLine("识别中" + buf);
                FaceData res = JsonConvert.DeserializeObject<FaceData>(buf);
                if (res != null && res.msg == "success")
                {
                    if (res.data != null && res.data.result != null && res.data.result.Count > 0)
                    {
                        if (double.Parse(res.data.result[0].score) > 90)
                        {
                            return res;
                        }
                    }
                    return null;
                }
                else
                {
                    Console.WriteLine(buf);
                }
            }
            catch (Exception ex)
            {
                Log.Error("identify_by_buf_with_all", ex);
            }
            return null;
        }

        public int FaceTrack(IntPtr ptT, IntPtr CvPtr, ref int curSize)
        {
            int faceSize = DevFace.track_mat(ptT, CvPtr, ref curSize);
            return faceSize;
        }

    }


    //   [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Size = 596)]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TrackFaceInfo
    {
        [MarshalAs(UnmanagedType.Struct)]
        public FaceInfo box;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 144)]
        public int[] landmarks;// = new int[144];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] headPose;// = new float[3];
        public float score;// = 0.0F;
        public UInt32 face_id;// = 0;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    //[StructLayout(LayoutKind.Explicit)]
    public struct FaceInfo
    {
        public FaceInfo(float iWidth, float iAngle, float iCenter_x, float iCenter_y, float iConf)
        {
            mWidth = iWidth;
            mAngle = iAngle;
            mCenter_x = iCenter_x;
            mCenter_y = iCenter_y;
            mConf = iConf;
        }
        public float mWidth;     // rectangle width
        public float mAngle;//; = 0.0F;     // rectangle tilt angle [-45 45] in degrees
        public float mCenter_y;// = 0.0F;  // rectangle center y
        public float mCenter_x;// = 0.0F;  // rectangle center x
        public float mConf;// = 0.0F;
    };


    public class BaseData
    {
        public int errno { get; set; }
        public string msg { get; set; }
    }
    public class FaceData : BaseData
    {
        public ResultData data { get; set; }

    }



    public class ResultData
    {
        public string face_token { get; set; }
        public string log_id { get; set; }
        public int result_num { get; set; }
        public List<UserResult> result = new List<UserResult>();

    }

    public class UserResult
    {
        public string group_id { get; set; }
        public string user_id { get; set; }
        public string score { get; set; }
    }
    public class FaceListData : BaseData
    {
        public listDetal data { get; set; }
    }
    public class listDetal
    {
        public string log_id { get; set; }
        public List<UserList> user_id_list { get; set; } = new List<UserList>();
    }
    public class UserList
    {
        public string user_id { get; set; }
    }
    public class mathData : BaseData
    {
        public mathDetal data { get; set; }
    }

    public class mathDetal
    {
        public string score { get; set; }
        public string log_id { get; set; }
        public override string ToString()
        {
            Console.WriteLine(score + ">" + log_id);
            return base.ToString();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/7/24 11:56:25
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Trical.Common
{
    public class SunmiUtil
    {
        private static object obj = new object();
        private static SunmiUtil _ins;

        public static SunmiUtil Ins
        {
            get
            {
                lock (obj)
                {
                    if (_ins == null)
                        _ins = new SunmiUtil();
                }
                return _ins;
            }
        }

        string Host = "https://store.uat.sunmi.com/openapi";
        public SunmiUtil()
        {

        }
        public void BindShop()
        {
            string url = "/shop/bind";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("company_id", "476934531001");
            paras.Add("shop_name", "医智联测试");
            paras.Add("company_name", "医智联");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("sunmi_shop_key", "DDZ1D3382UHRERPUNKE3");
            paras.Add("contact_person", "陈夏松");
            paras.Add("phone", "13928459050");
            Post(url, paras);
        }
        #region 商品

        public void AddGoods(string GoodsList)
        {
            string url = "/product/create";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("product_list", GoodsList);
            Post(url, paras);

        }
        public void UpdateGoods(string GoodsList)
        {
            string url = "/product/update";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("product_list", GoodsList);
            Post(url, paras);


        }
        public void QueryGoods()
        {
            string url = "/product/getList";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            Post(url, paras);
        }
        public void DeleteGoods(List<int> Ids)
        {
            string url = "/product/delete";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            string jsstr = JsonConvert.SerializeObject(Ids);
            paras.Add("product_key_list", jsstr);
            Post(url, paras);
        }
        #endregion
        public void BindLable()
        {
            string url = "/product/create";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("product_id", "470206181008");
            paras.Add("esl_code", "470206181008");
            paras.Add("template_id", "");
            Post(url, paras);
        }

        #region 模板

        public void CreateTemp()
        {
            string url = "/template/create";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("template_name", "470206181008");
            paras.Add("template_color", "470206181008");
            paras.Add("template_screen", "");
            paras.Add("template_json", "");//json格式
            Post(url, paras);
        }
        public void UpdateTemp()
        {
            string url = "/template/update";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("template_name", "470206181008");
            paras.Add("template_json", "");//json格式
            Post(url, paras);
        }
        public void QueryTemp()
        {
            string url = "/template/getList";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("page_num", "1");
            paras.Add("page_size", "10");
            Post(url, paras);
        }
        public void GetTempInfo()
        {
            string url = "/template/getInfo";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("template_id", "470206181008");
            Post(url, paras);
        }
        public void DeleteTemp()
        {
            string url = "/template/delete";
            Dictionary<string, string> paras = new Dictionary<string, string>();
            paras.Add("shop_id", "470206181008");
            paras.Add("sunmi_shop_no", "470206181008");
            paras.Add("template_id_list", "470206181008");
            Post(url, paras);
        }
        #endregion

        private void Post(string url, IDictionary<string, string> paras)
        {
            long timeStamp = DateTime.Now.DateTimeToUnixTimestamp();
            List<string> para = new List<string>();
            para.Add("app_id=06W7827Y7NPM3");
            para.Add("random=yzl123");
            para.Add($"timestamp={timeStamp}");
            foreach (var item in paras)
            {
                para.Add($"{item.Key}={item.Value}");
            }
            para.Sort();
            para.Add("key=DKLzRdvAeWLvSCeCn0HP");
            string sign = string.Join("&", para).MD5Encrypt();

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            for (int i = 0; i < para.Count; i++)
            {
                string[] strs = para[i].Split('=');
                if (strs[0] == "key") continue;
                parameters.Add(strs[0], strs[1]);
            }
            parameters.Add("sign", sign);
            string result = Common.HttpWebResponseUtility.CreatePostHttpResponse(Host + url, parameters, null, null, Encoding.GetEncoding("UTF-8"), null);
        }
    }
    public class ESLGoodsModel
    {
        public long id { get; set; }
        public long seq_num { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string bar_code { get; set; }
        public string alias { get; set; }
        public string unit { get; set; }
        public string spec { get; set; }
        public string level { get; set; }
        public string area { get; set; }
        public string brand { get; set; }
        public string qr_code { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public double promote_price { get; set; }
        public string promote_price_description { get; set; }
        public double member_price { get; set; }
        public string member_price_description { get; set; }

    }
}

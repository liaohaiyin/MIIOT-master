using MIIOT.Model.Trical;
using MIIOT.Trical.Common;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Trical
{
    public class HttpResolver
    {
        HttpClientHelper httpClientHelper = new HttpClientHelper();
        public HttpResolver()
        {
            httpClientHelper.init(CacheData.Ins.JsonConfig["Host"]);
        }
        HttpHelperEx HTTP = new HttpHelperEx();
        public async Task<HttpResultBase> Request(HMethod method, string url, string token, Dictionary<string, string> UrlPara = null, string data = "")
        {
            HttpResultBase result = new HttpResultBase();
            HttpResult res = new HttpResult();
            switch (method)
            {
                case HMethod.Get:
                    res = await httpClientHelper.GetRequest(url, token);
                    break;
                case HMethod.Post:
                    res = await httpClientHelper.PostRequest(url, UrlPara, token, data);
                    break;
                case HMethod.Put:
                    res = await httpClientHelper.PutRequest(url, token, data);
                    break;
                case HMethod.Delete:
                    res = await httpClientHelper.DeleteRequest(url, token);
                    break;
                default:
                    res = await httpClientHelper.GetRequest(url, token);
                    break;
            }
            Log.Info(method + url + data);
            Log.Info(res.data);
            string rece = "";
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("token", CacheData.Ins.Token);
            //switch (method)
            //{
            //    case HMethod.Get:
            //        rece = HttpHelperEx.HttpGet(CacheData.Ins.JsonConfig["Host"]+url, "application/json", dic);
            //        break;
            //    case HMethod.Post:
            //        url = httpClientHelper.BuildUrl(url, "username", "admin");
            //        url = httpClientHelper.BuildUrl(url, "password", "123456");
            //        rece = HttpHelperEx.HttpPost(CacheData.Ins.JsonConfig["Host"] + url, data);
            //        break;
            //    case HMethod.Put:
            //        break;
            //    case HMethod.Delete:
            //        break;
            //    default:
            //        res = await httpClientHelper.GetRequest(url, token);
            //        break;
            //}
            if (res.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<HttpResultBase>(res.data);
                    if (result != null && result.code == 200)
                    {
                        return result;
                    }
                    else
                    {
                        if (result.code == 401)
                        {
                            //CacheData.Ins.mainWindow.Login();
                            //result = await Request(method, url, token, UrlPara, data);
                        }
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    result.code = -1;
                    return result;
                }
                
            }
            else
            {
                result.code = (int)res.StatusCode;
                result.msg = "";
                return result;
            }

        }
    }
    public enum HMethod
    {
        Get = 0,
        Post = 1,
        Put = 2,
        Delete = 3
    }
}

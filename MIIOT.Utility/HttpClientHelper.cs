using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    public class HttpClientHelper
    {
       
        public HttpClientHelper()
        {
           
        }
        public void init(string _Host)
        {
            Host = _Host;
            var handler = new HttpClientHandler() { UseCookies = false };
            httpClient = new HttpClient(handler);
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private string Host = "";
        HttpClient httpClient ;
        public async Task<HttpResult> GetRequest(string url, string token)
        {
            HttpResult httpResult = new HttpResult();
            if (httpClient.DefaultRequestHeaders.Contains("token"))
                httpClient.DefaultRequestHeaders.Remove("token");
            httpClient.DefaultRequestHeaders.Add("token", token);
            await httpClient.GetAsync(Host + url).ContinueWith(
            async (requestTask) =>
             {
                 HttpResponseMessage response = requestTask.Result;
                 httpResult.StatusCode = response.StatusCode;
                 // 确认响应成功，否则抛出异常
                 response.EnsureSuccessStatusCode();
                 // 异步读取响应为字符串
                 httpResult.data = await response.Content.ReadAsStringAsync();
             });
            return httpResult;

        }
        public async Task<HttpResult> PostRequest(string url, Dictionary<string, string> UrlPara, string token, string data)
        {
            if (UrlPara != null)
            {
                foreach (var item in UrlPara)
                {
                    url = BuildUrl(url, item.Key, item.Value);
                }
            }

            HttpResult httpResult = new HttpResult();
            HttpContent postContent = new StringContent(data);
            //if (!url.Contains("login"))
                postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            if (httpClient.DefaultRequestHeaders.Contains("token"))
                httpClient.DefaultRequestHeaders.Remove("token");
            httpClient.DefaultRequestHeaders.Add("token", token);
            await httpClient
                    .PostAsync(Host + url, postContent)
                    .ContinueWith(
                  async (postTask) =>
                    {
                        HttpResponseMessage response = postTask.Result;
                        httpResult.StatusCode = response.StatusCode;

                        // 确认响应成功，否则抛出异常
                        var tem = response.EnsureSuccessStatusCode();
                        // 异步读取响应为字符串
                        httpResult.data = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("响应是否成功：" + response.IsSuccessStatusCode);
                        var headers = response.Headers;
                        foreach (var header in headers)
                        {
                            Console.WriteLine("{0}: {1}", header.Key, string.Join("", header.Value.ToList()));
                        }
                    });
            return httpResult;
        }
        public async Task<HttpResult> PutRequest(string url, string token, string data)
        {
            HttpResult httpResult = new HttpResult();
            HttpContent postContent = new StringContent(data);
            if (httpClient.DefaultRequestHeaders.Contains("token"))
                httpClient.DefaultRequestHeaders.Remove("token");
            httpClient.DefaultRequestHeaders.Add("token", token);
            await httpClient
                    .PutAsync(Host + url, postContent)
                    .ContinueWith(
                  async (postTask) =>
                  {
                      HttpResponseMessage response = postTask.Result;
                      httpResult.StatusCode = response.StatusCode;
                      // 确认响应成功，否则抛出异常
                      response.EnsureSuccessStatusCode();
                      // 异步读取响应为字符串
                      httpResult.data = await response.Content.ReadAsStringAsync();
                      Console.WriteLine("响应是否成功：" + response.IsSuccessStatusCode);

                      var headers = response.Headers;
                      foreach (var header in headers)
                      {
                          Console.WriteLine("{0}: {1}", header.Key, string.Join("", header.Value.ToList()));
                      }
                  });
            return httpResult;
        }
        public async Task<HttpResult> DeleteRequest(string url, string token)
        {
            HttpResult httpResult = new HttpResult();
            if (httpClient.DefaultRequestHeaders.Contains("token"))
                httpClient.DefaultRequestHeaders.Remove("token");
            httpClient.DefaultRequestHeaders.Add("token", token);
            await httpClient.DeleteAsync(Host + url).ContinueWith(
            async (requestTask) =>
            {
                HttpResponseMessage response = requestTask.Result;
                httpResult.StatusCode = response.StatusCode;
                // 确认响应成功，否则抛出异常
                response.EnsureSuccessStatusCode();
                // 异步读取响应为字符串
                httpResult.data = await response.Content.ReadAsStringAsync();
            });
            return httpResult;

        }
        public string BuildUrl(string url, string ParamText, string ParamValue)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", ParamText), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string _url = reg.Replace(url, "");
            //_url = reg1.Replace(_url, "");
            if (_url.IndexOf("?") == -1)
                _url += string.Format("?{0}={1}", ParamText, ParamValue);//?
            else
                _url += string.Format("&{0}={1}", ParamText, ParamValue);//&
            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }
    }
    public class HttpResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string data { get; set; } = "";
    }
}

using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/21 14:42:02
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart.Common
{
    public class SMHttpRequester
    {
        public string Host { get; set; }

        HttpClient httpClient;
        public SMHttpRequester(string host)
        {
            Host = host; 
            var handler = new HttpClientHandler() { UseCookies = false };
            httpClient = new HttpClient(handler);
        }
        public async Task<HttpResult> PostRequest(string url,  string data)
        {
            HttpResult httpResult = new HttpResult();
            HttpContent postContent = new StringContent(data);
            //if (!url.Contains("login"))
            postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            string TS = DateTime.Now.DateTimeToUnixTimestamp().ToString();
            postContent.Headers.Add("_t", TS);
            string sign = (TS + "20200821").sm3Digest();
            postContent.Headers.Add("sign", sign);
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
    }
}

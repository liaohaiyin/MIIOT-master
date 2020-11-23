using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/21 10:31:08
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Utility
{
    public class ConnectDetection
    {
        public static bool isConnet(string Host)
        {
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                options.DontFragment = true;

                PingReply reply = pingSender.Send(Host, 10000);
                if (reply.Status == IPStatus.Success || reply.Status == IPStatus.TimedOut)
                {
                    return true;
                }
                else
                {

                }
                {

                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

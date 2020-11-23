using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    public class SSRequestInfo : IRequestInfo
    {
        public SSRequestInfo(byte[] header, byte[] body)
        {
            Key = (header[2]).ToString();
            Body = System.Text.Encoding.UTF8.GetString(body, 0, body.Length);
            IsHeart = string.Equals("143", Key);
        }
        public string Key { get; set; }

        public bool IsHeart { get; set; }

        public string Body { get; set; }
    }
}

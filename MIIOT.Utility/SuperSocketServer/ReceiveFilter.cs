using SuperSocket.Facility.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    //数据格式：
    //  -------+----------+------------------------------------------------------+
    //  FAF50000| 00000010|  4C36 3150 2D43 4D2B 4C30 3643 5055 2D43 4D2B 4C 4A  |
    //  固定头  | 数据长度  |  数据                                                |
    //         |          |                                                      |
    //  -------+----------+------------------------------------------------------+

    public class SSReceiveFilter : FixedHeaderReceiveFilter<SSRequestInfo>
    {
        //前8个字节为包头长度（headerSize）
        public SSReceiveFilter() : base(8)
        {

        }

        //解析消息中长度
        protected override int GetBodyLengthFromHeader(byte[] header, int offset, int length)
        {
            byte[] buffLen = new byte[] { header[offset + 4], header[offset + 5], header[offset + 6], header[offset + 7] };
            
            var bodyLength = BitConverter.ToInt32(buffLen, 0); //(int)header[offset + 2] * 256 + (int)header[offset + 3];
            return bodyLength;
        }

        //解析收到的数据
        protected override SSRequestInfo ResolveRequestInfo(ArraySegment<byte> header, byte[] bodyBuffer, int offset, int length)
        {
            if (bodyBuffer == null) return null;

            var body = bodyBuffer.Skip(offset).Take(length).ToArray();

            var info = new SSRequestInfo(header.ToArray(), body);
            return info;
        }
    }
}

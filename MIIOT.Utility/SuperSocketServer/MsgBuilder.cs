using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    /// <summary>
    /// 对传输命令的封装
    /// </summary>
    public class MsgBuilder
    {
        //消息包头部为01
        private const ushort HeartMsg = 0xFAF5;
        //心跳包头部为02
        private const ushort HeaderHeart = 2;

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static byte[] BuildMsgCmd(string msg)
        {
            var arr = new List<byte> { };
            //转为byte数组并进行大小端转换
            var byteHeader = BitConverter.GetBytes(HeartMsg).Reverse().ToList();
            var byteMsg = Encoding.UTF8.GetBytes(msg);
            var byteLength = BitConverter.GetBytes(byteMsg.Length).ToList();
            byte[] headerTail = new byte[2];
            headerTail[0] = 0x16;
            headerTail[1] = byteMsg.CalcationCheckout();
            arr.AddRange(byteHeader);
            arr.AddRange(headerTail);
            arr.AddRange(byteLength);
            arr.AddRange(byteMsg);
            return arr.ToArray();
        }
        /// <summary>
        /// 发送心跳
        /// </summary>
        /// <returns></returns>
        public static byte[] BuildHeartCmd()
        {
            var arr = new List<byte> { };
            //心跳内容部分传输当前时间
            var byteMsg = Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //转为byte数组并进行大小端转换
            var byteHeader = BitConverter.GetBytes(HeaderHeart).Reverse().ToList();
            var byteLength = BitConverter.GetBytes((ushort)byteMsg.Length).Reverse().ToList();

            arr.AddRange(byteHeader);
            arr.AddRange(byteLength);
            arr.AddRange(byteMsg);
            return arr.ToArray();
        }
    }
}

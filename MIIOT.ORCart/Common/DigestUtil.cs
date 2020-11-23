using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/21 14:38:36
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart.Common
{
    public static class DigestUtil
    {
        public static string sm3Digest(this string code)
        {
            //加密
            byte[] msg = Encoding.Default.GetBytes(code);
            byte[] md = new byte[32];
            SM3Digest sm3 = new SM3Digest();
            sm3.BlockUpdate(msg, 0, msg.Length);
            sm3.DoFinal(md, 0);
            string PasswdDigest = new UTF8Encoding().GetString(Hex.Encode(md));
            return PasswdDigest;
        }
    }
}

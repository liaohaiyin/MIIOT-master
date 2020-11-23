using System;
using System.IO;

namespace MIIOT.DiagManager.Core
{
    public static class GlobalConst
    {
        /// <summary>
        /// 登录记录保存路径
        /// </summary>
        public static readonly string LoginRecordSavePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "login.json");
    }
}

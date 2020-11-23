using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Core
{
    public static class GlobalParameters
    {
        /// <summary>
        /// 登录密码最大长度
        /// </summary>
        public static int PwdMaxLength { get; set; } = 20;

        /// <summary>
        /// 登录密码最小长度
        /// </summary>
        public static int PwdMinLength { get; set; } = 1;

        /// <summary>
        /// Slider控件最大值
        /// </summary>
        public static double MaxValue { get; set; } = 1000;

    }
}

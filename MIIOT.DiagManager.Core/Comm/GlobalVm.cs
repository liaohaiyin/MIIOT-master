
using MIIOT.Data.Service.Helper;
using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MIIOT.DiagManager.Core
{
    /// <summary>
    /// 全局可绑定参数（通知）
    /// </summary>
    public class GlobalVm : BaseVm
    {
        public void UpdateLogo()
        {
            AppRuntime.ExecuteOnUI(() =>
            {
                try
                {
                    this.LogoSource = ImageHelper.Base64StringToBitmapImage(SystemConfigHelper.SystemLogo);
                }
                catch
                {
                    this.LogoSource = new BitmapImage(new Uri(@"Resources\Logo.png", UriKind.RelativeOrAbsolute));
                }
            });
        }


        protected ImageSource _logoSource;
        /// <summary>
        /// logo图标
        /// </summary>
        public virtual ImageSource LogoSource
        {
            get { return _logoSource; }
            set { _logoSource = value; OnPropertyChanged(nameof(LogoSource)); }
        }
    }
}

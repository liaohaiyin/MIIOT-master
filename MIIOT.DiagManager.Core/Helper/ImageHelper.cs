using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MIIOT.DiagManager.Core
{
    public class ImageHelper
    {
        /// <summary>
        /// base64格式转BitmapImage
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static BitmapImage Base64StringToBitmapImage(string base64)
        {
            byte[] buffer = Convert.FromBase64String(base64);
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(buffer);
            bmp.EndInit();
            return bmp;
        }
    }
}

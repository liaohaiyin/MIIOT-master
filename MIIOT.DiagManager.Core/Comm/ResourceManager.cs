using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.UI.Core
{
    public class ResourceManager
    {
        /// <summary>
        /// 加载资源文件
        /// </summary>
        /// <param name="path"></param>
        public static void Load(string path)
        {
            var uri = new Uri(path, UriKind.RelativeOrAbsolute);
            var res = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Add(res);
        }
    }
}

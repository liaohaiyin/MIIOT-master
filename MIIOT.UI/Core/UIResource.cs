using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MIIOT.UI
{
    /// <summary>
    /// ui资源
    /// </summary>
    public class UIResource
    {
        /// <summary>
        /// 资源引用的索引
        /// </summary>
        public static int ResourceIndex { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Geometry GetIcon(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;
            try
            {
                if (key.StartsWith("path:"))
                    return new PathGeometry() { Figures = PathFigureCollection.Parse(key.Substring(5, key.Length - 5)) };

                return System.Windows.Application.Current.Resources.MergedDictionaries[ResourceIndex][key] as Geometry;
            }
            catch
            {
                return null;
            }
        }

        public static List<string> GetAllIconKeys()
        {
            List<string> allKeys = new List<string>();
            try
            {
                var res = Application.LoadComponent(new Uri(@"/Brilliants.UI;Component/Styles/Default/Icon.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;
                foreach (string key in res.Keys)
                {
                    allKeys.Add(key);
                }
            }
            catch
            {
                
            }
            return allKeys;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Brush GetBrush(string key)
        {
            try
            {
                return System.Windows.Application.Current.Resources.MergedDictionaries[ResourceIndex][key] as Brush;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 寻找指定类型的第一个子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        /// <summary>
        /// 搜索指定名称的子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindVisualChild<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T && (child as T).Name.Equals(name))
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child, name);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}

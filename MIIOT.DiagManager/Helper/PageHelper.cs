using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Pages;
using MIIOT.DiagManager.Core;
using MIIOT.UI;
using MIIOT.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MIIOT.DiagManager
{
    public class PageHelper
    {
        private static Dictionary<object, Page> _menuPageCache = new Dictionary<object, Page>();
        /// <summary>
        /// 根据菜单信息加载页面
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static Page LoadPage(MenuInfo mi)
        {
            Page page = null;
            if (_menuPageCache.ContainsKey(mi))
            {
                page = _menuPageCache[mi];
                if (page is CommMenuPage)
                {

                    var tabItem = (page as CommMenuPage).MenuTabControl.SelectedItem as NbMenuTabItem;
                    if (tabItem != null)
                    {
                        var subPage = (tabItem.Content as Frame).Tag as NbPage;
                        AppRuntime.CurrentPage = subPage;
                        if (subPage != null)
                            subPage.Init();
                    }
                }
            }
            else
            {
                page = LoadPage(mi?.Path);
                page.Tag = mi;                
                _menuPageCache[mi] = page;
                var subPage = page as NbPage;
                if (subPage != null)
                    subPage.Init();
            }
            return page;
        }

        /// <summary>
        /// 加载指定url的页面
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Page LoadPage(string url)
        {
            Page page = null;
            try
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                page = Application.LoadComponent(uri) as Page;
            }
            catch
            {
                page = new Page();
                var label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.Content = "空页面";
                label.FontSize = 24;
                page.Content = label;
            }
            return page;
        }

        /// <summary>
        /// 加载表单页面
        /// </summary>
        /// <param name="url"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static Page LoadFormPage(string url, object tag = null)
        {
            NbFormPage page = null;
            try
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                page = Application.LoadComponent(uri) as NbFormPage;
                var vm = page.DataContext as NbPageVm;
                if (vm != null)
                    vm.Tag = tag;
            }
            catch
            {
                page = new NbFormPage();
                page.Width = 300;
                page.Height = 300;
                var label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.Content = "空页面";
                label.FontSize = 24;
                page.Content = label;
            }
            return page;
        }


    }
}

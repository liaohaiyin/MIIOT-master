using MIIOT.Comm;
using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.Model;
using MIIOT.UI.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace MIIOT.DiagManager.Pages
{
    /// <summary>
    /// CommMenuPage.xaml 的交互逻辑
    /// </summary>
    public partial class CommMenuPage : Page
    {
        private bool _isInit = false;
        public CommMenuPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isInit)
                return;
            try
            {
                var data = this.Tag as MenuInfo;
                var parentID = data.ID;
                var mis = Core.AppRuntime.Context.ManagerMenus.FindAll(p => p.ParentID == parentID);
                RenderNbMenuItems(mis);
                _isInit = true;
            }
            catch
            {

            }
        }

        private NbMenuTabItem CreateNbMenuTabItem(MenuInfo mi)
        {
            var item = new NbMenuTabItem();
            item.Header = mi.Name;
            item.Tag = mi;
            var frame = new Frame();
            frame.Tag = PageHelper.LoadPage(mi);
            frame.Content = frame.Tag;
            item.Content = frame;
            return item;
        }

        private void RenderNbMenuItems(List<MenuInfo> menuInfos)
        {
            this.MenuTabControl.Items.Clear();
            this.MenuTabControl.SelectionChanged += MenuTabControl_SelectionChanged;
            menuInfos.Sort(new SortAscComparer());
            foreach (var mi in menuInfos)
            {
                this.MenuTabControl.Items.Add(CreateNbMenuTabItem(mi));
            }

            if (this.MenuTabControl.Items.Count > 0)
                (this.MenuTabControl.Items[0] as NbMenuTabItem).IsSelected = true;
        }

        private void MenuTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != e.Source)
                return;//不是tab控件触发,直接返回

            var tabItem = this.MenuTabControl.SelectedItem as NbMenuTabItem;
            if (tabItem == null)
                return;

            var subPage = (tabItem.Content as Frame).Tag as NbPage;
            AppRuntime.CurrentPage = subPage;
            if (subPage != null)
                subPage.Init();
        }

    }
}

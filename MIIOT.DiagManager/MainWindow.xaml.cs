using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Pages;
using MIIOT.DiagManager.Core;
using MIIOT.UI;
using MIIOT.UI.ControlEx;
using MIIOT.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media;

namespace MIIOT.DiagManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : NbWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NbWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var mis = Core.AppRuntime.Context.ManagerMenus.FindAll(p => p.ParentID == -1);
            if (mis?.Count > 0)
                this.RenderNavMenuItems(mis);

            if (string.IsNullOrWhiteSpace(Core.AppRuntime.Context.User?.Name))
                this.UserInfoBtn.Content = "未登录";
            else
                this.UserInfoBtn.Content = Core.AppRuntime.Context.User.Name;

            this.PageContainer.Content = new WorkAcceptInfoPage();
            (this.MenuView.Children[0] as NbNavMenuItem).IsChecked = true;
        }

        private void NbNavMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as NbNavMenuItem;

            this.PageContainer.Navigate(new Page());
            if (this.PageContainer.CanGoBack)
            {
                var entry = this.PageContainer.RemoveBackEntry();
                while (entry != null)
                {
                    entry = this.PageContainer.RemoveBackEntry();
                }
            }

            var page = PageHelper.LoadPage(menuItem.Tag as MenuInfo);            
            this.PageContainer.Content = page;
            //this.PageTitle.Content = menuItem.Content.ToString();
            //this.PageTitle.Icon = menuItem.Icon;
            //AppRuntime.CurrentPage = page;
        }

        private NbNavMenuItem CreateNbNavMenuItem(MenuInfo mi)
        {
            var item = new NbNavMenuItem();
            item.Content = mi.Name;
            item.MenuPath = mi.Path;
            item.MenuParam = mi.Param;
            item.IconWidth = 20;
            item.IconHeight = 20;
            item.MenuID = mi.ID;
            item.Tag = mi;
            try
            {
                item.Icon = UIResource.GetIcon(mi.Icon);
            }
            catch
            {

            }
            item.Checked += NbNavMenuItem_Click;
            return item;
        }

        private void RenderNavMenuItems(List<MenuInfo> menuInfos)
        {
            menuInfos.Sort(new SortAscComparer());
            foreach (var mi in menuInfos)
            {
                this.MenuView.Children.Add(CreateNbNavMenuItem(mi));
            }

            //if (this.MenuView.Children.Count > 0)
            //    (this.MenuView.Children[0] as NbNavMenuItem).IsChecked = true;
        }

        //private void UserInfoBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    //this.UserInfoPopup.HorizontalOffset = this.UserInfoBtn.ActualWidth - this.UserInfoPopup.Width;
        //    this.UserInfoPopup.IsOpen = true;
        //}

        protected override void OnStateChanged(EventArgs e)
        {
            //base.OnStateChanged(e);
            //if (this.WindowState == WindowState.Maximized)
            //    this.MaxButton.ToolTip = "向下还原";
            //else
            //    this.MaxButton.ToolTip = "最大化";
        }

        private void UncheckedNavMenuItem()
        {
            foreach (var item in this.MenuView.Children)
            {
                var navItem = item as NbNavMenuItem;
                if (navItem != null && navItem.IsChecked.HasValue && navItem.IsChecked.Value) navItem.IsChecked = false;
            }
        }

        private void PageContainer_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Refresh)
                e.Cancel = true;
            else if (e.NavigationMode == NavigationMode.Back)
                e.Cancel = true;
        }

        private void NbWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!AppRuntime.IsLogout && NbMessageBox.Warn("是否关闭诊断管理系统") != NbDialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

    }
}

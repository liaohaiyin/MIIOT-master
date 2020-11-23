using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.DataSync;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.ORCart.MainView
{
    /// <summary>
    /// SysSettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SysSettingView : UserControl, IMenuMsgSend
    {
        public SysSettingView()
        {
            InitializeComponent();

            this.Loaded += SysSettingView_Loaded;
        }

        private void SysSettingView_Loaded(object sender, RoutedEventArgs e)
        {
            string autosync = CacheData.Ins.JsonConfig["AutoSync"];
            btnAutoSync.IsChecked = bool.Parse(autosync);
            txtspdHost.Text = CacheData.Ins.JsonConfig["Host"];
            string DBStr = CacheData.Ins.JsonConfig["DBStr"];
            txtDBHost.Text = DBStr.RegexRegion("Data Source=", ";Initial Catalog=");
            txtDBName.Text = DBStr.RegexRegion(";Initial Catalog=", ";Persist Security Info=");
            txtDBuserName.Text = DBStr.RegexRegion(";User ID=", ";Password=");
            txtPassword.Password = DBStr.RegexRegion(";Password=", "");
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
                txtPassword.Password = DBStr.Substring(DBStr.IndexOf("Password=") + 9, DBStr.Length - DBStr.IndexOf("Password=") - 9);
            var temp = CacheData.Ins.dbUtil.Query<spd_stockhouse>("select * from spd_stockhouse where storehousestatus=1 and storehousetype=5", null);
            txtstore.ItemsSource = temp;
            var stock = CacheData.Ins.dbUtil.Query<sys_param>("select * from sys_param where paramtype=@paratype", new { paratype = "CurrStock" });
            if (stock != null && stock.Count > 0)
            {
                int index = 0;
                for (var i = 0; i < temp.Count; i++)
                {
                    if (temp[i].storehouseid == stock[0].paramkey)
                    {
                        index = i;
                        CacheData.Ins.currStock = temp[i];
                    }
                }
                txtstore.SelectedIndex = index;
            }
            btnAutoSync.IsChecked = CacheData.Ins.isAutoSync;
        }

        public void MenuSelected(string menuName)
        {

        }

        public void sendMsg(string code, string MsgType)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string dbstr = $"DataSource={txtDBHost.Text};InitialCatalog={txtDBName.Text};PersistSecurityInfo=True;UserID={txtDBuserName.Text};Password={txtPassword.Password}";
            CacheData.Ins.JsonConfig.Update("DBStr", dbstr);
            CacheData.Ins.JsonConfig.Update("Host", txtspdHost.Text);
            CacheData.Ins.JsonConfig.Update("AutoSync", btnAutoSync.IsChecked.ToString());

            CacheData.Ins.currStock = txtstore.SelectedItem as spd_stockhouse;
            sys_param para = new sys_param();
            para.paramtype = "CurrStock";
            para.paramkey = CacheData.Ins.currStock.storehouseid;
            para.defparamvalue = CacheData.Ins.currStock.storehousename;
            int i = CacheData.Ins.dbUtil.Update(para);
            if (i == 0)
                i = CacheData.Ins.dbUtil.Insert(para);

            CacheData.Ins.mainWindow.MessageTips("保存成功，重启系统后生效");
        }
#if Net
        DataSync.SPDSystemData systemData = new DataSync.SPDSystemData();
#endif
        private void btnSays_Click(object sender, RoutedEventArgs e)
        {
#if  Net

            Task.Run(() =>
            {
                try
                {
                    systemData.InitBasicsData();
                    systemData.GetSurgery(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.GetPreSurgeryReceive(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.GetGoods(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.GetSinglecode(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.GetSinglecodeexec(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.Getvalidity(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.GetRepnelish(DateTime.Now.AddDays(-55), DateTime.Now);
                    systemData.GetStock();
                }
                catch (Exception ex)
                {
                    Log.Error("", ex);
                }
            });
#endif
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton btn)
            {
                CacheData.Ins.isAutoSync = btn.IsChecked == true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

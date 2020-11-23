using MIIOT.Control;
using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Drivers;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace MIIOT
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : UserControl
    {
        List<MacPara> MacParas = new List<MacPara>();
        List<SysPara> _sysParas = new List<SysPara>();
        List<SysPara> macType = new List<SysPara>();
        public SettingView()
        {
            InitializeComponent();
            this.Loaded += SettingView_Loaded;
            foreach (MacTypeEnum item in Enum.GetValues(typeof(MacTypeEnum)))
            {
                macType.Add(new SysPara { ParaKey = item.ToString(), ParaValue = item.GetEnumDescription() });
            }
        }

        private void SettingView_Loaded(object sender, RoutedEventArgs e)
        {
            StkPara.Children.Clear();

            var macParas = LiteDBHelper.Ins.GetCollection<MacPara>();
            if (macParas == null) macParas = new List<MacPara>();
            if (macParas.Count == 0)
            {
                MacPara para = new MacPara();
                para.Active = true;
                para.Cabinet = "1";
                para.COM = "127.0.0.1";
                para.BaudRate = "4000";
                para.Para1 = "2";
                para.MacType = "服务器";
                para.Para2 = "10";
                para.MacKey = "Server";
                macParas.Add(para);
            }
            var macs = macParas.GroupBy(a => a.Cabinet);
            foreach (var item in macs)
            {
                int CabinetId = 0;
                int.TryParse(item.Key, out CabinetId);
                GrpCabinet boxs = new GrpCabinet();
                boxs.CabinetID = CabinetId;
                if (CabinetId > 1)
                    boxs.CabinetName = $"从柜{CabinetId - 1}";
                boxs.DoGrpBtnBack += Box_DoGrpBtnBack;
                boxs.MacTypes.AddRange(macType);
                foreach (var mac in macParas)
                {
                    if (mac.Cabinet == item.Key)
                    {
                        var control = boxs.NewControl(mac);
                        if (control == null) continue;
                        var macc = MacParas.FirstOrDefault(a => a.Cabinet == item.Key && a.MacKey == mac.MacKey);
                        if (macc != null)
                        {
                            control.UpdateStatus(macc.isConnect);
                        }
                    }
                }
                StkPara.Children.Add(boxs);
            }

            txtCabinetID.Text = _sysParas.FirstOrDefault(a => a.ParaKey == txtCabinetID.Name) == null ? "" : _sysParas.FirstOrDefault(a => a.ParaKey == txtCabinetID.Name).ParaValue;
        }

        public void OnConnect(object obj,string macType,string cabientNo,bool isConnected)
        {
            DriverManager NetServer = obj as DriverManager;
            if (NetServer != null)
            {
                UpdateStatus(cabientNo, macType, isConnected);
                // AddLine(NetServer.COM + NetServer.BoudRate);
            }
        }
        private void Control_OnDelete(COMBaudRateControl send)
        {
            StkPara.Children.Remove(send);
            LiteDBHelper.Ins.Delete<MacPara>(send.MacPara.Id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Box_DoGrpBtnBack(GrpCabinet send, int CabinetID)
        {
            if (CabinetID == 1)
            {
                btnAdd_Click(null, null);
            }
            else
            {
                StkPara.Children.Remove(send);
                foreach (var item in send.MacParas)
                {
                    LiteDBHelper.Ins.Delete<MacPara>(item.Id);
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in StkPara.Children)
            {
                if (item is GrpCabinet grp)
                {
                    foreach (var pa in grp.MacParas)
                    {
                        LiteDBHelper.Ins.Upsert<MacPara>(pa);
                    }
                }
            }
            var para = _sysParas.FirstOrDefault(a => a.ParaKey == txtCabinetID.Name);
            if (para == null)
                LiteDBHelper.Ins.Insert<SysPara>(new SysPara() { ParaType = "Para", ParaKey = txtCabinetID.Name, ParaValue = txtCabinetID.Text });
            else
            {
                para.ParaValue = txtCabinetID.Text;
                LiteDBHelper.Ins.Update<SysPara>(para);
            }
            foreach (var item in StkPara.Children)
            {
                if (item is GroupBox gb)
                {

                    if (gb.Content is COMBaudRateControl control)
                    {
                        if (control.MacPara.Id == 0)
                            LiteDBHelper.Ins.Insert<MacPara>(control.MacPara);
                        else
                            LiteDBHelper.Ins.Update<MacPara>(control.MacPara);
                    }
                }

            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            GrpCabinet box = new GrpCabinet();
            box.DoGrpBtnBack += Box_DoGrpBtnBack;
            box.CabinetID = StkPara.Children.Count + 1;
            box.CabinetName = "从柜" + StkPara.Children.Count;
            var paras = _sysParas.FindAll(a => a.ParaType == "MacType");

            box.MacTypes.AddRange(macType);
            foreach (var item in macType)
            {
                if (item.ParaKey != MacTypeEnum.RFID.ToString()) continue;
                MacPara macPara = new MacPara() { MacType = item.ParaType, MacKey = item.ParaKey, Description=item.ParaValue, COM = "COM1", TimeOut = 200, BaudRate = "9600", Active = true, Cabinet = box.CabinetID.ToString() };
                box.NewControl(macPara);
            }
            StkPara.Children.Add(box);
        }

        private void txtCabinetCount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void UpdateStatus(string Cabinet, string MacName, bool isConnect)
        {
            MacParas.Add(new MacPara() { Cabinet = Cabinet, MacKey = MacName, isConnect = isConnect });
        }
    }
}

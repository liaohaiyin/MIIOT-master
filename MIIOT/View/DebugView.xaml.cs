using MIIOT.Drivers;
using MIIOT.Drivers.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.View
{
    /// <summary>
    /// DebugView.xaml 的交互逻辑
    /// </summary>
    public partial class DebugView : UserControl
    {
        public DebugView()
        {
            InitializeComponent();
            RunFlowDoc.Blocks.Add(Runparagraph);
            txtRichText.Document = RunFlowDoc;
        }



        DriverManager driverManager = new DriverManager();

        #region LogEdit

        public FlowDocument RunFlowDoc = new FlowDocument();
        public Paragraph Runparagraph = new Paragraph();
        private void AddLine(string test)
        {
            this.Dispatcher.Invoke(() =>
            {
                Runparagraph.Inlines.Add(DateTime.Now.ToString("HH:mm:ss.fff ->") + test);
                Runparagraph.Inlines.Add("\r");
                if (CanScroll)
                {
                    txtRichText.ScrollToEnd();
                }
                int count = Runparagraph.Inlines.Count;
                if (count > 500)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Runparagraph.Inlines.Remove(Runparagraph.Inlines.ElementAt(0));
                    }
                }
            });
        }
        bool CanScroll = true;
        private void txtRichText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var ee = txtRichText.Selection.Text;
            if (!string.IsNullOrWhiteSpace(ee))
            {
                CanScroll = false;
            }
            else
            {
                CanScroll = true;
            }
        }

        #endregion


        #region MCU

        private void btnLEDLight_click(object sender, RoutedEventArgs e)
        {

           
        



            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                int layer = chkFrame.SelectedIndex == 0 ? 255 : chkFrame.SelectedIndex;
                int box = chkdrawer.SelectedIndex == 0 ? 255 : chkdrawer.SelectedIndex;
                int status = cmbLED.SelectedIndex;
                if (status == 0)
                {
                    MCUManager.Instance.LCD(chest, layer, box, new byte[] { 0x00 });
                }
                else
                {
                    MCUManager.Instance.LEDLight(chest, layer, box, status.ToString());
                }
                driverManager.NetM.LEDComplete();
            }
            catch (Exception ex)
            {
                Log.Error("LED", ex);
            }
        }
        public void OnConnect(object obj, string macType, string cabientNo,bool isConnected)
        {
            NetServerManager NetServer = obj as NetServerManager;
            if (NetServer != null)
            {
                
            }
        }
        private void btnOpenDoor_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                MCUManager.Instance.OpenGate(chest);
            }
            catch (Exception ex)
            {
                Log.Error("OpenGate", ex);
            }
        }

        private void btnBLight_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int va = 0;
                int.TryParse(txtFingerID.Text, out va);
                if (va > 255)
                {
                    MessageBox.Show("输入非法值");
                    return;
                }
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                MCUManager.Instance.backLight(chest, (byte)va);
                driverManager.NetM.BLightComplete();
            }
            catch (Exception ex)
            {
                Log.Error("btnBLight_click", ex);
            }
        }

        private void btnPowerOn_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                MCUManager.Instance.CardPowerOn(chest);
            }
            catch (Exception ex)
            {
                Log.Error("btnPowerOn_click", ex);
            }
        }

        private void btnGateStatus_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                MCUManager.Instance.GateSatus(chest);
            }
            catch (Exception ex)
            {
                Log.Error("GateStatus", ex);
            }
        }

        private void btnLCD_Click(object sender, RoutedEventArgs e)
        {
            List<LCDFontFormat> datas = new List<LCDFontFormat>();
            byte[] color = txtnamecolor.Text.StrToToHexByte();
            datas.Add(new LCDFontFormat() { DataType = 0x01, Data = txtname.Text, FSize = 20, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            color = txtspeccolor.Text.StrToToHexByte();
            datas.Add(new LCDFontFormat() { DataType = 0x02, Data = txtspec.Text, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            color = txtqtycolor.Text.StrToToHexByte();
            datas.Add(new LCDFontFormat() { DataType = 0x03, Data = txtqty.Text, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x04, Data = txtNickName.Text, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x05, Data = txtFactory.Text, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x06, Data = txtCode.Text, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            datas.Add(new LCDFontFormat() { DataType = 0x07, Data = txtUnit.Text, FSize = 15, ColorR = color[0], ColorG = color[1], ColorB = color[2] });
            int TotalLen = 0;
            byte[] DataBuff = new byte[4096];
            foreach (var item in datas)
            {
                byte[] temp = Encoding.GetEncoding("GB18030").GetBytes(item.Data);
                byte[] itemBuff = new byte[temp.Length + 2];
                itemBuff[0] = (byte)item.DataType;
                itemBuff[1] = (byte)temp.Length;
                //itemBuff[2] = (byte)item.FSize;
                //itemBuff[3] = (byte)item.ColorR;
                //itemBuff[4] = (byte)item.ColorG;
                //itemBuff[5] = (byte)item.ColorB;
                Array.Copy(temp, 0, itemBuff, 2, temp.Length);
                Array.Copy(itemBuff, 0, DataBuff, TotalLen, itemBuff.Length);
                TotalLen += itemBuff.Length;
            }
            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                int layer = chkFrame.SelectedIndex == 0 ? 255 : chkFrame.SelectedIndex;
                int box = chkdrawer.SelectedIndex == 0 ? 255 : chkdrawer.SelectedIndex;
                int status = cmbLED.SelectedIndex;
                MCUManager.Instance.LCD(chest, layer, box, DataBuff.Skip(0).Take(TotalLen).ToArray());
                driverManager.NetM.LEDComplete();
            }
            catch (Exception ex)
            {
                Log.Error("LCD", ex);
            }
        }

        #endregion
        #region RFID

        private void RFIDStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                //RFIDM.Read(chest);
            }
            catch (Exception ex)
            {
                Log.Error("btnSRFID_click", ex);
            }
        }

        private void RFIDStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int chest = chkchest.SelectedIndex == 0 ? 255 : chkchest.SelectedIndex;
                //RFIDM.Read(chest);
            }
            catch (Exception ex)
            {
                Log.Error("btnSRFID_click", ex);
            }
        }

        #endregion
        #region 指静脉

        private void btnChkFinger_click(object sender, RoutedEventArgs e)
        {
            try
            {
                MCUManager.Instance.StopReg(1);
                Thread.Sleep(300);
                MCUManager.Instance.FingerChk(1);//只有主柜有指静态，默认1
            }
            catch (Exception ex)
            {
                Log.Error("btnChkFinger_click", ex);
            }
        }

        private void btnRegFinger_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int fid = 0;
                if (int.TryParse(txtFingerID.Text, out fid))
                {
                    if (CacheData.Ins.JsonConfig["Ver"] == "V1")
                    {
                        driverManager.FingerM.RegistFinger(fid);
                    }
                    else if (CacheData.Ins.JsonConfig["Ver"] == "V2")
                    {
                        MCUManager.Instance.StopReg(1);
                        Thread.Sleep(300);
                        MCUManager.Instance.FingerReg(1, fid);
                    }
                    else if (CacheData.Ins.JsonConfig["Ver"] == "V3")
                    {

                    }


                }
            }
            catch (Exception ex)
            {
                Log.Error("btnRegFinger_click", ex);
            }
        }

        private void btnDelFinger_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int fid = 0;
                if (int.TryParse(txtFingerID.Text, out fid))
                {
                    if (CacheData.Ins.JsonConfig["Ver"] == "V1")
                    {
                        driverManager.FingerM.DeleteFinger(fid);
                    }
                    else if (CacheData.Ins.JsonConfig["Ver"] == "V2")
                    {
                        MCUManager.Instance.StopReg(1);
                        Thread.Sleep(300);
                        MCUManager.Instance.FingerDel(1, fid);
                    }
                    else if (CacheData.Ins.JsonConfig["Ver"] == "V3")
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("btnDelFinger_click", ex);
            }
        }

        private void btnDelAll_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CacheData.Ins.JsonConfig["Ver"] == "V1")
                {
                    driverManager.FingerM.DeleteAllFinger();
                }
                else if (CacheData.Ins.JsonConfig["Ver"] == "V2")
                {
                    MCUManager.Instance.StopReg(1);
                    Thread.Sleep(300);
                    MCUManager.Instance.FingerDelAll(1);
                }
                else if (CacheData.Ins.JsonConfig["Ver"] == "V3")
                {

                }
            }
            catch (Exception ex)
            {
                Log.Error("btnDelAll_click", ex);
            }
        }

        private void btnFingerList_click(object sender, RoutedEventArgs e)
        {
            driverManager.FingerM.FingerList();
        }

        private void btnUploadTemp_click(object sender, RoutedEventArgs e)
        {
            //int fid = 0;
            //if (int.TryParse(txtFingerID.Text, out fid))
            //    MCUManager.Instance.GetTemplate(fid);
        }

        private void btnDownLoadTemp_click(object sender, RoutedEventArgs e)
        {
            //int fid = 0;
            //if (int.TryParse(txtFingerID.Text, out fid))
            //    MCUManager.Instance.DownLoadTemplate(fid, fingerBuff.ToArray());
        }

        #endregion
        #region 扫码仪

        private void btnTrgScan_click(object sender, RoutedEventArgs e)
        {
            driverManager.hIDScan.TrgScan();
        }

        private void btnScaning_click(object sender, RoutedEventArgs e)
        {
            driverManager.hIDScan.Scaning();
        }

        private void btnScanEnd_click(object sender, RoutedEventArgs e)
        {
            driverManager.hIDScan.ScanEnd();
        }

        #endregion
    }





    // 委托充当订阅者接口类
    public delegate void NotifyEventHandler(object sender);

    // 抽象订阅号类
    public class TenXun
    {
        public NotifyEventHandler NotifyEvent;

        public string Symbol { get; set; }
        public string Info { get; set; }
        public TenXun(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        #region 新增对订阅号列表的维护操作
        public void AddObserver(NotifyEventHandler ob)
        {
            NotifyEvent += ob;
        }
        public void RemoveObserver(NotifyEventHandler ob)
        {
            NotifyEvent -= ob;
        }

        #endregion

        public void Update()
        {
            if (NotifyEvent != null)
            {
                NotifyEvent(this);
            }
        }
    }

    // 具体订阅号类
    public class TenXunGame : TenXun
    {
        public TenXunGame(string symbol, string info)
            : base(symbol, info)
        {
        }
    }

    // 具体订阅者类
    public class Subscriber
    {
        public string Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void ReceiveAndPrint(Object obj)
        {
            TenXun tenxun = obj as TenXun;

            if (tenxun != null)
            {
                MessageBox.Show($"Notified {Name} of {tenxun.Symbol}'s  Info is: {tenxun.Info}");
            }
        }
    }



}

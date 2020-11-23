using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Trical.Common;
using MIIOT.Trical.Controls;
using Newtonsoft.Json;
using NFMES.UI.Base;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.View
{
    /// <summary>
    /// AcceptView.xaml 的交互逻辑
    /// </summary>
    public partial class AcceptView : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        #endregion

        private ObservableCollection<pubAccept> _Accepts = new ObservableCollection<pubAccept>();

        public ObservableCollection<pubAccept> Accepts
        {
            get
            {
                return _Accepts;
            }
            set
            {
                _Accepts = value;
                OnPropertyChanged("Accepts");
            }
        }
        private pubAccept _accept;

        public pubAccept accept
        {
            get { return _accept; }
            set
            {
                _accept = value;
                OnPropertyChanged("accept");
            }
        }
        private pubAcceptDtl _SelectDataViewData;

        public pubAcceptDtl SelectDataViewData
        {
            get { return _SelectDataViewData; }
            set
            {
                _SelectDataViewData = value;
                OnPropertyChanged("SelectDataViewData");
            }
        }

        private string _organ;

        public string Organ
        {
            get { return _organ; }
            set
            {
                _organ = value;
                OnPropertyChanged("Organ");
            }
        }

        HashSet<string> rfidSet = new HashSet<string>();
        HttpResolver httpResolver = new HttpResolver();
        public AcceptView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += AcceptView_Loaded;


            try
            {
                //while (CacheData.Ins.Token == null || CacheData.Ins.Token.Length == 0)
                //{
                //    Thread.Sleep(50);
                //}
                //getData();

                //while (true)
                //{
                //    Thread.Sleep(60000);
                //    int count = Accepts.Count;
                //    for (int i = 0; i < count; i++)
                //    {
                //        GetDetail(Accepts[i].sourceNo, true);
                //        Thread.Sleep(5000);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }

        }
        DateTime RefreshTime = DateTime.Now.AddDays(-1);
        private void AcceptView_Loaded(object sender, RoutedEventArgs e)
        {
          
            Task.Run(() =>
            {
                try
                {
                    if ((DateTime.Now - RefreshTime).TotalMinutes < 20)
                    {
                        return;
                    }
                    RefreshTime = DateTime.Now;
                    var list = LiteDBHelper.Ins.GetCollection<pubAccept>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].status > 2) continue;
                        list[i].IsChecked = false;
                        list[i].acceptDtlList.ForEach(a => a.IsNew = false);
                        if (Accepts.FirstOrDefault(a => a.sourceNo == list[i].sourceNo) == null)
                            this.Dispatcher.Invoke(() => { Accepts.Add(list[i]); });

                    }
                    while (CacheData.Ins.Token == null || CacheData.Ins.Token.Length == 0)
                    {
                        Thread.Sleep(50);
                    }
                    getData();
                }
                catch (Exception ex)
                {
                    Log.Error("accept load", ex);
                }
            });
        }
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (accept == null || accept.acceptDtlList == null)
                {
                    return;
                }
                //accept = LiteDBHelper.Ins.GetCollection<pubAccept>().FirstOrDefault(a => a._id == accept.id);
                Task.Run(() =>
                {
                    try
                    {

                        foreach (var item in accept.acceptDtlList)
                        {
                            int count = item.deliveryQty - item.RFID.Count;
                            for (int i = 0; i < count; i++)
                            {
                                MainWindow.msgQueue.Enqueue($"打印" );
                                var rfid = "";
                                int res = Print(item, out rfid);
                                if (res != 0 || rfid.Length == 0)
                                {
                                    errCode Err = (errCode)res;
                                    string ErrMsg = new RFIDPrinter().GetErrMsg(res);
                                    MainWindow.msgQueue.Enqueue($"打印机异常：{res}:" + ErrMsg);
                                    return;
                                }
                                // rfid = Guid.NewGuid().ToString("N");
                                this.Dispatcher.Invoke(() =>
                                {
                                    item.RFID.Add(new RFIDInfo() { rfid = rfid });
                                });
                                LiteDBHelper.Ins.Update(accept);
                            }
                            accept = accept;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error("DoPrint", ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error("btnPrint_Click", ex);
            }
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtBarcode.Text == "202008240001")
                {
                    var data = new { a = 1, b = 5, c = 1, name = "Ca钙液体试剂盒", spec = "8*70ml", qty = "1", nickname = "ca", factory = "德赛诊断系统(上海)有限公司", code = "NNSY5000059", unit = "盒" };
                    MainWindow.SendMSg(data);
                    data = new { a = 1, b = 5, c = 3, name = "冷冻试剂", spec = "10ml", qty = "1", nickname = "ca", factory = "美国伯乐", code = "NNSE0017759", unit = "盒" };
                    MainWindow.SendMSg(data);
                    data = new { a = 1, b = 4, c = 2, name = "总胆红素试剂盒", spec = "5*60ml 5*15ml  ", qty = "1", nickname = "zdhssjh", factory = "德赛诊断系统(上海)有限公司", code = "NNSY5000114", unit = "盒" };
                    MainWindow.SendMSg(data);
                }
                if (txtBarcode.Text == "202008240002")
                {
                    var data = new { a = 2, b = 5, c = 1, name = "Ca钙液体试剂盒", spec = "8*70ml", qty = "1", nickname = "ca", factory = "德赛诊断系统(上海)有限公司", code = "NNSY5000059", unit = "盒" };
                    MainWindow.SendMSg(data);
                    data = new { a = 2, b = 5, c = 3, name = "冷冻试剂", spec = "10ml", qty = "1", nickname = "ca", factory = "美国伯乐", code = "NNSE0017759", unit = "盒" };
                    MainWindow.SendMSg(data);
                    data = new { a = 2, b = 4, c = 1, name = "人附睾蛋白4定标液", spec = "5*60ml", qty = "1", nickname = "zdhssjh", factory = "德赛诊断系统(上海)有限公司", code = "NNSY5000114", unit = "盒" };
                    MainWindow.SendMSg(data);
                    data = new { a = 2, b = 4, c = 2, name = "D-二聚体质控试剂盒", spec = "4*1.0ml", qty = "1", nickname = "zdhssjh", factory = "德赛诊断系统(上海)有限公司", code = "NNSY5000114", unit = "盒" };
                    MainWindow.SendMSg(data);
                    data = new { a = 2, b = 4, c = 5, name = "总甲状腺素校准品", spec = "50人份/盒", qty = "1", nickname = "zdhssjh", factory = "德赛诊断系统(上海)有限公司", code = "NNSY5000114", unit = "盒" };
                    MainWindow.SendMSg(data);
                }
                txtBarcode.SelectAll();
                if (txtBarcode.Text == "")
                {
                    ShowNullTip();
                }
                else
                {
                    GetDetail(txtBarcode.Text);
                    var info = Accepts.FirstOrDefault(a => a.sourceNo.Contains(txtBarcode.Text));
                    if (info != null)
                    {
                        int index = Accepts.IndexOf(info);
                        listCodes.SelectedIndex = index;
                        return;
                    }
                    ShowNullTip();
                }
                
            }
        }
        private void ShowNullTip()
        {
            txtifNull.Visibility = Visibility.Visible;
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                this.Dispatcher.Invoke(() =>
                {
                    txtifNull.Visibility = Visibility.Collapsed;
                });
            });
        }
        private async void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dtls = new List<object>();
                foreach (var item in accept.acceptDtlList)
                {
                    if (item.IsNew)
                    {
                        MainWindow.msgQueue.Enqueue("验收有异常商品，请人工处理");
                        return;
                    }
                    var paras = new { id = item.id, checkQty = item.deliveryQty };
                    dtls.Add(paras);
                }
                var data = new { id = accept.id, status = 2, dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("验收确认取消接口_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.data != null && result.code == 0)
                {
                    accept.status = 2;
                    accept.acceptDtlList.ForEach(a => a.status = 2);
                    LiteDBHelper.Ins.Update(accept);
                    MoveCode();
                    mygird.Items.Refresh();
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("验收确认取消接口_Post", ex);
            }
        }

        private void txtBarcode_GotFocus(object sender, RoutedEventArgs e)
        {
            if (KeyboardShow.IsShow)
            {
                KeyboadHelper.ShowInputPanel();
            }
        }


        private async void btnCancelAccept_Click(object sender, RoutedEventArgs e)
        {
            if (accept.status > 1)
            {
                MainWindow.msgQueue.Enqueue($"{accept.sourceNo}已经验收确认，无法拒收");
                return;
            }
            string memo = "";
            var askView = new AskView();
            var CanClose = await MaterialDesignThemes.Wpf.DialogHost.Show(askView, "RootDialog");
            if ((bool)CanClose)
            {
                memo = askView.txt.Text;
            }
            else
                return;

            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                dic.Add("id", accept.id.ToString());
                dic.Add("status", "99");
                dic.Add("memo", memo);
                var data = new { id = accept.id, status = 99, memo };
                string datastr = JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("验收确认取消接口_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
                if (result != null && result.code == 0)
                {
                    accept.status = 99;
                    accept.acceptDtlList.ForEach(a => a.status = 99);
                    LiteDBHelper.Ins.Update(accept);
                    List<string> code = new List<string>();
                    code.Add(accept.sourceNo);
                    var datas = new { Ids = code };
                    var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                    ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "RM", TCPPushTagEnum.DELIVERY.ToString(), dataJson);

                    MainWindow.msgQueue.Enqueue("取消成功");
                    int index = Accepts.IndexOf(accept);
                    Accepts.Remove(accept);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index - 1;
                    else
                        accept = new pubAccept();
                    listCodes.Items.Refresh();
                    mygird.Items.Refresh();
                    listCodes.SelectedIndex = 0;
                    //CheckMrg();
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("验收确认取消接口_Post", ex);
            }

        }

        private void btnCheckAccept_Click(object sender, RoutedEventArgs e)
        {
            DoCheck();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var item in Accepts)
                {
                    var info = listCodes.SelectedValue as pubAccept;
                    if (info == null) continue;
                    if (item.sourceNo == info.sourceNo)
                    {
                        item.IsChecked = true;
                        accept = item;
                        accept.acceptDtlList.ForEach(a => a.status = accept.status);

                        mygird.Items.Refresh();
                        //txtlog.ItemsSource = null;
                        // txtlog.ItemsSource = accept.acceptDtlList;
                        //RefreshTree();
                        //appeptDtls.Clear();
                        //foreach (var dtls in item.acceptDtlList)
                        //{
                        //    appeptDtls.Add(dtls);
                        //}
                    }
                    else
                    {
                        item.IsChecked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ListView_SelectionChanged", ex);
            }
        }

        private void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnHMScreen_Click(object sender, RoutedEventArgs e)
        {

            MainWindow.msgQueue.Enqueue("保温箱未连接");
        }
        public void RefreshTree()
        {
            txtlog.Items.Clear();
            foreach (var item in accept.acceptDtlList)
            {
                TreeViewItem tr = new TreeViewItem();
                tr.IsExpanded = true;
                tr.Header = item.goodsName;
                txtlog.Items.Add(tr);
                foreach (var dtl in item.RFID)
                {
                    tr.Items.Add(dtl.rfid + ":" + dtl.status);
                }
            }
        }
        public async void getData()
        {
            try
            {

                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(new { });
                string url = CacheData.Ins.JsonConfig.GetValue("查询验收单列表_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, jsonstr);
                if (result != null && result.data != null && result.code == 0)
                {
                    List<string> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result.data.ToString());
                    if (data.Count == 0)
                        this.Dispatcher.Invoke(() =>
                        {
                            foreach (var item in Accepts)
                            {
                                LiteDBHelper.Ins.Delete<pubAccept>(item.id);
                            }
                            accept = new pubAccept();
                            Accepts.Clear();
                        });
                    data.Sort();
                    List<pubAccept> rmInfos = new List<pubAccept>();
                    foreach (var item in Accepts)
                    {
                        var temp = data.FirstOrDefault(a => a == item.sourceNo);
                        if (temp == null)
                        {
                            rmInfos.Add(item);
                        }
                    }
                    foreach (var item in rmInfos)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            Accepts.Remove(item);
                        });
                        LiteDBHelper.Ins.Delete<pubAccept>(item.id);
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        var info = Accepts.FirstOrDefault(a => a.sourceNo == data[i]);
                        if (info == null)
                        {
                            GetDetail(data[i]);
                            Thread.Sleep(100);
                        }
                    }
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
                //listCodes.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.Error("getData", ex);
            }
        }
        public async void GetDetail(string sourceNo, bool isBackFetch = false)
        {

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(new { sourceNo = sourceNo });
            string url = CacheData.Ins.JsonConfig.GetValue("查询验收单列表和详细_Get");

            HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, jsonstr);
            if (result != null && result.code == 0 && result.data != null)
            {
                try
                {
                    var aaa = JsonConvert.DeserializeObject<List<pubAccept>>(result.data.ToString());
                }
                catch (Exception ex)
                {
                    Log.Error("", ex);
                }
                this.Dispatcher.Invoke(() =>
               {
                   try
                   {
                       var temp = JsonConvert.DeserializeObject<List<pubAccept>>(result.data.ToString());
                       if (temp == null || temp.Count == 0) return;
                       List<string> codes = new List<string>();
                       codes.Add(sourceNo);
                       var datas = new { Ids = codes };
                       var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                       ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", TCPPushTagEnum.ACCEPT.ToString(), dataJson);

                       var acc = Accepts.FirstOrDefault(a => a.sourceNo == sourceNo);
                       int index = Accepts.Count;
                       if (acc != null)
                       {
                           if (temp.Count > 0)
                               acc.status = temp[0].status;
                           index = Accepts.IndexOf(acc);
                           if (!isBackFetch)
                               listCodes.SelectedIndex = index;
                           LiteDBHelper.Ins.Update(acc);
                           return;
                       }
                       accept = temp[0];
                       Accepts.Insert(index, accept);

                       if (acc == null)
                           LiteDBHelper.Ins.Insert(accept);
                   }
                   catch (Exception ex)
                   {
                       Log.Error("accept getdtls", ex);
                   }
               });
            }

            else if (result != null && result.msg != null && result.msg.Length > 0)
            {
                MainWindow.msgQueue.Enqueue(result.msg);
            }
            else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");

        }
        public async void getSinglecodes(string rfid)
        {
            try
            {
                if (accept == null) return;
                if (accept.status != 2)
                {
                    MainWindow.msgQueue.Enqueue("请先验收确认后再进行复核感应");
                    return;
                }
                if (rfidSet.Contains(rfid))
                {
                    MainWindow.msgQueue.Enqueue($"{rfid}标签已存在库内");
                    return;
                }
                foreach (var dtls in accept.acceptDtlList)
                {
                    int rfidcount = 0;
                    foreach (var rfids in dtls.RFID)
                    {
                        if (dtls.Qty <= rfidcount)
                        {
                            MainWindow.msgQueue.Enqueue($"{rfid}标签异常");
                            return;
                        }
                        if (rfids.rfid == rfid && rfids.status == 0)
                        {

                            rfids.status = 1;
                            dtls.checkQty++;
                            rfidcount++;
                            rfidSet.Add(rfid);
                            dtls.rfidCode = rfid;
                            LiteDBHelper.Ins.Insert(dtls);
                            mygird.Items.Refresh();
                            txtlog.Items.Refresh();
                            return;
                        }
                    }
                }
                MainWindow.msgQueue.Enqueue($"{rfid}标签异常");
                return;
                List<string> rfIds = new List<string>();
                rfIds.Add(rfid);
                var RFIDS = new { type = 3, rfIds = rfIds };
                string data = JsonConvert.SerializeObject(RFIDS);
                string url = CacheData.Ins.JsonConfig.GetValue("感应标签_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, data);
                if (result != null && result.data != null && result.code == 0)
                {
                    var temo = result.data.ToString();
                    SingleCode rfiddata = JsonConvert.DeserializeObject<SingleCode>(result.data.ToString());
                    pubAcceptDtl item = new pubAcceptDtl();
                    if (rfiddata.records.Count == 0)
                    {
                        MainWindow.msgQueue.Enqueue("未识别的RFID标签");
                        return;
                    }
                    item.goodsNo = rfiddata.records[0].goodsCode;
                    item.goodsName = rfiddata.records[0].goodsName;
                    item.goodsSpec = rfiddata.records[0].goodsSpec;
                    item.goodsFactoryName = rfiddata.records[0].goodsFactoryName;
                    item.goodsUnit = rfiddata.records[0].goodsUnit;
                    item.lotNo = rfiddata.records[0].lotNo;
                    item.pprodDate = rfiddata.records[0].pprodDate;
                    item.pvalidDate = rfiddata.records[0].pvalidDate;
                    item.deliveryQty = 0;

                    var Ritem = accept.acceptDtlList.FirstOrDefault(a => a.goodsNo == item.goodsNo
                    && a.lotNo == item.lotNo);
                    if (Ritem != null)
                    {
                        var temp = Ritem.RFID.FirstOrDefault(a => a.rfid == rfid);
                        if (temp != null)
                            return;
                        Ritem.checkQty++;
                        if (Ritem.IsNew)
                        {
                            Ritem.RFID.Add(new RFIDInfo() { rfid = rfid, status = 1 });
                            rfidSet.Add(rfid);
                        }
                        //  Ritem.RFID.Add(rfid);
                    }
                    else
                    {
                        item.IsNew = true;
                        item.checkQty = 1;
                        accept.acceptDtlList.Add(item);
                    }
                    rfidSet.Add(rfid);
                    RFIDInfo rfidInfo = new RFIDInfo() { rfid = rfid };
                    if (item.IsNew) rfidInfo.status = 1;
                    item.RFID.Add(rfidInfo);
                    mygird.Items.Refresh();
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");

            }
            catch (Exception ex)
            {


                Log.Error("accGetSingle", ex);
            }
        }

        public async void DoCheck()
        {

            try
            {
                if (accept.status != 2)
                {
                    MainWindow.msgQueue.Enqueue("此单还未验收");
                    return;
                }
                if (accept.acceptDtlList.Count == 0)
                {
                    MainWindow.msgQueue.Enqueue("明细不能为空");
                    return;
                }
                var dtls = new List<object>();
                foreach (var item in accept.acceptDtlList)
                {
                    if (item.IsNew)
                    {
                        MainWindow.msgQueue.Enqueue("复核有异常商品，请人工处理");
                        return;
                    }
                    List<string> codes = new List<string>();
                    item.RFID.ForEach(a => { if (a.status == 1) codes.Add(a.rfid); });
                    if (codes.Count != item.Qty)
                    {
                        MainWindow.msgQueue.Enqueue("复核数量有误");
                        return;
                    }
                    var paras = new { dtlId = item.id, codes };
                    if (item.Qty > 0 && codes.Count == item.Qty)
                        dtls.Add(paras);
                }
                var data = new { id = accept.id, dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("验收复核单_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.data != null && result.code == 0)
                {
                    string[] code = new string[] { accept.sourceNo };
                    var datas = new { Ids = code };
                    dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                    ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "RM", TCPPushTagEnum.ACCEPT.ToString(), dataJson);
                    accept.status = 3;
                    LiteDBHelper.Ins.Update(accept);
                    MainWindow.msgQueue.Enqueue("复核成功");
                    int index = Accepts.IndexOf(accept);
                    Accepts.Remove(accept);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index - 1;
                    else
                        accept = new pubAccept();

                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("验收复核单_Post", ex);
            }
        }
        private void MoveCode()
        {
            Task.Run(() =>
            {
                try
                {


                    List<string> code = new List<string>();
                    code.Add(accept.sourceNo);
                    var datas = new { Ids = code };
                    var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                    ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "RM", TCPPushTagEnum.DELIVERY.ToString(), dataJson);

                    Thread.Sleep(100);
                    code = new List<string>();
                    code.Add(accept.sourceNo);
                    datas = new { Ids = code };
                    dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                    ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "ADD", TCPPushTagEnum.ACCEPT.ToString(), dataJson);
                }
                catch (Exception ex)
                {
                    Log.Error("", ex);
                }
            });
        }
        #region 打印
        PrintHelper printer = new PrintHelper();
        public int Print(pubAcceptDtl acceptDtl, out string code)
        {
            DateTime dt = DateTime.Now;
            DateTime ptime = dt.UnixToDateTime(acceptDtl.pvalidDate ?? 0);
            List<string> aaa = new List<string>();
            aaa.Add($"名称:{acceptDtl.goodsName}");
            aaa.Add($"编码:{acceptDtl.goodsNo}");
            aaa.Add($"规格:{acceptDtl.goodsSpec}");
            aaa.Add($"单位:{acceptDtl.goodsUnit}");
            aaa.Add($"效期:{ptime.ToString("yyyy-MM-dd")}");
            aaa.Add($"批号:{acceptDtl.lotNo}");
            aaa.Add($"批次:{acceptDtl.batchNo}");
            return printer.Print(aaa, out code);
        }
        private List<string> StrCut(string Str)
        {
            List<string> Cuts = new List<string>();
            string tempstr = "";
            foreach (var item in Str)
            {
                tempstr += item;
                double wid = StrWidth(tempstr);
                if (wid < 250)
                {
                    continue;
                }
                else
                {
                    Cuts.Add(tempstr);
                    tempstr = "";
                }
            }
            Cuts.Add(tempstr);
            tempstr = "";
            return Cuts;
        }
        private double StrWidth(string Str)
        {
            return System.Windows.Forms.TextRenderer.MeasureText(Str,
              new System.Drawing.Font("黑体", 15)).Width;
        }
        private Size GetTextSize(string Text)
        {
            var formattedText = new FormattedText(Text, CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, new Typeface(new FontFamily("黑体"), new FontStyle(), new FontWeight(), new FontStretch()), 35, Brushes.Black);
            Size size = new Size(formattedText.Width, formattedText.Height);
            return size;
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is pubAcceptDtl dtl)
                {
                    accept.acceptDtlList.Remove(dtl);
                    mygird.Items.Refresh();
                }
            }
        }

        private void TextBoxNumberUpADown_DoChange(object sender, bool IsAdd)
        {
            int index = mygird.SelectedIndex;

            if (IsAdd)
            {
                accept.acceptDtlList[index].deliveryQty++;
            }
            else
            {
                accept.acceptDtlList[index].deliveryQty--;
                if (accept.acceptDtlList[index].deliveryQty < 0)
                    accept.acceptDtlList[index].deliveryQty = 0;
            }
            accept = accept;
            mygird.Items.Refresh();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                getSinglecodes(txttest.Text);
        }
    }

}

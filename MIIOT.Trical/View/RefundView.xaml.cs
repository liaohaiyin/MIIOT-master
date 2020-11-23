using LiteDB;
using MaterialDesignThemes.Wpf;
using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Model.TricalModel;
using MIIOT.Trical.Common;
using MIIOT.Trical.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MIIOT.Trical.View
{
    /// <summary>
    /// RefundView.xaml 的交互逻辑
    /// </summary>
    public partial class RefundView : UserControl, INotifyPropertyChanged
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
        //private ObservableCollection<RefundDtlsItem> _RefundItem = new ObservableCollection<RefundDtlsItem>();

        //public ObservableCollection<RefundDtlsItem> RefundItems
        //{
        //    get { return _RefundItem; }
        //    set
        //    {
        //        _RefundItem = value;
        //        OnPropertyChanged("RefundItem");
        //    }
        //}
        private ObservableCollection<RefundInfo> _RefundInfos = new ObservableCollection<RefundInfo>();

        public ObservableCollection<RefundInfo> RefundInfos
        {
            get { return _RefundInfos; }
            set
            {
                _RefundInfos = value;
                OnPropertyChanged("RefundItem");
            }
        }

        private RefundInfo refundInfo = new RefundInfo();

        public RefundInfo RefundInfo
        {
            get { return refundInfo; }
            set
            {
                refundInfo = value;
                OnPropertyChanged("RefundInfo");
            }
        }
        private List<RefundReasonModel> _Reasons = new List<RefundReasonModel>();

        public List<RefundReasonModel> Reasons
        {
            get { return _Reasons; }
            set
            {
                _Reasons = value;
                OnPropertyChanged("Reasons");
            }
        }
        private readonly string ADDTAG = "新增退货单";
        public RefundView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += RefundView_Loaded;

        }


        DateTime RefreshTime = DateTime.Now.AddDays(-1);
        private async void RefundView_Loaded(object sender, RoutedEventArgs e)
        {
            if ((DateTime.Now - RefreshTime).TotalMinutes < 20)
            {
                return;
            }
            RefreshTime = DateTime.Now;
            if (Reasons == null || Reasons.Count == 0)
                Reasons = await new TrialBusiness().GetReason("REFUND");
            var list = LiteDBHelper.Ins.GetCollection<RefundInfo>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].status > 2) continue;
                list[i].IsChecked = false;
                list[i].refundDtls.ForEach(a => { a.IsNew = false; a.IsHandOrder = false; });
                if (RefundInfos.FirstOrDefault(a => a.sourceNo == list[i].sourceNo) == null)
                    RefundInfos.Add(list[i]);
            }
            mygird.Items.Refresh();
            ReFreshData();
            this.txtCreater.Text = CacheData.Ins._user_info.displayName;
            txthouse.Text = CacheData.Ins.JsonConfig["WarehouseName"];

        }
        private async void ReFreshData()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig.GetValue("退货单列表_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
                if (result != null && result.code == 0)
                {
                    List<string> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result.data.ToString());
                    if (data.Count == 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            foreach (var item in RefundInfos)
                            {
                                LiteDBHelper.Ins.Delete<RefundInfo>(item.id);
                            }
                            RefundInfo = new RefundInfo();
                            RefundInfos.Clear();
                        });
                    }
                    data.Sort();

                    List<RefundInfo> rmInfos = new List<RefundInfo>();
                    foreach (var item in RefundInfos)
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
                            RefundInfos.Remove(item);
                        });
                        LiteDBHelper.Ins.Delete<RefundInfo>(item.id);
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        var temp = RefundInfos.FirstOrDefault(a => a.sourceNo == data[i]);
                        if (temp == null)
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
            }
            catch (Exception ex)
            {
                Log.Error("ReFreshData", ex);
            }
        }

        private void ShowNullMsg()
        {
            try
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
            catch (Exception ex)
            {
                Log.Error("ShowNullMsg", ex);
            }
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtBarcode.Text == "")
                    {
                        ShowNullMsg();
                    }
                    else
                    {
                        GetDetail(txtBarcode.Text);
                        var info = RefundInfos.FirstOrDefault(a => a.sourceNo == txtBarcode.Text);
                        if (info != null)
                        {
                            int index = RefundInfos.IndexOf(info);
                            listCodes.SelectedIndex = index;
                            return;
                        }
                        ShowNullMsg();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("txtBarcode_KeyDown", ex);
            }
        }
        HttpResolver httpResolver = new HttpResolver();
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            DoCheck();
        }
        private async void MamualCheck()
        {
            try
            {
                var dtls = new List<object>();
                foreach (var item in RefundInfo.refundDtls)
                {
                    if (string.IsNullOrWhiteSpace(item.backReason))
                    {
                        MainWindow.msgQueue.Enqueue("退货原因不能为空！");
                        return;
                    }
                    var codes = item.RFIDS;
                    var paras = new { reason = item.backReason, codes };
                    dtls.Add(paras);
                }

                var data = new { storehouseId = CacheData.Ins.WarehouseID, dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("退货手工复核单_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.code == 0)
                {
                    MainWindow.msgQueue.Enqueue("复核成功，退货成功");
                    int index = RefundInfos.IndexOf(refundInfo);
                    RefundInfos.Remove(refundInfo);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index - 1;
                    else
                        RefundInfo = new RefundInfo();
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
                Log.Error("MamualCheck", ex);
            }
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var info = RefundInfos.FirstOrDefault(a => a.sourceNo == ADDTAG);
                if (info != null)
                {
                    int index = RefundInfos.IndexOf(info);
                    listCodes.SelectedIndex = index;
                    return;
                }

                RefundInfo = new RefundInfo() { sourceNo = ADDTAG, IsHandOrder = true };
                RefundInfos.Insert(0, RefundInfo);
                listCodes.SelectedIndex = 0;
                //CheckMrg();
                txthouse.Text = CacheData.Ins.JsonConfig["WarehouseName"];
                this.txtCreater.Text = CacheData.Ins._user_info.displayName;
            }
            catch (Exception ex)
            {
                Log.Error("btnNewOrder_Click", ex);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int count = RefundInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    var info = listCodes.SelectedValue as RefundInfo;
                    if (info == null) continue;
                    if (RefundInfos[i].sourceNo == info.sourceNo)
                    {
                        RefundInfos[i].IsChecked = true;
                        RefundInfo = RefundInfos[i];
                    }
                    else
                    {
                        RefundInfos[i].IsChecked = false;
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

        public async void GetDetail(string RefundNo)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig.GetValue("退货单id查询退货明细_Get");
                url += "/" + RefundNo;
                HttpResultBase result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
                if (result != null && result.code == 0)
                {
                    //RefundItems.Clear();
                    if (result.data == null)
                    {
                        RefundInfo = new RefundInfo();
                        ShowNullMsg();
                        return;
                    }
                    RefundInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<RefundInfo>(result.data.ToString());
                    this.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            RefundInfo.refundDtls.ForEach(a => { a.supply_id = RefundInfo.supplyId; a.sourceNo = RefundInfo.sourceNo; a.Reasons = this.Reasons; });
                            var temp = RefundInfos.FirstOrDefault(a => a.sourceNo == RefundNo);
                            int index = RefundInfos.Count;
                            if (temp != null)
                            {
                                index = RefundInfos.IndexOf(temp);
                                RefundInfos.Remove(temp);
                                LiteDBHelper.Ins.Update(RefundInfo);
                            }
                            RefundInfos.Insert(index, RefundInfo);
                            listCodes.SelectedIndex = index;
                            if (temp == null)
                                LiteDBHelper.Ins.Insert(RefundInfo);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Refund GetDtls", ex);
                        }
                    });
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("退货单id查询退货明细_Get", ex);
            }
        }

        public async void getSinglecodes(string rfid)
        {
            try
            {
                if (refundInfo == null) return;
                var Ritem = RefundInfo.refundDtls.FirstOrDefault(a => a.RFIDS.Contains(rfid));
                if (Ritem != null)
                {
                    MainWindow.msgQueue.Enqueue("RFID已在列表中"); return;
                }
                List<string> rfIds = new List<string>();
                rfIds.Add(rfid);
                var RFIDS = new { type = 0, storehouseId = CacheData.Ins.JsonConfig["WarehouseID"], rfIds = rfIds };
                string data = JsonConvert.SerializeObject(RFIDS);
                string url = CacheData.Ins.JsonConfig.GetValue("感应标签_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, data);
                if (result != null && result.data != null && result.code == 0)
                {
                    SingleCode rfiddata = JsonConvert.DeserializeObject<SingleCode>(result.data.ToString());

                    RefundDtlsItem item = new RefundDtlsItem();
                    if (rfiddata.records.Count == 0)
                    {
                        MainWindow.msgQueue.Enqueue("RFID未录入系统");
                        return;
                    }
                    item.goodsNo = rfiddata.records[0].goodsCode;
                    item.goodsName = rfiddata.records[0].goodsName;
                    item.goodsSpec = rfiddata.records[0].goodsSpec;
                    item.goodsFactoryName = rfiddata.records[0].goodsFactoryName;
                    item.goodsUnit = rfiddata.records[0].goodsUnit;
                    item.batchNo = rfiddata.records[0].batchNo;
                    item.lotNo = rfiddata.records[0].lotNo;
                    item.pprodDate = rfiddata.records[0].pprodDate;
                    item.pvalidDate = rfiddata.records[0].pvalidDate;
                    item.refundQty = 0;
                    item.supply_id = rfiddata.records[0].supplyId;
                    item.RFIDS.Add(rfid);
                    item.IsHandOrder = RefundInfo.IsHandOrder;
                    item.Reasons = this.Reasons;
                    if (RefundInfo.IsHandOrder)
                    {
                        HandOrder(item, rfid);
                    }
                    else
                    {
                        Ritem = RefundInfo.refundDtls.FirstOrDefault(a => a.goodsNo == item.goodsNo
                                && a.lotNo == item.lotNo && a.supply_id == item.supply_id);
                        if (Ritem != null)
                        {
                            if (!Ritem.IsNew)
                            {
                                if (Ritem.checkQty == Ritem.refundQty)
                                {
                                    MainWindow.msgQueue.Enqueue($"数量超出，只能复核{Ritem.refundQty}个");
                                    return;
                                }
                            }
                            if (Ritem.RFIDS.Contains(rfid))
                                return;
                            Ritem.checkQty++;
                            Ritem.RFIDS.Add(rfid);
                        }
                        else
                        {
                            item.checkQty = 1;
                            RefundInfo.refundDtls.Add(item);
                            item.IsNew = true;
                        }
                    }
                    RefundInfo.storehouseId = int.Parse(CacheData.Ins.JsonConfig["WarehouseID"]);
                    refundInfo.supplyId = rfiddata.records[0].supplyId;
                    RefundInfo.supplyName = rfiddata.records[0].supplyName;


                    mygird.Items.Refresh();
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else
                    MainWindow.msgQueue.Enqueue($"接口返回{result.code}");

            }
            catch (Exception ex)
            {
                Log.Error("退货单id查询退货明细_Get", ex);
            }
        }
        public void HandOrder(RefundDtlsItem item, string rfid)
        {
            RefundDtlsItem Ritem = null;
            RefundInfo Info = null;
            foreach (var info in RefundInfos)
            {
                if (!info.IsHandOrder) continue;
                Ritem = info.refundDtls.FirstOrDefault(a => a.goodsNo == item.goodsNo
                     && a.lotNo == item.lotNo && a.supply_id == item.supply_id);
                if (Ritem != null)
                {
                    Info = info;
                    break;
                }
            }
            if (Ritem != null)
            {
                if (Ritem.RFIDS.Contains(rfid))
                    return;
                Ritem.checkQty++;
                Ritem.refundQty++;
                Ritem.RFIDS.Add(rfid);
                int index = RefundInfos.IndexOf(Info);
                listCodes.SelectedIndex = index;
                listCodes.Items.Refresh();
            }
            else
            {
                if (RefundInfo.refundDtls.Count == 0)
                {
                    item.checkQty = 1;
                    RefundInfo.refundDtls.Add(item);
                }
                else
                {
                    string NsourceNo = ADDTAG;
                    var temp = RefundInfos.Where(a => a.sourceNo.Contains(ADDTAG)).ToList();
                    if (temp != null)
                        NsourceNo += $"({temp.Count})";
                    RefundInfo = new RefundInfo() { sourceNo = NsourceNo, IsHandOrder = true };
                    RefundInfo.storehouseId = int.Parse(CacheData.Ins.JsonConfig["WarehouseID"]);
                    RefundInfos.Insert(0, RefundInfo);
                    item.checkQty = 1;
                    RefundInfo.refundDtls.Add(item);
                    listCodes.SelectedIndex = 0;
                }
                item.refundQty = 1;

            }
        }
        public async void DoCheck()
        {
            try
            {
                if (RefundInfo.refundDtls.Count == 0)
                {
                    MainWindow.msgQueue.Enqueue("请感应明细商品");
                    return;
                }
                if (RefundInfo.IsHandOrder)
                {
                    MamualCheck();
                    return;
                }
                List<string> codes = new List<string>();
                List<object> dtls = new List<object>();
                foreach (var item in RefundInfo.refundDtls)
                {
                    if (item.IsNew)
                    {
                        MainWindow.msgQueue.Enqueue("复核有异常商品，请人工处理");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(item.backReason))
                    {
                        MainWindow.msgQueue.Enqueue("退货原因不能为空！");
                        return;
                    }
                    var dtl = new { reason = item.backReason, dtlId = item.id, codes = item.RFIDS };
                    dtls.Add(dtl);
                }
                var data = new { id = RefundInfo.id, dtls };
                string datastr = JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("退货复核单_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
                if (result != null && result.code == 0)
                {
                    MainWindow.msgQueue.Enqueue("复核成功，退货成功");
                    RefundInfo.status = 3;
                    LiteDBHelper.Ins.Update(RefundInfo);
                    int index = RefundInfos.IndexOf(RefundInfo);
                    RefundInfos.Remove(RefundInfo);
                    //CheckMrg();
                    string[] code = new string[] { RefundInfo.sourceNo };
                    var datas = new { Ids = code };
                    var dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                    ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "RM", TCPPushTagEnum.ACCEPT.ToString(), dataJson);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index - 1;
                    else
                        RefundInfo = new RefundInfo();
                    listCodes.Items.Refresh();
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
                Log.Error("退货复核单_Post", ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {

                if (btn.Tag is RefundDtlsItem dtl)
                {
                    RefundInfo.refundDtls.Remove(dtl);
                    mygird.Items.Refresh();
                }
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                getSinglecodes(txttest.Text);
        }

        private void btnDeleteHandOrder_Click(object sender, RoutedEventArgs e)
        {
            RefundInfos.Remove(RefundInfo);
            listCodes.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox com)
            {
                if (com.Tag is RefundDtlsItem item)
                {
                    if (com.SelectedItem is RefundReasonModel sItem)
                    {
                        item.backReason = sItem.RValue;
                    }

                }
            }
        }
    }
}

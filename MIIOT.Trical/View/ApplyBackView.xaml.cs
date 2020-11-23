using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Model.TricalModel;
using MIIOT.Trical.Common;
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
    /// ApplyBackView.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyBackView : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<ApplyBackInfo> _ApplyBackInfos = new ObservableCollection<ApplyBackInfo>();

        public ObservableCollection<ApplyBackInfo> ApplyBackInfos
        {
            get { return _ApplyBackInfos; }
            set
            {
                _ApplyBackInfos = value;
                OnPropertyChanged("GoodsList");
            }
        }//ApplyBackDtlListItem

        private ApplyBackInfo _applyBackInfo = new ApplyBackInfo();

        public ApplyBackInfo ApplyBackInfo
        {
            get { return _applyBackInfo; }
            set
            {
                _applyBackInfo = value;
                OnPropertyChanged("ApplyBackInfo");
            }
        }
        private ApplyBackDtlListItem _applyBackDtlListItem;

        public ApplyBackDtlListItem applyBackDtlListItem
        {
            get { return _applyBackDtlListItem; }
            set { _applyBackDtlListItem = value; }
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
        private ObservableCollection<Stock> _Stocks = new ObservableCollection<Stock>();

        public ObservableCollection<Stock> Stocks
        {
            get { return _Stocks; }
            set
            {
                _Stocks = value;
                OnPropertyChanged("Stocks");
            }
        }
        private ObservableCollection<DepartmentModel> _Depts = new ObservableCollection<DepartmentModel>();

        public ObservableCollection<DepartmentModel> Depts
        {
            get { return _Depts; }
            set
            {
                _Depts = value;
                OnPropertyChanged("Depts");
            }
        }
        private readonly string ADDTAG = "新增退库单";
        public ApplyBackView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += CancellingStockView_Loaded;
            txthouse.Text = CacheData.Ins.JsonConfig["WarehouseName"];


        }
        TrialBusiness trialBusiness = new TrialBusiness();
        private async void LoadDept()
        {
            try
            {
                Depts.Clear();
                Stocks.Clear();
                var dept = await trialBusiness.GetDept();
                if (dept != null)
                    Depts.AddRange(dept);
                if (Depts != null && Depts.Count > 0)
                {
                    var temp = await trialBusiness.GetHouseById(Depts[0].id.ToString());
                    if (temp != null)
                        Stocks.AddRange(temp);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Depts init", ex);
            }
        }

        DateTime RefreshTime = DateTime.Now.AddDays(-1);
        private async void CancellingStockView_Loaded(object sender, RoutedEventArgs e)
        {
            if ((DateTime.Now - RefreshTime).TotalMinutes < 20)
            {
                return;
            }
            RefreshTime = DateTime.Now;
            LoadDept();
            this.txtCreater.Text = CacheData.Ins._user_info.displayName;
            txthouse.Text = CacheData.Ins.JsonConfig["WarehouseName"];
            if (Reasons.Count == 0)
                Reasons = (await new TrialBusiness().GetReason("BACK"));
            this.txtCreater.Text = CacheData.Ins._user_info.displayName;
            var list = LiteDBHelper.Ins.GetCollection<ApplyBackInfo>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].status > 2) continue;
                list[i].IsChecked = false;
                list[i].applyBackDtlList.ForEach(a => { a.IsNew = false; a.Reasons = this.Reasons; });
                if (ApplyBackInfos.FirstOrDefault(a => a.sourceNo == list[i].sourceNo) == null)
                    ApplyBackInfos.Add(list[i]);
            }

            getData();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtBarcode.Text == "")
                    {
                        ShowNullTip();
                    }
                    else
                    {
                        GetDetail(txtBarcode.Text);
                        var info = ApplyBackInfos.FirstOrDefault(a => a.sourceNo == txtBarcode.Text);
                        if (info != null)
                        {
                            int index = ApplyBackInfos.IndexOf(info);
                            listCodes.SelectedIndex = index;
                            return;
                        }
                        ShowNullTip();
                        //txtifNull.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("txtBarcode_KeyDown", ex);
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
        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var info = ApplyBackInfos.FirstOrDefault(a => a.sourceNo == ADDTAG);
                if (info != null)
                {
                    int index = ApplyBackInfos.IndexOf(info);
                    listCodes.SelectedIndex = index;
                    return;
                }
                ApplyBackInfo = new ApplyBackInfo() { sourceNo = ADDTAG, IsHandOrder = true };
                ApplyBackInfos.Insert(0, ApplyBackInfo);
                listCodes.SelectedIndex = 0;
                txthouse.Text = CacheData.Ins.JsonConfig["WarehouseName"];
                this.txtCreater.Text = CacheData.Ins._user_info.displayName;
                txthouse1.Text = CacheData.Ins.JsonConfig["WarehouseName"];
                this.txtCreater1.Text = CacheData.Ins._user_info.displayName;
                //CheckMrg();
            }
            catch (Exception ex)
            {
                Log.Error("btnNewOrder_Click", ex);
            }
        }
        private void btnApplyBackCheck_Click(object sender, RoutedEventArgs e)
        {
            DoCheck();
        }
        HttpResolver httpResolver = new HttpResolver();
        private async void MamualCheck()
        {
            try
            {
                if (txtDept.SelectedValue == null)
                {
                    MainWindow.msgQueue.Enqueue("请选择科室");
                    return;
                }
                if (txtStock.SelectedValue == null)
                {
                    MainWindow.msgQueue.Enqueue("请选择库房");
                    return;
                }
                var temo = txtStock.SelectedValue;
                var dtllist = new List<object>();
                foreach (var item in ApplyBackInfo.applyBackDtlList)
                {
                    if (string.IsNullOrWhiteSpace(item.backReason))
                    {
                        MainWindow.msgQueue.Enqueue("退货原因不能为空！");
                        return;
                    }
                    var codes = new { reason = item.backReason, codes = item.RFIDs };
                    dtllist.Add(codes);
                }
                var data = new { depmId = txtDept.SelectedValue, sourceStorehouseId = txtStock.SelectedValue, argetStorehouseId = CacheData.Ins.WarehouseID, dtllist };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("退库手工单复核_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.code == 0)
                {
                    int index = ApplyBackInfos.IndexOf(ApplyBackInfo);
                    ApplyBackInfos.Remove(ApplyBackInfo);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index -1;
                    else
                        ApplyBackInfo = new ApplyBackInfo();
                    mygird.Items.Refresh();
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
                Log.Error("退库手工单复核_Post", ex);
            }
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var item in ApplyBackInfos)
                {
                    if (listCodes.SelectedValue is ApplyBackInfo info)
                    {
                        if (item.sourceNo == info.sourceNo)
                        {
                            item.IsChecked = true;
                            ApplyBackInfo = item;
                        }
                        else
                        {
                            item.IsChecked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ListView_SelectionChanged", ex);
            }
        }


        public async void getData()
        {
            try
            {
                var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(new { });
                string url = CacheData.Ins.JsonConfig.GetValue("退库单列表_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, jsonstr);
                if (result != null && result.data != null && result.code == 0)
                {
                    List<string> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result.data.ToString());
                    if (data.Count == 0)
                    {
                        this.Dispatcher.Invoke(() =>
                          {
                              foreach (var item in ApplyBackInfos)
                              {
                                  LiteDBHelper.Ins.Delete<ApplyBackInfo>(item.id);
                              }
                              ApplyBackInfo = new ApplyBackInfo();
                              ApplyBackInfos.Clear();
                          });
                    }
                    data.Sort();

                    List<ApplyBackInfo> rmInfos = new List<ApplyBackInfo>();
                    foreach (var item in ApplyBackInfos)
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
                            ApplyBackInfos.Remove(item);
                        });
                        LiteDBHelper.Ins.Delete<ApplyBackInfo>(item.id);
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        var temp = ApplyBackInfos.FirstOrDefault(a => a.sourceNo == data[i]);
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
                Log.Error("", ex);
            }
        }
        public async void GetDetail(string sourceNo)
        {
            try
            {
                var jsondata = new { sourceNo };
                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(jsondata);
                string url = CacheData.Ins.JsonConfig.GetValue("退库单_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, jsonstr);
                if (result != null && result.data != null && result.code == 0)
                {
                    List<ApplyBackInfo> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ApplyBackInfo>>(result.data.ToString());
                    if (data == null || data.Count == 0)
                    {
                        ShowNullTip();
                        return;
                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        try
                        {
                            foreach (var item in data)
                            {
                                var app = ApplyBackInfos.FirstOrDefault(a => a.sourceNo == item.sourceNo);
                                int index = ApplyBackInfos.Count;
                                if (app != null)
                                {
                                    index = ApplyBackInfos.IndexOf(app);
                                    LiteDBHelper.Ins.Update(item);
                                    return;
                                }

                                ApplyBackInfos.Insert(index, item);
                                listCodes.SelectedIndex = index;
                                if (app == null)
                                    LiteDBHelper.Ins.Insert(item);
                                //CheckMrg();
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Error("", ex);
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
                Log.Error("", ex);
            }
        }
        public async void getSinglecodes(string rfid)
        {
            try
            {
                if (ApplyBackInfo == null) return;
                var Ritem = ApplyBackInfo.applyBackDtlList.FirstOrDefault(a => a.RFIDs.Contains(rfid));
                if (Ritem != null)
                {
                    MainWindow.msgQueue.Enqueue("RFID已在列表中"); return;
                }
                List<string> rfIds = new List<string>();
                rfIds.Add(rfid);
                var RFIDS = new { type = 1, storehouseId = CacheData.Ins.JsonConfig["WarehouseID"], rfIds = rfIds };
                string data = JsonConvert.SerializeObject(RFIDS);
                string url = CacheData.Ins.JsonConfig.GetValue("感应标签_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, data);
                if (result != null && result.data != null && result.code == 0)
                {
                    SingleCode rfiddata = JsonConvert.DeserializeObject<SingleCode>(result.data.ToString());
                    ApplyBackDtlListItem item = new ApplyBackDtlListItem();
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
                    item.lotNo = rfiddata.records[0].lotNo;
                    item.pprodDate = rfiddata.records[0].pprodDate;
                    item.pvalidDate = rfiddata.records[0].pvalidDate;
                    item.IsHandOrder = ApplyBackInfo.IsHandOrder;
                    item.Reasons = this.Reasons;
                    item.RFIDs.Add(rfid);
                    Ritem = ApplyBackInfo.applyBackDtlList.FirstOrDefault(a => a.goodsNo == item.goodsNo
                    && a.lotNo == item.lotNo);

                    if (Ritem != null)
                    {
                        if (!item.IsNew && ApplyBackInfo.sourceNo != ADDTAG)
                            if (Ritem.checkQty == Ritem.backQty)
                            {
                                MainWindow.msgQueue.Enqueue($"数量超出，只能复核{Ritem.backQty}个");
                                return;
                            }
                        if (Ritem.RFIDs.Contains(rfid))
                            return;
                        Ritem.checkQty++;
                        if (ApplyBackInfo.sourceNo == ADDTAG)
                            Ritem.backQty++;
                        //if (item.IsNew)
                        //    Ritem.backQty++;
                        Ritem.RFIDs.Add(rfid);
                    }
                    else
                    {
                        if (ApplyBackInfo.sourceNo == ADDTAG)
                            item.backQty = 1;
                        else
                            item.IsNew = true;
                        //if (ApplyBackInfo.sourceNo == ADDTAG)
                        item.checkQty = 1;
                        ApplyBackInfo.applyBackDtlList.Add(item);
                    }

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
                Log.Error("感应标签_Post", ex);
            }
        }
        public async void DoCheck()
        {
            try
            {
                if (ApplyBackInfo.applyBackDtlList.Count == 0)
                {
                    MainWindow.msgQueue.Enqueue("请感应明细商品");
                    return;
                }
                if (ApplyBackInfo.sourceNo == ADDTAG)
                {
                    MamualCheck();
                    return;
                }
                var dtls = new List<object>();
                foreach (var item in ApplyBackInfo.applyBackDtlList)
                {
                    if (item.IsNew)
                    {
                        MainWindow.msgQueue.Enqueue("复核有异常商品，请人工处理");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(item.backReason))
                    {
                        MainWindow.msgQueue.Enqueue("原因不能为空");
                        return;
                    }
                    var paras = new { dtlId = item.id, codes = item.RFIDs, reason = item.backReason };
                    dtls.Add(paras);
                }

                var data = new { id = ApplyBackInfo.id, dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("退库复核_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.data != null && result.code == 0)
                {
                    MainWindow.msgQueue.Enqueue("复核成功");
                    string[] code = new string[] { ApplyBackInfo.sourceNo };
                    var datas = new { Ids = code };
                    dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);
                    ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.MasterTCP], CacheData.Ins.MasterTCP, "RM", TCPPushTagEnum.ACCEPT.ToString(), dataJson);
                    int index = ApplyBackInfos.IndexOf(ApplyBackInfo);
                    ApplyBackInfo.status = 3;
                    LiteDBHelper.Ins.Update(ApplyBackInfo);
                    ApplyBackInfos.Remove(ApplyBackInfo);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index -1;
                    else
                        ApplyBackInfo = new ApplyBackInfo();
                    mygird.Items.Refresh();

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
                Log.Error("退库复核_Post", ex);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is ApplyBackDtlListItem dtl)
                {
                    ApplyBackInfo.applyBackDtlList.Remove(dtl);
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
            ApplyBackInfos.Remove(ApplyBackInfo);
            listCodes.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox com)
            {
                if (com.Tag is ApplyBackDtlListItem item)
                {
                    if (com.SelectedItem is RefundReasonModel sItem)
                    {
                        item.backReason = sItem.RValue;
                    }
                }
            }
        }

        private async void ComboBoxDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Stocks.Clear();
                if (sender is ComboBox combo)
                {
                    var select = combo.SelectedItem;
                    if (combo.SelectedItem is DepartmentModel dept)
                    {
                        Stocks.AddRange(await trialBusiness.GetHouseById(dept.id.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("ComboBox_SelectionChanged", ex);
            }
        }
    }
}
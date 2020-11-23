using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Model.TricalModel;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.View
{
    /// <summary>
    /// ApplyView.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyView : UserControl, INotifyPropertyChanged
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

        TrialBusiness trialBusiness = new TrialBusiness();
        private ApplyInfo _ApplyInfo = new ApplyInfo();

        public ApplyInfo ApplyInfo
        {
            get { return _ApplyInfo; }
            set
            {
                _ApplyInfo = value;
                OnPropertyChanged("ApplyInfo");
            }
        }
        private ObservableCollection<ApplyInfo> _appLyInfos = new ObservableCollection<ApplyInfo>();

        public ObservableCollection<ApplyInfo> ApplyInfos
        {
            get { return _appLyInfos; }
            set
            {
                _appLyInfos = value;
                OnPropertyChanged("ApplyInfos");
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
        public ApplyView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += ApplyView_Loaded;

        }
        DateTime RefreshTime = DateTime.Now.AddDays(-1);
        private async void ApplyView_Loaded(object sender, RoutedEventArgs e)
        {
            if ((DateTime.Now - RefreshTime).TotalMinutes < 20)
            {
                return;
            }
            RefreshTime = DateTime.Now;
            var list = LiteDBHelper.Ins.GetCollection<ApplyInfo>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].status > 2) continue;
                list[i].IsChecked = false;
                list[i].applyDtls.ForEach(a => a.IsNew = false);
                if (ApplyInfos.FirstOrDefault(a => a.sourceNo == list[i].sourceNo) == null)
                    ApplyInfos.Add(list[i]);
            }
            FetchData();

            Depts.Clear();
            Stocks.Clear();
            try
            {
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
        private async void FetchData()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig.GetValue("申领单号列表查询_Get");
                HttpResultBase result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
                if (result != null && result.code == 0)
                {
                    List<string> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(result.data.ToString());
                    if (data.Count == 0)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            foreach (var item in ApplyInfos)
                            {
                                LiteDBHelper.Ins.Delete<ApplyInfo>(item.id);
                            }
                            ApplyInfo = new ApplyInfo();
                            ApplyInfos.Clear();
                        });
                    }
                    data.Sort();

                    List<ApplyInfo> rmInfos = new List<ApplyInfo>();
                    foreach (var item in ApplyInfos)
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
                            ApplyInfos.Remove(item);
                        });
                        LiteDBHelper.Ins.Delete<ApplyInfo>(item.id);
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        var temp = ApplyInfos.FirstOrDefault(a => a.sourceNo == data[i]);
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
                Log.Error("ApplyView_Loaded", ex);
            }
        }

        private void CheckMrg()
        {
            try
            {
                if (ApplyInfos.Count > 0)
                {
                    var hasCheck = false;
                    foreach (var item in ApplyInfos)
                        if (item.IsChecked)
                        {
                            hasCheck = true;
                            break;
                        }
                    if (!hasCheck)
                    {
                        ApplyInfos[0].IsChecked = true;
                        ApplyInfo = ApplyInfos[0];
                        //ApplyItems.Clear();
                        //foreach (var item in ApplyInfo.applyDtls)
                        //{
                        //    ApplyItems.Add(item);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("CheckMrg", ex);
            }
        }
        private void ShowNullMsg()
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
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
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
                        bool Succeed = await GetDetail(txtBarcode.Text);
                        var info = ApplyInfos.FirstOrDefault(a => a.sourceNo == txtBarcode.Text);
                        if (info != null)
                        {
                            int index = ApplyInfos.IndexOf(info);
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

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            DoCheck();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foreach (var item in ApplyInfos)
                {
                    var info = listCodes.SelectedValue as ApplyInfo;
                    if (info == null) continue;
                    if (item.sourceNo == info.sourceNo)
                    {
                        item.IsChecked = true;
                        ApplyInfo = item;
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

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public async Task<bool> GetDetail(string applyNo)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig.GetValue("根据申领单号查询明细记录_Get");
                url += "/" + applyNo;
                HttpResultBase result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
                if (result != null && result.code == 0)
                {
                    if (result.data == null)
                        return false;
                    this.Dispatcher.Invoke(() =>
                     {
                         try
                         {
                             ApplyInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplyInfo>(result.data.ToString());
                             var temp = ApplyInfos.FirstOrDefault(a => a.sourceNo == applyNo);
                             int index = ApplyInfos.Count;
                             if (temp != null)
                             {
                                 index = ApplyInfos.IndexOf(temp);
                                 ApplyInfos.Remove(temp);
                                 LiteDBHelper.Ins.Update(ApplyInfo);
                             }
                             ApplyInfos.Insert(index, ApplyInfo);
                             listCodes.SelectedIndex = index;
                             if (temp == null)
                                 LiteDBHelper.Ins.Insert(ApplyInfo);

                             //CheckMrg();
                         }
                         catch (Exception ex)
                         {
                             Log.Error("apply getdtls", ex);
                         }
                     });
                    return true;
                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("GetDetail", ex);
            }
            return false;
        }
        HttpResolver httpResolver = new HttpResolver();
        public async void getSinglecodes(string rfid)
        {
            


            try
            {
                if (ApplyInfo == null) return;
                var Ritem = ApplyInfo.applyDtls.FirstOrDefault(a => a.RFIDs.Contains(rfid));
                if (Ritem != null)
                {
                    MainWindow.msgQueue.Enqueue("RFID已存在列表中"); return;
                }
                List<string> rfIds = new List<string>();
                rfIds.Add(rfid);
                var RFIDS = new { type = 2, storehouseId = CacheData.Ins.JsonConfig["WarehouseID"], rfIds = rfIds };
                string data = JsonConvert.SerializeObject(RFIDS);
                string url = CacheData.Ins.JsonConfig.GetValue("感应标签_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, data);
                if (result != null && result.data != null && result.code == 0)
                {
                    SingleCode rfiddata = JsonConvert.DeserializeObject<SingleCode>(result.data.ToString());
                    ApplyDtlsItem item = new ApplyDtlsItem();
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
                    item.applyQty = 0;
                    item.IsHandOrder = ApplyInfo.IsHandOrder;
                    item.RFIDs.Add(rfid);
                    Ritem = ApplyInfo.applyDtls.FirstOrDefault(a => a.goodsNo == item.goodsNo);
                    if (Ritem != null)
                    {
                        if (!Ritem.IsNew && !ApplyInfo.IsHandOrder)
                        {
                            if (Ritem.checkQty == Ritem.applyQty)
                            {
                                MainWindow.msgQueue.Enqueue($"数量异常，只能复核{Ritem.applyQty}个");
                                return;
                            }
                        }
                        if (Ritem.RFIDs.Contains(rfid))
                            return;
                        if (ApplyInfo.IsHandOrder)
                            Ritem.applyQty++;
                        //if(Ritem.IsNew)
                        //    Ritem.applyQty++;
                        Ritem.checkQty++;
                        Ritem.RFIDs.Add(rfid);
                    }
                    else
                    {
                        item.checkQty = 1;
                        item.applyQty = 0;
                        if (ApplyInfo.IsHandOrder)
                            item.applyQty = 1;
                        else
                            item.IsNew = true;
                        //ApplyItems.Add(item);
                        ApplyInfo.applyDtls.Add(item);
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
                if (ApplyInfo.applyDtls.Count == 0)
                {
                    MainWindow.msgQueue.Enqueue("请感应明细商品");
                    return;
                }
                if (ApplyInfo.IsHandOrder)
                {
                    HandCheck();
                    return;
                }
                var dtls = new List<object>();
                foreach (var item in ApplyInfo.applyDtls)
                {
                    if (item.IsNew)
                    {
                        MainWindow.msgQueue.Enqueue("复核有异常商品，请人工处理");
                        return;
                    }
                    var paras = new { dtlId = item.id, codes = item.RFIDs };
                    dtls.Add(paras);
                }
                var data = new { id = ApplyInfo.id, dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("申领复核_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.code == 0)
                {
                    MainWindow.msgQueue.Enqueue("复核完成");
                    ApplyInfo.status = 3;
                    LiteDBHelper.Ins.Update(ApplyInfo);
                    int index = ApplyInfos.IndexOf(ApplyInfo);
                    ApplyInfos.Remove(ApplyInfo);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index -1;
                    else
                        ApplyInfo = new ApplyInfo();
                    //CheckMrg();
                    //string[] code = new string[] { ApplyInfo.sourceNo };
                    //var datas = new { Ids = code };
                    //dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(datas);

                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("btnCheck_Click", ex);
            }
        }
        private async void HandCheck()
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

                var dtls = new List<object>();
                foreach (var item in ApplyInfo.applyDtls)
                {
                    var paras = new { codes = item.RFIDs };
                    dtls.Add(paras);
                }
                string depmId = "";
                var storehouseId = 0;
                if (txtDept.SelectedItem is DepartmentModel depts)
                {
                    depmId = depts.id.ToString();
                }
                if (txtStock.SelectedItem is Stock stocks)
                {
                    storehouseId = stocks.id;
                }
                var data = new { depmId, storehouseId = CacheData.Ins.WarehouseID, applyStorehouseId = storehouseId, dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("申领手工复核_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.code == 0)
                {
                    MainWindow.msgQueue.Enqueue("复核完成");
                    ApplyInfo.status = 3;
                    LiteDBHelper.Ins.Update(ApplyInfo);
                    int index = ApplyInfos.IndexOf(ApplyInfo);
                    ApplyInfos.Remove(ApplyInfo);
                    if (listCodes.Items.Count > 0)
                        listCodes.SelectedIndex = index == 0 ? 0 : index -1;
                    else
                        ApplyInfo = new ApplyInfo();

                }
                else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");
            }
            catch (Exception ex)
            {
                Log.Error("btnCheck_Click", ex);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is ApplyDtlsItem dtl)
                {
                    ApplyInfo.applyDtls.Remove(dtl);
                    mygird.Items.Refresh();
                }
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                getSinglecodes(txttest.Text);
        }

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var info = ApplyInfos.FirstOrDefault(a => a.sourceNo == "新增申领单");
                if (info != null)
                {
                    int index = ApplyInfos.IndexOf(info);
                    listCodes.SelectedIndex = index;
                    return;
                }

                ApplyInfo = new ApplyInfo() { sourceNo = "新增申领单", IsHandOrder = true };
                ApplyInfos.Insert(0, ApplyInfo);
                listCodes.SelectedIndex = 0;

                txthouse.Text = CacheData.Ins.JsonConfig["WarehouseName"];
                this.txtCreater.Text = CacheData.Ins._user_info.displayName;
                //CheckMrg();
            }
            catch (Exception ex)
            {
                Log.Error("btnNewOrder_Click", ex);
            }
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void btnDeleteHandOrder_Click(object sender, RoutedEventArgs e)
        {
            ApplyInfos.Remove(ApplyInfo);
            listCodes.SelectedIndex = 0;
        }
       
        
    }

}

using MIIOT.Model;
using MIIOT.Model.Trical;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.View
{
    /// <summary>
    /// ReceiveView.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiveView : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<ApplyDtlsItem> _ApplyItem = new ObservableCollection<ApplyDtlsItem>();

        public ObservableCollection<ApplyDtlsItem> ApplyItems
        {
            get { return _ApplyItem; }
            set
            {
                _ApplyItem = value;
                OnPropertyChanged("ApplyItem");
            }
        }
        public ReceiveView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += ReceiveView_Loaded;
        }

        private void ReceiveView_Loaded(object sender, RoutedEventArgs e)
        {
            if (ApplyItems.Count==0)
            {
                DisPlay();
            }
        }
        #region 动画操作
        private void Disappear()
        {
            TranslateTransform tt = new TranslateTransform();
            DoubleAnimation da = new DoubleAnimation();
            //动画时间
            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
            //设置按钮的转换效果
            covert.RenderTransform = tt;
            tt.Y = 0;
            da.To = covert.ActualHeight + 20;
            da.Duration = duration;
            //开始动画
            tt.BeginAnimation(TranslateTransform.YProperty, da);
        }
        private void DisPlay()
        {
            TranslateTransform tt = new TranslateTransform();
            DoubleAnimation da = new DoubleAnimation();
            //动画时间
            Duration duration = new Duration(TimeSpan.FromSeconds(0.5));
            //设置按钮的转换效果
            covert.RenderTransform = tt;
            tt.Y = -covert.ActualHeight;
            da.To = 0;
            da.Duration = duration;
            //开始动画
            tt.BeginAnimation(TranslateTransform.YProperty, da);
        }
        #endregion
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Disappear();
        }
        HttpResolver httpResolver = new HttpResolver();
        private async void btnReceive_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ApplyItems.Count == 0)
                {
                    MainWindow.msgQueue.Enqueue("请感应明细商品");
                    return;
                }
                var dtls = new List<object>();
                foreach (var item in ApplyItems)
                {
                    var codes = item.RFIDs;
                    var paras = new { codes };
                    dtls.Add(paras);
                }
                var data = new { storehouseId = int.Parse(CacheData.Ins.JsonConfig["WarehouseID"]), dtls };
                string dataJson = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                string url = CacheData.Ins.JsonConfig.GetValue("领用_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, dataJson);
                if (result != null && result.code == 0)
                {
                    MainWindow.msgQueue.Enqueue("领用消耗成功");
                    ApplyItems.Clear();
                    DisPlay();
                }
               else if (result != null && result.msg != null && result.msg.Length > 0)
                {
                    MainWindow.msgQueue.Enqueue(result.msg);
                }
                else MainWindow.msgQueue.Enqueue($"服务器返回{result.code}");

            }
            catch (Exception ex)
            {
                Log.Error("btnReceive_Click", ex);
            }
        }

        public async void getSinglecodes(string rfid)
        {
            try
            {
                Disappear();
                var Ritem = ApplyItems.FirstOrDefault(a => a.RFIDs.Contains(rfid));
                if (Ritem != null)
                {
                    MainWindow.msgQueue.Enqueue("RFID已存在列表中"); return;
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
                    item.batchNo = rfiddata.records[0].batchNo;
                    item.lotNo = rfiddata.records[0].lotNo;
                    item.pprodDate = rfiddata.records[0].pprodDate;
                    item.pvalidDate= rfiddata.records[0].pvalidDate;
                    item.applyQty = 0;

                    item.RFIDs.Add(rfid);
                    Ritem = ApplyItems.FirstOrDefault(a => a.goodsNo == item.goodsNo
                    &&a.lotNo==item.lotNo && a.goodsFactoryName == item.goodsFactoryName);
                    if (Ritem != null)
                    {
                        Ritem.checkQty++;
                        Ritem.applyQty++;
                        Ritem.RFIDs.Add(rfid);
                    }
                    else
                    {
                        item.checkQty = 1;
                        item.applyQty = 1;
                        item.IsNew = true;
                        ApplyItems.Add(item);
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                if (btn.Tag is ApplyDtlsItem dtl)
                {
                    ApplyItems.Remove(dtl);
                    mygird.Items.Refresh();
                }
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                getSinglecodes(txttest.Text);
        }
    }
}

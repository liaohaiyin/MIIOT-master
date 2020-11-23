using MIIOT.Drivers;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.View
{
    /// <summary>
    /// PrintLabelView.xaml 的交互逻辑
    /// </summary>
    public partial class PrintLabelView : UserControl, INotifyPropertyChanged
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
        private ObservableCollection<CodeItems> _codeItems = new ObservableCollection<CodeItems>();

        public ObservableCollection<CodeItems> codeItems
        {
            get { return _codeItems; }
            set
            {
                _codeItems = value;
                OnPropertyChanged("codeItems");
            }
        }

        public PrintLabelView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        MIIOT.Trical.Common.PrintHelper Printer = new Common.PrintHelper();
        private async void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (codeItems.Count == 0) return;
            List<object> list = new List<object>();
            foreach (var item in codeItems)
            {
                var rfid = "";
                int res = Print(item, out rfid);
                if (res != 0 || string.IsNullOrWhiteSpace(rfid))
                {
                    errCode Err = (errCode)res;
                    string ErrMsg = Err.GetEnumDescription();
                    MainWindow.msgQueue.Enqueue("打印机异常：" + ErrMsg);
                    return;
                }
                item.newRFID = rfid;
                var data = new { oldRfid = item.RFID, newRfid = rfid };
                list.Add(data);
            }
            string datastr = JsonConvert.SerializeObject(list);
            string url = CacheData.Ins.JsonConfig.GetValue("重打标签_Post");
            HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (result != null && result.code == 0)
            {
                MainWindow.msgQueue.Enqueue("打印成功");
            }
        }
        public int Print(CodeItems acceptDtl, out string code)
        {
            DateTime dt = DateTime.Now;
            DateTime ptime = dt.UnixToDateTime(acceptDtl.pvalidDate);
            List<string> aaa = new List<string>();
            aaa.Add($"名称:{acceptDtl.goodsName}");
            aaa.Add($"编码:{acceptDtl.goodsCode}");
            aaa.Add($"规格:{acceptDtl.goodsSpec}");
            aaa.Add($"单位:{acceptDtl.goodsUnit}");
            aaa.Add($"效期:{ptime.ToString("yyyy-MM-dd")}");
            aaa.Add($"批号:{acceptDtl.lotNo}");
            aaa.Add($"批次:{acceptDtl.batchNo}");
            return Printer.Print(aaa, out code);
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                getSinglecodes(txtBarcode.Text);
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        HttpResolver httpResolver = new HttpResolver();
        public async void getSinglecodes(string rfid)
        {
            try
            {
                foreach (var item in CacheData.Ins.mainWindow.acceptView.Accepts)
                {
                    foreach (var dtl in item.acceptDtlList)
                    {
                        foreach (var rf in dtl.RFID)
                        {
                            if (rf.rfid == rfid)
                            {

                            }
                        }
                    }
                }

                var Ritem = codeItems.FirstOrDefault(a => a.RFID == (rfid));
                if (Ritem != null)
                {
                    MainWindow.msgQueue.Enqueue("RFID已存在列表中"); return;
                }
                List<string> rfIds = new List<string>();
                rfIds.Add(rfid);
                var RFIDS = new { rfIds = rfIds };
                string data = JsonConvert.SerializeObject(RFIDS);
                string url = CacheData.Ins.JsonConfig.GetValue("感应标签_Post");
                HttpResultBase result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, data);
                if (result != null && result.data != null && result.code == 0)
                {
                    SingleCode rfiddata = JsonConvert.DeserializeObject<SingleCode>(result.data.ToString());
                    if (rfiddata != null && rfiddata.records != null)
                    {
                        foreach (var item in rfiddata.records)
                        {
                            item.RFID = rfid;
                            codeItems.Add(item);
                        }
                        mygird.Items.Refresh();
                    }
                    else
                        MainWindow.msgQueue.Enqueue("返回数据为空");
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
                if (btn.Tag is CodeItems dtl)
                {
                    codeItems.Remove(dtl);
                    mygird.Items.Refresh();
                }
            }
        }
    }
}

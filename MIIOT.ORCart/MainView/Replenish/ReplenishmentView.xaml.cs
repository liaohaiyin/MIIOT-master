using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Controls;
using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.Common;
using MIIOT.ORCart.MainView.Dialogs;
using MIIOT.Utility;
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
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
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
    /// RelenishmentView.xaml 的交互逻辑
    /// </summary>
    public partial class ReplenishmentView : UserControl, INotifyPropertyChanged, IMenuMsgSend
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

        private ObservableCollection<spd_replenishdtl> _ReplenishDtls = new ObservableCollection<spd_replenishdtl>();

        public ObservableCollection<spd_replenishdtl> ReplenishDtls
        {
            get { return _ReplenishDtls; }
            set
            {
                _ReplenishDtls = value;
                OnPropertyChanged("ReplenishDtls");
            }
        }
        private ObservableCollection<spd_replenishdtl> _ReplenishedDtls = new ObservableCollection<spd_replenishdtl>();

        public ObservableCollection<spd_replenishdtl> ReplenishedDtls
        {
            get { return _ReplenishedDtls; }
            set
            {
                _ReplenishedDtls = value;
                OnPropertyChanged("ReplenishedDtls");
            }
        }
        private spd_replenishdtl _selectDtlDataDtl;

        public spd_replenishdtl SelectDtlDataDtl
        {
            get { return _selectDtlDataDtl; }
            set
            {
                _selectDtlDataDtl = value;
                OnPropertyChanged("SelectDtlDataDtl");
            }
        }
        private spd_replenishdtl _selectDtlData;

        public spd_replenishdtl SelectDtlData
        {
            get { return _selectDtlData; }
            set
            {
                _selectDtlData = value;
                OnPropertyChanged("SelectDtlData");
            }
        }

        private ObservableCollection<spd_replenishdtl> _Replenished = new ObservableCollection<spd_replenishdtl>();

        public ObservableCollection<spd_replenishdtl> Replenished
        {
            get { return _Replenished; }
            set
            {
                _Replenished = value;
                OnPropertyChanged("Replenished");
            }
        }

        private bool _showDetail;

        public bool ShowDetail
        {
            get { return _showDetail; }
            set
            {
                _showDetail = value;
                OnPropertyChanged("ShowDetail");
            }
        }
        private bool _showedDetail;

        public bool ShowedDetail
        {
            get { return _showedDetail; }
            set
            {
                _showedDetail = value;
                OnPropertyChanged("ShowedDetail");
            }
        }
        private bool _isReplenishCom;

        public bool isReplenishComplete
        {
            get { return _isReplenishCom; }
            set
            {
                _isReplenishCom = value;
                OnPropertyChanged("isReplenishComplete");
            }
        }

        public delegate void DelBackMsg();
        public event DelBackMsg DoBack;
        public delegate void DelNextRecord(spd_replenish replenish);
        public event DelNextRecord DoNext;

        DapperUtil dbUtil = new DapperUtil(CacheData.Ins.JsonConfig["DBStr"]);
        public ReplenishmentView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += RelenishmentView_Loaded;
        }

        public void RefreshReplenished(DateTime sdate, DateTime edate)
        {
            try
            {
                Replenished.Clear();
                var temp = dbUtil.Query<spd_replenishdtl>("select * from cart_replenish where replenish_status <> 0 and outstore_time >@sdate and outstore_time <@edate", new { sdate, edate });
                var group = temp.GroupBy(a => a.replenishid);
                foreach (var item in group)
                {
                    spd_replenishdtl cart_Replenish = item.First();
                    var status = item.GroupBy(a => a.replenish_status);
                    if (status.Count() == 1)
                    {
                        if (status.First().Key == 0)
                        {
                            cart_Replenish.replenish_status = 0;
                        }
                        if (status.First().Key == 1)
                        {
                            cart_Replenish.replenish_status = 1;
                        }
                    }
                    else
                    {
                        cart_Replenish.replenish_status = 3;
                    }

                    Replenished.Add(cart_Replenish);
                }
            }
            catch (Exception ex)
            {
                Log.Error("RefreshReplenished", ex);
            }
        }
        private void RelenishmentView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.Error("RelenishmentView_Loaded", ex);
            }
        }
        List<spd_replenish> CurrReplenishes = new List<spd_replenish>();
        List<spd_replenish> rollBackReplenishes = new List<spd_replenish>();

        public void DoReplenish(List<spd_replenish> replenishes)
        {
            ReplenishDtls.Clear();
            rollBackReplenishes = replenishes.DeepClone();
            CurrReplenishes = replenishes;
            txtMainNo.Text = "";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("select * from spd_replenishdtl where rackstatus<3 and ");
            for (int i = 0; i < replenishes.Count; i++)
            {
                if (i == 0)
                    txtMainNo.Text += replenishes[i].replenishno;
                else
                    txtMainNo.Text += Environment.NewLine + replenishes[i].replenishno;
                replenishes[i].detail = dbUtil.Query<spd_replenishdtl>($"select * from spd_replenishdtl where rackstatus<3 and replenishid='{replenishes[i].replenishid}'", null);
                if (replenishes[i].detail == null) continue;
                foreach (var dtl in replenishes[i].detail)
                {
                    var item = dtl.DeepClone();
                    item.need_replenish_qty = (int)item.replenishqty - item.old_replenish_qty;
                    item.need_replenish_qty = item.need_replenish_qty < 0 ? 0 : item.need_replenish_qty;
                    var goods = ReplenishDtls.FirstOrDefault(a => a.goodsid == item.goodsid);
                    if (goods == null)
                        ReplenishDtls.Add(item);
                    else
                    {
                        goods.need_replenish_qty += item.need_replenish_qty;
                        goods.old_replenish_qty += item.old_replenish_qty;
                        goods.new_replenish_qty += item.new_replenish_qty;
                    }
                }
            }
            girdRelenish.SelectedIndex = 0;
        }

        private async void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (CacheData.Ins.isReplenishEdit)
            {
                if (await MainWindow.ShowCheckWin("本次补货数量还未确认，退出将不会保存，是否确认退出补货界面？"))
                {
                    CacheData.Ins.isReplenishEdit = false;
                    DoBack?.Invoke();
                    Transitioner.MovePreviousCommand.Execute("", this);

                }
            }
            else
            {
                DoBack?.Invoke();
                Transitioner.MovePreviousCommand.Execute("", this);
            }
        }
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (CacheData.Ins.isReplenishEdit)
            {
                CacheData.Ins.mainWindow.MessageTips("请先确认本次补货数量");
                return;
            }
            if (CurrReplenishes.Count > CurrReplenishes.Count - 1)
                DoNext?.Invoke(CurrReplenishes[CurrReplenishes.Count - 1]);
        }



        private async void btnReplenish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnOK.IsEnabled = false;
                bool isErr = false;
                foreach (var item in ReplenishDtls)
                {
                    if (item.need_replenish_qty < item.new_replenish_qty)
                        isErr = true;
                }
                if (isErr)
                {

                    CacheData.Ins.mainWindow.MessageTips("补货数量超出，暂不支持");
                    return;
                }

                bool isEmpty = true;
                List<DapperParams> sqlstr = new List<DapperParams>();
                List<spd_replenishdtl> requestDtls = new List<spd_replenishdtl>();
                int totalQty = 0;
                int repTotalQty = 0;
                for (int i = 0; i < CurrReplenishes.Count; i++)
                {
                    bool complete = true;
                    bool isDetalEmpty = true;
                    foreach (var dtls in CurrReplenishes[i].detail)
                    {
                        isDetalEmpty = false;
                        isEmpty = false;
                        int thisQty = 0;
                        if (dtls.old_replenish_qty == dtls.replenishqty) continue;//数量相等说明补货完成
                        var goods = ReplenishDtls.FirstOrDefault(a => a.goodsid == dtls.goodsid);
                        if (goods != null)
                        {
                         
                            if (goods.new_replenish_qty == 0)
                            {
                                if (goods.need_replenish_qty > 0)
                                    complete = false;
                            }
                            else
                            {
                               

                                if (i == CurrReplenishes.Count - 1)
                                    thisQty = goods.new_replenish_qty;
                                else if (goods.new_replenish_qty >= ((int)dtls.replenishqty - dtls.old_replenish_qty))
                                    thisQty = (int)dtls.replenishqty - dtls.old_replenish_qty;
                                else
                                    thisQty = goods.new_replenish_qty;
                                int otherQty = goods.new_replenish_qty - thisQty;
                                if (otherQty >= 0)//扣完了还剩下的数量
                                {
                                    if (i == CurrReplenishes.Count - 1)
                                    {//最后一个
                                        dtls.thisTimeQty = thisQty;
                                        dtls.old_replenish_qty += thisQty;

                                        goods.old_replenish_qty += goods.new_replenish_qty;
                                        goods.need_replenish_qty -= thisQty;
                                        goods.need_replenish_qty = goods.need_replenish_qty < 0 ? 0 : goods.need_replenish_qty;
                                        goods.new_replenish_qty = 0;
                                    }
                                    else
                                    {
                                        dtls.thisTimeQty = thisQty;
                                        dtls.old_replenish_qty += thisQty;

                                        goods.old_replenish_qty += thisQty;
                                        goods.need_replenish_qty -= thisQty;
                                        goods.need_replenish_qty = goods.need_replenish_qty < 0 ? 0 : goods.need_replenish_qty;

                                        goods.new_replenish_qty -= thisQty;
                                        goods.new_replenish_qty = goods.new_replenish_qty < 0 ? 0 : goods.new_replenish_qty;
                                    }
                                    if (dtls.old_replenish_qty == dtls.replenishqty)
                                        dtls.rackstatus = 3;
                                }
                                else
                                {
                                    complete = false;
                                    if (i == CurrReplenishes.Count - 1)
                                    {//最后一个
                                        dtls.thisTimeQty = thisQty;
                                        dtls.old_replenish_qty += thisQty;

                                        goods.old_replenish_qty += goods.new_replenish_qty;
                                        goods.need_replenish_qty -= goods.new_replenish_qty;
                                        goods.need_replenish_qty = goods.need_replenish_qty < 0 ? 0 : goods.need_replenish_qty;
                                        goods.new_replenish_qty = 0;
                                    }
                                    else
                                    {
                                        if (goods.new_replenish_qty > 0)
                                        {
                                            dtls.thisTimeQty = thisQty;
                                            dtls.old_replenish_qty += thisQty;

                                            goods.old_replenish_qty += goods.new_replenish_qty;
                                            goods.need_replenish_qty -= dtls.old_replenish_qty;
                                            goods.need_replenish_qty = goods.need_replenish_qty < 0 ? 0 : goods.need_replenish_qty;
                                        }
                                        goods.new_replenish_qty = 0;
                                    }
                                }
                            }
                        }
                        if (thisQty > 0)
                        {
                            var item = dtls;
                            item.editdate = DateTime.Now;
                            sqlstr.Add(new DapperParams("UPDATE spd_replenishdtl set old_replenish_qty=@old_replenish_qty,rackstatus=@rackstatus where sourceid=@sourceid", item));

                            var stock = Getcart_stock(item,thisQty);
                            sqlstr.Add(new DapperParams(dbUtil.BuildInsertSqlStr(stock), stock));

                            var instock = Getcart_instock(item,thisQty);
                            sqlstr.Add(new DapperParams(dbUtil.BuildInsertSqlStr(instock), instock));
                            requestDtls.Add(dtls);
                        }
                        totalQty += (int)dtls.replenishqty;
                        repTotalQty += dtls.old_replenish_qty;
                    }

                    if (repTotalQty>0&&totalQty == repTotalQty)//完成
                    {
                        CurrReplenishes[i].replenishstatus = 1;
                        sqlstr.Add(new DapperParams($"UPDATE spd_replenish set replenishstatus=1, replenishdata='{DateTime.Now}' where replenishid='{CurrReplenishes[i].replenishid}'", null));
                        cart_replenish replenish = new cart_replenish()
                        {
                            replenish_id = CurrReplenishes[i].replenishid,
                            replenish_no = CurrReplenishes[i].replenishno,
                            replenish_time = DateTime.Now,
                            out_storehouse_name = CurrReplenishes[i].orgstorehousename,
                            out_storehouse_id = CurrReplenishes[i].orgstorehouseid,
                            in_storehouse_id = CurrReplenishes[i].destorehouseid,
                            in_storehouse_name = CurrReplenishes[i].destorehousename,
                            creman_id = CacheData.Ins.User.id,
                            creman_name = CacheData.Ins.User.display_name
                        };
                        sqlstr.Add(new DapperParams(dbUtil.BuildInsertSqlStr(replenish), replenish));
                    }
                    else if (isDetalEmpty)
                    {

                    }
                    else if(repTotalQty > 0)
                    {
                        CurrReplenishes[i].replenishstatus = 3;
                        sqlstr.Add(new DapperParams($"UPDATE spd_replenish set replenishstatus=3, replenishdata='{DateTime.Now}' where replenishid='{CurrReplenishes[i].replenishid}'", null));
                    }

                }
                var NewRecord = ReplenishDtls.ToList().FindAll(a => a.IsNew);
                foreach (var item in NewRecord)
                {
                    isEmpty = false;
                    item.old_replenish_qty += item.new_replenish_qty;
                    item.new_replenish_qty = 0;
                    item.editdate = DateTime.Now;
                    sqlstr.Add(new DapperParams(dbUtil.BuildInsertSqlStr(item), item));
                    cart_stock stock = new cart_stock()
                    {
                        stock_id = CacheData.Ins.SnowId.nextId(),
                        stockhouse_id = CacheData.Ins.currStock.storehouseid,
                        goods_id = item.goodsid,
                        plot_id = item.plotid,
                        plot_no = item.plotno,
                        validity_date = item.validto ?? DateTime.MinValue,
                        qty = item.old_replenish_qty,
                        creman_id = CacheData.Ins.User.id,
                        creman_name = CacheData.Ins.User.display_name,
                        cretime = DateTime.Now
                    };
                    sqlstr.Add(new DapperParams(dbUtil.BuildInsertSqlStr(stock), stock));
                    cart_instock instock = new cart_instock()
                    {
                        instock_id = CacheData.Ins.SnowId.nextId(),
                        goods_id = item.goodsid,
                        plot_id = item.plotid,
                        plot_no = item.plotno,
                        validity_date = item.validto ?? DateTime.MinValue,
                        instock_qty = item.old_replenish_qty,
                        storehouse_id = CacheData.Ins.currStock.storehouseid,
                        creman_id = CacheData.Ins.User.id,
                        creman_name = CacheData.Ins.User.display_name,
                        create_time = DateTime.Now
                    };
                    sqlstr.Add(new DapperParams(dbUtil.BuildInsertSqlStr(instock), instock));
                    requestDtls.Add(item);
                }
                if (isEmpty)
                {
                    CacheData.Ins.mainWindow.MessageTips("未操作");
                    return;
                }
#if Net

                int rec = await NetReplenish(requestDtls.DeepClone());
#else
 int rec =1;
#endif
                if (rec > 0)
                {
                    if (sqlstr.Count > 0)
                    {
                        bool succ = dbUtil.TransactionAction(sqlstr);
                        if (succ)
                        {
                            CacheData.Ins.isReplenishEdit = false;
                            isReplenishComplete = true;
                            CacheData.Ins.mainWindow.MessageTips("补货成功");
                            btnOK.IsEnabled = true;
                        }
                        RefreshReplenished(DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek + 1), DateTime.Now.Date.AddDays(1));
                    }
                }
                else
                {
                    DoReplenish(rollBackReplenishes);
                }


                return;


            }
            catch (Exception ex)
            {
                DoReplenish(rollBackReplenishes);
                Log.Error("btnReplenish_Click", ex);
            }
            finally
            {
                btnOK.IsEnabled = true;
            }
        }
        private cart_instock Getcart_instock(spd_replenishdtl item,int thisQty)
        {
            cart_instock instock = new cart_instock()
            {
                instock_id = CacheData.Ins.SnowId.nextId(),
                goods_id = item.goodsid,
                plot_id = item.plotid,
                plot_no = item.plotno,
                validity_date = item.validto ?? DateTime.MinValue,
                instock_qty = thisQty,
                storehouse_id = CacheData.Ins.currStock.storehouseid,
                creman_id = CacheData.Ins.User.id,
                creman_name = CacheData.Ins.User.display_name,
                create_time = DateTime.Now
            };
            return instock;
        }
        private cart_stock Getcart_stock(spd_replenishdtl item,int thisQty)
        {
            cart_stock stock = new cart_stock()
            {
                stock_id = CacheData.Ins.SnowId.nextId(),
                stockhouse_id = CacheData.Ins.currStock.storehouseid,
                goods_id = item.goodsid,
                plot_id = item.plotid,
                plot_no = item.plotno,
                validity_date = item.validto ?? DateTime.MinValue,
                qty = thisQty,
                creman_id = CacheData.Ins.User.id,
                creman_name = CacheData.Ins.User.display_name,
                cretime = DateTime.Now
            };
            return stock;
        }
        private async Task<int> NetReplenish(IList<spd_replenishdtl> spd_Replenishes)
        {

            string url = CacheData.Ins.JsonConfig["补货"];
            List<ReplenishPara> datas = new List<ReplenishPara>();
            foreach (var item in spd_Replenishes)
            {
                if (item.old_replenish_qty == 0) continue;
                ReplenishPara para = new ReplenishPara()
                {
                    uOrganId = CacheData.Ins.currStock.uorganid,
                    rackId = item.sourceid,
                    code = item.replenishid,
                    categoryType = "1",
                    goodsId = item.goodsid,
                    plotId = item.batchid,
                    plotNo = item.batchno,
                    batchId = item.batchid,
                    batchNo = item.batchno,
                    locId = item.cabinetcode,
                    locNo = item.cabinetcode,
                    storehouseId = CacheData.Ins.currStock.storehouseid,
                    inStoreTime = DateTime.Now.ToLongString(),
                    operationType = "6",
                    operateManId = CacheData.Ins.User.user_source_id,
                    operateManName = CacheData.Ins.User.display_name,
                    operateManNo = CacheData.Ins.User.user_code,
                    rackerno = CacheData.Ins.User.user_code,
                    rackQty = item.thisTimeQty
                };
                datas.Add(para);
            }
            var jsondata = new { rows = datas };
            string dataJsonstr = JsonConvert.SerializeObject(jsondata);
            HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
            if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                if (res != null && res.code == 0)
                {
                    return 1;
                }
                else
                    CacheData.Ins.mainWindow.MessageTips($"请求错误：{res.msg.Replace("上架", "补货")}");

            }
            else
                CacheData.Ins.mainWindow.MessageTips("请求错误：" + result.StatusCode.GetEnumDescription());
            return 0;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //sendMsg(txtBarcode.Text);
            }
        }
        private async void BarcodeIn(string code)
        {
            try
            {
                cart_goods cart_Goods = new cart_goods();
                var temp = ReplenishDtls.FirstOrDefault(a => a.goodsno == code);
                if (temp == null)
                {
                    List<cart_goods> goods = new StockUtil().GetAllStockGoods().FindAll(a => a.goods_no == code);// dbUtil.Query<cart_goods>("SELECT * from cart_goods where goods_no=@goods_no", new { goods_no = code });
                    if (goods != null && goods.Count > 0)
                    {
                        cart_Goods = goods[0];
                        temp = new spd_replenishdtl();
                        temp.replenishid = CurrReplenishes[CurrReplenishes.Count - 1].replenishid;
                        temp.goodsid = cart_Goods.goods_id;
                        temp.goodsno = cart_Goods.goods_no;
                        temp.goodsname = cart_Goods.goods_name;
                        temp.goodstype = cart_Goods.goods_spec;
                        temp.factoryname = cart_Goods.factory_name;
                        temp.goodsunitname = cart_Goods.unit;
                        temp.validto = cart_Goods.validity_date;
                        //temp.outstore_time = DateTime.Now;
                        //temp.replenish_time = DateTime.Now;
                        //temp.new_replenish_qty = cart_Goods.qty = 0;
                        //temp.need_replenish_qty = cart_Goods.need_qty = 0;
                        //temp.cart_id = CacheData.Ins.JsonConfig["CartNo"];
                        //temp.cart_name = CacheData.Ins.JsonConfig["CartName"];
                        temp.QtyErr = true;
                        temp.IsNew = true;
                        ReplenishDtls.Insert(0, temp);
                    }
                    else
                    {
                        CacheData.Ins.mainWindow.MessageTips("商品不在库内");
                        return;
                    }
                }
                else
                {
                    girdRelenish.ScrollIntoView(temp);
                    cart_Goods.goods_no = temp.goodsno;
                    cart_Goods.goods_name = temp.goodsname;
                    cart_Goods.goods_spec = temp.goodstype;
                    cart_Goods.factory_name = temp.factoryname;
                    cart_Goods.unit = temp.goodsunitname;
                    cart_Goods.validity_date = temp.validto ?? DateTime.MinValue;
                    cart_Goods.batch_no = temp.plotno;
                    cart_Goods.qty = temp.new_replenish_qty;
                    cart_Goods.need_qty = temp.need_replenish_qty;
                }

                GoodsScan scaner = new GoodsScan();
                scaner.goods = cart_Goods;
                scaner.ShowType = 1;
                DialogHost host = new DialogHost();

                //var temo = await host.ShowDialog(scaner);
                var temo = await DialogHost.Show(scaner, "RootDialog");
                if ((bool)temo == false)
                {

                }
                else
                {
                    if (temp.new_replenish_qty != scaner.goods.qty)
                    {
                        CacheData.Ins.isReplenishEdit = true;
                        isReplenishComplete = false;
                    }
                    var tem = temp.QtyErr;
                    temp.new_replenish_qty = scaner.goods.qty;

                }
                girdRelenish.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log.Error("BarcodeIn", ex);
            }
        }

        private void girdRelenish_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void sendMsg(string code, string MsgType)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (CacheData.Ins.SelectedMenu == "待补货" && !ShowDetail)
                {
                    Log.Debug(this.ToString() + "待补货!:" + code);
                    //GridFilter(girdGoods, code);
                }
                else if (CacheData.Ins.SelectedMenu == "待补货" && ShowDetail)
                {
                    Log.Debug(this.ToString() + "待补货:" + code);
                    BarcodeIn(code);
                }
                else
                {
                    Log.Debug(this.ToString() + ":" + code);
                    //GridFilter(girdReplenished, code);
                }
            });
        }

        public void SendMsg(string code)
        {
            BarcodeIn(code);
        }
        public void MenuSelected(string menuName)
        {
        }

        private async void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender is TextBox txt)
                {
                    if (txt.Tag is spd_replenishdtl Dtl)
                    {
                        if (Dtl == null) return;
                        if (Dtl.need_replenish_qty == 0)
                        {
                            CacheData.Ins.mainWindow.MessageTips("已补完");
                            return;
                        }
                        int qty = Dtl.new_replenish_qty;
                        cart_goods cart_Goods = new cart_goods();
                        cart_Goods.goods_no = Dtl.goodsno;
                        cart_Goods.goods_name = Dtl.goodsname;
                        cart_Goods.goods_spec = Dtl.goodstype;
                        cart_Goods.factory_name = Dtl.factoryname;
                        cart_Goods.unit = Dtl.goodsunitname;
                        cart_Goods.validity_date = Dtl.validto ?? DateTime.MinValue;
                        cart_Goods.batch_no = Dtl.plotno;
                        cart_Goods.qty = 0;
                        cart_Goods.need_qty = Dtl.need_replenish_qty;
                        GoodsScan scaner = new GoodsScan();
                        scaner.ShowType = 1;
                        scaner.goods = cart_Goods;
                        var temo = await DialogHost.Show(scaner, "RootDialog");
                        if ((bool)temo == false)
                        {

                        }
                        else
                        {
                            if (scaner.goods.qty > 0)
                            {
                                CacheData.Ins.isReplenishEdit = true;
                                isReplenishComplete = false;
                            }
                            int editQty = scaner.goods.qty;
                            if (editQty > Dtl.need_replenish_qty)
                            {
                                FindSameGoods(Dtl, editQty);

                            }
                            Dtl.new_replenish_qty = editQty;
                            if (Dtl.new_replenish_qty > Dtl.need_replenish_qty)
                                Dtl.largeQty = true;
                            girdRelenish.Items.Refresh();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("girdRelenish_SelectionChanged", ex);
            }
        }

        public void FindSameGoods(spd_replenishdtl dtls, int qty)
        {

            List<spd_replenish> replenishs = CacheData.Ins.dbUtil.Query<spd_replenish>("SELECT * from spd_replenish where  replenishid in( SELECT replenishid from spd_replenishdtl where rackstatus=2)  order by replenishno desc", null);
            List<spd_replenishdtl> replenishdtls = CacheData.Ins.dbUtil.Query<spd_replenishdtl>("SELECT * from spd_replenishdtl  where rackstatus=2 and sourceid <> @sourceid", dtls);
            var curr = replenishdtls.FindAll(a => a.goodsid == dtls.goodsid && a.plotid == dtls.plotid);
            if (curr != null && curr.Count > 0)
            {
                bool isEnough = false;
                bool IsAdd = false;
                foreach (var item in curr)
                {
                    if (item.replenishid != dtls.replenishid)
                    {
                        IsAdd = true;
                        var main = replenishs.FirstOrDefault(a => a.replenishid == item.replenishid);
                        if (main != null)
                        {
                            if (txtMainNo.Text.Contains(main.replenishno))
                                continue;
                            CurrReplenishes.Add(main);
                            main.detail = replenishdtls.FindAll(a => a.replenishid == main.replenishid);

                            txtMainNo.Text += Environment.NewLine + main.replenishno;
                            foreach (var dtl in main.detail)
                            {
                                var dtlitem = dtl.DeepClone();
                                dtlitem.need_replenish_qty = (int)dtlitem.replenishqty - dtlitem.old_replenish_qty;
                                dtlitem.need_replenish_qty = dtlitem.need_replenish_qty < 0 ? 0 : dtlitem.need_replenish_qty;
                                var goods = ReplenishDtls.FirstOrDefault(a => a.goodsid == dtlitem.goodsid);
                                if (goods == null)
                                    ReplenishDtls.Add(dtlitem);
                                else
                                {
                                    goods.need_replenish_qty += dtlitem.need_replenish_qty;
                                    goods.old_replenish_qty += dtlitem.old_replenish_qty;
                                }
                                if (goods.need_replenish_qty >= qty)
                                {
                                    isEnough = true;
                                    break;
                                }
                                if (dtlitem.need_replenish_qty >= qty)
                                {
                                    isEnough = true;
                                    break;
                                }
                            }
                            if (isEnough)
                                break;

                        }

                    }
                }
                if (IsAdd)
                    CacheData.Ins.mainWindow.MessageTips("数量超出本单，已自动关联相同商品的补货单");
            }
        }
    }
}

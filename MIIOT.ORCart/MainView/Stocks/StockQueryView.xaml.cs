using MIIOT.Controls;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.ORCart.Common;
using MIIOT.Utility;
using Newtonsoft.Json;
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

namespace MIIOT.ORCart.MainView
{
    /// <summary>
    /// ApplyView.xaml 的交互逻辑
    /// </summary>
    public partial class StockQueryView : UserControl, IMenuMsgSend
    {
        public StockQueryView()
        {
            InitializeComponent();
            this.Loaded += StockQueryView_Loaded;


        }
        List<cart_goods> goods = new List<cart_goods>();
        private void StockQueryView_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                this.Dispatcher.Invoke(async () =>
               {
                   try
                   {

                       List<cart_goods> goodslist = CacheData.Ins.dbUtil.Query<cart_goods>("select * from cart_goods", null);
                       List<DapperParams> sqlstr = new List<DapperParams>();
                       goods = new StockUtil().GetAllStock();// dbUtil.Query<cart_goods>("SELECT b.goods_id, b.goods_no,b.goods_name,b.goods_spec,b.factory_name,b.unit,SUM(a.qty) qty, c.uplimit,c.downlimit from cart_stock a, cart_goods b,spd_goodsofstock c where a.goods_id=b.goods_id and a.goods_id=c.goodsid GROUP BY b.goods_id, a.plot_id", null);
                       if (goods == null) return;
#if Net

                       string url = CacheData.Ins.JsonConfig["库存信息"];
                       var data = new { storehouseid = CacheData.Ins.currStock.storehouseid };
                       string dataJsonstr = JsonConvert.SerializeObject(data);
                       HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                       if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                       {
                           Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                           if (res != null && res.code == 0)
                           {
                               List<spd_stock> netStocks = new List<spd_stock>();
                               List<spd_stock> rfiddata = JsonConvert.DeserializeObject<List<spd_stock>>(res.data.ToString());
                               var group = rfiddata.GroupBy(a => a.goodsid);
                               foreach (var grp in group)
                               {
                                   var item = grp.First();
                                   item.stockqty = grp.Sum(a => a.stockqty);
                                   netStocks.Add(item);
                               }
                               foreach (var item in goods)
                               {
                                   var netstock = netStocks.FirstOrDefault(a => a.goodsid == item.goods_id);
                                   if (netstock != null)
                                   {
                                       item.SpdQty = (int)(netstock.stockqty ?? 0);
                                   }
                                   else
                                   {

                                   }
                               }
                               foreach (var item in netStocks)
                               {
                                   var netstock = goods.FirstOrDefault(a => a.goods_id == item.goodsid);
                                   if (netstock == null)
                                   {
                                       var good = goodslist.FirstOrDefault(a => a.goods_id == item.goodsid);
                                       if (good != null)
                                       {
                                           goods.Add(new cart_goods()
                                           {
                                               goods_id = good.goods_id,
                                               goods_no = good.goods_no,
                                               goods_name = good.goods_name,
                                               goods_spec = good.goods_spec,
                                               goods_type = good.goods_type,
                                               factory_name = good.factory_name,
                                               unit = good.unit,
                                               qty = (int)(item.stockqty ?? 0),
                                               SpdQty = (int)(item.stockqty ?? 0),

                                           });
                                           cart_stock stock = new cart_stock()
                                           {
                                               stock_id = CacheData.Ins.SnowId.nextId(),
                                               stockhouse_id = CacheData.Ins.currStock.storehouseid,
                                               goods_id = item.goodsid,
                                               qty = (int)(item.stockqty ?? 0),
                                               creman_id = CacheData.Ins.User.id,
                                               creman_name = CacheData.Ins.User.display_name,
                                               cretime = DateTime.Now
                                           };
                                           sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(stock), stock));
                                       }
                                   }
                               }
                               if (sqlstr.Count > 0)
                               {
                                   bool succ = CacheData.Ins.dbUtil.TransactionAction(sqlstr);
                                   if (succ)
                                   {
                                   }
                               }
                           }
                           else
                               CacheData.Ins.mainWindow.MessageTips("请求错误：" + res.msg);
                       }
                       else
                           CacheData.Ins.mainWindow.MessageTips("请求错误：" + result.StatusCode.ToString());

#endif
                       int Qty = 0;
                       for (int i = 0; i < goods.Count; i++)
                       {
                           goods[i].use_status = 0;
                           Qty += goods[i].qty;
                           if (goods[i].uplimit == 0)
                           {
                               goods[i].use_status = 0;
                           }
                           else
                           {

                               if (goods[i].qty > goods[i].uplimit)
                                   goods[i].use_status = 2;
                           }
                           if (goods[i].use_status == 0)
                               if (goods[i].downlimit == 0)
                               {
                                   goods[i].use_status = 0;
                               }
                               else
                               {
                                   if (goods[i].qty < goods[i].downlimit)
                                       goods[i].use_status = 1;
                               }


                       }
                       TypeCount.Text = goods.Count.ToString();
                       TotailQty.Text = Qty.ToString();
                       gridStock.ItemsSource = goods;
                   }
                   catch (Exception ex)
                   {
                       Log.Error("StockQueryView_Loaded disp", ex);
                   }
               });
            });


        }

        public void sendMsg(string code, string MsgType)
        {
            this.Dispatcher.Invoke(() =>
            {
                Log.Debug(this.ToString() + code);
                StockUtil.GridFilter(gridStock, code);
            });
        }
        public void MenuSelected(string menuName)
        {
        }

        private void gridStock_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void gridStock_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                if (grid.Items != null)
                {
                    for (int i = 0; i < grid.Items.Count; i++)
                    {
                        try
                        {
                            DataGridRow row = grid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                            if (row != null)
                            {
                                row.Header = (i + 1).ToString();
                            }
                        }
                        catch { }
                    }
                }
            }
        }


        private void btnDownStock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpStock_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnFixStock_Click(object sender, RoutedEventArgs e)
        {
            List<DapperParams> sqlstr = new List<DapperParams>();
            foreach (var item in goods)
            {
                cart_stock stock = new cart_stock()
                {
                    stock_id = CacheData.Ins.SnowId.nextId(),
                    stockhouse_id = CacheData.Ins.currStock.storehouseid,
                    goods_id = item.goods_id,
                    qty = item.SpdQty,
                    creman_id = CacheData.Ins.User.id,
                    creman_name = CacheData.Ins.User.display_name,
                    cretime = DateTime.Now
                };
                sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(stock), stock));
            }
            if (sqlstr.Count > 0)
            {
                bool succ = CacheData.Ins.dbUtil.TransactionAction(sqlstr);
                if (succ)
                {
                }
            }
        }
    }
}

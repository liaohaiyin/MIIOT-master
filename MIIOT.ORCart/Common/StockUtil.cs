using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/4 14:04:54
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart
{
    public class StockUtil
    {
        public StockUtil()
        {

        }
        public List<cart_goods> GetStockGoodsByNo(string goodNo)
        {
            List<cart_goods> list = CacheData.Ins.dbUtil.Query<cart_goods>("SELECT b.goods_id, b.goods_no,b.goods_name,b.goods_spec,b.factory_name,b.unit,a.qty,a.repair_status use_status,b.goods_type " +
                     "from cart_stock a left JOIN cart_goods b on a.goods_id=b.goods_id WHERE goods_no =@goods_no", new { goods_no = goodNo });
            return list;
        }

        public List<cart_goods> GetAllStockGoods()
        {
            List<cart_goods> list = CacheData.Ins.dbUtil.Query<cart_goods>(@"SELECT b.ischarge,b.recyclableflag,b.singleflag, b.goods_type,b.price, b.goods_id, b.goods_no,b.goods_name,b.goods_spec,b.factory_name,b.unit,a.plot_id,a.plot_no,a.validity_date,  SUM(a.qty) qty  from 
cart_stock a INNER JOIN cart_goods b on a.goods_id=b.goods_id  GROUP BY b.goods_id ", new { });
            return list;
        }
        public List<cart_goods> GetAllStock()
        {
            List<cart_goods> goodsResult = new List<cart_goods>();
            List<cart_goods> goods = CacheData.Ins.dbUtil.Query<cart_goods>("select * from cart_goods", null);
            List<cart_stock> stocks = CacheData.Ins.dbUtil.Query<cart_stock>("select SUM(qty) qty,goods_id,plot_id,plot_no,validity_date from cart_stock a GROUP BY goods_id,plot_no", null);
            List<spd_goodsofstock> ofstocks = CacheData.Ins.dbUtil.Query<spd_goodsofstock>("select * from spd_goodsofstock", null);

            foreach (var item in stocks)
            {
                cart_goods cugoods = null;
                var dbgoods = goods.FirstOrDefault(a => a.goods_id == item.goods_id);
                if (dbgoods != null)
                {
                    cugoods = new cart_goods();
                    cugoods.goods_id = dbgoods.goods_id;
                    cugoods.goods_no = dbgoods.goods_no;
                    cugoods.goods_name = dbgoods.goods_name;
                    cugoods.goods_spec = dbgoods.goods_spec;
                    cugoods.goods_type = dbgoods.goods_type;
                    cugoods.plot_id = item.plot_id;
                    cugoods.plot_no = item.plot_no;
                    cugoods.unit = dbgoods.unit;
                    cugoods.qty = item.qty;
                    cugoods.factory_name = dbgoods.factory_name;
                   
                    var ofstock = ofstocks.FirstOrDefault(a=>a.goodsid==item.goods_id);
                    if (ofstock != null)
                    {
                        double up = 0, down = 0;
                        double.TryParse(ofstock.uplimit, out up);
                        double.TryParse(ofstock.downlimit, out down);
                        cugoods.uplimit = up;
                        cugoods.downlimit = down;
                        goodsResult.Add(cugoods);
                    }
                }
            }
          
            return goodsResult;
        }

        public List<cart_goods> GetStrategyGoods()
        {
            List<cart_goods> list = CacheData.Ins.dbUtil.Query<cart_goods>(@"SELECT	b.goods_id,	b.goods_no,	b.goods_name,	b.goods_spec,	b.factory_name,	b.unit,	b.goods_type FROM	spd_goodsofstock a INNER JOIN cart_goods b ON a.goodsid = b.goods_id", new { });
            return list;
        }
        public bool Instock(string goods_id, int qty, string stockhouse_id)
        {

            List<DapperParams> sqlStr = new List<DapperParams>();
            List<cart_stock> stocks = CacheData.Ins.dbUtil.Query<cart_stock>("SELECT * from cart_stock where goods_id=@goods_id", new { goods_id });
            if (stocks != null && stocks.Count > 0)
            {
                sqlStr.Add(new DapperParams($" UPDATE cart_stock set qty=qty+{qty} where goods_id={goods_id}; ", null));
            }
            else
            {
                long No = CacheData.Ins.SnowId.nextId();
                cart_stock stock = new cart_stock()
                {
                    goods_id = goods_id,
                    qty = qty,
                    stock_id = No,
                    creman_id = CacheData.Ins.User.id.ToString(),
                    creman_name = CacheData.Ins.User.user_name,
                    cretime = DateTime.Now,
                    stockhouse_id = stockhouse_id

                };
                int res = CacheData.Ins.dbUtil.Insert(stock);


            }
            var code = CacheData.Ins.SnowId.nextId();
            cart_instock instock = new cart_instock()
            {
                cart_code = CacheData.Ins.CartNo,
                goods_id = goods_id,
                storehouse_id = stockhouse_id,
                instock_qty = qty,
                instock_id = code,
                instock_no = code.ToString(),
                create_time = DateTime.Now
            };
            var scu = CacheData.Ins.dbUtil.Insert(instock);
            bool succ = CacheData.Ins.dbUtil.TransactionAction(sqlStr);
            return true;
        }

        public bool Outstock(string goods_id, int qty, string stockhouse_id, string OpType)
        {
            List<DapperParams> sqlStr = new List<DapperParams>();
            List<cart_stock> stocks = CacheData.Ins.dbUtil.Query<cart_stock>("SELECT * from cart_stock where goods_id=@goods_id", new { goods_id });
            if (stocks != null && stocks.Count > 0)
            {
                int dbqty = stocks[0].qty - qty;
                //if (dbqty > 0)
                sqlStr.Add(new DapperParams($" UPDATE cart_stock set qty={dbqty} where stock_id='{stocks[0].stock_id}'; ", null));
                //else
                {
                    //long No = CacheData.Ins.SnowId.nextId();
                    //cart_stock_err stock = new cart_stock_err()
                    //{
                    //    goods_id = goods_id,
                    //    qty = Math.Abs(dbqty),
                    //    stock_id = No,
                    //    creman_id = CacheData.Ins.User.id.ToString(),
                    //    creman_name = CacheData.Ins.User.user_name,
                    //    cretime = DateTime.Now,
                    //    stockhouse_id = stockhouse_id,
                    //    repair_status = OpType
                    //};
                    //int res = CacheData.Ins.dbUtil.Insert(stock);
                }
            }
            else
            {
                long No = CacheData.Ins.SnowId.nextId();
                cart_stock stock = new cart_stock()
                {
                    goods_id = goods_id,
                    qty = qty,
                    stock_id = No,
                    creman_id = CacheData.Ins.User.id.ToString(),
                    creman_name = CacheData.Ins.User.user_name,
                    cretime = DateTime.Now,
                    stockhouse_id = stockhouse_id

                };
                int res = CacheData.Ins.dbUtil.Insert(stock);
            }
            var code = CacheData.Ins.SnowId.nextId();
            cart_outstock outstock = new cart_outstock()
            {
                cart_code = CacheData.Ins.currStock.storehouseid,
                goods_id = goods_id,
                storehouse_id = stockhouse_id,
                outstock_qty = qty,
                outstock_id = code,
                outstock_no = code.ToString(),
                create_time = DateTime.Now,

            };
            var suc = CacheData.Ins.dbUtil.Insert(outstock);
            bool succ = CacheData.Ins.dbUtil.TransactionAction(sqlStr);
            return true;
        }
        public string StockErrBulder(cart_stock_err item, int qty)
        {
            string No = CacheData.Ins.SnowId.nextId().ToString();
            return $" INSERT into cart_stock_err(stock_id,goods_id,stockhouse_no,qty,creman_name,cretime,repair_status)VALUES(" +
             $"'{No}','{item.goods_id}','{CacheData.Ins.currStock.storehouseid}',{qty},'{CacheData.Ins.User.display_name}','{DateTime.Now.ToLongString()}','0');";
        }

        public string StockBulder(cart_stock item)
        {





            var stocks = CacheData.Ins.dbUtil.Query<cart_stock>("SELECT goods_id,qty from cart_stock where stockhouse_no=@stockhouse_no",
                new { stockhouse_no = CacheData.Ins.currStock.storehouseid });
            string No = CacheData.Ins.SnowId.nextId().ToString();

            var goods = stocks.FirstOrDefault(a => a.goods_id == item.goods_id);
            if (goods == null)
                return $" INSERT into cart_stock(stock_id,goods_id,stockhouse_no,plot_id,plot_no,validity_date, qty,cretime)VALUES(" +
                  $"'{No}','{item.goods_id}','{CacheData.Ins.currStock.storehouseid}',{item.qty},'{DateTime.Now.ToLongString()}');";
            else
                return $" UPDATE cart_stock set qty=qty+{item.qty} where goods_id='{item.goods_id}';";
        }
        public string InStockBulder(cart_instock item)
        {
            string No = CacheData.Ins.SnowId.nextId().ToString();
            return $"INSERT into cart_instock(instock_id,instock_no,goods_id,instock_qty,creman_id,creman_name,cart_code)VALUES" +
                $"('{No}','{No}','{item.goods_id}',{ item.instock_qty},'{CacheData.Ins.User.id}'," +
                $"'{CacheData.Ins.User.user_name}','{CacheData.Ins.currStock.storehouseid}')";
        }
        public string OutStockBulder(cart_outstock item)
        {
            string No = CacheData.Ins.SnowId.nextId().ToString();
            return $"INSERT into cart_outstock(outstock_id,outstock_no,goods_id,outstock_qty,creman_id,creman_name,cart_code)VALUES" +
                $"('{No}','{No}','{item.goods_id}',{ item.outstock_qty},'{CacheData.Ins.User.id}'," +
                $"'{CacheData.Ins.User.user_name}','{CacheData.Ins.currStock.storehouseid}')";
        }
        public static void GridFilter(DataGrid dataGrid, string ParaStr)
        {
            var cvs = CollectionViewSource.GetDefaultView(dataGrid.ItemsSource);
            if (cvs != null && cvs.CanFilter)
            {
                cvs.Filter = new Predicate<object>(a =>
                {
                    if (a is cart_goods)
                    {
                        var text = ParaStr.ToLower();
                        return (a as cart_goods).goods_no.ToLower().Contains(text)
                            || (a as cart_goods).goods_name.ToLower().Contains(text);
                    }
                    return false;
                });

            }
        }

    }
}

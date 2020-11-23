using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/26 18:45:40
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart.Common
{
    public class AccountUtil
    {

        public bool KeepAccount(List<cart_clear> SelectClears, spd_surgeryinfo SelectPubSurgery)
        {
            try
            {

                if (SelectClears != null && SelectClears.Count > 0)
                {
                    List<cart_clear> clears = CacheData.Ins.dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status =1", new { origin_id = SelectPubSurgery.surgeryid });
                    List<DapperParams> sqlstr = new List<DapperParams>();
                    foreach (var seItem in SelectClears)
                    {
                        var dbItem = clears.FirstOrDefault(a => a.id != seItem.id && a.goods_id == seItem.goods_id);
                        if (dbItem != null)
                        {
                            dbItem.qty += seItem.qty;
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.UpdateStr(dbItem), dbItem));
                            sqlstr.Add(new DapperParams("delete from cart_clear where id=@id", seItem));
                        }
                        else
                        {
                            seItem.status = 1;
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.UpdateStr(seItem), seItem));
                        }
                        cart_clear_log log = new cart_clear_log()
                        {
                            id = CacheData.Ins.SnowId.nextId(),
                            source_id = seItem.origin_id,
                            goods_id = seItem.goods_id,
                            opt_type = "记账",
                            single_code=seItem.single_code,
                            pay_tag=seItem.pay_flag?"是":"否",
                            creater_id = CacheData.Ins.User.id,
                            creater = CacheData.Ins.User.display_name,
                            creat_time = DateTime.Now,
                            qty = seItem.qty

                        };
                        sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(log), log));
                        if (seItem.goods_type < 2 && !seItem.recevie_tag)
                        {
                            cart_stock stock = new cart_stock()
                            {
                                stock_id = CacheData.Ins.SnowId.nextId(),
                                stockhouse_id = CacheData.Ins.currStock.storehouseid,
                                goods_id = seItem.goods_id,
                                plot_id = seItem.plot_id,
                                plot_no = seItem.plot_no,
                                validity_date = seItem.validity_date,
                                qty = 0 - seItem.qty,
                                creman_id = CacheData.Ins.User.id,
                                creman_name = CacheData.Ins.User.display_name,
                                cretime = DateTime.Now
                            };
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(stock), stock));
                            cart_outstock outstock = new cart_outstock()
                            {
                                outstock_id = CacheData.Ins.SnowId.nextId(),
                                goods_id = seItem.goods_id,
                                plot_id = seItem.plot_id,
                                plot_no = seItem.plot_no,
                                validity_date = seItem.validity_date,
                                outstock_qty = seItem.qty,
                                storehouse_id = CacheData.Ins.currStock.storehouseid,
                                creman_id = CacheData.Ins.User.id,
                                creman_name = CacheData.Ins.User.display_name,
                                create_time = DateTime.Now
                            };
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(outstock), outstock));
                        }
                        else if (seItem.recevie_tag)
                        {
                            sqlstr.Add(new DapperParams("UPDATE spd_receiveinfodtl set receivedtlstatus=2 where receivedtlid =@receivedtlid", new { receivedtlid = seItem.receiveddtl_id }));
                        }
                    }

                    if (sqlstr.Count > 0)
                    {
                        bool succ = CacheData.Ins.dbUtil.TransactionAction(sqlstr);
                        if (succ)
                        {
                            CacheData.Ins.mainWindow.PreSurgery = SelectPubSurgery.surgeonname + "  " + SelectPubSurgery.customname;
                            return true;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    CacheData.Ins.mainWindow.MessageTips("未选中任何记录");
                }
            }
            catch (Exception ex)
            {
                Log.Error("btnKeepAccount_click", ex);
            }
            return false;
        }
        public bool CancelAccount(List<cart_clear> ClearList, spd_surgeryinfo SelectPubSurgery)
        {
            var SelectClears = ClearList.FindAll(a => a.Selected);
            if (SelectClears != null && SelectClears.Count > 0)
            {
                List<cart_clear> clears = CacheData.Ins.dbUtil.Query<cart_clear>("SELECT * from cart_clear WHERE origin_id=@origin_id and status =1", new { origin_id = SelectPubSurgery.surgeryid });
                List<DapperParams> sqlstr = new List<DapperParams>();
                foreach (var seItem in SelectClears)
                {
                    if (seItem.recevie_tag)
                    {
                        sqlstr.Add(new DapperParams("UPDATE spd_receiveinfodtl set receivedtlstatus=3 where receivedtlid=@receiveddtl_id", seItem));
                        sqlstr.Add(new DapperParams("UPDATE cart_clear set status=4 where id=@id", seItem));
                        continue;
                    }
                    var dbItem = clears.FirstOrDefault(a => a.goods_id == seItem.goods_id && a.single_code == seItem.single_code);
                    if (dbItem != null)
                    {
                        dbItem.qty -= seItem.qty;
                        if (dbItem.qty == 0)//清空
                        {
                            if (!seItem.recevie_tag)
                                sqlstr.Add(new DapperParams("UPDATE cart_clear set status=4 where id=@id", seItem));
                            if (!dbItem.recevie_tag)
                                sqlstr.Add(new DapperParams("UPDATE cart_clear set status=4 where id=@id", dbItem));
                            ClearList.Remove(seItem);
                        }
                        else if (dbItem.qty > 0)
                        {
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.UpdateStr(dbItem), dbItem));
                            if (!seItem.recevie_tag)
                                sqlstr.Add(new DapperParams("UPDATE cart_clear set status=4 where id=@id", seItem));
                            ClearList.Remove(seItem);
                        }
                        else//异常
                        {
                            CacheData.Ins.mainWindow.MessageTips($"销账失败,[{seItem.goods_name}]销账数量超过库存数量[{0 - dbItem.qty}]");
                            return false;
                        }

                        cart_clear_log log = new cart_clear_log()
                        {
                            id = CacheData.Ins.SnowId.nextId(),
                            source_id = seItem.origin_id,
                            goods_id = seItem.goods_id,
                            single_code = seItem.single_code,
                            opt_type = "销账",
                            pay_tag = seItem.pay_flag ? "是" : "否",
                            creater_id = CacheData.Ins.User.id,
                            creater = CacheData.Ins.User.display_name,
                            creat_time = DateTime.Now,
                            qty = 0 - seItem.qty

                        };
                        sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(log), log));
                        if (seItem.goods_type < 2 && !seItem.recevie_tag)
                        {
                            cart_stock stock = new cart_stock()
                            {
                                stock_id = CacheData.Ins.SnowId.nextId(),
                                stockhouse_id = CacheData.Ins.currStock.storehouseid,
                                goods_id = seItem.goods_id,
                                plot_id = seItem.plot_id,
                                plot_no = seItem.plot_no,
                                validity_date = seItem.validity_date,
                                qty = seItem.qty,
                                creman_id = CacheData.Ins.User.id,
                                creman_name = CacheData.Ins.User.display_name,
                                cretime = DateTime.Now
                            };
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(stock), stock));
                            cart_instock instock = new cart_instock()
                            {
                                instock_id = CacheData.Ins.SnowId.nextId(),
                                goods_id = seItem.goods_id,
                                plot_id = seItem.plot_id,
                                plot_no = seItem.plot_no,
                                validity_date = seItem.validity_date,
                                instock_qty = seItem.qty,
                                storehouse_id = CacheData.Ins.currStock.storehouseid,
                                creman_id = CacheData.Ins.User.id,
                                creman_name = CacheData.Ins.User.display_name,
                                create_time = DateTime.Now
                            };
                            sqlstr.Add(new DapperParams(CacheData.Ins.dbUtil.BuildInsertSqlStr(instock), instock));
                        }


                    }
                    else
                    {
                        CacheData.Ins.mainWindow.MessageTips($"销账失败,[{seItem.goods_name}]未记账");
                        return false;
                    }
                }
                if (sqlstr.Count > 0)
                {
                    bool succ = CacheData.Ins.dbUtil.TransactionAction(sqlstr);
                    if (succ)
                    {
                        CacheData.Ins.mainWindow.MessageTips("销账成功");
                        return true;
                    }
                }
                return true;
            }
            else
            {
                CacheData.Ins.mainWindow.MessageTips("请选中记录后进行操作");
            }
            return false;
        }
    }
}

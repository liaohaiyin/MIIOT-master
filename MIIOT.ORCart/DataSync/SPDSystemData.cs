using MaterialDesignThemes.Wpf.Transitions;
using MIIOT.Model;
using MIIOT.Model.ORCart.SPDModel;
using MIIOT.Model.ORCart.SPDPara;
using MIIOT.ORCart.Common;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/27 16:53:53
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart.DataSync
{
    public class SPDSystemData
    {
        public SPDSystemData()
        {

        }
        public async Task<bool> TestConnected(string Host, string url)
        {
            try
            {
                var data = new { };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await new SMHttpRequester(Host).PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return false;
        }
        #region 基础数据 

        public async void GetBackReason()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["退库原因"];
                var data = new { };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        CacheData.Ins.dbUtil.Query<spd_room>("delete from spd_backreason", null);
                        List<spd_backreason> rfiddata = JsonConvert.DeserializeObject<List<spd_backreason>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_backreason>(rfiddata);
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }

        public async void GetBackStore()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["退库目标库房"];
                var data = new { begintime = DateTime.Now.AddDays(-73).ToLongString(), endtime = DateTime.Now.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        CacheData.Ins.dbUtil.Query<spd_room>("delete from spd_backtargetstore", null);
                        List<spd_backtargetstore> rfiddata = JsonConvert.DeserializeObject<List<spd_backtargetstore>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_backtargetstore>(rfiddata);
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async void GetORoom()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["手术间信息"];
                var data = new { storehouseid = CacheData.Ins.currStock.storehouseid };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        CacheData.Ins.dbUtil.Query<spd_room>("delete from spd_room", null);
                        var temo = result.data.ToString();
                        List<spd_room> rfiddata = JsonConvert.DeserializeObject<List<spd_room>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Insert<spd_room>(rfiddata);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async Task<bool> GetGoodsStrategy()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["商品库房策略"];
                var data = new { storehouseid = CacheData.Ins.currStock.storehouseid };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        CacheData.Ins.dbUtil.Query<spd_room>("delete from spd_goodsofstock", null);
                        List<spd_goodsofstock> rfiddata = JsonConvert.DeserializeObject<List<spd_goodsofstock>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_goodsofstock>(rfiddata);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return false;
        }
        public async void GetDept()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["部门信息"];
                var data = new { };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        var temo = result.data.ToString();
                        List<spd_dept> rfiddata = JsonConvert.DeserializeObject<List<spd_dept>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_dept>(rfiddata);
                        foreach (var item in rfiddata)
                        {
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async void GetHouse()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["库房信息"];
                var data = new { };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && !string.IsNullOrWhiteSpace(result.data))
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    var temo = result.data.ToString();
                    List<spd_stockhouse> rfiddata = JsonConvert.DeserializeObject<List<spd_stockhouse>>(res.data.ToString());
                    int i = CacheData.Ins.dbUtil.Replace<spd_stockhouse>(rfiddata);
                }

            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public void InitBasicsData()
        {
            GetBackReason();
            GetBackStore();
            GetORoom();
            GetGoodsStrategy();
            GetDept();
            GetHouse();
        }

        #endregion

        public void SyncDataByEditTime()
        {
            Task.Run(async () =>
            {

                try
                {
                    DateTime Stime = DateTime.Now;
                    await GetSurgery(Stime.AddDays(-77), Stime);
                    GetPreSurgeryReceive(Stime.AddDays(-77), Stime);
                    GetGoods(Stime.AddDays(-77), Stime);
                    GetSinglecode(Stime.AddDays(-77), Stime);
                    GetSinglecodeexec(Stime.AddDays(-77), Stime);
                    Getvalidity(Stime.AddDays(-77), Stime);
                    GetRepnelish(Stime.AddDays(-77), Stime);
                    GetStock();
                    while (true)
                    {
                        while (CacheData.Ins.isAutoSync)
                        {
                            try
                            {
                                DateTime now = DateTime.Now;
                                if (Stime < now)
                                {
                                    await GetSurgery(Stime, now);
                                    GetPreSurgeryReceive(Stime, now);
                                    GetGoods(Stime, now);
                                    GetSinglecode(Stime, now);
                                    GetSinglecodeexec(Stime, now);
                                    Getvalidity(Stime, now);
                                    GetRepnelish(Stime, now);
                                    GetStock();
                                    InitBasicsData();
                                    //Log.Debug($"{Stime.ToLongString()}~{now.ToLongString()}");
                                    Stime = now.AddSeconds(-10);
                                }
                                Thread.Sleep(20 * 1000);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("LoadTime", ex);
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("SyncDataByEditTime", ex);
                }
            });
        }

        public async Task<bool> GetSurgery(DateTime begintime, DateTime endtime)
        {
            try
            {
                CacheData.Ins.dbUtil.Query<spd_surgeryinfo>("SELECT editdate from spd_surgeryinfo ORDER BY editdate DESC LIMIT 1", null);
                string url = CacheData.Ins.JsonConfig["手术单信息"];
                var data = new { begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        var temo = result.data.ToString();
                        List<spd_surgeryinfo> rfiddata = JsonConvert.DeserializeObject<List<spd_surgeryinfo>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_surgeryinfo>(rfiddata);
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("GetSurgery", ex);
            }
            return false;
        }
        public async void GetPreSurgeryReceive(DateTime begintime, DateTime endtime)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["术前领用单信息"];
                var data = new { sourceno= "sourceno", begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        var temo = result.data.ToString();
                        List<spd_receiveinfo> rfiddata = JsonConvert.DeserializeObject<List<spd_receiveinfo>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_receiveinfo>(rfiddata);
                        foreach (var info in rfiddata)
                        {
                            if (info.detail != null)
                                foreach (var item in info.detail)
                                {
                                    item.sourceno = info.sourceno;
                                    i = CacheData.Ins.dbUtil.Replace(item);
                                }

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async void GetGoods(DateTime begintime, DateTime endtime, int currentPage = 1)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["商品信息"];
                var data = new { current = currentPage, storehouseid = "", begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        dynamic resData = JsonConvert.DeserializeObject(res.data.ToString());
                        int recordCount = resData.total;
                        List<spd_goodslist> rfiddata = JsonConvert.DeserializeObject<List<spd_goodslist>>(resData.records.ToString());
                        List<cart_goods> goods = new List<cart_goods>();
                        foreach (var item in rfiddata)
                        {
                            if (item.goodsid == null || item.goodsid == "")
                            {
                                continue;
                            }
                            int catecory = 0;
                            int.TryParse(item.categorytype, out catecory);
                            cart_goods carts = new cart_goods()
                            {
                                goods_id = item.goodsid,
                                goods_no = item.goodsno,
                                goods_name = item.goodsname,
                                common_name = item.commonname,
                                use_status = int.Parse(item.usestatus),
                                goods_spec = item.goodstype,
                                goods_type = catecory,
                                factory_name = item.factoryname,
                                unit = item.goodsunit,
                                price = item.bidprice ?? 0,
                                ischarge = item.ischarge ?? 0,
                                recyclableflag = item.recyclableflag ?? 0,
                                singleflag = item.singleflag ?? 0

                                // validity_date=item.validperiod
                            };
                            if (catecory == 1)
                            {
                                carts.goods_type = item.recyclableflag == 0 ? (int)GoodsTypeEnum.HIGH : (int)GoodsTypeEnum.REUSE;
                            }
                            else if (catecory == 2)
                            {
                                carts.goods_type = item.ischarge == 0 ? (int)GoodsTypeEnum.LOWFREE : (int)GoodsTypeEnum.LOWPAY;
                            }
                            else
                            {

                            }
                            //item.editdate = DateTime.Now.UnixToDateTime(long.Parse(item.editdate)).ToLongString();
                            goods.Add(carts);
                        }
                        int i = CacheData.Ins.dbUtil.Replace<cart_goods>(goods);
                        if (i > 0 && recordCount / 100 >= currentPage)
                        {
                            int coutn = currentPage += 1;
                            GetGoods(begintime, endtime, coutn);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async Task<List<spd_stock>> GetStock(bool isQuery = false)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["库存信息"];
                var data = new { storehouseid = CacheData.Ins.currStock.storehouseid };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        List<spd_stock> rfiddata = JsonConvert.DeserializeObject<List<spd_stock>>(res.data.ToString());
                        if (!isQuery)
                        {
                            int i = CacheData.Ins.dbUtil.Replace<spd_stock>(rfiddata);
                        }
                        return rfiddata;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return null;
        }


        public async void GetSinglecode(DateTime begintime, DateTime endtime)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["单品码信息"];
                var data = new { begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && !string.IsNullOrWhiteSpace(result.data))
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null)
                    {
                        var temo = result.data.ToString();
                        List<spd_singlecode> rfiddata = JsonConvert.DeserializeObject<List<spd_singlecode>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_singlecode>(rfiddata);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async void GetSinglecodeexec(DateTime begintime, DateTime endtime)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["异常单品码信息"];
                var data = new { begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && !string.IsNullOrWhiteSpace(result.data))
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null)
                    {
                        var temo = result.data.ToString();
                        List<spd_singleexec> rfiddata = JsonConvert.DeserializeObject<List<spd_singleexec>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_singleexec>(rfiddata);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }
        public async void Getvalidity(DateTime begintime, DateTime endtime)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["效期预警信息"];
                var data = new { begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && !string.IsNullOrWhiteSpace(result.data))
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null)
                    {
                        var temo = result.data.ToString();
                        List<spd_validityalarm> rfiddata = JsonConvert.DeserializeObject<List<spd_validityalarm>>(res.data.ToString());
                        int i = CacheData.Ins.dbUtil.Replace<spd_validityalarm>(rfiddata);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
        }

        public async void GetRepnelish(DateTime begintime, DateTime endtime)
        {
            try
            {
                GetHouse();
                string url = CacheData.Ins.JsonConfig["补货单信息"];
                var data = new { CacheData.Ins.currStock.storehouseid, begintime = begintime.ToLongString(), endtime = endtime.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);
                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && result.data != null)
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null)
                    {
                        List<spd_replenish> rfiddata = JsonConvert.DeserializeObject<List<spd_replenish>>(res.data.ToString());
                        int i = 0;
                        foreach (var rep in rfiddata)
                        {
                            foreach (var item in rep.detail)
                            {
                                item.replenishid = rep.replenishid;
                                rep.replenishdtlstatus = "0";
                                var dtls = CacheData.Ins.dbUtil.Query<spd_replenishdtl>("SELECT * from spd_replenishdtl where sourceid=@id", new { id = item.sourceid });
                                if (dtls != null && dtls.Count > 0)
                                    CacheData.Ins.dbUtil.Update(item);
                                else
                                    CacheData.Ins.dbUtil.Insert(item);
                            }
                            var replenish = CacheData.Ins.dbUtil.Query<spd_replenish>("SELECT * from spd_replenish where replenishid=@id", new { id = rep.replenishid });
                            if (replenish != null && replenish.Count > 0)
                                CacheData.Ins.dbUtil.Update(rep);
                            else
                                CacheData.Ins.dbUtil.Insert(rep);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetRepnelish", ex);
            }
        }
        public async Task<bool> GetFollowInfo()
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["跟台信息"];
                var data = new {  begintime = DateTime.Now.AddDays(-90).ToLongString(), endtime = DateTime.Now.ToLongString() };
                string dataJsonstr = JsonConvert.SerializeObject(data);

                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && !string.IsNullOrWhiteSpace(result.data))
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("StockTackSPD", ex);
            }
            return false;
        }
        public async Task<bool> StockTackSPD(List<stockTackPara> datas)
        {
            try
            {
                string url = CacheData.Ins.JsonConfig["盘点数据行"];
                var jsondata = new { rows = datas };
                string dataJsonstr = JsonConvert.SerializeObject(jsondata);

                HttpResult result = await CacheData.Ins.sMHttpRequester.PostRequest(url, dataJsonstr);
                if (result != null && !string.IsNullOrWhiteSpace(result.data))
                {
                    Model.Trical.HttpResultBase res = JsonConvert.DeserializeObject<Model.Trical.HttpResultBase>(result.data.ToString());
                    if (res != null && res.code == 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("StockTackSPD", ex);
            }
            return false;
        }

    }
}

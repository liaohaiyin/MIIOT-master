using MIIOT.Drivers;
using MIIOT.Model;
using MIIOT.Model.Trical;
using MIIOT.Model.TricalModel;
using MIIOT.Trical.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace MIIOT.Trical
{
    public class TrialBusiness
    {
        HttpResolver httpResolver = new HttpResolver();
        public async void getSinglecodes()
        {
            List<int> rfIds = new List<int>();
            rfIds.Add(1);
            var RFIDS = new { rfIds = rfIds };
            string data = JsonConvert.SerializeObject(RFIDS);
            string url = CacheData.Ins.JsonConfig.GetValue("感应标签_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, data);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                RFIDRecord record = JsonConvert.DeserializeObject<RFIDRecord>(Result.data.ToString());
            }
        }

        public async void PickList()
        {
            string PickId = "1249548620553490434";
            string url = CacheData.Ins.JsonConfig.GetValue("查询拣选信息_Post");
            url += "/" + PickId;
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                PickInfo record = JsonConvert.DeserializeObject<PickInfo>(Result.data.ToString());
            }
        }

        public async void Refund()
        {
            List<string> codes = new List<string>();
            codes.Add("1");
            List<object> dtls = new List<object>();
            var dtl = new { dtlId = "1249548620469604353", codes };
            dtls.Add(dtl);
            var data = new { id = "1", dtls };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("退货复核单_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                RFIDRecord record = JsonConvert.DeserializeObject<RFIDRecord>(Result.data.ToString());
            }
        }
        public async void Apply()
        {
            List<string> codes = new List<string>();
            codes.Add("1");
            List<object> dtls = new List<object>();
            var dtl = new { dtlId = "1249548620469604353", codes };
            dtls.Add(dtl);
            var data = new { id = "1", dtls };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("申领复核_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                RFIDRecord record = JsonConvert.DeserializeObject<RFIDRecord>(Result.data.ToString());
            }
        }
        public async Task<string> GeOrgan()
        {
            string url = CacheData.Ins.JsonConfig.GetValue("机构_Get");
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                dynamic organ = JsonConvert.DeserializeObject(Result.data.ToString());
                CacheData.Ins.Organ = organ.organ_name;
            }
            return CacheData.Ins.Organ;
        }
        public async Task<List<Stock>> GetStockList()
        {
            List<Stock> stocks;
            string url = CacheData.Ins.JsonConfig.GetValue("库房列表_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                stocks = JsonConvert.DeserializeObject<List<Stock>>(Result.data.ToString());
            }
            else stocks = null;
            return stocks;
        }
        public async Task<List<DepartmentModel>> GetDept()
        {
            List<DepartmentModel> depts = null;
            string url = CacheData.Ins.JsonConfig.GetValue("科室列表查询_Get");
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                depts = JsonConvert.DeserializeObject<List<DepartmentModel>>(Result.data.ToString());

            }
            return depts;
        }
        public async Task<List<Stock>> GetHouseById(string DeptId)
        {
            List<Stock> stocks = null;
            string url = CacheData.Ins.JsonConfig.GetValue("库房列表查询_Get");
            url += "/" + DeptId;
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                stocks = JsonConvert.DeserializeObject<List<Stock>>(Result.data.ToString());
            }
            return stocks;
        }
        public async Task<List<RefundReasonModel>> GetReason(string BType)
        {
            List<RefundReasonModel> Reasons = null;
            string url = CacheData.Ins.JsonConfig.GetValue("获取退库退货的原因_Get");
            url += "/" + BType;
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                dynamic temp = JsonConvert.DeserializeObject(Result.data.ToString());
                if (temp!=null)
                {
                    Reasons = new List<RefundReasonModel>();
                    foreach (var item in temp)
                    {
                        Reasons.Add(new RefundReasonModel() { RKey = item.key, RValue = item.value });
                    }
                }
            }
            return Reasons;
        }
        public async void RackList(string sourceNo)
        {
            var data = new { sourceNo = sourceNo, status = 0 };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("查询上架信息_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                List<RackInfo> rackInfo = JsonConvert.DeserializeObject<List<RackInfo>>(Result.data.ToString());
                LiteDBHelper.Ins.Insert(rackInfo);
                List<object> list = new List<object>();
                if (rackInfo.Count == 0) return;
                foreach (var item in rackInfo[0].rackDtlList)
                {
                    var dtls = new { locNo = item.locNo, goodsId = item.goodsId, goodsName = item.goodsName, goodsSpec = item.goodsSpec, qty = item.rackQty };
                    list.Add(dtls);
                }
                var sendData = new { personName = rackInfo[0].rackPersonName, sourceNo = rackInfo[0].sourceNo, dtls = list };
                string jsonstr = JsonConvert.SerializeObject(sendData);
                ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.SlaveTCP], CacheData.Ins.SlaveTCP, "ADD", TCPPushTagEnum.RACK.ToString(), jsonstr);

            }
        }
        public async Task<bool> RackComplete(string sourceNo)
        {
            var data = new { sourceNo = 0, status = 0 };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("上架确认_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                return true;
            }
            return false;
        }
        public async void PickList(string sourceNo)
        {
            var data = new { sourceNo = sourceNo, status = 0 };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("查询拣选信息_Post");
            url += "/" + sourceNo;
            HttpResultBase Result = await httpResolver.Request(HMethod.Get, url, CacheData.Ins.Token, null);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                
                List<RackInfo> rackInfo = JsonConvert.DeserializeObject<List<RackInfo>>(Result.data.ToString());
                LiteDBHelper.Ins.Insert(rackInfo);
                List<object> list = new List<object>();
                foreach (var item in rackInfo[0].rackDtlList)
                {
                    var dtls = new { locNo = item.locNo, goodsId = item.goodsId, goodsName = item.goodsName, goodsSpec = item.goodsSpec, qty = item.rackQty };
                    list.Add(dtls);
                }
                var sendData = new { personName = rackInfo[0].rackPersonName, sourceNo = rackInfo[0].sourceNo, dtls = list };
                string jsonstr = JsonConvert.SerializeObject(sendData);
                ScreenManager.Ins.SendMsg(CacheData.Ins.Screens[CacheData.Ins.SlaveTCP], CacheData.Ins.SlaveTCP, "ADD", TCPPushTagEnum.PICK.ToString(), jsonstr);
            }
        }
        public async Task<bool> PickComplete(string PickId)
        {
            string url = CacheData.Ins.JsonConfig.GetValue("拣选完成_Post");
            url += "/" + PickId;
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                return true;
            }
            return false;
        }
        public async void manualRefund()
        {
            List<string> codes = new List<string>();
            codes.Add("1");
            List<object> dtls = new List<object>();
            var dtl = new { dtlId = "1249548620469604353", codes };
            dtls.Add(dtl);
            var data = new { storehouseId = "1", dtls };
            string datastr = JsonConvert.SerializeObject(data);
            string url = CacheData.Ins.JsonConfig.GetValue("退货手工复核单_Post");
            HttpResultBase Result = await httpResolver.Request(HMethod.Post, url, CacheData.Ins.Token, null, datastr);
            if (Result != null && Result.data != null && Result.code == 0)
            {
                RFIDRecord record = JsonConvert.DeserializeObject<RFIDRecord>(Result.data.ToString());
            }
        }
    }
}

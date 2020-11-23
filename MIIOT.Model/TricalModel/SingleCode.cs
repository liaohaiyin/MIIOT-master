using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{

    public class SingleCode
    {
        public List<CodeItems> records { get; set; } = new List<CodeItems>();
    }
    public class CodeItems
    {
        /// <summary>
        /// 
        /// </summary>
        public string singlecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rfid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lotId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supplyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceDtlId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsSpec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsCode { get; set; }
        /// <summary>
        /// 盒
        /// </summary>
        public string goodsUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsForecastDays { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsFactoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lotNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pprodDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pvalidDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supplyName { get; set; }



        //public string goods_name { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long supply_id { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string batch_no { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long batch_id { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string goods_code { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long goods_id { get; set; }
        ///// <summary>
        ///// 河南赛诺特生物技术有限公司
        ///// </summary>
        //public string goods_factory_name { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long? pvalid_date { get; set; }
        //public string supply_name { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string lot_no { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string goods_unit { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long lot_id { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public double price { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string goods_spec { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long pprod_date { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public long goods_forecast_days { get; set; }
        public string RFID { get; set; }
        public string newRFID { get; set; }
    }


}

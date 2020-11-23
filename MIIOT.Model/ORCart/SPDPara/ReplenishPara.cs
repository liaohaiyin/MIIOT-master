using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/9/7 17:10:39
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart
{
    public class ReplenishPara
    {
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inStoreTime { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string operateManId { get; set; }
        public string operateManNo { get; set; }
        public string operateManName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string locId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string locNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operationType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plotId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plotNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string singleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string memo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string categoryType { get; set; }
        public string uOrganId { get; set; }
        public string rackId { get; set; }
        public int rackQty { get; set; }
        public string rackerno { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/9/7 17:17:45
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDPara
{
    public class applyOutStockPara
    {
        /// <summary>
        /// 
        /// </summary>
        public string orgStorehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string destStorehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsQty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string singleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operateManId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operateManNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operateTime { get; set; }
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
        public int categoryType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/9/7 15:39:06
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_stock
    {
        /// <summary>
        /// 
        /// </summary>
        public string storehouseid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string stockid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plotid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? stockqty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string singlecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string locid { get; set; }
    }
}

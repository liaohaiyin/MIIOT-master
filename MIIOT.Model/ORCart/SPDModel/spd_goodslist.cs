using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/28 9:45:41
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_goodslist
    {
        /// <summary>
        /// 
        /// </summary>
        public string goodsid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsno { get; set; }
        /// <summary>
        /// 小柴胡
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 通用名1
        /// </summary>
        public string commonname { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string goodsshortname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodspy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodstype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string usestatus { get; set; }
        /// <summary>
        /// 广州白云山
        /// </summary>
        public string factoryname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsunit { get; set; }
        /// <summary>
        /// 盒
        /// </summary>
        public string supplyname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storetemp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string validperiod { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string categorytype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editdate { get; set; }
        public double? reusetimes { get; set; }

        public int? ischarge { get; set; }
        public int? recyclableflag { get; set; }
        public int? singleflag { get; set; }
        public decimal? bidprice { get; set; }
    }
}

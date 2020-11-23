using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/27 20:06:29
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_stockhouse
    {
        /// <summary>
        /// 
        /// </summary>
        public int prewarnflag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cabinetid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uorganid { get; set; }
        /// <summary>
        /// 高值耗材柜
        /// </summary>
        public string storehousename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cabinetcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string prestorehouseid { get; set; }
        public int storehousetype { get; set; }
        public int storehousestatus { get; set; }
    }
}

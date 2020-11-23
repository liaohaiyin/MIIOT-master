using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/31 14:36:50
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_receiveinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string customage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string prestorehouseno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uorganid { get; set; }
        /// <summary>
        /// 超级管理员
        /// </summary>
        public string cremanname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? receivestatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiveid { get; set; }
        /// <summary>
        /// 02间
        /// </summary>
        public string roomname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiveno { get; set; }
        /// <summary>
        /// 封杏
        /// </summary>
        public string customname { get; set; }
        /// <summary>
        /// 手术室XS
        /// </summary>
        public string prestorehousename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? customsex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("External")]
        public List<spd_receiveinfodtl> detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cremanid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string prestorehouseid { get; set; }
    }

    public class spd_receiveinfodtl
    {
        /// <summary>
        /// 
        /// </summary>
        public int? receivedtlstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceno { get; set; }
        public string receivedtlid { get; set; }
        public string goodsid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unitid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? receiveqty { get; set; }
        public string singlecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? goodsqty { get; set; }
    }
}

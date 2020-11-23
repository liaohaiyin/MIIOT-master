using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/29 23:21:33
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_replenish : BaseRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public string destorehouseno { get; set; }
        /// <summary>
        /// 药剂二级库
        /// </summary>
        public string destorehousename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editmanid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editmanname { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [Description("Key")]
        public string replenishid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgstorehouseno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int replenishstatus { get; set; }
        /// <summary>
        /// 药剂中心库
        /// </summary>
        public string orgstorehousename { get; set; }
        /// <summary>
        /// 五官科
        /// </summary>
        public string cremanname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pickmanid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string replenishno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgstorehouseid { get; set; }
        /// <summary>
        /// 五官科
        /// </summary>
        public string pickmanname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string destorehouseid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outstoretime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("External")]
        public List<spd_replenishdtl> detail { get; set; } = new List<spd_replenishdtl>();
        /// <summary>
        /// 
        /// </summary>
        public DateTime? editdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int replenishqty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cremanid { get; set; }
        public string replenishdtlstatus { get; set; }
        [Description("OnlyInsert")]
        public DateTime replenishdata { get; set; }
    }
    public class spd_replenishdtl : BaseRecord
    {
        public string replenishid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchno { get; set; }
        /// <summary>
        /// 五官科
        /// </summary>
        public string editmanname { get; set; }
        /// <summary>
        /// 一次性无菌中心静脉导管及附件
        /// </summary>
        public string goodsname { get; set; }
        /// <summary>
        /// 套
        /// </summary>
        public string goodsunitname { get; set; }
        /// <summary>
        /// 五官科
        /// </summary>
        public string cremanname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string credate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double rackqty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string singlecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string supplyname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourcesinglecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourcetype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Description("OnlyInsert")]
        public double replenishqty { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [Description("Key")]
        public string sourceid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int replenishdtlstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plotno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editmanid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cabinetcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int instocktype { get; set; }
        /// <summary>
        /// 
        /// </summary>    
        [Description("OnlyInsert")]
        public int rackstatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string plotid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? editdate { get; set; }
        /// <summary>
        /// 双腔 7Fr-20cm
        /// </summary>
        public string goodstype { get; set; }
        /// <summary>
        /// 广东百合医疗科技股份有限公司
        /// </summary>
        public string factoryname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cremanid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? validto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        [Description("External")]
        public string out_storehouse_name { get; set; }
        [Description("External")]
        public int replenish_status { get; set; }
        private int _need_replenish_qty;
        [Description("External")]

        public int need_replenish_qty
        {
            get { return _need_replenish_qty; }
            set
            {
                _need_replenish_qty = value;
                OnPropertyChanged("need_replenish_qty");
            }
        }
        private int _new_replenish_qty;
        [Description("External")]

        public int new_replenish_qty
        {
            get { return _new_replenish_qty; }
            set
            {
                if (value > need_replenish_qty)
                    largeQty = true;
                else
                    largeQty = false;
                _new_replenish_qty = value;
                OnPropertyChanged("new_replenish_qty");
            }
        }

        private int _old_replenish_qty;

        [Description("OnlyInsert")]
        public int old_replenish_qty
        {
            get { return _old_replenish_qty; }
            set
            {
                _old_replenish_qty = value;
                OnPropertyChanged("old_replenish_qty");
            }
        }
        [Description("External")]
        public int thisTimeQty { get; set; }
        private bool qtyErr;

        [Description("External")]
        public bool QtyErr
        {
            get { return qtyErr; }
            set
            {
                qtyErr = value;
                OnPropertyChanged("QtyErr");
            }
        }
        private bool _largeQty;

        [Description("External")]
        public bool largeQty
        {
            get { return _largeQty; }
            set
            {
                _largeQty = value;
                OnPropertyChanged("largeQty");
            }
        }
    }


}

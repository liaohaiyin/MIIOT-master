using MIIOT.Model.TricalModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{


    public class ApplyBackInfo : ModelBase
    {
        public long _id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private long ID;

        public long id
        {
            get
            {
                ID = _id;
                return ID;
            }
            set
            {
                _id = value;
                ID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ApplyBackDtlListItem> applyBackDtlList { get; set; } = new List<ApplyBackDtlListItem>();
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string depmId { get; set; }
        /// <summary>
        /// 试剂中心库
        /// </summary>
        public string depmName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceStorehouseId { get; set; }
        /// <summary>
        /// 检验组冰箱2
        /// </summary>
        public string sourceStorehouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int targetStorehouseId { get; set; }
        /// <summary>
        /// 试剂中心库_
        /// </summary>
        public string targetStorehouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int checkPersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string checkPersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isDelete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int createrId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtModified { get; set; }

    }
    public class ApplyBackDtlListItem : CheckInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string applyBackId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceDtlId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originDtlId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsNo { get; set; }
        /// <summary>
        /// (APTT)活化部分凝血活酶时间试剂Forlrx
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsSpec { get; set; }
        /// <summary>
        /// 法国思达高
        /// </summary>
        public string goodsFactoryName { get; set; }
        /// <summary>
        /// 盒
        /// </summary>
        public string goodsUnit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int BackQty;
        public int backQty
        {
            get
            {
                BackQty = Qty;
                return BackQty;
            }
            set
            {
                Qty = value;
                BackQty = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private int _CheckQty;
        public int checkQty
        {
            get
            {
                _CheckQty = CheckQty;
                return _CheckQty;
            }
            set
            {
                CheckQty = value;
                _CheckQty = value;
                OnPropertyChanged("checkQty");
            }
        }

        private List<RefundReasonModel> _reasons;

        public List<RefundReasonModel> Reasons
        {
            get { return _reasons; }
            set
            {
                _reasons = value;
                OnPropertyChanged("Reasons");
            }
        }
        private string _reason;

        public string backReason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                OnPropertyChanged("Reason");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isDelete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int createrId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtModified { get; set; }
        public string lotNo { get; set; }
        private long? PprodDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public long? pprodDate
        {
            get { return PprodDate; }
            set { PprodDate = value; }
        }
        private long? PvalidDate;
        /// <summary>
        /// 有效日期至
        /// </summary>
        public long? pvalidDate
        {
            get { return PvalidDate; }
            set { PvalidDate = value; }
        }
        private long? _invalidDate;
        /// <summary>
        /// 有效日期至
        /// </summary>
        public long? invalidDate
        {
            get { return _invalidDate; }
            set { _invalidDate = value; }
        }
        public List<string> RFIDs { get; set; } = new List<string>();
    }



}

// File:    pub_refund.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_refund

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MIIOT.Model.TricalModel;
using Newtonsoft.Json;
namespace MIIOT.Model
{
    /// 退货单

    public class RefundInfo : ModelBase
    {
        public long _id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private long ID;

        public long id
        {
            get {
                ID = _id;
                return ID; }
            set {
                _id = value;
                ID = value; }
        }

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
        public long supplyId { get; set; }
        /// <summary>
        /// 广州市湘晨贸易有限公司
        /// </summary>
        private string _supplyName;

        public string supplyName
        {
            get { return _supplyName; }
            set
            {
                _supplyName = value;
                OnPropertyChanged("supplyName");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int storehouseId { get; set; }
        /// <summary>
        /// 试剂中心库_
        /// </summary>
        public string storehouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int refundPersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refundPersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refundTime { get; set; }
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
        public string checkTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<RefundDtlsItem> refundDtls { get; set; } = new ObservableCollection<RefundDtlsItem>();
        public object control { get; set; }
    }
    public class RefundDtlsItem : CheckInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refundId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceId { get; set; }
        public string sourceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceDtlId { get; set; }
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
        /// 活化部分凝血活酶时间(APTT)试剂
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
        public double price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int _refundQty;

        public int refundQty
        {
            get
            {
                _refundQty = Qty;
                return _refundQty;
            }
            set
            {
                Qty = value;
                _refundQty = value;
                OnPropertyChanged("refundQty");
            }
        }
        public long supply_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int _checkQty;

        public int checkQty
        {
            get
            {
                _checkQty = CheckQty;
                return _checkQty;
            }
            set
            {
                CheckQty = value;
                _checkQty = value;
                OnPropertyChanged("checkQty");
            }
        }
        private List<RefundReasonModel> _reasons;

        public List<RefundReasonModel> Reasons
        {
            get { return _reasons; }
            set { _reasons = value;
                OnPropertyChanged("Reasons");
            }
        }
        private string _reason;

        public string backReason
        {
            get { return _reason; }
            set { _reason = value;
                OnPropertyChanged("Reason");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lotNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batchNo { get; set; }
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
        public List<string> RFIDS { get; set; } = new List<string>();
        
    }
}
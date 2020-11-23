// File:    pub_apply.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_apply

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
/// 申领
namespace MIIOT.Model
{
    public class ApplyInfo : ModelBase
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
        public long organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sourceId { get; set; }
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
        /// <summary>
        /// 申领科室
        /// </summary>
        public string depmName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int applyStorehouseId { get; set; }
        /// <summary>
        /// 申领库房
        /// </summary>
        public string applyStorehouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private long _storehouseId;

        public long storehouseId
        {
            get { return _storehouseId; }
            set
            {
                _storehouseId = value;
                OnPropertyChanged("storehouseId");
            }
        }
        private long _depmId;

        public long depmId
        {
            get { return _depmId; }
            set { _depmId = value;
                OnPropertyChanged("depmId");
            }
        }

        /// <summary>
        /// 入库名称
        /// </summary>
        public string storehouseName { get; set; }
        /// <summary>
        /// 申请人名称
        /// </summary>
        public string applyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string applyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long checkPersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string checkPersonName { get; set; }
        public long? checkTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ApplyDtlsItem> applyDtls { get; set; } = new List<ApplyDtlsItem>();
    }
    public class ApplyDtlsItem : CheckInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long applyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long sourceDtlId { get; set; }
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
        public long? goodsId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsNo { get; set; }
        /// <summary>
        /// 检查试纸
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsSpec { get; set; }
        /// <summary>
        /// 中医药制造厂
        /// </summary>
        public string goodsFactoryName { get; set; }
        /// <summary>
        /// 个
        /// </summary>
        public string goodsUnit { get; set; }
        public string batchNo { get; set; }
        public string lotNo { get; set; }
        private long PprodDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        public long pprodDate
        {
            get { return PprodDate; }
            set { PprodDate = value; }
        }
        private long PvalidDate;
        /// <summary>
        /// 有效日期至
        /// </summary>
        public long pvalidDate
        {
            get { return PvalidDate; }
            set { PvalidDate = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private int _applyQty;

        public int applyQty
        {
            get
            {
                _applyQty = Qty;
                return _applyQty;
            }
            set
            {
                Qty = value;
                _applyQty = value;
                OnPropertyChanged("applyQty");
            }
        }
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


        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        public List<string> RFIDs { get; set; } = new List<string>();
    }


}
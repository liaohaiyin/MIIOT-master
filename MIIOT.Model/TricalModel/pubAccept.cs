// File:    pubAccept.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pubAccept
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
namespace MIIOT.Model
{
    /// 验收单
    public class pubAccept : ModelBase
    {
        public long _id { get; set; }
        private long Id;
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty]
        public long id
        {
            get { Id = _id; return Id; }
            set
            {
                _id = value;
                Id = value;
            }
        }
        private long OrganId;
        /// <summary>
        /// 机构ID
        /// </summary>
        [JsonProperty]
        public long organId
        {
            get { return OrganId; }
            set { OrganId = value; }
        }
        private int SourceType;
        /// <summary>
        /// 0=送货单
        /// </summary>
        [JsonProperty]
        public int sourceType
        {
            get { return SourceType; }
            set { SourceType = value; }
        }
        private string SourceId;
        /// <summary>
        /// 来源单ID
        /// </summary>
        [JsonProperty]
        public string sourceId
        {
            get { return SourceId; }
            set { SourceId = value; }
        }
        private string SourceNo;
        /// <summary>
        /// 来源单号
        /// </summary>
        [JsonProperty]
        public string sourceNo
        {
            get { return SourceNo; }
            set { SourceNo = value; }
        }
        private string OriginId;
        /// <summary>
        /// 原始单ID
        /// </summary>
        [JsonProperty]
        public string originId
        {
            get { return OriginId; }
            set { OriginId = value; }
        }
        private long SupplyId;
        /// <summary>
        /// 供应商ID
        /// </summary>
        [JsonProperty]
        public long supplyId
        {
            get { return SupplyId; }
            set { SupplyId = value; }
        }
        private string SupplyName;
        /// <summary>
        /// 供应商名称
        /// </summary>
        [JsonProperty]
        public string supplyName
        {
            get { return SupplyName; }
            set { SupplyName = value; }
        }
        private long SupplyTime;
        /// <summary>
        /// 送货时间
        /// </summary>
        [JsonProperty]
        public long supplyTime
        {
            get { return SupplyTime; }
            set { SupplyTime = value; }
        }
        private int StorehouseId;
        /// <summary>
        /// 收货库房
        /// </summary>
        [JsonProperty]
        public int storehouseId
        {
            get { return StorehouseId; }
            set
            {
                StorehouseId = value;
                OnPropertyChanged("storehouseId");
            }
        }
        private string StorehouseName;
        [JsonProperty]
        public string storehouseName
        {
            get { return StorehouseName; }
            set
            {
                StorehouseName = value;
                OnPropertyChanged("storehouseName");
            }
        }
        private long AcceptPersonId;
        /// <summary>
        /// 验收人ID
        /// </summary>
        [JsonProperty]
        public long acceptPersonId
        {
            get { return AcceptPersonId; }
            set { AcceptPersonId = value; }
        }
        private string AcceptPersonName;
        /// <summary>
        /// 验收人
        /// </summary>
        [JsonProperty]
        public string acceptPersonName
        {
            get { return AcceptPersonName; }
            set { AcceptPersonName = value; }
        }
        private long CheckPersonId;
        /// <summary>
        /// 复核人ID
        /// </summary>
        [JsonProperty]
        public long checkPersonId
        {
            get { return CheckPersonId; }
            set { CheckPersonId = value; }
        }
        private string CheckPersonName;
        /// <summary>
        /// 复核人
        /// </summary>
        [JsonProperty]
        public string checkPersonName
        {
            get { return CheckPersonName; }
            set { CheckPersonName = value; }
        }
        private int Status;
        /// <summary>
        /// 0=初始 1=验收 2=验收确认 3=复核 99=拒收
        /// </summary>
        [JsonProperty]
        public int status
        {
            get { return Status; }
            set
            {
                Status = value;
                OnPropertyChanged("status");
            }
        }
        private int IsDelete;
        /// <summary>
        /// 0=正常 1=删除
        /// </summary>
        [JsonProperty]
        public int isDelete
        {
            get { return IsDelete; }
            set { IsDelete = value; }
        }
        private long CreaterId;
        /// <summary>
        /// 创建人id
        /// </summary>
        [JsonProperty]
        public long createrId
        {
            get { return CreaterId; }
            set { CreaterId = value; }
        }
        private string GmtCreate;
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public string gmtCreate
        {
            get { return GmtCreate; }
            set { GmtCreate = value; }
        }
        private long ModifierId;
        /// <summary>
        /// 修改人id
        /// </summary>
        [JsonProperty]
        public long modifierId
        {
            get { return ModifierId; }
            set { ModifierId = value; }
        }
        private string GmtModified;
        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonProperty]
        public string gmtModified
        {
            get { return GmtModified; }
            set { GmtModified = value; }
        }
        public string Hospital { get; set; }
        private ObservableCollection<pubAcceptDtl> AcceptDtlList = new ObservableCollection<pubAcceptDtl>();
        [JsonProperty]
        public ObservableCollection<pubAcceptDtl> acceptDtlList
        {
            get { return AcceptDtlList; }
            set { AcceptDtlList = value;
                OnPropertyChanged("acceptDtlList");
            }
        }

    }
    /// 验收明细

    public class pubAcceptDtl : CheckInfo
    {
        public long _id { get; set; }
        private long Id;
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty]
        public long id
        {
            get {
                Id = _id;
                return Id; }
            set {
                _id = value;
                Id = value; }
        }
        private long OrganId;
        /// <summary>
        /// 机构ID
        /// </summary>
        [JsonProperty]
        public long organId
        {
            get { return OrganId; }
            set { OrganId = value; }
        }
        private long AcceptId;
        /// <summary>
        /// 验收表ID
        /// </summary>
        [JsonProperty]
        public long acceptId
        {
            get { return AcceptId; }
            set { AcceptId = value; }
        }

        private int SourceType;
        /// <summary>
        /// 0=送货单
        /// </summary>
        [JsonProperty]
        public int sourceType
        {
            get { return SourceType; }
            set { SourceType = value; }
        }
        private string SourceId;
        /// <summary>
        /// 来源单ID
        /// </summary>
        [JsonProperty]
        public string sourceId
        {
            get { return SourceId; }
            set { SourceId = value; }
        }
        private long SourceDtlId;
        /// <summary>
        /// 来源明细ID
        /// </summary>
        [JsonProperty]
        public long sourceDtlId
        {
            get { return SourceDtlId; }
            set { SourceDtlId = value; }
        }
        private string SourceNo;
        /// <summary>
        /// 来源单号
        /// </summary>
        [JsonProperty]
        public string sourceNo
        {
            get { return SourceNo; }
            set { SourceNo = value; }
        }
        private string OriginId;
        /// <summary>
        /// 原始单ID
        /// </summary>
        [JsonProperty]
        public string originId
        {
            get { return OriginId; }
            set { OriginId = value; }
        }
        private string OriginDtlId;
        /// <summary>
        /// 原始明细ID
        /// </summary>
        [JsonProperty]
        public string originDtlId
        {
            get { return OriginDtlId; }
            set { OriginDtlId = value; }
        }
        private long GoodsId;
        /// <summary>
        /// 商品ID
        /// </summary>
        [JsonProperty]
        public long goodsId
        {
            get { return GoodsId; }
            set { GoodsId = value; }
        }
        private string GoodsNo;
        /// <summary>
        /// 商品编码
        /// </summary>
        [JsonProperty]
        public string goodsNo
        {
            get { return GoodsNo; }
            set { GoodsNo = value; }
        }
        private string GoodsName;
        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty]
        public string goodsName
        {
            get { return GoodsName; }
            set { GoodsName = value; }
        }
        private string GoodsSpec;
        /// <summary>
        /// 商品规格
        /// </summary>
        [JsonProperty]
        public string goodsSpec
        {
            get { return GoodsSpec; }
            set { GoodsSpec = value; }
        }
        private string GoodsFactoryName;
        /// <summary>
        /// 生产厂家
        /// </summary>
        [JsonProperty]
        public string goodsFactoryName
        {
            get { return GoodsFactoryName; }
            set { GoodsFactoryName = value; }
        }
        private string GoodsUnit;
        /// <summary>
        /// 单位
        /// </summary>
        [JsonProperty]
        public string goodsUnit
        {
            get { return GoodsUnit; }
            set { GoodsUnit = value; }
        }
        private int DeliveryQty;
        /// <summary>
        /// 送货数量
        /// </summary>
        [JsonProperty]
        public int deliveryQty
        {
            get
            {
                DeliveryQty = Qty;
                return DeliveryQty;
            }
            set
            {
                Qty = value;
                DeliveryQty = value;
            }
        }
        private int _CheckQty;
        /// <summary>
        /// 复核数量
        /// </summary>
        [JsonProperty]
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
        private long LotId;
        /// <summary>
        /// 批号ID
        /// </summary>
        [JsonProperty]
        public long lotId
        {
            get { return LotId; }
            set { LotId = value; }
        }
        private string LotNo;
        /// <summary>
        /// 批号
        /// </summary>
        [JsonProperty]
        public string lotNo
        {
            get { return LotNo; }
            set { LotNo = value; }
        }
        private long BatchId;
        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty]
        public long batchId
        {
            get { return BatchId; }
            set { BatchId = value; }
        }
        private long BatchNo;
        /// <summary>
        /// 批次
        /// </summary>
        [JsonProperty]
        public long batchNo
        {
            get { return BatchNo; }
            set { BatchNo = value; }
        }
        private double Price;
        /// <summary>
        /// 单价
        /// </summary>
        [JsonProperty]
        public double price
        {
            get { return Price; }
            set { Price = value; }
        }
        private long? PprodDate;
        /// <summary>
        /// 生产日期
        /// </summary>
        [JsonProperty]
        public long? pprodDate
        {
            get { return PprodDate; }
            set { PprodDate = value; }
        }
        private long? PvalidDate;
        /// <summary>
        /// 有效日期至
        /// </summary>
        [JsonProperty]
        public long? pvalidDate
        {
            get { return PvalidDate; }
            set { PvalidDate = value; }
        }
        private int Status;
        /// <summary>
        /// 0=初始 1=已生成验收 99=作废
        /// </summary>
        [JsonProperty]
        public int status
        {
            get { return Status; }
            set { Status = value; }
        }
        private int IsDelete;
        /// <summary>
        /// 0=正常 1=删除
        /// </summary>
        [JsonProperty]
        public int isDelete
        {
            get { return IsDelete; }
            set { IsDelete = value; }
        }
        private long CreaterId;
        /// <summary>
        /// 创建人id
        /// </summary>
        [JsonProperty]
        public long createrId
        {
            get { return CreaterId; }
            set { CreaterId = value; }
        }
        private string GmtCreate;
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty]
        public string gmtCreate
        {
            get { return GmtCreate; }
            set { GmtCreate = value; }
        }
        private long ModifierId;
        /// <summary>
        /// 修改人id
        /// </summary>
        [JsonProperty]
        public long modifierId
        {
            get { return ModifierId; }
            set { ModifierId = value; }
        }
        private string GmtModified;
        /// <summary>
        /// 修改时间
        /// </summary>
        [JsonProperty]
        public string gmtModified
        {
            get { return GmtModified; }
            set { GmtModified = value; }
        }
        private ObservableCollection<RFIDInfo> _RFID = new ObservableCollection<RFIDInfo>();
        public ObservableCollection<RFIDInfo> RFID
        {
            get { return _RFID; }
            set { _RFID = value;
                OnPropertyChanged("RFID");
            }
        }
        public string rfidCode { get; set; }
    }

    public class RFIDInfo
    {
        public string rfid { get; set; }
        public int status { get; set; }
    }
}
// File:    pub_delivery_note_dtl.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_delivery_note_dtl

using System;

using Newtonsoft.Json;
/// 送货明细
[JsonObject(MemberSerialization.OptIn)]
public class pub_delivery_note_dtl
{
   private long _id;
   /// <summary>
   /// id
   /// </summary>
   [JsonProperty]
   public long id{
      get { return _id; }
      set { _id = value; }
   }
   private long _organ_id;
   /// <summary>
   /// 机构ID
   /// </summary>
   [JsonProperty]
   public long organ_id{
      get { return _organ_id; }
      set { _organ_id = value; }
   }
   private long _delivery_id;
   /// <summary>
   /// 收货表ID
   /// </summary>
   [JsonProperty]
   public long delivery_id{
      get { return _delivery_id; }
      set { _delivery_id = value; }
   }
   private byte _source_type;
   /// <summary>
   /// 0=中台
   /// </summary>
   [JsonProperty]
   public byte source_type{
      get { return _source_type; }
      set { _source_type = value; }
   }
   private long _source_id;
   /// <summary>
   /// 来源单ID
   /// </summary>
   [JsonProperty]
   public long source_id{
      get { return _source_id; }
      set { _source_id = value; }
   }
   private long _source_dtl_id;
   /// <summary>
   /// 来源明细ID
   /// </summary>
   [JsonProperty]
   public long source_dtl_id{
      get { return _source_dtl_id; }
      set { _source_dtl_id = value; }
   }
   private string _source_no;
   /// <summary>
   /// 来源单号
   /// </summary>
   [JsonProperty]
   public string source_no{
      get { return _source_no; }
      set { _source_no = value; }
   }
   private string _origin_id;
   /// <summary>
   /// 原始单ID
   /// </summary>
   [JsonProperty]
   public string origin_id{
      get { return _origin_id; }
      set { _origin_id = value; }
   }
   private string _origin_dtl_id;
   /// <summary>
   /// 原始明细ID
   /// </summary>
   [JsonProperty]
   public string origin_dtl_id{
      get { return _origin_dtl_id; }
      set { _origin_dtl_id = value; }
   }
   private long _goods_id;
   /// <summary>
   /// 商品ID
   /// </summary>
   [JsonProperty]
   public long goods_id{
      get { return _goods_id; }
      set { _goods_id = value; }
   }
   private int _delivery_qty;
   /// <summary>
   /// 送货数量
   /// </summary>
   [JsonProperty]
   public int delivery_qty{
      get { return _delivery_qty; }
      set { _delivery_qty = value; }
   }
   private long _lot_id;
   /// <summary>
   /// 批号ID
   /// </summary>
   [JsonProperty]
   public long lot_id{
      get { return _lot_id; }
      set { _lot_id = value; }
   }
   private string _lot_no;
   /// <summary>
   /// 批号
   /// </summary>
   [JsonProperty]
   public string lot_no{
      get { return _lot_no; }
      set { _lot_no = value; }
   }
   private long _batch_id;
   /// <summary>
   /// 批次
   /// </summary>
   [JsonProperty]
   public long batch_id{
      get { return _batch_id; }
      set { _batch_id = value; }
   }
   private double _price;
   /// <summary>
   /// 单价
   /// </summary>
   [JsonProperty]
   public double price{
      get { return _price; }
      set { _price = value; }
   }
   private DateTime _pprod_date;
   /// <summary>
   /// 生产日期
   /// </summary>
   [JsonProperty]
   public DateTime pprod_date{
      get { return _pprod_date; }
      set { _pprod_date = value; }
   }
   private DateTime _pvalid_date;
   /// <summary>
   /// 有效日期至
   /// </summary>
   [JsonProperty]
   public DateTime pvalid_date{
      get { return _pvalid_date; }
      set { _pvalid_date = value; }
   }
   private byte _status;
   /// <summary>
   /// 0=初始 1=已生成验收 99=作废
   /// </summary>
   [JsonProperty]
   public byte status{
      get { return _status; }
      set { _status = value; }
   }
   private byte _is_delete;
   /// <summary>
   /// 0=正常 1=删除
   /// </summary>
   [JsonProperty]
   public byte is_delete{
      get { return _is_delete; }
      set { _is_delete = value; }
   }
   private long _creater_id;
   /// <summary>
   /// 创建人id
   /// </summary>
   [JsonProperty]
   public long creater_id{
      get { return _creater_id; }
      set { _creater_id = value; }
   }
   private DateTime _gmt_create;
   /// <summary>
   /// 创建时间
   /// </summary>
   [JsonProperty]
   public DateTime gmt_create{
      get { return _gmt_create; }
      set { _gmt_create = value; }
   }
   private long _modifier_id;
   /// <summary>
   /// 修改人id
   /// </summary>
   [JsonProperty]
   public long modifier_id{
      get { return _modifier_id; }
      set { _modifier_id = value; }
   }
   private DateTime _gmt_modified;
   /// <summary>
   /// 修改时间
   /// </summary>
   [JsonProperty]
   public DateTime gmt_modified{
      get { return _gmt_modified; }
      set { _gmt_modified = value; }
   }

}
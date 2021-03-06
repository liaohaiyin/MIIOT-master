// File:    pub_store_in_dtl.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_store_in_dtl

using System;

using Newtonsoft.Json;
/// 入库单明细
[JsonObject(MemberSerialization.OptIn)]
public class pub_store_in_dtl
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
   private long _store_in_id;
   /// <summary>
   /// 入库单ID
   /// </summary>
   [JsonProperty]
   public long store_in_id{
      get { return _store_in_id; }
      set { _store_in_id = value; }
   }
   private byte _source_type;
   /// <summary>
   /// 0=验收单 1=退库单
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
   private string _goods_no;
   /// <summary>
   /// 商品编码
   /// </summary>
   [JsonProperty]
   public string goods_no{
      get { return _goods_no; }
      set { _goods_no = value; }
   }
   private int _qty;
   /// <summary>
   /// 数量
   /// </summary>
   [JsonProperty]
   public int qty{
      get { return _qty; }
      set { _qty = value; }
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
   private string _single_code;
   /// <summary>
   /// 单品码
   /// </summary>
   [JsonProperty]
   public string single_code{
      get { return _single_code; }
      set { _single_code = value; }
   }

}
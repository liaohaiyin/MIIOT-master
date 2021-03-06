// File:    pub_apply_dtl.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_apply_dtl

using System;

using Newtonsoft.Json;
/// 申领明细
[JsonObject(MemberSerialization.OptIn)]
public class pub_apply_dtl
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
   private long _apply_id;
   /// <summary>
   /// 申领表ID
   /// </summary>
   [JsonProperty]
   public long apply_id{
      get { return _apply_id; }
      set { _apply_id = value; }
   }
   private byte _source_type;
   /// <summary>
   /// 0=正常申领 1=消耗申领
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
   /// 原始明细单ID
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
   private string _goods_name;
   /// <summary>
   /// 商品名称
   /// </summary>
   [JsonProperty]
   public string goods_name{
      get { return _goods_name; }
      set { _goods_name = value; }
   }
   private string _goods_spec;
   /// <summary>
   /// 商品规格
   /// </summary>
   [JsonProperty]
   public string goods_spec{
      get { return _goods_spec; }
      set { _goods_spec = value; }
   }
   private string _goods_factory_name;
   /// <summary>
   /// 生产厂家
   /// </summary>
   [JsonProperty]
   public string goods_factory_name{
      get { return _goods_factory_name; }
      set { _goods_factory_name = value; }
   }
   private string _goods_unit;
   /// <summary>
   /// 单位
   /// </summary>
   [JsonProperty]
   public string goods_unit{
      get { return _goods_unit; }
      set { _goods_unit = value; }
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
   private int _apply_qty;
   /// <summary>
   /// 申领数量
   /// </summary>
   [JsonProperty]
   public int apply_qty{
      get { return _apply_qty; }
      set { _apply_qty = value; }
   }
   private int _check_qty;
   /// <summary>
   /// 复核数量
   /// </summary>
   [JsonProperty]
   public int check_qty{
      get { return _check_qty; }
      set { _check_qty = value; }
   }
   private byte _status;
   /// <summary>
   /// 0=初始 1=拣选 2=复核完成 99=作废
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
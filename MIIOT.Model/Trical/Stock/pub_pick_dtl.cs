// File:    pub_pick_dtl.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_pick_dtl

using System;

using Newtonsoft.Json;
/// 拣选明细
[JsonObject(MemberSerialization.OptIn)]
public class pub_pick_dtl
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
   private long _pick_id;
   /// <summary>
   /// 拣选单ID
   /// </summary>
   [JsonProperty]
   public long pick_id{
      get { return _pick_id; }
      set { _pick_id = value; }
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
   private byte _source_type;
   /// <summary>
   /// 0=申领 1=退货
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
   private string _source_no;
   /// <summary>
   /// 来源单号
   /// </summary>
   [JsonProperty]
   public string source_no{
      get { return _source_no; }
      set { _source_no = value; }
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
   /// 原始明细ID
   /// </summary>
   [JsonProperty]
   public string origin_dtl_id{
      get { return _origin_dtl_id; }
      set { _origin_dtl_id = value; }
   }
   private long _storehouse_id;
   /// <summary>
   /// 库房
   /// </summary>
   [JsonProperty]
   public long storehouse_id{
      get { return _storehouse_id; }
      set { _storehouse_id = value; }
   }
   private long _loc_id;
   /// <summary>
   /// 货位ID
   /// </summary>
   [JsonProperty]
   public long loc_id{
      get { return _loc_id; }
      set { _loc_id = value; }
   }
   private long _zone_id;
   /// <summary>
   /// 区域ID
   /// </summary>
   [JsonProperty]
   public long zone_id{
      get { return _zone_id; }
      set { _zone_id = value; }
   }
   private string _loc_no;
   /// <summary>
   /// 货位编码
   /// </summary>
   [JsonProperty]
   public string loc_no{
      get { return _loc_no; }
      set { _loc_no = value; }
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
   private int _pick_qty;
   /// <summary>
   /// 拣选数量
   /// </summary>
   [JsonProperty]
   public int pick_qty{
      get { return _pick_qty; }
      set { _pick_qty = value; }
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
   /// 0=初始 1=已拣选 99=作废
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
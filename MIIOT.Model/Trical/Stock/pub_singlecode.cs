// File:    pub_singlecode.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_singlecode

using System;

using Newtonsoft.Json;
/// 单品码定义表
[JsonObject(MemberSerialization.OptIn)]
public class pub_singlecode
{
   private string _singlecode;
   /// <summary>
   /// 单品码
   /// </summary>
   [JsonProperty]
   public string singlecode{
      get { return _singlecode; }
      set { _singlecode = value; }
   }
   private string _rfid;
   /// <summary>
   /// RFID
   /// </summary>
   [JsonProperty]
   public string rfid{
      get { return _rfid; }
      set { _rfid = value; }
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
   private long _lot_id;
   /// <summary>
   /// 批次ID
   /// </summary>
   [JsonProperty]
   public long lot_id{
      get { return _lot_id; }
      set { _lot_id = value; }
   }
   private long _batch_id;
   /// <summary>
   /// 批号ID
   /// </summary>
   [JsonProperty]
   public long batch_id{
      get { return _batch_id; }
      set { _batch_id = value; }
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
   private long _supply_id;
   /// <summary>
   /// 供应商ID
   /// </summary>
   [JsonProperty]
   public long supply_id{
      get { return _supply_id; }
      set { _supply_id = value; }
   }
   private byte _source_type;
   /// <summary>
   /// 来源类型
   /// </summary>
   [JsonProperty]
   public byte source_type{
      get { return _source_type; }
      set { _source_type = value; }
   }
   private byte _source_id;
   /// <summary>
   /// 来源ID
   /// </summary>
   [JsonProperty]
   public byte source_id{
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
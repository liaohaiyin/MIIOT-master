// File:    pub_store_out.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_store_out

using System;

using Newtonsoft.Json;
/// 出库单
[JsonObject(MemberSerialization.OptIn)]
public class pub_store_out
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
   private string _origin_id;
   /// <summary>
   /// 原始单ID
   /// </summary>
   [JsonProperty]
   public string origin_id{
      get { return _origin_id; }
      set { _origin_id = value; }
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
   private byte _result_status;
   /// <summary>
   /// 0=初始 1=已返回中台
   /// </summary>
   [JsonProperty]
   public byte result_status{
      get { return _result_status; }
      set { _result_status = value; }
   }

}
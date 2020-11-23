// File:    pub_storehosue.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_storehosue

using System;

using Newtonsoft.Json;
/// 库房表
[JsonObject(MemberSerialization.OptIn)]
public class pub_storehouse
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
   /// 机构id
   /// </summary>
   [JsonProperty]
   public long organ_id{
      get { return _organ_id; }
      set { _organ_id = value; }
   }
   private string _storehouse_source_id;
   /// <summary>
   /// 原库房id
   /// </summary>
   [JsonProperty]
   public string storehouse_source_id{
      get { return _storehouse_source_id; }
      set { _storehouse_source_id = value; }
   }
   private string _storehouse_code;
   /// <summary>
   /// 库房编码
   /// </summary>
   [JsonProperty]
   public string storehouse_code{
      get { return _storehouse_code; }
      set { _storehouse_code = value; }
   }
   private string _storehouse_name;
   /// <summary>
   /// 库房名称
   /// </summary>
   [JsonProperty]
   public string storehouse_name{
      get { return _storehouse_name; }
      set { _storehouse_name = value; }
   }
   private string _storehouse_addr;
   /// <summary>
   /// 库房地址
   /// </summary>
   [JsonProperty]
   public string storehouse_addr{
      get { return _storehouse_addr; }
      set { _storehouse_addr = value; }
   }
   private byte _storehouse_status;
   /// <summary>
   /// 库房状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte storehouse_status{
      get { return _storehouse_status; }
      set { _storehouse_status = value; }
   }
   private string _storehouse_memo;
   /// <summary>
   /// 库房备注
   /// </summary>
   [JsonProperty]
   public string storehouse_memo{
      get { return _storehouse_memo; }
      set { _storehouse_memo = value; }
   }
   private byte _is_delete;
   /// <summary>
   /// 是否删除(0:否,1:是)
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
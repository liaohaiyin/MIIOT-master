// File:    pub_organ.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_organ

using System;

using Newtonsoft.Json;
/// 机构表
[JsonObject(MemberSerialization.OptIn)]
public class pub_organ
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
   private string _organ_code;
   /// <summary>
   /// 机构编码
   /// </summary>
   [JsonProperty]
   public string organ_code{
      get { return _organ_code; }
      set { _organ_code = value; }
   }
   private string _organ_name;
   /// <summary>
   /// 机构名称
   /// </summary>
   [JsonProperty]
   public string organ_name{
      get { return _organ_name; }
      set { _organ_name = value; }
   }
   private string _organ_addr;
   /// <summary>
   /// 机构地址
   /// </summary>
   [JsonProperty]
   public string organ_addr{
      get { return _organ_addr; }
      set { _organ_addr = value; }
   }
   private byte _organ_status;
   /// <summary>
   /// 机构状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte organ_status{
      get { return _organ_status; }
      set { _organ_status = value; }
   }
   private long _organ_parent_id;
   /// <summary>
   /// 父机构id(0为一级机构)
   /// </summary>
   [JsonProperty]
   public long organ_parent_id{
      get { return _organ_parent_id; }
      set { _organ_parent_id = value; }
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
   private string _creater_name;
   /// <summary>
   /// 创建人名称
   /// </summary>
   [JsonProperty]
   public string creater_name{
      get { return _creater_name; }
      set { _creater_name = value; }
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
   private string _modifier_name;
   /// <summary>
   /// 修改人名称
   /// </summary>
   [JsonProperty]
   public string modifier_name{
      get { return _modifier_name; }
      set { _modifier_name = value; }
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
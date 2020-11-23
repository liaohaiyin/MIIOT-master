// File:    sys_role.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class sys_role

using System;

using Newtonsoft.Json;
/// 角色表
[JsonObject(MemberSerialization.OptIn)]
public class sys_role
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
   private string _role_code;
   /// <summary>
   /// 角色编码
   /// </summary>
   [JsonProperty]
   public string role_code{
      get { return _role_code; }
      set { _role_code = value; }
   }
   private string _role_name;
   /// <summary>
   /// 角色名称
   /// </summary>
   [JsonProperty]
   public string role_name{
      get { return _role_name; }
      set { _role_name = value; }
   }
   private string _role_desc;
   /// <summary>
   /// 角色描述
   /// </summary>
   [JsonProperty]
   public string role_desc{
      get { return _role_desc; }
      set { _role_desc = value; }
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
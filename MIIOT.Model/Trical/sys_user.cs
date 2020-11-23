// File:    sys_user.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class sys_user

using System;

using Newtonsoft.Json;
/// 用户表
[JsonObject(MemberSerialization.OptIn)]
public class sys_user
{
   private long _id;
   /// <summary>
   /// 主键id
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
   private string _user_source_id;
   /// <summary>
   /// 原用户id
   /// </summary>
   [JsonProperty]
   public string user_source_id{
      get { return _user_source_id; }
      set { _user_source_id = value; }
   }
   private string _user_code;
   /// <summary>
   /// 用户编码
   /// </summary>
   [JsonProperty]
   public string user_code{
      get { return _user_code; }
      set { _user_code = value; }
   }
   private string _user_name;
   /// <summary>
   /// 用户名称
   /// </summary>
   [JsonProperty]
   public string user_name{
      get { return _user_name; }
      set { _user_name = value; }
   }
   private string _password;
   /// <summary>
   /// 密码
   /// </summary>
   [JsonProperty]
   public string password{
      get { return _password; }
      set { _password = value; }
   }
   private byte _user_status;
   /// <summary>
   /// 用户状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte user_status{
      get { return _user_status; }
      set { _user_status = value; }
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
    private string _display_name;
    [JsonProperty]
    public string display_name
    {
        get { return _display_name; }
        set { _display_name = value; }
    }
    private string _user_department_name;

    [JsonProperty]
    public string user_department_name
    {
        get { return _user_department_name; }
        set { _user_department_name = value; }
    }

}
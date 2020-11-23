// File:    sys_user_binding.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class sys_user_binding

using System;

using Newtonsoft.Json;
/// 用户绑定表
[JsonObject(MemberSerialization.OptIn)]
public class sys_user_binding
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
   private long _binding_user_id;
   /// <summary>
   /// 绑定用户id
   /// </summary>
   [JsonProperty]
   public long binding_user_id{
      get { return _binding_user_id; }
      set { _binding_user_id = value; }
   }
   private string _binding_user_code;
   /// <summary>
   /// 绑定用户编码
   /// </summary>
   [JsonProperty]
   public string binding_user_code{
      get { return _binding_user_code; }
      set { _binding_user_code = value; }
   }
   private byte _binding_type;
   /// <summary>
   /// 绑定类型(0:磁卡,1:手指,2:人脸)
   /// </summary>
   [JsonProperty]
   public byte binding_type{
      get { return _binding_type; }
      set { _binding_type = value; }
   }
   private string _binding_machine_code;
   /// <summary>
   /// 绑定设备编码
   /// </summary>
   [JsonProperty]
   public string binding_machine_code{
      get { return _binding_machine_code; }
      set { _binding_machine_code = value; }
   }
   private string _binding_identity;
   /// <summary>
   /// 绑定身份特征
   /// </summary>
   [JsonProperty]
   public string binding_identity{
      get { return _binding_identity; }
      set { _binding_identity = value; }
   }
   private byte _binding_status;
   /// <summary>
   /// 绑定状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte binding_status{
      get { return _binding_status; }
      set { _binding_status = value; }
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
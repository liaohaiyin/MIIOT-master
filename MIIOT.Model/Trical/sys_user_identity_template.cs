// File:    sys_user_identity_template.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class sys_user_identity_template

using System;

using Newtonsoft.Json;
/// 用户身份特征模板
[JsonObject(MemberSerialization.OptIn)]
public class sys_user_identity_template
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
   private long _user_binding_id;
   /// <summary>
   /// 用户绑定id
   /// </summary>
   [JsonProperty]
   public long user_binding_id{
      get { return _user_binding_id; }
      set { _user_binding_id = value; }
   }
   private string _user_identity_template;
   /// <summary>
   /// 身份特征模板
   /// </summary>
   [JsonProperty]
   public string user_identity_template{
      get { return _user_identity_template; }
      set { _user_identity_template = value; }
   }

}
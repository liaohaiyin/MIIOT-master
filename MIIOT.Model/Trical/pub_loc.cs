// File:    pub_loc.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_loc

using System;

using Newtonsoft.Json;
/// 货位表
[JsonObject(MemberSerialization.OptIn)]
public class pub_loc
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
   private long _storehouse_id;
   /// <summary>
   /// 所属库房id
   /// </summary>
   [JsonProperty]
   public long storehouse_id{
      get { return _storehouse_id; }
      set { _storehouse_id = value; }
   }
   private long _zone_id;
   /// <summary>
   /// 所属区域id
   /// </summary>
   [JsonProperty]
   public long zone_id{
      get { return _zone_id; }
      set { _zone_id = value; }
   }
   private string _loc_code;
   /// <summary>
   /// 货位编码
   /// </summary>
   [JsonProperty]
   public string loc_code{
      get { return _loc_code; }
      set { _loc_code = value; }
   }
   private string _loc_addr;
   /// <summary>
   /// 货位位置
   /// </summary>
   [JsonProperty]
   public string loc_addr{
      get { return _loc_addr; }
      set { _loc_addr = value; }
   }
   private byte _loc_status;
   /// <summary>
   /// 货位状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte loc_status{
      get { return _loc_status; }
      set { _loc_status = value; }
   }
   private string _loc_memo;
   /// <summary>
   /// 货位备注
   /// </summary>
   [JsonProperty]
   public string loc_memo{
      get { return _loc_memo; }
      set { _loc_memo = value; }
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
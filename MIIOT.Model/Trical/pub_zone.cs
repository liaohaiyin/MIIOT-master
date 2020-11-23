// File:    pub_zone.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_zone

using System;

using Newtonsoft.Json;
/// 区域表
[JsonObject(MemberSerialization.OptIn)]
public class pub_zone
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
   private string _zone_code;
   /// <summary>
   /// 区域编码
   /// </summary>
   [JsonProperty]
   public string zone_code{
      get { return _zone_code; }
      set { _zone_code = value; }
   }
   private byte _zone_type;
   /// <summary>
   /// 区域类型(0:正常,1:待验,2:暂存,3:不合格,4:退货)
   /// </summary>
   [JsonProperty]
   public byte zone_type{
      get { return _zone_type; }
      set { _zone_type = value; }
   }
   private string _zone_addr;
   /// <summary>
   /// 区域位置
   /// </summary>
   [JsonProperty]
   public string zone_addr{
      get { return _zone_addr; }
      set { _zone_addr = value; }
   }
   private byte _zone_storage_nature;
   /// <summary>
   /// 区域存储属性(0:常温,1:阴凉,2:冷藏,3:冷冻)
   /// </summary>
   [JsonProperty]
   public byte zone_storage_nature{
      get { return _zone_storage_nature; }
      set { _zone_storage_nature = value; }
   }
   private byte _zone_status;
   /// <summary>
   /// 区域状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte zone_status{
      get { return _zone_status; }
      set { _zone_status = value; }
   }
   private string _zone_memo;
   /// <summary>
   /// 区域备注
   /// </summary>
   [JsonProperty]
   public string zone_memo{
      get { return _zone_memo; }
      set { _zone_memo = value; }
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
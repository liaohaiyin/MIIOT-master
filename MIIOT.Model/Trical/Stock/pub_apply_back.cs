// File:    pub_apply_back.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_apply_back

using System;

using Newtonsoft.Json;
/// 退库单
[JsonObject(MemberSerialization.OptIn)]
public class pub_apply_back
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
   /// 0=中台 1=手工
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
   private long _depm_id;
   /// <summary>
   /// 申退科室ID
   /// </summary>
   [JsonProperty]
   public long depm_id{
      get { return _depm_id; }
      set { _depm_id = value; }
   }
   private string _depm_name;
   /// <summary>
   /// 申退科室
   /// </summary>
   [JsonProperty]
   public string depm_name{
      get { return _depm_name; }
      set { _depm_name = value; }
   }
   private long _source_storehouse_id;
   /// <summary>
   /// 申退库房ID
   /// </summary>
   [JsonProperty]
   public long source_storehouse_id{
      get { return _source_storehouse_id; }
      set { _source_storehouse_id = value; }
   }
   private string _source_storehouse_name;
   /// <summary>
   /// 申退库房
   /// </summary>
   [JsonProperty]
   public string source_storehouse_name{
      get { return _source_storehouse_name; }
      set { _source_storehouse_name = value; }
   }
   private long _target_storehouse_id;
   /// <summary>
   /// 退回库房ID
   /// </summary>
   [JsonProperty]
   public long target_storehouse_id{
      get { return _target_storehouse_id; }
      set { _target_storehouse_id = value; }
   }
   private string _target_storehouse_name;
   /// <summary>
   /// 退回库房
   /// </summary>
   [JsonProperty]
   public string target_storehouse_name{
      get { return _target_storehouse_name; }
      set { _target_storehouse_name = value; }
   }
   private long _check_person_id;
   /// <summary>
   /// 复核人ID
   /// </summary>
   [JsonProperty]
   public long check_person_id{
      get { return _check_person_id; }
      set { _check_person_id = value; }
   }
   private string _check_person_name;
   /// <summary>
   /// 复核人
   /// </summary>
   [JsonProperty]
   public string check_person_name{
      get { return _check_person_name; }
      set { _check_person_name = value; }
   }
   private byte _status;
   /// <summary>
   /// 0=初始 1=复核 99=作废
   /// </summary>
   [JsonProperty]
   public byte status{
      get { return _status; }
      set { _status = value; }
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

}
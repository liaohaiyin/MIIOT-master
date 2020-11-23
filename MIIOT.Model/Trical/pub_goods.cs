// File:    pub_goods.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_goods

using System;

using Newtonsoft.Json;
/// 商品表
[JsonObject(MemberSerialization.OptIn)]
public class pub_goods
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
   private string _goods_source_id;
   /// <summary>
   /// 原商品id
   /// </summary>
   [JsonProperty]
   public string goods_source_id{
      get { return _goods_source_id; }
      set { _goods_source_id = value; }
   }
   private string _goods_code;
   /// <summary>
   /// 商品编码
   /// </summary>
   [JsonProperty]
   public string goods_code{
      get { return _goods_code; }
      set { _goods_code = value; }
   }
   private string _goods_name;
   /// <summary>
   /// 商品名称
   /// </summary>
   [JsonProperty]
   public string goods_name{
      get { return _goods_name; }
      set { _goods_name = value; }
   }
   private string _goods_common_name;
   /// <summary>
   /// 商品通用名称
   /// </summary>
   [JsonProperty]
   public string goods_common_name{
      get { return _goods_common_name; }
      set { _goods_common_name = value; }
   }
   private string _goods_bar_code;
   /// <summary>
   /// 商品条形码(DI码)
   /// </summary>
   [JsonProperty]
   public string goods_bar_code{
      get { return _goods_bar_code; }
      set { _goods_bar_code = value; }
   }
   private string _goods_spec;
   /// <summary>
   /// 商品规格
   /// </summary>
   [JsonProperty]
   public string goods_spec{
      get { return _goods_spec; }
      set { _goods_spec = value; }
   }
   private string _goods_unit;
   /// <summary>
   /// 商品单位
   /// </summary>
   [JsonProperty]
   public string goods_unit{
      get { return _goods_unit; }
      set { _goods_unit = value; }
   }
   private string _goods_factory_name;
   /// <summary>
   /// 商品厂家名称
   /// </summary>
   [JsonProperty]
   public string goods_factory_name{
      get { return _goods_factory_name; }
      set { _goods_factory_name = value; }
   }
   private string _goods_prodarea;
   /// <summary>
   /// 商品产地
   /// </summary>
   [JsonProperty]
   public string goods_prodarea{
      get { return _goods_prodarea; }
      set { _goods_prodarea = value; }
   }
   private decimal _goods_pack_size;
   /// <summary>
   /// 商品包装大小
   /// </summary>
   [JsonProperty]
   public decimal goods_pack_size{
      get { return _goods_pack_size; }
      set { _goods_pack_size = value; }
   }
   private decimal _goods_forecast_days;
   /// <summary>
   /// 商品效期预警天数
   /// </summary>
   [JsonProperty]
   public decimal goods_forecast_days{
      get { return _goods_forecast_days; }
      set { _goods_forecast_days = value; }
   }
   private byte _goods_type;
   /// <summary>
   /// 商品类型(0:耗材,1:试剂,2:药品)
   /// </summary>
   [JsonProperty]
   public byte goods_type{
      get { return _goods_type; }
      set { _goods_type = value; }
   }
   private byte _goods_status;
   /// <summary>
   /// 商品状态(0:禁用,1:启用)
   /// </summary>
   [JsonProperty]
   public byte goods_status{
      get { return _goods_status; }
      set { _goods_status = value; }
   }
   private string _goods_memo;
   /// <summary>
   /// 商品备注
   /// </summary>
   [JsonProperty]
   public string goods_memo{
      get { return _goods_memo; }
      set { _goods_memo = value; }
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
    public int stock_qty { get; set; }
    public int Index { get; set; }

}
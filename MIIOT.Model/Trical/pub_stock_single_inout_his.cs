// File:    pub_stock_single_inout_his.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_stock_single_inout_his

using System;

using Newtonsoft.Json;
/// 单品码出入库历史表
[JsonObject(MemberSerialization.OptIn)]
public class pub_stock_single_inout_his
{
   private long _single_inout_his_id;
   /// <summary>
   /// 单品码出入库历史id
   /// </summary>
   [JsonProperty]
   public long single_inout_his_id{
      get { return _single_inout_his_id; }
      set { _single_inout_his_id = value; }
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
   private string _organ_name;
   /// <summary>
   /// 机构名称
   /// </summary>
   [JsonProperty]
   public string organ_name{
      get { return _organ_name; }
      set { _organ_name = value; }
   }
   private long _source_inout_id;
   /// <summary>
   /// 来源出入库id(库存出入库记录id)
   /// </summary>
   [JsonProperty]
   public long source_inout_id{
      get { return _source_inout_id; }
      set { _source_inout_id = value; }
   }
   private long _source_id;
   /// <summary>
   /// 来源id(上级id)
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
   private long _source_detail_id;
   /// <summary>
   /// 来源明细id
   /// </summary>
   [JsonProperty]
   public long source_detail_id{
      get { return _source_detail_id; }
      set { _source_detail_id = value; }
   }
   private long _origin_id;
   /// <summary>
   /// 原始id(ERP或MCP的供货单、或验收单)
   /// </summary>
   [JsonProperty]
   public long origin_id{
      get { return _origin_id; }
      set { _origin_id = value; }
   }
   private string _origin_no;
   /// <summary>
   /// 原始单号
   /// </summary>
   [JsonProperty]
   public string origin_no{
      get { return _origin_no; }
      set { _origin_no = value; }
   }
   private int _task_type;
   /// <summary>
   /// 作业类型(0:验收,1:申领,2:退库,3:退货)
   /// </summary>
   [JsonProperty]
   public int task_type{
      get { return _task_type; }
      set { _task_type = value; }
   }
   private int _inout_type;
   /// <summary>
   /// 出入库类型(0:入库,1:出库)
   /// </summary>
   [JsonProperty]
   public int inout_type{
      get { return _inout_type; }
      set { _inout_type = value; }
   }
   private DateTime _inout_date;
   /// <summary>
   /// 出入库日期
   /// </summary>
   [JsonProperty]
   public DateTime inout_date{
      get { return _inout_date; }
      set { _inout_date = value; }
   }
   private long _storehouse_id;
   /// <summary>
   /// 库房id
   /// </summary>
   [JsonProperty]
   public long storehouse_id{
      get { return _storehouse_id; }
      set { _storehouse_id = value; }
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
   private long _loc_id;
   /// <summary>
   /// 货位id
   /// </summary>
   [JsonProperty]
   public long loc_id{
      get { return _loc_id; }
      set { _loc_id = value; }
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
   private long _batch_id;
   /// <summary>
   /// 批次id
   /// </summary>
   [JsonProperty]
   public long batch_id{
      get { return _batch_id; }
      set { _batch_id = value; }
   }
   private string _batch_code;
   /// <summary>
   /// 批次编码
   /// </summary>
   [JsonProperty]
   public string batch_code{
      get { return _batch_code; }
      set { _batch_code = value; }
   }
   private long _goods_id;
   /// <summary>
   /// 商品id
   /// </summary>
   [JsonProperty]
   public long goods_id{
      get { return _goods_id; }
      set { _goods_id = value; }
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
   private string _goods_spec;
   /// <summary>
   /// 商品规格
   /// </summary>
   [JsonProperty]
   public string goods_spec{
      get { return _goods_spec; }
      set { _goods_spec = value; }
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
   private string _single_code;
   /// <summary>
   /// 单品码
   /// </summary>
   [JsonProperty]
   public string single_code{
      get { return _single_code; }
      set { _single_code = value; }
   }
   private string _factory_code;
   /// <summary>
   /// 厂家码
   /// </summary>
   [JsonProperty]
   public string factory_code{
      get { return _factory_code; }
      set { _factory_code = value; }
   }
   private long _plot_id;
   /// <summary>
   /// 批号id
   /// </summary>
   [JsonProperty]
   public long plot_id{
      get { return _plot_id; }
      set { _plot_id = value; }
   }
   private string _plot_no;
   /// <summary>
   /// 批号
   /// </summary>
   [JsonProperty]
   public string plot_no{
      get { return _plot_no; }
      set { _plot_no = value; }
   }
   private long _confirm_id;
   /// <summary>
   /// 确认人id
   /// </summary>
   [JsonProperty]
   public long confirm_id{
      get { return _confirm_id; }
      set { _confirm_id = value; }
   }
   private string _confirm_name;
   /// <summary>
   /// 确认人姓名
   /// </summary>
   [JsonProperty]
   public string confirm_name{
      get { return _confirm_name; }
      set { _confirm_name = value; }
   }
   private DateTime _confirm_time;
   /// <summary>
   /// 确认时间
   /// </summary>
   [JsonProperty]
   public DateTime confirm_time{
      get { return _confirm_time; }
      set { _confirm_time = value; }
   }

}
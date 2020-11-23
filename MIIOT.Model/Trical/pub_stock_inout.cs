// File:    pub_stock_inout.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_stock_inout

using System;

using Newtonsoft.Json;
/// 库存出入库记录表
[JsonObject(MemberSerialization.OptIn)]
public class pub_stock_inout
{
   private long _inout_id;
   /// <summary>
   /// 出入库id
   /// </summary>
   [JsonProperty]
   public long inout_id{
      get { return _inout_id; }
      set { _inout_id = value; }
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
   private long _batch_id;
   /// <summary>
   /// 批次id
   /// </summary>
   [JsonProperty]
   public long batch_id{
      get { return _batch_id; }
      set { _batch_id = value; }
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
   private long _zone_id;
   /// <summary>
   /// 区域id
   /// </summary>
   [JsonProperty]
   public long zone_id{
      get { return _zone_id; }
      set { _zone_id = value; }
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
   /// 交易类型(0:入库,1:出库)
   /// </summary>
   [JsonProperty]
   public int inout_type{
      get { return _inout_type; }
      set { _inout_type = value; }
   }
   private DateTime _inout_date;
   /// <summary>
   /// 交易日期
   /// </summary>
   [JsonProperty]
   public DateTime inout_date{
      get { return _inout_date; }
      set { _inout_date = value; }
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
   private long _plot_id;
   /// <summary>
   /// 批号id
   /// </summary>
   [JsonProperty]
   public long plot_id{
      get { return _plot_id; }
      set { _plot_id = value; }
   }
   private int _quality_status;
   /// <summary>
   /// 质量状态(0:合格;1:不合格)
   /// </summary>
   [JsonProperty]
   public int quality_status{
      get { return _quality_status; }
      set { _quality_status = value; }
   }
   private int _goods_status;
   /// <summary>
   /// 商品状态(0:可配;1:短少;2:锁定)
   /// </summary>
   [JsonProperty]
   public int goods_status{
      get { return _goods_status; }
      set { _goods_status = value; }
   }
   private decimal _inout_quantity;
   /// <summary>
   /// 出入数量
   /// </summary>
   [JsonProperty]
   public decimal inout_quantity{
      get { return _inout_quantity; }
      set { _inout_quantity = value; }
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
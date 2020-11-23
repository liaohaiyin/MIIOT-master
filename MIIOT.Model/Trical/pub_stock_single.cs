// File:    pub_stock_single.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_stock_single

using System;

using Newtonsoft.Json;
/// 单品码库存表
[JsonObject(MemberSerialization.OptIn)]
public class pub_stock_single
{
   private long _stock_single_id;
   /// <summary>
   /// 单品码库存id
   /// </summary>
   [JsonProperty]
   public long stock_single_id{
      get { return _stock_single_id; }
      set { _stock_single_id = value; }
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
   private string _single_code;
   /// <summary>
   /// 单品码
   /// </summary>
   [JsonProperty]
   public string single_code{
      get { return _single_code; }
      set { _single_code = value; }
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
   private long _loc_id;
   /// <summary>
   /// 货位id
   /// </summary>
   [JsonProperty]
   public long loc_id{
      get { return _loc_id; }
      set { _loc_id = value; }
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
   private DateTime _gmt_create;
   /// <summary>
   /// 创建时间
   /// </summary>
   [JsonProperty]
   public DateTime gmt_create{
      get { return _gmt_create; }
      set { _gmt_create = value; }
   }

}
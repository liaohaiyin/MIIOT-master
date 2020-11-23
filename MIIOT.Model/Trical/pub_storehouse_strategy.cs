// File:    pub_storehouse_strategy.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_storehouse_strategy

using System;

using Newtonsoft.Json;
/// 库房商品策略表
[JsonObject(MemberSerialization.OptIn)]
public class pub_storehouse_strategy
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
   private long _strategy_storehouse_id;
   /// <summary>
   /// 策略库房id
   /// </summary>
   [JsonProperty]
   public long strategy_storehouse_id{
      get { return _strategy_storehouse_id; }
      set { _strategy_storehouse_id = value; }
   }
   private long _strategy_goods_id;
   /// <summary>
   /// 策略商品id
   /// </summary>
   [JsonProperty]
   public long strategy_goods_id{
      get { return _strategy_goods_id; }
      set { _strategy_goods_id = value; }
   }
   private decimal _strategy_upper_limit;
   /// <summary>
   /// 策略库存上限
   /// </summary>
   [JsonProperty]
   public decimal strategy_upper_limit{
      get { return _strategy_upper_limit; }
      set { _strategy_upper_limit = value; }
   }
   private decimal _strategy_lower_limit;
   /// <summary>
   /// 策略库存下限
   /// </summary>
   [JsonProperty]
   public decimal strategy_lower_limit{
      get { return _strategy_lower_limit; }
      set { _strategy_lower_limit = value; }
   }
   private decimal _strategy_suggest;
   /// <summary>
   /// 建议补货量
   /// </summary>
   [JsonProperty]
   public decimal strategy_suggest{
      get { return _strategy_suggest; }
      set { _strategy_suggest = value; }
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
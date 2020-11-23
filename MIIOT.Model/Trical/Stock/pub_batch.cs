// File:    pub_batch.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_batch

using System;

using Newtonsoft.Json;
/// 批次
[JsonObject(MemberSerialization.OptIn)]
public class pub_batch
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
   private long _goods_id;
   /// <summary>
   /// 商品ID
   /// </summary>
   [JsonProperty]
   public long goods_id{
      get { return _goods_id; }
      set { _goods_id = value; }
   }
   private long _lot_id;
   /// <summary>
   /// 批号ID
   /// </summary>
   [JsonProperty]
   public long lot_id{
      get { return _lot_id; }
      set { _lot_id = value; }
   }
   private long _supply_id;
   /// <summary>
   /// 供应商ID
   /// </summary>
   [JsonProperty]
   public long supply_id{
      get { return _supply_id; }
      set { _supply_id = value; }
   }
   private DateTime _stock_in_date;
   /// <summary>
   /// 生成时间
   /// </summary>
   [JsonProperty]
   public DateTime stock_in_date{
      get { return _stock_in_date; }
      set { _stock_in_date = value; }
   }
   private double _price;
   /// <summary>
   /// 单价
   /// </summary>
   [JsonProperty]
   public double price{
      get { return _price; }
      set { _price = value; }
   }
   private byte _source_type;
   /// <summary>
   /// 0=送货单
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
   private long _source_dtl_id;
   /// <summary>
   /// 来源明细ID
   /// </summary>
   [JsonProperty]
   public long source_dtl_id{
      get { return _source_dtl_id; }
      set { _source_dtl_id = value; }
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
   private string _origin_dtl_id;
   /// <summary>
   /// 原始明细ID
   /// </summary>
   [JsonProperty]
   public string origin_dtl_id{
      get { return _origin_dtl_id; }
      set { _origin_dtl_id = value; }
   }
   private string _origin_batch_id;
   /// <summary>
   /// 中台批次ID
   /// </summary>
   [JsonProperty]
   public string origin_batch_id{
      get { return _origin_batch_id; }
      set { _origin_batch_id = value; }
   }

}
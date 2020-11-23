// File:    dsc_goods.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class dsc_goods

using System;

using Newtonsoft.Json;
/// 试剂商品属性表
[JsonObject(MemberSerialization.OptIn)]
public class dsc_goods
{
   private long _id;
   /// <summary>
   /// id(附属属性主键id=商品主键id)
   /// </summary>
   [JsonProperty]
   public long id{
      get { return _id; }
      set { _id = value; }
   }
   private byte _goods_storage_nature;
   /// <summary>
   /// 商品存储属性(0:常温,1:阴凉,2:冷藏,3:冷冻)
   /// </summary>
   [JsonProperty]
   public byte goods_storage_nature{
      get { return _goods_storage_nature; }
      set { _goods_storage_nature = value; }
   }
   private decimal _goods_storage_temperate;
   /// <summary>
   /// 商品存储温度
   /// </summary>
   [JsonProperty]
   public decimal goods_storage_temperate{
      get { return _goods_storage_temperate; }
      set { _goods_storage_temperate = value; }
   }
   private byte _goods_reagent_type;
   /// <summary>
   /// 商品试剂类别(0:常规试剂1:生化试剂,2:免疫试剂,3:体外诊断试剂,,4:分子诊断试剂,5:其它试剂)
   /// </summary>
   [JsonProperty]
   public byte goods_reagent_type{
      get { return _goods_reagent_type; }
      set { _goods_reagent_type = value; }
   }
   private byte _goods_classify;
   /// <summary>
   /// 商品分类(字典)
   /// </summary>
   [JsonProperty]
   public byte goods_classify{
      get { return _goods_classify; }
      set { _goods_classify = value; }
   }
   private byte _goods_nature;
   /// <summary>
   /// 商品属性(0:进口,1:国产)
   /// </summary>
   [JsonProperty]
   public byte goods_nature{
      get { return _goods_nature; }
      set { _goods_nature = value; }
   }
   private byte _goods_manage_level;
   /// <summary>
   /// 管理级别(0:一级,1:二级;2:三级)
   /// </summary>
   [JsonProperty]
   public byte goods_manage_level{
      get { return _goods_manage_level; }
      set { _goods_manage_level = value; }
   }

}
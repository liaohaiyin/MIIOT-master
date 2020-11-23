// File:    pub_stock2.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class pub_stock2

using System;

using Newtonsoft.Json;
/// 库存表
[JsonObject(MemberSerialization.OptIn)]
public class pub_stock
{
    private long _stock_id;
    /// <summary>
    /// 库存id
    /// </summary>
    [JsonProperty]
    public long stock_id
    {
        get { return _stock_id; }
        set { _stock_id = value; }
    }
    private long _organ_id;
    /// <summary>
    /// 机构id
    /// </summary>
    [JsonProperty]
    public long organ_id
    {
        get { return _organ_id; }
        set { _organ_id = value; }
    }
    private long _storehouse_id;
    /// <summary>
    /// 库房id
    /// </summary>
    [JsonProperty]
    public long storehouse_id
    {
        get { return _storehouse_id; }
        set { _storehouse_id = value; }
    }
    private long _zone_id;
    /// <summary>
    /// 区域id
    /// </summary>
    [JsonProperty]
    public long zone_id
    {
        get { return _zone_id; }
        set { _zone_id = value; }
    }
    private long _loc_id;
    /// <summary>
    /// 货位id
    /// </summary>
    [JsonProperty]
    public long loc_id
    {
        get { return _loc_id; }
        set { _loc_id = value; }
    }
    private long _batch_id;
    /// <summary>
    /// 批次id
    /// </summary>
    [JsonProperty]
    public long batch_id
    {
        get { return _batch_id; }
        set { _batch_id = value; }
    }
    private long _goods_id;
    /// <summary>
    /// 商品id
    /// </summary>
    [JsonProperty]
    public long goods_id
    {
        get { return _goods_id; }
        set { _goods_id = value; }
    }
    private long _plot_id;
    /// <summary>
    /// 批号id
    /// </summary>
    [JsonProperty]
    public long plot_id
    {
        get { return _plot_id; }
        set { _plot_id = value; }
    }
    private int _quality_status;
    /// <summary>
    /// 质量状态(0:合格;1:不合格)
    /// </summary>
    [JsonProperty]
    public int quality_status
    {
        get { return _quality_status; }
        set { _quality_status = value; }
    }
    private int _goods_status;
    /// <summary>
    /// 商品状态(0:可配;1:短少;2:锁定)
    /// </summary>
    [JsonProperty]
    public int goods_status
    {
        get { return _goods_status; }
        set { _goods_status = value; }
    }
    private decimal _stock_quantity;
    /// <summary>
    /// 库存数量
    /// </summary>
    [JsonProperty]
    public decimal stock_quantity
    {
        get { return _stock_quantity; }
        set { _stock_quantity = value; }
    }
    private long _creater_id;
    /// <summary>
    /// 创建人id
    /// </summary>
    [JsonProperty]
    public long creater_id
    {
        get { return _creater_id; }
        set { _creater_id = value; }
    }
    private DateTime _gmt_create;
    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonProperty]
    public DateTime gmt_create
    {
        get { return _gmt_create; }
        set { _gmt_create = value; }
    }
    private long _modifier_id;
    /// <summary>
    /// 修改人id
    /// </summary>
    [JsonProperty]
    public long modifier_id
    {
        get { return _modifier_id; }
        set { _modifier_id = value; }
    }
    private DateTime _gmt_modified;
    /// <summary>
    /// 修改时间
    /// </summary>
    [JsonProperty]
    public DateTime gmt_modified
    {
        get { return _gmt_modified; }
        set { _gmt_modified = value; }
    }
   
}
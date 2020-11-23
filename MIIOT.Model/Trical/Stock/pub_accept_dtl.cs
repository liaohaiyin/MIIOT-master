// File:    pub_accept_dtl.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_accept_dtl

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
/// 验收明细
[JsonObject(MemberSerialization.OptIn)]
public class pub_accept_dtl : CheckInfo
{
    private long _id;
    /// <summary>
    /// id
    /// </summary>
    [JsonProperty]
    public long id
    {
        get { return _id; }
        set { _id = value; }
    }
    private long _organ_id;
    /// <summary>
    /// 机构ID
    /// </summary>
    [JsonProperty]
    public long organ_id
    {
        get { return _organ_id; }
        set { _organ_id = value; }
    }
    private long _accept_id;
    /// <summary>
    /// 验收表ID
    /// </summary>
    [JsonProperty]
    public long accept_id
    {
        get { return _accept_id; }
        set { _accept_id = value; }
    }
    private int _source_type;
    /// <summary>
    /// 0=送货单
    /// </summary>
    [JsonProperty]
    public int source_type
    {
        get { return _source_type; }
        set { _source_type = value; }
    }
    private string _source_id;
    /// <summary>
    /// 来源单ID
    /// </summary>
    [JsonProperty]
    public string source_id
    {
        get { return _source_id; }
        set { _source_id = value; }
    }
    private long _source_dtl_id;
    /// <summary>
    /// 来源明细ID
    /// </summary>
    [JsonProperty]
    public long source_dtl_id
    {
        get { return _source_dtl_id; }
        set { _source_dtl_id = value; }
    }
    private string _source_no;
    /// <summary>
    /// 来源单号
    /// </summary>
    [JsonProperty]
    public string source_no
    {
        get { return _source_no; }
        set { _source_no = value; }
    }
    private string _origin_id;
    /// <summary>
    /// 原始单ID
    /// </summary>
    [JsonProperty]
    public string origin_id
    {
        get { return _origin_id; }
        set { _origin_id = value; }
    }
    private string _origin_dtl_id;
    /// <summary>
    /// 原始明细ID
    /// </summary>
    [JsonProperty]
    public string origin_dtl_id
    {
        get { return _origin_dtl_id; }
        set { _origin_dtl_id = value; }
    }
    private long _goods_id;
    /// <summary>
    /// 商品ID
    /// </summary>
    [JsonProperty]
    public long goods_id
    {
        get { return _goods_id; }
        set { _goods_id = value; }
    }
    private string _goods_no;
    /// <summary>
    /// 商品编码
    /// </summary>
    [JsonProperty]
    public string goods_no
    {
        get { return _goods_no; }
        set { _goods_no = value; }
    }
    private string _goods_name;
    /// <summary>
    /// 商品名称
    /// </summary>
    [JsonProperty]
    public string goods_name
    {
        get { return _goods_name; }
        set { _goods_name = value; }
    }
    private string _goods_spec;
    /// <summary>
    /// 商品规格
    /// </summary>
    [JsonProperty]
    public string goods_spec
    {
        get { return _goods_spec; }
        set { _goods_spec = value; }
    }
    private string _goods_factory_name;
    /// <summary>
    /// 生产厂家
    /// </summary>
    [JsonProperty]
    public string goods_factory_name
    {
        get { return _goods_factory_name; }
        set { _goods_factory_name = value; }
    }
    private string _goods_unit;
    /// <summary>
    /// 单位
    /// </summary>
    [JsonProperty]
    public string goods_unit
    {
        get { return _goods_unit; }
        set { _goods_unit = value; }
    }
    private int _delivery_qty;
    /// <summary>
    /// 送货数量
    /// </summary>
    [JsonProperty]
    public int delivery_qty
    {
        get
        {
            _delivery_qty = Qty;
            return _delivery_qty;
        }
        set
        {
            Qty = value;
            _delivery_qty = value;
        }
    }
    private int _check_qty;
    /// <summary>
    /// 复核数量
    /// </summary>
    [JsonProperty]
    public int check_qty
    {
        get
        {
            _check_qty = CheckQty;
            return _check_qty;
        }
        set
        {
            CheckQty = value;
            _check_qty = value;
            OnPropertyChanged("check_qty");
        }
    }
    private long _lot_id;
    /// <summary>
    /// 批号ID
    /// </summary>
    [JsonProperty]
    public long lot_id
    {
        get { return _lot_id; }
        set { _lot_id = value; }
    }
    private string _lot_no;
    /// <summary>
    /// 批号
    /// </summary>
    [JsonProperty]
    public string lot_no
    {
        get { return _lot_no; }
        set { _lot_no = value; }
    }
    private long _batch_id;
    /// <summary>
    /// 批次
    /// </summary>
    [JsonProperty]
    public long batch_id
    {
        get { return _batch_id; }
        set { _batch_id = value; }
    }
    private double _price;
    /// <summary>
    /// 单价
    /// </summary>
    [JsonProperty]
    public double price
    {
        get { return _price; }
        set { _price = value; }
    }
    private long _pprod_date;
    /// <summary>
    /// 生产日期
    /// </summary>
    [JsonProperty]
    public long pprod_date
    {
        get { return _pprod_date; }
        set { _pprod_date = value; }
    }
    private long _pvalid_date;
    /// <summary>
    /// 有效日期至
    /// </summary>
    [JsonProperty]
    public long pvalid_date
    {
        get { return _pvalid_date; }
        set { _pvalid_date = value; }
    }
    private int _status;
    /// <summary>
    /// 0=初始 1=已生成验收 99=作废
    /// </summary>
    [JsonProperty]
    public int status
    {
        get { return _status; }
        set { _status = value; }
    }
    private int _is_delete;
    /// <summary>
    /// 0=正常 1=删除
    /// </summary>
    [JsonProperty]
    public int is_delete
    {
        get { return _is_delete; }
        set { _is_delete = value; }
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
    private string _gmt_create;
    /// <summary>
    /// 创建时间
    /// </summary>
    [JsonProperty]
    public string gmt_create
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
    private string _gmt_modified;
    /// <summary>
    /// 修改时间
    /// </summary>
    [JsonProperty]
    public string gmt_modified
    {
        get { return _gmt_modified; }
        set { _gmt_modified = value; }
    }
    private List<string> _RFID = new List<string>();

    public List<string> RFID
    {
        get { return _RFID; }
        set { _RFID = value; }
    }

}
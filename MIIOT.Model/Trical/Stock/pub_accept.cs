// File:    pub_accept.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class pub_accept

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
/// 验收单
[JsonObject(MemberSerialization.OptIn)]
public class pub_accept : CheckInfo
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
    private long _supply_id;
    /// <summary>
    /// 供应商ID
    /// </summary>
    [JsonProperty]
    public long supply_id
    {
        get { return _supply_id; }
        set { _supply_id = value; }
    }
    private string _supply_name;
    /// <summary>
    /// 供应商名称
    /// </summary>
    [JsonProperty]
    public string supply_name
    {
        get { return _supply_name; }
        set { _supply_name = value; }
    }
    private long _supply_time;
    /// <summary>
    /// 送货时间
    /// </summary>
    [JsonProperty]
    public long supply_time
    {
        get { return _supply_time; }
        set { _supply_time = value; }
    }
    private int _storehouse_id;
    /// <summary>
    /// 收货库房
    /// </summary>
    [JsonProperty]
    public int storehouse_id
    {
        get { return _storehouse_id; }
        set { _storehouse_id = value;
            OnPropertyChanged("storehouse_id");
        }
    }
    private string _storehouse_name;

    [JsonProperty]
    public string storehouse_name
    {
        get { return _storehouse_name; }
        set { _storehouse_name = value;
            OnPropertyChanged("storehouse_name");
        }
    }


    private long _accept_person_id;
    /// <summary>
    /// 验收人ID
    /// </summary>
    [JsonProperty]
    public long accept_person_id
    {
        get { return _accept_person_id; }
        set { _accept_person_id = value; }
    }
    private string _accept_person_name;
    /// <summary>
    /// 验收人
    /// </summary>
    [JsonProperty]
    public string accept_person_name
    {
        get { return _accept_person_name; }
        set { _accept_person_name = value; }
    }
    private long _check_person_id;
    /// <summary>
    /// 复核人ID
    /// </summary>
    [JsonProperty]
    public long check_person_id
    {
        get { return _check_person_id; }
        set { _check_person_id = value; }
    }
    private string _check_person_name;
    /// <summary>
    /// 复核人
    /// </summary>
    [JsonProperty]
    public string check_person_name
    {
        get { return _check_person_name; }
        set { _check_person_name = value; }
    }
    private int _status;
    /// <summary>
    /// 0=初始 1=验收 2=验收确认 3=复核 99=拒收
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
    public string Hospital { get; set; }
    private List<pub_accept_dtl> _acceptDtlList=new List<pub_accept_dtl>();
    [JsonProperty]
    public List<pub_accept_dtl> acceptDtlList
    {
        get { return _acceptDtlList; }
        set { _acceptDtlList = value; }
    }
    
}

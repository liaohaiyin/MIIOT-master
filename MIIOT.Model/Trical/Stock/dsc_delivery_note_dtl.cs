// File:    dsc_delivery_note_dtl.cs
// Author:  si'a'zon
// Created: 2020年3月28日 18:15:59
// Purpose: Definition of Class dsc_delivery_note_dtl

using System;

using Newtonsoft.Json;
/// 试剂送货单附录
[JsonObject(MemberSerialization.OptIn)]
public class dsc_delivery_note_dtl
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
    private byte _coldchain_tempctrlmode;
    /// <summary>
    /// 0=车载冰箱 1=冷藏(冻)箱 2=保温箱加冰袋 3=保温箱加冰排 4=冷链车
    /// </summary>
    [JsonProperty]
    public byte coldchain_tempctrlmode
    {
        get { return _coldchain_tempctrlmode; }
        set { _coldchain_tempctrlmode = value; }
    }
    private byte _coldchain_coldstorage;
    /// <summary>
    /// 0=汽运 1=空运 2=中铁
    /// </summary>
    [JsonProperty]
    public byte coldchain_coldstorage
    {
        get { return _coldchain_coldstorage; }
        set { _coldchain_coldstorage = value; }
    }
    private DateTime _coldchain_departuretime;
    /// <summary>
    /// 冷链启运时间
    /// </summary>
    [JsonProperty]
    public DateTime coldchain_departuretime
    {
        get { return _coldchain_departuretime; }
        set { _coldchain_departuretime = value; }
    }
    private DateTime _coldchain_arrivedtime;
    /// <summary>
    /// 冷链到达时间
    /// </summary>
    [JsonProperty]
    public DateTime coldchain_arrivedtime
    {
        get { return _coldchain_arrivedtime; }
        set { _coldchain_arrivedtime = value; }
    }
    private double _coldchain_departuretemp;
    /// <summary>
    /// 冷链启运温度
    /// </summary>
    [JsonProperty]
    public double coldchain_departuretemp
    {
        get { return _coldchain_departuretemp; }
        set { _coldchain_departuretemp = value; }
    }
    private double _coldchain_arrivedtemp;
    /// <summary>
    /// 冷链到达温度
    /// </summary>
    [JsonProperty]
    public double coldchain_arrivedtemp
    {
        get { return _coldchain_arrivedtemp; }
        set { _coldchain_arrivedtemp = value; }
    }
    private double _coldchain_departurehumidity;
    /// <summary>
    /// 冷链启运湿度
    /// </summary>
    [JsonProperty]
    public double coldchain_departurehumidity
    {
        get { return _coldchain_departurehumidity; }
        set { _coldchain_departurehumidity = value; }
    }
    private double _coldchain_arrivedhumidity;
    /// <summary>
    /// 冷链到达湿度
    /// </summary>
    [JsonProperty]
    public double coldchain_arrivedhumidity
    {
        get { return _coldchain_arrivedhumidity; }
        set { _coldchain_arrivedhumidity = value; }
    }

}
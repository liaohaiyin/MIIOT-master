// File:    sys_menu.cs
// Author:  si'a'zon
// Created: 2020年3月19日 18:20:36
// Purpose: Definition of Class sys_menu

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MIIOT.Model;
using Newtonsoft.Json;
/// 菜单表
[JsonObject(MemberSerialization.OptIn)]
public class sys_menu: BaseNotify
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
   private string _menu_name;
   /// <summary>
   /// 菜单名称
   /// </summary>
   [JsonProperty]
   public string menu_name{
      get { return _menu_name; }
      set { _menu_name = value; }
   }
   private long _menu_parent_id;
   /// <summary>
   /// 父菜单id,一级菜单为0
   /// </summary>
   [JsonProperty]
   public long menu_parent_id{
      get { return _menu_parent_id; }
      set { _menu_parent_id = value; }
   }
   private string _menu_url;
   /// <summary>
   /// 菜单url
   /// </summary>
   [JsonProperty]
   public string menu_url{
      get { return _menu_url; }
      set { _menu_url = value; }
   }
   private byte _menu_type;
   /// <summary>
   /// 菜单类型(0:菜单,1:按钮)
   /// </summary>
   [JsonProperty]
   public byte menu_type{
      get { return _menu_type; }
      set { _menu_type = value; }
   }
   private string _menu_icon;
   /// <summary>
   /// 菜单图标
   /// </summary>
   [JsonProperty]
   public string menu_icon{
      get { return _menu_icon; }
      set { _menu_icon = value; }
   }
    private string _menuCode;

    public string menu_code
    {
        get { return _menuCode; }
        set { _menuCode = value; }
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
    private ObservableCollection<sys_menu> _ChildMenu=new ObservableCollection<sys_menu>();

    public ObservableCollection<sys_menu> ChildMenu
    {
        get { return _ChildMenu; }
        set { _ChildMenu = value;
            OnPropertyChanged("ChildMenu");
        }
    }

    private bool _isChecked;

    public bool IsChecked
    {
        get { return _isChecked; }
        set { _isChecked = value;
            OnPropertyChanged("IsChecked");
        }
    }

}
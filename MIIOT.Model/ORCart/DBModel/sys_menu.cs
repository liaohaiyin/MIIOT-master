/**  版本信息模板在安装目录下，可自行修改。
* sys_menu.cs
*
* 功 能： N/A
* 类 名： sys_menu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:40:13   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace MIIOT.Model
{
	/// <summary>
	/// sys_menu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_menu
	{
		public sys_menu()
		{}
		#region Model
		private long _id;
		private string _menu_name;
		private long? _menu_parent_id=0;
		private string _menu_url;
		private int? _menu_type=0;
		private string _menu_code;
		private string _menu_icon;
		private long? _creater_id=0;
		private DateTime? _gmt_create= DateTime.Now;
		private long? _modifier_id=0;
		private DateTime? _gmt_modified= DateTime.Now;
		/// <summary>
		/// auto_increment
		/// </summary>
		public long id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menu_name
		{
			set{ _menu_name=value;}
			get{return _menu_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? menu_parent_id
		{
			set{ _menu_parent_id=value;}
			get{return _menu_parent_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menu_url
		{
			set{ _menu_url=value;}
			get{return _menu_url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? menu_type
		{
			set{ _menu_type=value;}
			get{return _menu_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menu_code
		{
			set{ _menu_code=value;}
			get{return _menu_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menu_icon
		{
			set{ _menu_icon=value;}
			get{return _menu_icon;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? creater_id
		{
			set{ _creater_id=value;}
			get{return _creater_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? gmt_create
		{
			set{ _gmt_create=value;}
			get{return _gmt_create;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? modifier_id
		{
			set{ _modifier_id=value;}
			get{return _modifier_id;}
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime? gmt_modified
		{
			set{ _gmt_modified=value;}
			get{return _gmt_modified;}
		}
		#endregion Model

	}
}


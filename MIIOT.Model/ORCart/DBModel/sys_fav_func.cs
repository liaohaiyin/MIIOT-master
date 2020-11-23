/**  版本信息模板在安装目录下，可自行修改。
* sys_fav_func.cs
*
* 功 能： N/A
* 类 名： sys_fav_func
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:24   N/A    初版
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
	/// sys_fav_func:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_fav_func
	{
		public sys_fav_func()
		{}
		#region Model
		private string _fav_id;
		private string _menu_id;
		private string _fav_moduleid;
		private string _user_id;
		private int? _fav_order;
		private string _fav_name;
		/// <summary>
		/// 
		/// </summary>
		public string fav_id
		{
			set{ _fav_id=value;}
			get{return _fav_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string menu_id
		{
			set{ _menu_id=value;}
			get{return _menu_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fav_moduleid
		{
			set{ _fav_moduleid=value;}
			get{return _fav_moduleid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? fav_order
		{
			set{ _fav_order=value;}
			get{return _fav_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fav_name
		{
			set{ _fav_name=value;}
			get{return _fav_name;}
		}
		#endregion Model

	}
}


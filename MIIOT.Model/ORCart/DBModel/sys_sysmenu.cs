/**  版本信息模板在安装目录下，可自行修改。
* sys_sysmenu.cs
*
* 功 能： N/A
* 类 名： sys_sysmenu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:41   N/A    初版
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
	/// sys_sysmenu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_sysmenu
	{
		public sys_sysmenu()
		{}
		#region Model
		private string _menu_id;
		private string _menu_name;
		private string _parent_id;
		private decimal? _menu_order;
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
		public string menu_name
		{
			set{ _menu_name=value;}
			get{return _menu_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string parent_id
		{
			set{ _parent_id=value;}
			get{return _parent_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? menu_order
		{
			set{ _menu_order=value;}
			get{return _menu_order;}
		}
		#endregion Model

	}
}


/**  版本信息模板在安装目录下，可自行修改。
* sys_root_pref.cs
*
* 功 能： N/A
* 类 名： sys_root_pref
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:40   N/A    初版
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
	/// sys_root_pref:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_root_pref
	{
		public sys_root_pref()
		{}
		#region Model
		private string _pref_id;
		private string _pref_hierarchy;
		private string _pref_key;
		private string _pref_value;
		/// <summary>
		/// 
		/// </summary>
		public string pref_id
		{
			set{ _pref_id=value;}
			get{return _pref_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pref_hierarchy
		{
			set{ _pref_hierarchy=value;}
			get{return _pref_hierarchy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pref_key
		{
			set{ _pref_key=value;}
			get{return _pref_key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pref_value
		{
			set{ _pref_value=value;}
			get{return _pref_value;}
		}
		#endregion Model

	}
}


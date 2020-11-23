/**  版本信息模板在安装目录下，可自行修改。
* sys_sysmenumapping.cs
*
* 功 能： N/A
* 类 名： sys_sysmenumapping
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:42   N/A    初版
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
	/// sys_sysmenumapping:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_sysmenumapping
	{
		public sys_sysmenumapping()
		{}
		#region Model
		private string _func_id;
		private string _menu_id;
		private string _func_rename;
		private int? _func_order;
		/// <summary>
		/// 
		/// </summary>
		public string func_id
		{
			set{ _func_id=value;}
			get{return _func_id;}
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
		public string func_rename
		{
			set{ _func_rename=value;}
			get{return _func_rename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? func_order
		{
			set{ _func_order=value;}
			get{return _func_order;}
		}
		#endregion Model

	}
}


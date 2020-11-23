/**  版本信息模板在安装目录下，可自行修改。
* sys_role.cs
*
* 功 能： N/A
* 类 名： sys_role
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:40:12   N/A    初版
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
	/// sys_role:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_role
	{
		public sys_role()
		{}
		#region Model
		private int _role_id;
		private string _role_name;
		private string _role_desc;
		private int? _roletype;
		private string _cremanid;
		private string _cremanname;
		private DateTime? _credate;
		/// <summary>
		/// 
		/// </summary>
		public int role_id
		{
			set{ _role_id=value;}
			get{return _role_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string role_name
		{
			set{ _role_name=value;}
			get{return _role_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string role_desc
		{
			set{ _role_desc=value;}
			get{return _role_desc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? roletype
		{
			set{ _roletype=value;}
			get{return _roletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cremanid
		{
			set{ _cremanid=value;}
			get{return _cremanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cremanname
		{
			set{ _cremanname=value;}
			get{return _cremanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? credate
		{
			set{ _credate=value;}
			get{return _credate;}
		}
		#endregion Model

	}
}


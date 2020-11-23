/**  版本信息模板在安装目录下，可自行修改。
* sys_accrolerule.cs
*
* 功 能： N/A
* 类 名： sys_accrolerule
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:19   N/A    初版
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
	/// sys_accrolerule:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_accrolerule
	{
		public sys_accrolerule()
		{}
		#region Model
		private string _account_id;
		private string _account_name;
		private int _role_id;
		private string _role_name;
		private int? _roletype;
		private int _rule_id;
		private int? _filtertype;
		private int? _data_type;
		private string _data_id;
		private int _accrolerule_id;
		/// <summary>
		/// 
		/// </summary>
		public string account_id
		{
			set{ _account_id=value;}
			get{return _account_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_name
		{
			set{ _account_name=value;}
			get{return _account_name;}
		}
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
		public int? roletype
		{
			set{ _roletype=value;}
			get{return _roletype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int rule_id
		{
			set{ _rule_id=value;}
			get{return _rule_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? filtertype
		{
			set{ _filtertype=value;}
			get{return _filtertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? data_type
		{
			set{ _data_type=value;}
			get{return _data_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string data_id
		{
			set{ _data_id=value;}
			get{return _data_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int accrolerule_id
		{
			set{ _accrolerule_id=value;}
			get{return _accrolerule_id;}
		}
		#endregion Model

	}
}


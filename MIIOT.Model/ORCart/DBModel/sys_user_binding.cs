/**  版本信息模板在安装目录下，可自行修改。
* sys_user_binding.cs
*
* 功 能： N/A
* 类 名： sys_user_binding
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
	/// sys_user_binding:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_user_binding
	{
		public sys_user_binding()
		{}
		#region Model
		private long _id;
		private long? _organ_id=0;
		private long? _binding_user_id;
		private string _binding_user_code;
		private int? _binding_type=0;
		private string _binding_machine_code;
		private string _binding_identity;
		private int? _binding_status=0;
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
		public long? organ_id
		{
			set{ _organ_id=value;}
			get{return _organ_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? binding_user_id
		{
			set{ _binding_user_id=value;}
			get{return _binding_user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string binding_user_code
		{
			set{ _binding_user_code=value;}
			get{return _binding_user_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? binding_type
		{
			set{ _binding_type=value;}
			get{return _binding_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string binding_machine_code
		{
			set{ _binding_machine_code=value;}
			get{return _binding_machine_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string binding_identity
		{
			set{ _binding_identity=value;}
			get{return _binding_identity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? binding_status
		{
			set{ _binding_status=value;}
			get{return _binding_status;}
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


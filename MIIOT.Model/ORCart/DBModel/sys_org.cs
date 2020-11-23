/**  版本信息模板在安装目录下，可自行修改。
* sys_org.cs
*
* 功 能： N/A
* 类 名： sys_org
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:32   N/A    初版
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
	/// sys_org:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_org
	{
		public sys_org()
		{}
		#region Model
		private int _org_id;
		private string _org_code;
		private string _org_name;
		private string _org_shortname;
		private string _org_type;
		private int? _org_pid;
		private string _org_remark;
		private string _org_address;
		private string _org_linkman;
		private string _org_postcode;
		private string _org_phone;
		private string _org_fax;
		private string _org_mobile;
		private string _org_email;
		private string _org_area;
		private DateTime? _org_proctm;
		private DateTime? _org_addtm;
		private DateTime? _org_updatetm;
		private int? _org_enabled;
		private int? _org_deleted;
		/// <summary>
		/// 
		/// </summary>
		public int org_id
		{
			set{ _org_id=value;}
			get{return _org_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_code
		{
			set{ _org_code=value;}
			get{return _org_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_name
		{
			set{ _org_name=value;}
			get{return _org_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_shortname
		{
			set{ _org_shortname=value;}
			get{return _org_shortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_type
		{
			set{ _org_type=value;}
			get{return _org_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? org_pid
		{
			set{ _org_pid=value;}
			get{return _org_pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_remark
		{
			set{ _org_remark=value;}
			get{return _org_remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_address
		{
			set{ _org_address=value;}
			get{return _org_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_linkman
		{
			set{ _org_linkman=value;}
			get{return _org_linkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_postcode
		{
			set{ _org_postcode=value;}
			get{return _org_postcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_phone
		{
			set{ _org_phone=value;}
			get{return _org_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_fax
		{
			set{ _org_fax=value;}
			get{return _org_fax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_mobile
		{
			set{ _org_mobile=value;}
			get{return _org_mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_email
		{
			set{ _org_email=value;}
			get{return _org_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string org_area
		{
			set{ _org_area=value;}
			get{return _org_area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? org_proctm
		{
			set{ _org_proctm=value;}
			get{return _org_proctm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? org_addtm
		{
			set{ _org_addtm=value;}
			get{return _org_addtm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? org_updatetm
		{
			set{ _org_updatetm=value;}
			get{return _org_updatetm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? org_enabled
		{
			set{ _org_enabled=value;}
			get{return _org_enabled;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? org_deleted
		{
			set{ _org_deleted=value;}
			get{return _org_deleted;}
		}
		#endregion Model

	}
}


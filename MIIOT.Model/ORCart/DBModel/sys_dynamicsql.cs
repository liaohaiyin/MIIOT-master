/**  版本信息模板在安装目录下，可自行修改。
* sys_dynamicsql.cs
*
* 功 能： N/A
* 类 名： sys_dynamicsql
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:23   N/A    初版
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
	/// sys_dynamicsql:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_dynamicsql
	{
		public sys_dynamicsql()
		{}
		#region Model
		private string _dysql_id;
		private int? _dysql_isfunc;
		private string _dysql_username;
		private string _account_code;
		private string _dysql_name;
		private int? _dysql_isshare;
		private byte[] _dysql_context;
		private string _dysql_desc;
		private DateTime? _lastmodtime;
		/// <summary>
		/// 
		/// </summary>
		public string dysql_id
		{
			set{ _dysql_id=value;}
			get{return _dysql_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dysql_isfunc
		{
			set{ _dysql_isfunc=value;}
			get{return _dysql_isfunc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dysql_username
		{
			set{ _dysql_username=value;}
			get{return _dysql_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_code
		{
			set{ _account_code=value;}
			get{return _account_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dysql_name
		{
			set{ _dysql_name=value;}
			get{return _dysql_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dysql_isshare
		{
			set{ _dysql_isshare=value;}
			get{return _dysql_isshare;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] dysql_context
		{
			set{ _dysql_context=value;}
			get{return _dysql_context;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dysql_desc
		{
			set{ _dysql_desc=value;}
			get{return _dysql_desc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? lastmodtime
		{
			set{ _lastmodtime=value;}
			get{return _lastmodtime;}
		}
		#endregion Model

	}
}


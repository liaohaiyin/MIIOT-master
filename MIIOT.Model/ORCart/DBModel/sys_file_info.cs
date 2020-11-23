/**  版本信息模板在安装目录下，可自行修改。
* sys_file_info.cs
*
* 功 能： N/A
* 类 名： sys_file_info
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:25   N/A    初版
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
	/// sys_file_info:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_file_info
	{
		public sys_file_info()
		{}
		#region Model
		private int _file_id;
		private int? _file_parent_id;
		private string _file_type;
		private DateTime? _file_modified_time;
		private string _file_name;
		/// <summary>
		/// 
		/// </summary>
		public int file_id
		{
			set{ _file_id=value;}
			get{return _file_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? file_parent_id
		{
			set{ _file_parent_id=value;}
			get{return _file_parent_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_type
		{
			set{ _file_type=value;}
			get{return _file_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? file_modified_time
		{
			set{ _file_modified_time=value;}
			get{return _file_modified_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_name
		{
			set{ _file_name=value;}
			get{return _file_name;}
		}
		#endregion Model

	}
}


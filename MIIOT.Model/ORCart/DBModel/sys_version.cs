/**  版本信息模板在安装目录下，可自行修改。
* sys_version.cs
*
* 功 能： N/A
* 类 名： sys_version
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:45   N/A    初版
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
	/// sys_version:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_version
	{
		public sys_version()
		{}
		#region Model
		private DateTime? _input_time;
		private string _version_code;
		private string _inputman_id;
		private string _inputman_name;
		private string _version_id;
		private string _version_desc;
		private string _memo;
		/// <summary>
		/// 
		/// </summary>
		public DateTime? input_time
		{
			set{ _input_time=value;}
			get{return _input_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string version_code
		{
			set{ _version_code=value;}
			get{return _version_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string inputman_id
		{
			set{ _inputman_id=value;}
			get{return _inputman_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string inputman_name
		{
			set{ _inputman_name=value;}
			get{return _inputman_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string version_id
		{
			set{ _version_id=value;}
			get{return _version_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string version_desc
		{
			set{ _version_desc=value;}
			get{return _version_desc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}


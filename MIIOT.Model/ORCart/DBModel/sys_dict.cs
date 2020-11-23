/**  版本信息模板在安装目录下，可自行修改。
* sys_dict.cs
*
* 功 能： N/A
* 类 名： sys_dict
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:21   N/A    初版
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
	/// sys_dict:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_dict
	{
		public sys_dict()
		{}
		#region Model
		private string _dict_mergetype;
		private int _dict_id;
		private string _dict_name;
		private string _dict_title;
		private DateTime _dict_lastmodify;
		private string _dict_operator;
		private string _dict_desc;
		/// <summary>
		/// 
		/// </summary>
		public string dict_mergetype
		{
			set{ _dict_mergetype=value;}
			get{return _dict_mergetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int dict_id
		{
			set{ _dict_id=value;}
			get{return _dict_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dict_name
		{
			set{ _dict_name=value;}
			get{return _dict_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dict_title
		{
			set{ _dict_title=value;}
			get{return _dict_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime dict_lastmodify
		{
			set{ _dict_lastmodify=value;}
			get{return _dict_lastmodify;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dict_operator
		{
			set{ _dict_operator=value;}
			get{return _dict_operator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dict_desc
		{
			set{ _dict_desc=value;}
			get{return _dict_desc;}
		}
		#endregion Model

	}
}


/**  版本信息模板在安装目录下，可自行修改。
* sys_dict_detail.cs
*
* 功 能： N/A
* 类 名： sys_dict_detail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:22   N/A    初版
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
	/// sys_dict_detail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_dict_detail
	{
		public sys_dict_detail()
		{}
		#region Model
		private int _dict_id;
		private string _dict_value;
		private string _dict_alias;
		private string _dict_text;
		private int? _dict_enable=1;
		private int? _dict_order=1;
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
		public string dict_value
		{
			set{ _dict_value=value;}
			get{return _dict_value;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dict_alias
		{
			set{ _dict_alias=value;}
			get{return _dict_alias;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dict_text
		{
			set{ _dict_text=value;}
			get{return _dict_text;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dict_enable
		{
			set{ _dict_enable=value;}
			get{return _dict_enable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dict_order
		{
			set{ _dict_order=value;}
			get{return _dict_order;}
		}
		#endregion Model

	}
}


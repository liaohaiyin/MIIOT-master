/**  版本信息模板在安装目录下，可自行修改。
* sys_accrule_detail.cs
*
* 功 能： N/A
* 类 名： sys_accrule_detail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:20   N/A    初版
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
	/// sys_accrule_detail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_accrule_detail
	{
		public sys_accrule_detail()
		{}
		#region Model
		private int _id;
		private int? _rule_id;
		private int _data_type;
		private string _data_id;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? rule_id
		{
			set{ _rule_id=value;}
			get{return _rule_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int data_type
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
		#endregion Model

	}
}


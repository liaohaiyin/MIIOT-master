/**  版本信息模板在安装目录下，可自行修改。
* sys_accrule.cs
*
* 功 能： N/A
* 类 名： sys_accrule
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
	/// sys_accrule:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_accrule
	{
		public sys_accrule()
		{}
		#region Model
		private int _rule_id;
		private string _rule_name;
		private int _rule_type;
		private string _creator;
		private string _remark;
		private int? _rule_enabled=1;
		private int? _filtertype;
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
		public string rule_name
		{
			set{ _rule_name=value;}
			get{return _rule_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int rule_type
		{
			set{ _rule_type=value;}
			get{return _rule_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? rule_enabled
		{
			set{ _rule_enabled=value;}
			get{return _rule_enabled;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? filtertype
		{
			set{ _filtertype=value;}
			get{return _filtertype;}
		}
		#endregion Model

	}
}


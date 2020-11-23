/**  版本信息模板在安装目录下，可自行修改。
* sys_accountparam.cs
*
* 功 能： N/A
* 类 名： sys_accountparam
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:17   N/A    初版
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
	/// sys_accountparam:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_accountparam
	{
		public sys_accountparam()
		{}
		#region Model
		private string _account_id;
		private string _paramkey;
		private string _paramvalue;
		private string _memo;
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
		public string paramkey
		{
			set{ _paramkey=value;}
			get{return _paramkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string paramvalue
		{
			set{ _paramvalue=value;}
			get{return _paramvalue;}
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


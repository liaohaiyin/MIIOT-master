/**  版本信息模板在安装目录下，可自行修改。
* sys_msg_disp.cs
*
* 功 能： N/A
* 类 名： sys_msg_disp
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:30   N/A    初版
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
	/// sys_msg_disp:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_msg_disp
	{
		public sys_msg_disp()
		{}
		#region Model
		private string _account_code;
		private int? _readed;
		private string _msg_id;
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
		public int? readed
		{
			set{ _readed=value;}
			get{return _readed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string msg_id
		{
			set{ _msg_id=value;}
			get{return _msg_id;}
		}
		#endregion Model

	}
}


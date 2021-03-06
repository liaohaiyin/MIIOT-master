﻿/**  版本信息模板在安装目录下，可自行修改。
* cart_nobilcabinetoperate.cs
*
* 功 能： N/A
* 类 名： cart_nobilcabinetoperate
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:08   N/A    初版
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
	/// cart_nobilcabinetoperate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_nobilcabinetoperate
	{
		public cart_nobilcabinetoperate()
		{}
		#region Model
		private string _nobillid;
		private string _sourcetransid;
		private string _cabinetoperateid;
		/// <summary>
		/// 
		/// </summary>
		public string nobillid
		{
			set{ _nobillid=value;}
			get{return _nobillid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sourcetransid
		{
			set{ _sourcetransid=value;}
			get{return _sourcetransid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cabinetoperateid
		{
			set{ _cabinetoperateid=value;}
			get{return _cabinetoperateid;}
		}
		#endregion Model

	}
}


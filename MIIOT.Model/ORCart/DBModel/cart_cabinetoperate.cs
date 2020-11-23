/**  版本信息模板在安装目录下，可自行修改。
* cart_cabinetoperate.cs
*
* 功 能： N/A
* 类 名： cart_cabinetoperate
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:06   N/A    初版
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
	/// cart_cabinetoperate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_cabinetoperate
	{
		public cart_cabinetoperate()
		{}
		#region Model
		private string _cabinetoperateid;
		private string _operatorid;
		private string _cabinetid;
		private decimal? _operatetype;
		private DateTime? _operatetime;
		/// <summary>
		/// 
		/// </summary>
		public string cabinetoperateid
		{
			set{ _cabinetoperateid=value;}
			get{return _cabinetoperateid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string operatorid
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cabinetid
		{
			set{ _cabinetid=value;}
			get{return _cabinetid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? operatetype
		{
			set{ _operatetype=value;}
			get{return _operatetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? operatetime
		{
			set{ _operatetime=value;}
			get{return _operatetime;}
		}
		#endregion Model

	}
}


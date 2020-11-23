/**  版本信息模板在安装目录下，可自行修改。
* cart_trench.cs
*
* 功 能： N/A
* 类 名： cart_trench
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:11   N/A    初版
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
	/// cart_trench:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_trench
	{
		public cart_trench()
		{}
		#region Model
		private int? _trenchstatus;
		private string _leavelno;
		private int? _isdelete;
		private DateTime? _credate;
		private string _cremanid;
		private DateTime? _editdate;
		private string _editmanid;
		private string _trenchid;
		private int? _trenchno;
		private string _cabinetid;
		private string _memo;
		/// <summary>
		/// 
		/// </summary>
		public int? trenchstatus
		{
			set{ _trenchstatus=value;}
			get{return _trenchstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string leavelno
		{
			set{ _leavelno=value;}
			get{return _leavelno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? isdelete
		{
			set{ _isdelete=value;}
			get{return _isdelete;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? credate
		{
			set{ _credate=value;}
			get{return _credate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cremanid
		{
			set{ _cremanid=value;}
			get{return _cremanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? editdate
		{
			set{ _editdate=value;}
			get{return _editdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string editmanid
		{
			set{ _editmanid=value;}
			get{return _editmanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string trenchid
		{
			set{ _trenchid=value;}
			get{return _trenchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? trenchno
		{
			set{ _trenchno=value;}
			get{return _trenchno;}
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
		public string memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}


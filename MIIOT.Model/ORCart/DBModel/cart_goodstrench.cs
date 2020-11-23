/**  版本信息模板在安装目录下，可自行修改。
* cart_goodstrench.cs
*
* 功 能： N/A
* 类 名： cart_goodstrench
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:07   N/A    初版
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
	/// cart_goodstrench:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_goodstrench
	{
		public cart_goodstrench()
		{}
		#region Model
		private string _goodstrench_id;
		private string _trench_id;
		private string _goodsid;
		private string _goodsno;
		private string _goodsname;
		private decimal? _uplimit;
		private int? _isdelete;
		private DateTime? _credate;
		private string _cremanid;
		private DateTime? _editdate;
		private string _editmanid;
		private string _goodstrenchid;
		private string _trenchid;
		/// <summary>
		/// 
		/// </summary>
		public string goodstrench_id
		{
			set{ _goodstrench_id=value;}
			get{return _goodstrench_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string trench_id
		{
			set{ _trench_id=value;}
			get{return _trench_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goodsid
		{
			set{ _goodsid=value;}
			get{return _goodsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goodsno
		{
			set{ _goodsno=value;}
			get{return _goodsno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goodsname
		{
			set{ _goodsname=value;}
			get{return _goodsname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? uplimit
		{
			set{ _uplimit=value;}
			get{return _uplimit;}
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
		public string goodstrenchid
		{
			set{ _goodstrenchid=value;}
			get{return _goodstrenchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string trenchid
		{
			set{ _trenchid=value;}
			get{return _trenchid;}
		}
		#endregion Model

	}
}


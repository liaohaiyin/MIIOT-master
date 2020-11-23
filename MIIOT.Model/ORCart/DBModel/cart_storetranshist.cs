/**  版本信息模板在安装目录下，可自行修改。
* cart_storetranshist.cs
*
* 功 能： N/A
* 类 名： cart_storetranshist
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:10   N/A    初版
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
	/// cart_storetranshist:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_storetranshist
	{
		public cart_storetranshist()
		{}
		#region Model
		private string _storetranshistid;
		private string _sourceid;
		private string _sourceno;
		private int? _sourcetype;
		private string _goodsid;
		private string _singlecode;
		private string _cabinetid;
		private string _trenchid;
		private int? _transcationtype;
		private DateTime? _transdate;
		private string _cretransmanid;
		private DateTime? _confirmdate;
		private string _confirmmanid;
		private string _sourcetransid;
		private string _repairstatus;
		private string _departmentname;
		private string _lotno;
		/// <summary>
		/// 
		/// </summary>
		public string storetranshistid
		{
			set{ _storetranshistid=value;}
			get{return _storetranshistid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sourceid
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sourceno
		{
			set{ _sourceno=value;}
			get{return _sourceno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sourcetype
		{
			set{ _sourcetype=value;}
			get{return _sourcetype;}
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
		public string singlecode
		{
			set{ _singlecode=value;}
			get{return _singlecode;}
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
		public string trenchid
		{
			set{ _trenchid=value;}
			get{return _trenchid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? transcationtype
		{
			set{ _transcationtype=value;}
			get{return _transcationtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? transdate
		{
			set{ _transdate=value;}
			get{return _transdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cretransmanid
		{
			set{ _cretransmanid=value;}
			get{return _cretransmanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? confirmdate
		{
			set{ _confirmdate=value;}
			get{return _confirmdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string confirmmanid
		{
			set{ _confirmmanid=value;}
			get{return _confirmmanid;}
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
		public string repairstatus
		{
			set{ _repairstatus=value;}
			get{return _repairstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string departmentname
		{
			set{ _departmentname=value;}
			get{return _departmentname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lotno
		{
			set{ _lotno=value;}
			get{return _lotno;}
		}
		#endregion Model

	}
}


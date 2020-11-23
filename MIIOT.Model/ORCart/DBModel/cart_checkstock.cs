/**  版本信息模板在安装目录下，可自行修改。
* cart_checkstock.cs
*
* 功 能： N/A
* 类 名： cart_checkstock
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
	/// cart_checkstock:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_checkstock
	{
		public cart_checkstock()
		{}
		#region Model
		private string _checkstockid;
		private string _goodsid;
		private string _singlecode;
		private string _cabinetid;
		private string _trenchid;
		private string _cremanid;
		private int? _checktype;
		private DateTime? _checktime;
		private string _operatemaneid;
		private int? _dscstockqty;
		private int? _stockqty;
		private int? _checkqty;
		private int? _checkno;
		private int? _checkstatus;
		private string _wrongsinglecode;
		private string _operatemaneno;
		private string _storehousename;
		private string _lotno;
		private string _factoryname;
		private string _goodsunit;
		private string _cabinetgroupcode;
		private string _goodstype;
		private string _goodsname;
		private string _goodsno;
		/// <summary>
		/// 
		/// </summary>
		public string checkstockid
		{
			set{ _checkstockid=value;}
			get{return _checkstockid;}
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
		public string cremanid
		{
			set{ _cremanid=value;}
			get{return _cremanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? checktype
		{
			set{ _checktype=value;}
			get{return _checktype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? checktime
		{
			set{ _checktime=value;}
			get{return _checktime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string operatemaneid
		{
			set{ _operatemaneid=value;}
			get{return _operatemaneid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? dscstockqty
		{
			set{ _dscstockqty=value;}
			get{return _dscstockqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? stockqty
		{
			set{ _stockqty=value;}
			get{return _stockqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? checkqty
		{
			set{ _checkqty=value;}
			get{return _checkqty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? checkno
		{
			set{ _checkno=value;}
			get{return _checkno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? checkstatus
		{
			set{ _checkstatus=value;}
			get{return _checkstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string wrongsinglecode
		{
			set{ _wrongsinglecode=value;}
			get{return _wrongsinglecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string operatemaneno
		{
			set{ _operatemaneno=value;}
			get{return _operatemaneno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storehousename
		{
			set{ _storehousename=value;}
			get{return _storehousename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lotno
		{
			set{ _lotno=value;}
			get{return _lotno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string factoryname
		{
			set{ _factoryname=value;}
			get{return _factoryname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goodsunit
		{
			set{ _goodsunit=value;}
			get{return _goodsunit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cabinetgroupcode
		{
			set{ _cabinetgroupcode=value;}
			get{return _cabinetgroupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goodstype
		{
			set{ _goodstype=value;}
			get{return _goodstype;}
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
		public string goodsno
		{
			set{ _goodsno=value;}
			get{return _goodsno;}
		}
		#endregion Model

	}
}


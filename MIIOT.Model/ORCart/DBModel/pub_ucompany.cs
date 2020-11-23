/**  版本信息模板在安装目录下，可自行修改。
* pub_ucompany.cs
*
* 功 能： N/A
* 类 名： pub_ucompany
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:15   N/A    初版
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
	/// pub_ucompany:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class pub_ucompany
	{
		public pub_ucompany()
		{}
		#region Model
		private string _ucompanyid;
		private string _ucompanyno;
		private string _ucompanyname;
		private string _ucompanypy;
		private string _ushortname;
		private decimal? _ucompanytype;
		private string _ucompanysum;
		private string _ucompanycode;
		private string _taxnamber;
		private string _legalpersion;
		private string _address;
		private string _postcode;
		private string _linkman;
		private string _linktel;
		private string _linkmobile;
		private string _fax;
		private string _managerange;
		private string _webaddr;
		private decimal? _gmpflag;
		private decimal? _gspflag;
		private decimal? _isoflag;
		private decimal? _corptype;
		private decimal? _customnature;
		private decimal? _economytype;
		private decimal? _qualitytype;
		private decimal? _relationtype;
		private string _comtype;
		private decimal? _companyzoneid;
		private string _companyzone;
		private string _pinstanceid;
		private decimal? _workflowstatus;
		private DateTime? _credate;
		private string _cremanid;
		private string _cremanname;
		private DateTime? _auditdate;
		private string _auditmanid;
		private string _auditmanname;
		private DateTime? _editdate;
		private string _editmanid;
		private string _editmanname;
		private string _memo;
		private string _ucompanyinfo;
		private string _udf1;
		private string _udf2;
		private string _udf3;
		private string _udf4;
		private string _udf5;
		private decimal? _usestatus;
		private decimal? _qccontrolflag;
		/// <summary>
		/// 
		/// </summary>
		public string ucompanyid
		{
			set{ _ucompanyid=value;}
			get{return _ucompanyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ucompanyno
		{
			set{ _ucompanyno=value;}
			get{return _ucompanyno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ucompanyname
		{
			set{ _ucompanyname=value;}
			get{return _ucompanyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ucompanypy
		{
			set{ _ucompanypy=value;}
			get{return _ucompanypy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ushortname
		{
			set{ _ushortname=value;}
			get{return _ushortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ucompanytype
		{
			set{ _ucompanytype=value;}
			get{return _ucompanytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ucompanysum
		{
			set{ _ucompanysum=value;}
			get{return _ucompanysum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ucompanycode
		{
			set{ _ucompanycode=value;}
			get{return _ucompanycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string taxnamber
		{
			set{ _taxnamber=value;}
			get{return _taxnamber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string legalpersion
		{
			set{ _legalpersion=value;}
			get{return _legalpersion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string postcode
		{
			set{ _postcode=value;}
			get{return _postcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string linkman
		{
			set{ _linkman=value;}
			get{return _linkman;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string linktel
		{
			set{ _linktel=value;}
			get{return _linktel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string linkmobile
		{
			set{ _linkmobile=value;}
			get{return _linkmobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string managerange
		{
			set{ _managerange=value;}
			get{return _managerange;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string webaddr
		{
			set{ _webaddr=value;}
			get{return _webaddr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? gmpflag
		{
			set{ _gmpflag=value;}
			get{return _gmpflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? gspflag
		{
			set{ _gspflag=value;}
			get{return _gspflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? isoflag
		{
			set{ _isoflag=value;}
			get{return _isoflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? corptype
		{
			set{ _corptype=value;}
			get{return _corptype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? customnature
		{
			set{ _customnature=value;}
			get{return _customnature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? economytype
		{
			set{ _economytype=value;}
			get{return _economytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? qualitytype
		{
			set{ _qualitytype=value;}
			get{return _qualitytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? relationtype
		{
			set{ _relationtype=value;}
			get{return _relationtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string comtype
		{
			set{ _comtype=value;}
			get{return _comtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? companyzoneid
		{
			set{ _companyzoneid=value;}
			get{return _companyzoneid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string companyzone
		{
			set{ _companyzone=value;}
			get{return _companyzone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pinstanceid
		{
			set{ _pinstanceid=value;}
			get{return _pinstanceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? workflowstatus
		{
			set{ _workflowstatus=value;}
			get{return _workflowstatus;}
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
		public string cremanname
		{
			set{ _cremanname=value;}
			get{return _cremanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? auditdate
		{
			set{ _auditdate=value;}
			get{return _auditdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string auditmanid
		{
			set{ _auditmanid=value;}
			get{return _auditmanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string auditmanname
		{
			set{ _auditmanname=value;}
			get{return _auditmanname;}
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
		public string editmanname
		{
			set{ _editmanname=value;}
			get{return _editmanname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ucompanyinfo
		{
			set{ _ucompanyinfo=value;}
			get{return _ucompanyinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string udf1
		{
			set{ _udf1=value;}
			get{return _udf1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string udf2
		{
			set{ _udf2=value;}
			get{return _udf2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string udf3
		{
			set{ _udf3=value;}
			get{return _udf3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string udf4
		{
			set{ _udf4=value;}
			get{return _udf4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string udf5
		{
			set{ _udf5=value;}
			get{return _udf5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? usestatus
		{
			set{ _usestatus=value;}
			get{return _usestatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? qccontrolflag
		{
			set{ _qccontrolflag=value;}
			get{return _qccontrolflag;}
		}
		#endregion Model

	}
}


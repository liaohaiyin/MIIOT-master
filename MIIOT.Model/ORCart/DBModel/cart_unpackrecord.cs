/**  版本信息模板在安装目录下，可自行修改。
* cart_unpackrecord.cs
*
* 功 能： N/A
* 类 名： cart_unpackrecord
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
	/// cart_unpackrecord:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_unpackrecord
	{
		public cart_unpackrecord()
		{}
		#region Model
		private string _unpackrecordid;
		private string _goodsid;
		private string _singlecode;
		private int? _quantity;
		private string _unpackcode;
		private int? _transcationtype;
		private string _departmentname;
		private string _cabinetid;
		private string _lotno;
		private DateTime? _credate;
		private string _cremanid;
		private string _cremanname;
		/// <summary>
		/// 
		/// </summary>
		public string unpackrecordid
		{
			set{ _unpackrecordid=value;}
			get{return _unpackrecordid;}
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
		public int? quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string unpackcode
		{
			set{ _unpackcode=value;}
			get{return _unpackcode;}
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
		public string departmentname
		{
			set{ _departmentname=value;}
			get{return _departmentname;}
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
		public string lotno
		{
			set{ _lotno=value;}
			get{return _lotno;}
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
		#endregion Model

	}
}


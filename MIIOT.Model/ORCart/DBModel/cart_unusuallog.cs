/**  版本信息模板在安装目录下，可自行修改。
* cart_unusuallog.cs
*
* 功 能： N/A
* 类 名： cart_unusuallog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:12   N/A    初版
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
	/// cart_unusuallog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_unusuallog
	{
		public cart_unusuallog()
		{}
		#region Model
		private DateTime? _createtime;
		private string _operatorid;
		private int? _unusualtype;
		private string _unusualname;
		private string _strategycabinetname;
		private string _localcabinetname;
		private string _lotno;
		private string _cabinetgroupcode;
		private string _factoryname;
		private string _goodstype;
		private string _goodsname;
		private string _goodsno;
		private string _singlecode;
		private string _id;
		private int? _transcationtype;
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
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
		public int? unusualtype
		{
			set{ _unusualtype=value;}
			get{return _unusualtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string unusualname
		{
			set{ _unusualname=value;}
			get{return _unusualname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string strategycabinetname
		{
			set{ _strategycabinetname=value;}
			get{return _strategycabinetname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string localcabinetname
		{
			set{ _localcabinetname=value;}
			get{return _localcabinetname;}
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
		public string cabinetgroupcode
		{
			set{ _cabinetgroupcode=value;}
			get{return _cabinetgroupcode;}
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
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? transcationtype
		{
			set{ _transcationtype=value;}
			get{return _transcationtype;}
		}
		#endregion Model

	}
}


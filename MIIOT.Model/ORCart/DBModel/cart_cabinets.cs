/**  版本信息模板在安装目录下，可自行修改。
* cart_cabinets.cs
*
* 功 能： N/A
* 类 名： cart_cabinets
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
	/// cart_cabinets:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_cabinets
	{
		public cart_cabinets()
		{}
		#region Model
		private string _cabinetid;
		private string _cabinetname;
		private string _cabinetcode;
		private string _storehouseid;
		private string _storehousename;
		private string _cabinetgroupcode;
		private int? _usestatus;
		private int? _cabinetaddress;
		private string _memo;
		private int? _leavel;
		private decimal? _templow;
		private decimal? _temphigh;
		private decimal? _humiditylow;
		private decimal? _humidityhigh;
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
		public string cabinetname
		{
			set{ _cabinetname=value;}
			get{return _cabinetname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cabinetcode
		{
			set{ _cabinetcode=value;}
			get{return _cabinetcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storehouseid
		{
			set{ _storehouseid=value;}
			get{return _storehouseid;}
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
		public string cabinetgroupcode
		{
			set{ _cabinetgroupcode=value;}
			get{return _cabinetgroupcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? usestatus
		{
			set{ _usestatus=value;}
			get{return _usestatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? cabinetaddress
		{
			set{ _cabinetaddress=value;}
			get{return _cabinetaddress;}
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
		public int? leavel
		{
			set{ _leavel=value;}
			get{return _leavel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? templow
		{
			set{ _templow=value;}
			get{return _templow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? temphigh
		{
			set{ _temphigh=value;}
			get{return _temphigh;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? humiditylow
		{
			set{ _humiditylow=value;}
			get{return _humiditylow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? humidityhigh
		{
			set{ _humidityhigh=value;}
			get{return _humidityhigh;}
		}
		#endregion Model

	}
}


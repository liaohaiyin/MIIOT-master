/**  版本信息模板在安装目录下，可自行修改。
* cart_log.cs
*
* 功 能： N/A
* 类 名： cart_log
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
	/// cart_log:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_log
	{
		public cart_log()
		{}
		#region Model
		private string _icmslogid;
		private string _uorganid;
		private string _interfacename;
		private string _cabinetid;
		private string _sourceid;
		private string _sourceno;
		private string _singlecode;
		private int? _logtype;
		private string _errormessage;
		private string _operatorid;
		private DateTime? _operatingtime;
		/// <summary>
		/// 
		/// </summary>
		public string icmslogid
		{
			set{ _icmslogid=value;}
			get{return _icmslogid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string uorganid
		{
			set{ _uorganid=value;}
			get{return _uorganid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string interfacename
		{
			set{ _interfacename=value;}
			get{return _interfacename;}
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
		public string singlecode
		{
			set{ _singlecode=value;}
			get{return _singlecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? logtype
		{
			set{ _logtype=value;}
			get{return _logtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string errormessage
		{
			set{ _errormessage=value;}
			get{return _errormessage;}
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
		public DateTime? operatingtime
		{
			set{ _operatingtime=value;}
			get{return _operatingtime;}
		}
		#endregion Model

	}
}


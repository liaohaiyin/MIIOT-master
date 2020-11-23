/**  版本信息模板在安装目录下，可自行修改。
* interface_log.cs
*
* 功 能： N/A
* 类 名： interface_log
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:14   N/A    初版
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
	/// interface_log:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class interface_log
	{
		public interface_log()
		{}
		#region Model
		private int _logid;
		private string _interfacename;
		private string _loglevel;
		private string _message;
		private string _param;
		private string _accountcode;
		private DateTime? _logtime;
		private int? _interfacestatus;
		/// <summary>
		/// 
		/// </summary>
		public int logid
		{
			set{ _logid=value;}
			get{return _logid;}
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
		public string loglevel
		{
			set{ _loglevel=value;}
			get{return _loglevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string message
		{
			set{ _message=value;}
			get{return _message;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string param
		{
			set{ _param=value;}
			get{return _param;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string accountcode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? logtime
		{
			set{ _logtime=value;}
			get{return _logtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? interfacestatus
		{
			set{ _interfacestatus=value;}
			get{return _interfacestatus;}
		}
		#endregion Model

	}
}


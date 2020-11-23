/**  版本信息模板在安装目录下，可自行修改。
* cart_xmlrecall.cs
*
* 功 能： N/A
* 类 名： cart_xmlrecall
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
	/// cart_xmlrecall:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_xmlrecall
	{
		public cart_xmlrecall()
		{}
		#region Model
		private string _xmlrecallid;
		private string _logid;
		private string _xml;
		private int? _recallflag;
		private string _cabinetgroupcode;
		private DateTime? _recalltime;
		private string _recallmanid;
		private string _recallmanname;
		/// <summary>
		/// 
		/// </summary>
		public string xmlrecallid
		{
			set{ _xmlrecallid=value;}
			get{return _xmlrecallid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string logid
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string xml
		{
			set{ _xml=value;}
			get{return _xml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? recallflag
		{
			set{ _recallflag=value;}
			get{return _recallflag;}
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
		public DateTime? recalltime
		{
			set{ _recalltime=value;}
			get{return _recalltime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string recallmanid
		{
			set{ _recallmanid=value;}
			get{return _recallmanid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string recallmanname
		{
			set{ _recallmanname=value;}
			get{return _recallmanname;}
		}
		#endregion Model

	}
}


/**  版本信息模板在安装目录下，可自行修改。
* cart_singlecode.cs
*
* 功 能： N/A
* 类 名： cart_singlecode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:09   N/A    初版
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
	/// cart_singlecode:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_singlecode
	{
		public cart_singlecode()
		{}
		#region Model
		private string _singlecodeid;
		private string _singlecode;
		private int? _singlestatus;
		private string _sourceid;
		private string _editmanid;
		private string _editmanname;
		private string _batchcode;
		private string _goodsid;
		/// <summary>
		/// 
		/// </summary>
		public string singlecodeid
		{
			set{ _singlecodeid=value;}
			get{return _singlecodeid;}
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
		public int? singlestatus
		{
			set{ _singlestatus=value;}
			get{return _singlestatus;}
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
		public string batchcode
		{
			set{ _batchcode=value;}
			get{return _batchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string goodsid
		{
			set{ _goodsid=value;}
			get{return _goodsid;}
		}
		#endregion Model

	}
}


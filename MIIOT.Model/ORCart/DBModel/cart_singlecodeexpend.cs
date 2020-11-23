/**  版本信息模板在安装目录下，可自行修改。
* cart_singlecodeexpend.cs
*
* 功 能： N/A
* 类 名： cart_singlecodeexpend
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
	/// cart_singlecodeexpend:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_singlecodeexpend
	{
		public cart_singlecodeexpend()
		{}
		#region Model
		private int _singlecodeexpendid;
		private string _singlecode;
		private int? _expendstatus=1;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int singlecodeexpendid
		{
			set{ _singlecodeexpendid=value;}
			get{return _singlecodeexpendid;}
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
		public int? expendstatus
		{
			set{ _expendstatus=value;}
			get{return _expendstatus;}
		}
		#endregion Model

	}
}


/**  版本信息模板在安装目录下，可自行修改。
* cart_cabinetrfip.cs
*
* 功 能： N/A
* 类 名： cart_cabinetrfip
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:06   N/A    初版
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
	/// cart_cabinetrfip:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class cart_cabinetrfip
	{
		public cart_cabinetrfip()
		{}
		#region Model
		private int _cabientrfipid;
		private string _cabinetgroupcode;
		private string _rfip;
		/// <summary>
		/// auto_increment
		/// </summary>
		public int cabientrfipid
		{
			set{ _cabientrfipid=value;}
			get{return _cabientrfipid;}
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
		public string rfip
		{
			set{ _rfip=value;}
			get{return _rfip;}
		}
		#endregion Model

	}
}


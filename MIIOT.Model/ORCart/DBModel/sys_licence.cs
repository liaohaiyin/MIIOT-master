/**  版本信息模板在安装目录下，可自行修改。
* sys_licence.cs
*
* 功 能： N/A
* 类 名： sys_licence
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:27   N/A    初版
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
	/// sys_licence:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_licence
	{
		public sys_licence()
		{}
		#region Model
		private string _productid;
		private byte[] _licence;
		private byte[] _datas;
		private string _description;
		/// <summary>
		/// 
		/// </summary>
		public string productid
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] licence
		{
			set{ _licence=value;}
			get{return _licence;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] datas
		{
			set{ _datas=value;}
			get{return _datas;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion Model

	}
}


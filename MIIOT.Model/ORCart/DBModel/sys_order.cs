/**  版本信息模板在安装目录下，可自行修改。
* sys_order.cs
*
* 功 能： N/A
* 类 名： sys_order
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:31   N/A    初版
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
	/// sys_order:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_order
	{
		public sys_order()
		{}
		#region Model
		private int _id;
		private int? _customerid;
		private DateTime? _order_date;
		private int? _shipped;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? customerid
		{
			set{ _customerid=value;}
			get{return _customerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? order_date
		{
			set{ _order_date=value;}
			get{return _order_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? shipped
		{
			set{ _shipped=value;}
			get{return _shipped;}
		}
		#endregion Model

	}
}


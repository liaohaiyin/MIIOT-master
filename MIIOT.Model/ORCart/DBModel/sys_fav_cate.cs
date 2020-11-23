/**  版本信息模板在安装目录下，可自行修改。
* sys_fav_cate.cs
*
* 功 能： N/A
* 类 名： sys_fav_cate
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:23   N/A    初版
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
	/// sys_fav_cate:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_fav_cate
	{
		public sys_fav_cate()
		{}
		#region Model
		private string _cate_id;
		private string _cate_name;
		private int? _cate_order;
		private string _parent_id;
		private string _user_id;
		/// <summary>
		/// 
		/// </summary>
		public string cate_id
		{
			set{ _cate_id=value;}
			get{return _cate_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cate_name
		{
			set{ _cate_name=value;}
			get{return _cate_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? cate_order
		{
			set{ _cate_order=value;}
			get{return _cate_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string parent_id
		{
			set{ _parent_id=value;}
			get{return _parent_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string user_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		#endregion Model

	}
}


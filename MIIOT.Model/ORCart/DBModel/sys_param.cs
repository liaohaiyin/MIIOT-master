/**  版本信息模板在安装目录下，可自行修改。
* sys_param.cs
*
* 功 能： N/A
* 类 名： sys_param
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:35   N/A    初版
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
	/// sys_param:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_param
	{
		public sys_param()
		{}
        #region Model
        public int id { get; set; }
        public string paramtype { get; set; }
        private string _paramkey;
		private string _paramlabel;
		private string _defparamvalue;
		private string _memo;
		/// <summary>
		/// 
		/// </summary>
		public string paramkey
		{
			set{ _paramkey=value;}
			get{return _paramkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string paramlabel
		{
			set{ _paramlabel=value;}
			get{return _paramlabel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string defparamvalue
		{
			set{ _defparamvalue=value;}
			get{return _defparamvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}


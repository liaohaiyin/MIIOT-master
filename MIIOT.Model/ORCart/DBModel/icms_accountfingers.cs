/**  版本信息模板在安装目录下，可自行修改。
* icms_accountfingers.cs
*
* 功 能： N/A
* 类 名： icms_accountfingers
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
	/// icms_accountfingers:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class icms_accountfingers
	{
		public icms_accountfingers()
		{}
		#region Model
		private int _fingersprintsid;
		private string _accountid;
		private int? _usestatus;
		private string _cabinetgroupid;
		/// <summary>
		/// 
		/// </summary>
		public int fingersprintsid
		{
			set{ _fingersprintsid=value;}
			get{return _fingersprintsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string accountid
		{
			set{ _accountid=value;}
			get{return _accountid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? usestatus
		{
			set{ _usestatus=value;}
			get{return _usestatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cabinetgroupid
		{
			set{ _cabinetgroupid=value;}
			get{return _cabinetgroupid;}
		}
		#endregion Model

	}
}


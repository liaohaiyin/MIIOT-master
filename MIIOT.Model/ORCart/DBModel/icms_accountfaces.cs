/**  版本信息模板在安装目录下，可自行修改。
* icms_accountfaces.cs
*
* 功 能： N/A
* 类 名： icms_accountfaces
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:13   N/A    初版
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
	/// icms_accountfaces:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class icms_accountfaces
	{
		public icms_accountfaces()
		{}
		#region Model
		private DateTime? _createtime;
		private int _faceid;
		private string _accountid;
		private string _accountcode;
		private string _cabinetgroupcode;
		private string _feature;
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int faceid
		{
			set{ _faceid=value;}
			get{return _faceid;}
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
		public string accountcode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
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
		public string feature
		{
			set{ _feature=value;}
			get{return _feature;}
		}
		#endregion Model

	}
}


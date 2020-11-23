/**  版本信息模板在安装目录下，可自行修改。
* sys_ud_attach.cs
*
* 功 能： N/A
* 类 名： sys_ud_attach
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:43   N/A    初版
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
	/// sys_ud_attach:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_ud_attach
	{
		public sys_ud_attach()
		{}
		#region Model
		private string _attach_id;
		private string _file_name;
		private string _custom_name;
		private string _table_id;
		private string _table_name;
		private string _file_type;
		private int? _file_size;
		private string _creator;
		private DateTime? _creattime;
		private DateTime? _edittime;
		private int _cover=0;
		/// <summary>
		/// 
		/// </summary>
		public string attach_id
		{
			set{ _attach_id=value;}
			get{return _attach_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_name
		{
			set{ _file_name=value;}
			get{return _file_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string custom_name
		{
			set{ _custom_name=value;}
			get{return _custom_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string table_id
		{
			set{ _table_id=value;}
			get{return _table_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string table_name
		{
			set{ _table_name=value;}
			get{return _table_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string file_type
		{
			set{ _file_type=value;}
			get{return _file_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? file_size
		{
			set{ _file_size=value;}
			get{return _file_size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? creattime
		{
			set{ _creattime=value;}
			get{return _creattime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? edittime
		{
			set{ _edittime=value;}
			get{return _edittime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int cover
		{
			set{ _cover=value;}
			get{return _cover;}
		}
		#endregion Model

	}
}


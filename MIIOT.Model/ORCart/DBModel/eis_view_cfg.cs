/**  版本信息模板在安装目录下，可自行修改。
* eis_view_cfg.cs
*
* 功 能： N/A
* 类 名： eis_view_cfg
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
	/// eis_view_cfg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class eis_view_cfg
	{
		public eis_view_cfg()
		{}
		#region Model
		private string _cfg_id;
		private string _cfg_title;
		private string _cfg_no;
		private string _cfg_sql;
		private string _group_col;
		private long? _since_time;
		private long? _mil_interval;
		private string _service_type;
		private string _service_param;
		private int? _cfg_enable=0;
		private string _data_source_id;
		/// <summary>
		/// 
		/// </summary>
		public string cfg_id
		{
			set{ _cfg_id=value;}
			get{return _cfg_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cfg_title
		{
			set{ _cfg_title=value;}
			get{return _cfg_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cfg_no
		{
			set{ _cfg_no=value;}
			get{return _cfg_no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cfg_sql
		{
			set{ _cfg_sql=value;}
			get{return _cfg_sql;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string group_col
		{
			set{ _group_col=value;}
			get{return _group_col;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? since_time
		{
			set{ _since_time=value;}
			get{return _since_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? mil_interval
		{
			set{ _mil_interval=value;}
			get{return _mil_interval;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string service_type
		{
			set{ _service_type=value;}
			get{return _service_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string service_param
		{
			set{ _service_param=value;}
			get{return _service_param;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? cfg_enable
		{
			set{ _cfg_enable=value;}
			get{return _cfg_enable;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string data_source_id
		{
			set{ _data_source_id=value;}
			get{return _data_source_id;}
		}
		#endregion Model

	}
}


/**  版本信息模板在安装目录下，可自行修改。
* sys_logger.cs
*
* 功 能： N/A
* 类 名： sys_logger
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:28   N/A    初版
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
	/// sys_logger:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_logger
	{
		public sys_logger()
		{}
		#region Model
		private int _log_id;
		private string _account_id;
		private string _account_code;
		private string _session_id;
		private string _utid;
		private DateTime? _log_time;
		private long? _log_millisecond;
		private string _log_level;
		private string _log_category;
		private string _log_message;
		private string _log_exception;
		private string _log_path;
		private string _log_line;
		private int? _oper_id;
		private string _log_act_process_def_id;
		private string _log_act_execution_id;
		private string _log_act_business_key;
		private string _log_act_process_instance_id;
		private string _log_ip;
		private string _server_ip;
		private string _server_name;
		private string _udf_param;
		/// <summary>
		/// 
		/// </summary>
		public int log_id
		{
			set{ _log_id=value;}
			get{return _log_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_id
		{
			set{ _account_id=value;}
			get{return _account_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_code
		{
			set{ _account_code=value;}
			get{return _account_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string session_id
		{
			set{ _session_id=value;}
			get{return _session_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string utid
		{
			set{ _utid=value;}
			get{return _utid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? log_time
		{
			set{ _log_time=value;}
			get{return _log_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? log_millisecond
		{
			set{ _log_millisecond=value;}
			get{return _log_millisecond;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_level
		{
			set{ _log_level=value;}
			get{return _log_level;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_category
		{
			set{ _log_category=value;}
			get{return _log_category;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_message
		{
			set{ _log_message=value;}
			get{return _log_message;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_exception
		{
			set{ _log_exception=value;}
			get{return _log_exception;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_path
		{
			set{ _log_path=value;}
			get{return _log_path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_line
		{
			set{ _log_line=value;}
			get{return _log_line;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? oper_id
		{
			set{ _oper_id=value;}
			get{return _oper_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_act_process_def_id
		{
			set{ _log_act_process_def_id=value;}
			get{return _log_act_process_def_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_act_execution_id
		{
			set{ _log_act_execution_id=value;}
			get{return _log_act_execution_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_act_business_key
		{
			set{ _log_act_business_key=value;}
			get{return _log_act_business_key;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_act_process_instance_id
		{
			set{ _log_act_process_instance_id=value;}
			get{return _log_act_process_instance_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string log_ip
		{
			set{ _log_ip=value;}
			get{return _log_ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string server_ip
		{
			set{ _server_ip=value;}
			get{return _server_ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string server_name
		{
			set{ _server_name=value;}
			get{return _server_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string udf_param
		{
			set{ _udf_param=value;}
			get{return _udf_param;}
		}
		#endregion Model

	}
}


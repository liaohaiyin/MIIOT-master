/**  版本信息模板在安装目录下，可自行修改。
* sys_outcoming.cs
*
* 功 能： N/A
* 类 名： sys_outcoming
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:34   N/A    初版
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
	/// sys_outcoming:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_outcoming
	{
		public sys_outcoming()
		{}
		#region Model
		private string _outcoming_id;
		private string _operator;
		private string _auth_info;
		private string _service_path;
		private string _service_param;
		private string _call_type;
		private string _serverid;
		private string _server_info;
		private int? _call_status;
		private string _return_val;
		private string _error_msg;
		private long? _start_time;
		private long? _end_time;
		private int? _time_used;
		private string _callback_type;
		private string _callback_info;
		private string _error_callback_type;
		private string _error_callback_info;
		/// <summary>
		/// 
		/// </summary>
		public string outcoming_id
		{
			set{ _outcoming_id=value;}
			get{return _outcoming_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string @operator
		{
			set{ _operator=value;}
			get{return _operator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string auth_info
		{
			set{ _auth_info=value;}
			get{return _auth_info;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string service_path
		{
			set{ _service_path=value;}
			get{return _service_path;}
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
		public string call_type
		{
			set{ _call_type=value;}
			get{return _call_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string serverid
		{
			set{ _serverid=value;}
			get{return _serverid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string server_info
		{
			set{ _server_info=value;}
			get{return _server_info;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? call_status
		{
			set{ _call_status=value;}
			get{return _call_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string return_val
		{
			set{ _return_val=value;}
			get{return _return_val;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string error_msg
		{
			set{ _error_msg=value;}
			get{return _error_msg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? start_time
		{
			set{ _start_time=value;}
			get{return _start_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? end_time
		{
			set{ _end_time=value;}
			get{return _end_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? time_used
		{
			set{ _time_used=value;}
			get{return _time_used;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string callback_type
		{
			set{ _callback_type=value;}
			get{return _callback_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string callback_info
		{
			set{ _callback_info=value;}
			get{return _callback_info;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string error_callback_type
		{
			set{ _error_callback_type=value;}
			get{return _error_callback_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string error_callback_info
		{
			set{ _error_callback_info=value;}
			get{return _error_callback_info;}
		}
		#endregion Model

	}
}


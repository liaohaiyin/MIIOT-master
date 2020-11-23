/**  版本信息模板在安装目录下，可自行修改。
* sys_account.cs
*
* 功 能： N/A
* 类 名： sys_account
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:16   N/A    初版
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
	/// sys_account:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class sys_account
	{
		public sys_account()
		{}
		#region Model
		private long _account_id;
		private DateTime? _account_creattime;
		private string _account_mobile;
		private int? _account_sex;
		private string _account_code;
		private string _account_password;
		private string _account_position;
		private string _account_address;
		private int? _account_deleted=0;
		private string _account_email;
		private DateTime? _account_invalid_date;
		private string _account_superior;
		private int? _account_type;
		private string _account_identityid;
		private int? _account_status;
		private string _account_name;
		private int? _account_enabled=1;
		private string _account_remark;
		private string _account_phone;
		private string _account_postalcode;
		private string _account_creator;
		private string _account_mender;
		private DateTime? _account_mendtime;
		private DateTime? _last_login_date;
		private string _last_login_ip;
		private string _last_login_area;
		private string _opcode;
		private string _spell;
		private string _email1;
		private string _email2;
		private string _email3;
		private string _phone;
		private string _barcode;
		private string _duty;
		private string _qqno;
		private DateTime? _joindate;
		private DateTime? _leavedate;
		private string _memo;
		private DateTime? _birthday;
		private string _creator;
		private DateTime? _createtime;
		private string _openid;
		private string _storehouseid;
		private string _dactylogramid;
		private string _cardcode;
		private long? _emergentflag;
		private string _fingersprintgroupsid;
		private string _sourceid;
		private string _face_recogintion;
		private string _uniquecode;
		private string _departmentname;
		private int? _roletype;
		/// <summary>
		/// 
		/// </summary>
		public long account_id
		{
			set{ _account_id=value;}
			get{return _account_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? account_creattime
		{
			set{ _account_creattime=value;}
			get{return _account_creattime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_mobile
		{
			set{ _account_mobile=value;}
			get{return _account_mobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? account_sex
		{
			set{ _account_sex=value;}
			get{return _account_sex;}
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
		public string account_password
		{
			set{ _account_password=value;}
			get{return _account_password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_position
		{
			set{ _account_position=value;}
			get{return _account_position;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_address
		{
			set{ _account_address=value;}
			get{return _account_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? account_deleted
		{
			set{ _account_deleted=value;}
			get{return _account_deleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_email
		{
			set{ _account_email=value;}
			get{return _account_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? account_invalid_date
		{
			set{ _account_invalid_date=value;}
			get{return _account_invalid_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_superior
		{
			set{ _account_superior=value;}
			get{return _account_superior;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? account_type
		{
			set{ _account_type=value;}
			get{return _account_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_identityid
		{
			set{ _account_identityid=value;}
			get{return _account_identityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? account_status
		{
			set{ _account_status=value;}
			get{return _account_status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_name
		{
			set{ _account_name=value;}
			get{return _account_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? account_enabled
		{
			set{ _account_enabled=value;}
			get{return _account_enabled;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_remark
		{
			set{ _account_remark=value;}
			get{return _account_remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_phone
		{
			set{ _account_phone=value;}
			get{return _account_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_postalcode
		{
			set{ _account_postalcode=value;}
			get{return _account_postalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_creator
		{
			set{ _account_creator=value;}
			get{return _account_creator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string account_mender
		{
			set{ _account_mender=value;}
			get{return _account_mender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? account_mendtime
		{
			set{ _account_mendtime=value;}
			get{return _account_mendtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? last_login_date
		{
			set{ _last_login_date=value;}
			get{return _last_login_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string last_login_ip
		{
			set{ _last_login_ip=value;}
			get{return _last_login_ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string last_login_area
		{
			set{ _last_login_area=value;}
			get{return _last_login_area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string opcode
		{
			set{ _opcode=value;}
			get{return _opcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string spell
		{
			set{ _spell=value;}
			get{return _spell;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email1
		{
			set{ _email1=value;}
			get{return _email1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email2
		{
			set{ _email2=value;}
			get{return _email2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string email3
		{
			set{ _email3=value;}
			get{return _email3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string barcode
		{
			set{ _barcode=value;}
			get{return _barcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string duty
		{
			set{ _duty=value;}
			get{return _duty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string qqno
		{
			set{ _qqno=value;}
			get{return _qqno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? joindate
		{
			set{ _joindate=value;}
			get{return _joindate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? leavedate
		{
			set{ _leavedate=value;}
			get{return _leavedate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
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
		public DateTime? createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string openid
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string storehouseid
		{
			set{ _storehouseid=value;}
			get{return _storehouseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dactylogramid
		{
			set{ _dactylogramid=value;}
			get{return _dactylogramid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cardcode
		{
			set{ _cardcode=value;}
			get{return _cardcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? emergentflag
		{
			set{ _emergentflag=value;}
			get{return _emergentflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fingersprintgroupsid
		{
			set{ _fingersprintgroupsid=value;}
			get{return _fingersprintgroupsid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sourceid
		{
			set{ _sourceid=value;}
			get{return _sourceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string face_recogintion
		{
			set{ _face_recogintion=value;}
			get{return _face_recogintion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string uniquecode
		{
			set{ _uniquecode=value;}
			get{return _uniquecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string departmentname
		{
			set{ _departmentname=value;}
			get{return _departmentname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? roletype
		{
			set{ _roletype=value;}
			get{return _roletype;}
		}
		#endregion Model

	}
}


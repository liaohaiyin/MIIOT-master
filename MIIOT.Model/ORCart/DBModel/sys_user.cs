/**  版本信息模板在安装目录下，可自行修改。
* sys_user.cs
*
* 功 能： N/A
* 类 名： sys_user
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:40:12   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;

namespace MIIOT.Model
{
    /// <summary>
    /// sys_user:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class sys_user
    {
        public sys_user()
        { }
        #region Model
        private string _id;
        private long? _organ_id = 0;
        private string _user_source_id;
        private string _user_code;
        private string _user_name;
        private string _password;
        private string _display_name;
        private int? _user_status = 0;
        private int? _is_delete = 0;
        private long? _creater_id = 0;
        private DateTime? _gmt_create = DateTime.Now;
        private long? _modifier_id = 0;
        private DateTime? _gmt_modified = DateTime.Now;
        private string _origin_id;
        private string _user_department_id;
        private string _user_department_name;
        /// <summary>
        /// 
        /// </summary>
        public string id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? organ_id
        {
            set { _organ_id = value; }
            get { return _organ_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_source_id
        {
            set { _user_source_id = value; }
            get { return _user_source_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_code
        {
            set { _user_code = value; }
            get { return _user_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string display_name
        {
            set { _display_name = value; }
            get { return _display_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? user_status
        {
            set { _user_status = value; }
            get { return _user_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? is_delete
        {
            set { _is_delete = value; }
            get { return _is_delete; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? creater_id
        {
            set { _creater_id = value; }
            get { return _creater_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? gmt_create
        {
            set { _gmt_create = value; }
            get { return _gmt_create; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? modifier_id
        {
            set { _modifier_id = value; }
            get { return _modifier_id; }
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>
        public DateTime? gmt_modified
        {
            set { _gmt_modified = value; }
            get { return _gmt_modified; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string origin_id
        {
            set { _origin_id = value; }
            get { return _origin_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_department_id
        {
            set { _user_department_id = value; }
            get { return _user_department_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string user_department_name
        {
            set { _user_department_name = value; }
            get { return _user_department_name; }
        }
        public string sotcks { get; set; }
        public int role { get; set; }
        public string idcard { get; set; }
        public string idface { get; set; }

        #endregion Model

    }
    public class sys_face
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public byte[] img { get; set; }
    }
}


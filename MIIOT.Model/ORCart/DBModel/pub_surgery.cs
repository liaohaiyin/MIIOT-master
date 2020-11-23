/**  版本信息模板在安装目录下，可自行修改。
* pub_surgery.cs
*
* 功 能： N/A
* 类 名： pub_surgery
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2020/6/24 11:17:15   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using MIIOT.Model.ORCart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MIIOT.Model
{
    /// <summary>
    /// pub_surgery:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pub_surgery : BaseNotify
    {
        public pub_surgery()
        { }
        #region Model
        private string _id;
        private long? _organ_id = 0;
        private string _origin_id;
        private string _surgery_code;
        private string _surgery_name;
        private string _dept;
        private string _doctor;
        private string _patient;
        private DateTime? _surgery_time = DateTime.Now;
        private int? _is_delete = 0;
        private long? _creater_id = 0;
        private DateTime? _gmt_create = DateTime.Now;
        private long? _modifier_id = 0;
        private DateTime? _gmt_modified = DateTime.Now;
        /// <summary>
        /// auto_increment
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
        public string origin_id
        {
            set { _origin_id = value; }
            get { return _origin_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string surgery_code
        {
            set { _surgery_code = value;
                OnPropertyChanged("surgery_code");
            }
            get
            {
                return _surgery_code;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string surgery_name
        {
            set { _surgery_name = value;
                OnPropertyChanged("surgery_name");
            }
            get { return _surgery_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dept
        {
            set { _dept = value;
                OnPropertyChanged("dept");
            }
            get { return _dept; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string doctor
        {
            set { _doctor = value;
                OnPropertyChanged("doctor");
            }
            get { return _doctor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string patient
        {
            set { _patient = value;
                OnPropertyChanged("patient");
            }
            get { return _patient; }
        }

        private string _roomName;

        public string room_name
        {
            get { return _roomName; }
            set { _roomName = value;
                OnPropertyChanged("room_name");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? surgery_time
        {
            set { _surgery_time = value;
                OnPropertyChanged("surgery_time");
            }
            get { return _surgery_time; }
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
        #endregion Model
        public string case_no { get; set; }
        [Description("External")]
        public List<ClearGroup> ClearGroups { get; set; } = new List<ClearGroup>();


    }
    public class ClearGroup :BaseNotify
    {
        public string groupName { get; set; }
        private ObservableCollection<cart_clear> _Clears = new ObservableCollection<cart_clear>();

        public ObservableCollection<cart_clear> Clears
        {
            get
            {
                return _Clears;
            }
            set
            {
                _Clears = value;
                OnPropertyChanged("Clears");
            }
        }
    }

}


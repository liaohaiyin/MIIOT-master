﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 退库单
    /// </summary>
    public class pub_apply_back : BaseEntity, ISort
    {
        protected int _sortNo;
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int SortNo
        {
            get { return _sortNo; }
            set { _sortNo = value; OnPropertyChanged(nameof(SortNo)); }
        }

        protected int _organ_id;
        /// <summary>
        /// 机构ID
        /// </summary>
        public virtual int organ_id
        {
            get { return _organ_id; }
            set { _organ_id = value; OnPropertyChanged(nameof(organ_id)); }
        }

        protected int _source_type;
        /// <summary>
        /// 0=中台 1=手工
        /// </summary>
        public virtual int source_type
        {
            get { return _source_type; }
            set { _source_type = value; OnPropertyChanged(nameof(source_type)); }
        }

        protected Int64 _source_id;
        /// <summary>
        /// 来源单ID
        /// </summary>
        public virtual Int64 source_id
        {
            get { return _source_id; }
            set { _source_id = value; OnPropertyChanged(nameof(source_id)); }
        }

        protected string _source_no;
        /// <summary>
        /// 来源单号
        /// </summary>
        public virtual string source_no
        {
            get { return _source_no; }
            set { _source_no = value; OnPropertyChanged(nameof(source_no)); }
        }

        protected string _origin_id;
        /// <summary>
        /// 原始单ID
        /// </summary>
        public virtual string origin_id
        {
            get { return _origin_id; }
            set { _origin_id = value; OnPropertyChanged(nameof(origin_id)); }
        }

        protected Int64 _depm_id;
        /// <summary>
        /// 申退科室ID
        /// </summary>
        public virtual Int64 depm_id
        {
            get { return _depm_id; }
            set { _depm_id = value; OnPropertyChanged(nameof(depm_id)); }
        }

        protected string _depm_name;
        /// <summary>
        /// 申退科室
        /// </summary>
        public virtual string depm_name
        {
            get { return _depm_name; }
            set { _depm_name = value; OnPropertyChanged(nameof(depm_name)); }
        }

        protected Int64 _source_storehouse_id;
        /// <summary>
        /// 申退库房ID
        /// </summary>
        public virtual Int64 source_storehouse_id
        {
            get { return _source_storehouse_id; }
            set { _source_storehouse_id = value; OnPropertyChanged(nameof(source_storehouse_id)); }
        }

        protected string _source_storehouse_name;
        /// <summary>
        /// 申退库房
        /// </summary>
        public virtual string source_storehouse_name
        {
            get { return _source_storehouse_name; }
            set { _source_storehouse_name = value; OnPropertyChanged(nameof(source_storehouse_name)); }
        }

        protected Int64 _target_storehouse_id;
        /// <summary>
        /// 退回库房ID
        /// </summary>
        public virtual Int64 target_storehouse_id
        {
            get { return _target_storehouse_id; }
            set { _target_storehouse_id = value; OnPropertyChanged(nameof(target_storehouse_id)); }
        }

        protected string _target_storehouse_name;
        /// <summary>
        /// 退回库房
        /// </summary>
        public virtual string target_storehouse_name
        {
            get { return _target_storehouse_name; }
            set { _target_storehouse_name = value; OnPropertyChanged(nameof(target_storehouse_name)); }
        }

        protected int _check_person_id;
        /// <summary>
        /// 复核人ID
        /// </summary>
        public virtual int check_person_id
        {
            get { return _check_person_id; }
            set { _check_person_id = value; OnPropertyChanged(nameof(check_person_id)); }
        }

        protected string _check_person_name;
        /// <summary>
        /// 复核人
        /// </summary>
        public virtual string check_person_name
        {
            get { return _check_person_name; }
            set { _check_person_name = value; OnPropertyChanged(nameof(check_person_name)); }
        }

        protected int _status;
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(nameof(status)); }
        }

        protected int _is_delete;
        /// <summary>
        /// 删除状态
        /// </summary>
        public virtual int is_delete
        {
            get { return _is_delete; }
            set { _is_delete = value; OnPropertyChanged(nameof(is_delete)); }
        }

        protected int _creater_id;
        /// <summary>
        /// 创建人id
        /// </summary>
        public virtual int creater_id
        {
            get { return _creater_id; }
            set { _creater_id = value; OnPropertyChanged(nameof(creater_id)); }
        }

        protected DateTime _gmt_create;
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime gmt_create
        {
            get { return _gmt_create; }
            set { _gmt_create = value; OnPropertyChanged(nameof(gmt_create)); }
        }

        protected int _modifier_id;
        /// <summary>
        /// 修改人id
        /// </summary>
        public virtual int modifier_id
        {
            get { return _modifier_id; }
            set { _modifier_id = value; OnPropertyChanged(nameof(modifier_id)); }
        }

        protected DateTime _gmt_modified;
        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime gmt_modified
        {
            get { return _gmt_modified; }
            set { _gmt_modified = value; OnPropertyChanged(nameof(gmt_modified)); }
        }
    }

}

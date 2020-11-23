using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 申领
    /// </summary>
    public class pub_apply : BaseEntity, ISort
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
        /// 0=送货单
        /// </summary>
        public virtual int source_type
        {
            get { return _source_type; }
            set { _source_type = value; OnPropertyChanged(nameof(source_type)); }
        }

        protected int _source_id;
        /// <summary>
        /// 来源单ID
        /// </summary>
        public virtual int source_id
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

        protected string _depm_id;
        /// <summary>
        /// 申领科室ID
        /// </summary>
        public virtual string depm_id
        {
            get { return _depm_id; }
            set { _depm_id = value; OnPropertyChanged(nameof(depm_id)); }
        }

        protected string _depm_name;
        /// <summary>
        /// 申领科室
        /// </summary>
        public virtual string depm_name
        {
            get { return _depm_name; }
            set { _depm_name = value; OnPropertyChanged(nameof(depm_name)); }
        }

        protected int _apply_storehouse_id;
        /// <summary>
        /// 申领科室ID
        /// </summary>
        public virtual int apply_storehouse_id
        {
            get { return _apply_storehouse_id; }
            set { _apply_storehouse_id = value; OnPropertyChanged(nameof(apply_storehouse_id)); }
        }

        protected string _apply_storehouse_name;
        /// <summary>
        /// 申领库房
        /// </summary>
        public virtual string apply_storehouse_name
        {
            get { return _apply_storehouse_name; }
            set { _apply_storehouse_name = value; OnPropertyChanged(nameof(apply_storehouse_name)); }
        }

        protected int _storehouse_id;
        /// <summary>
        /// 出库库房ID
        /// </summary>
        public virtual int storehouse_id
        {
            get { return _storehouse_id; }
            set { _storehouse_id = value; OnPropertyChanged(nameof(storehouse_id)); }
        }

        protected string _storehouse_name;
        /// <summary>
        /// 出库库房
        /// </summary>
        public virtual string storehouse_name
        {
            get { return _storehouse_name; }
            set { _storehouse_name = value; OnPropertyChanged(nameof(storehouse_name)); }
        }

        protected string _apply_name;
        /// <summary>
        /// 申领人
        /// </summary>
        public virtual string apply_name
        {
            get { return _apply_name; }
            set { _apply_name = value; OnPropertyChanged(nameof(apply_name)); }
        }

        protected DateTime _apply_time;
        /// <summary>
        /// 申领时间
        /// </summary>
        public virtual DateTime apply_time
        {
            get { return _apply_time; }
            set { _apply_time = value; OnPropertyChanged(nameof(apply_time)); }
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

    public class view_pub_apply : pub_apply
    {
        protected string _organ_name;
        /// <summary>
        /// 医院名称
        /// </summary>
        public virtual string organ_name
        {
            get { return _organ_name; }
            set { _organ_name = value; OnPropertyChanged(nameof(organ_name)); }
        }

    }
}

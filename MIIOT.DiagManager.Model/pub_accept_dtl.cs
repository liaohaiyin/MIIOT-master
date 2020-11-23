using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    public class pub_accept_dtl : BaseEntity, ISort
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

        protected Int64 _accept_id;
        /// <summary>
        /// 验收表ID
        /// </summary>
        public virtual Int64 accept_id
        {
            get { return _accept_id; }
            set { _accept_id = value; OnPropertyChanged(nameof(accept_id)); }
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

        protected Int64 _source_id;
        /// <summary>
        /// 来源单ID
        /// </summary>
        public virtual Int64 source_id
        {
            get { return _source_id; }
            set { _source_id = value; OnPropertyChanged(nameof(source_id)); }
        }

        protected Int64 _source_dtl_id;
        /// <summary>
        /// 来源明细ID
        /// </summary>
        public virtual Int64 source_dtl_id
        {
            get { return _source_dtl_id; }
            set { _source_dtl_id = value; OnPropertyChanged(nameof(source_dtl_id)); }
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

        protected string _origin_dtl_id;
        /// <summary>
        /// 原始明细ID
        /// </summary>
        public virtual string origin_dtl_id
        {
            get { return _origin_dtl_id; }
            set { _origin_dtl_id = value; OnPropertyChanged(nameof(origin_dtl_id)); }
        }

        protected int _goods_id;
        /// <summary>
        /// 商品ID
        /// </summary>
        public virtual int goods_id
        {
            get { return _goods_id; }
            set { _goods_id = value; OnPropertyChanged(nameof(goods_id)); }
        }

        protected string _goods_no;
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual string goods_no
        {
            get { return _goods_no; }
            set { _goods_no = value; OnPropertyChanged(nameof(goods_no)); }
        }

        protected string _goods_name;
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string goods_name
        {
            get { return _goods_name; }
            set { _goods_name = value; OnPropertyChanged(nameof(goods_name)); }
        }

        protected string _goods_spec;
        /// <summary>
        /// 商品规格
        /// </summary>
        public virtual string goods_spec
        {
            get { return _goods_spec; }
            set { _goods_spec = value; OnPropertyChanged(nameof(goods_spec)); }
        }

        protected string _goods_factory_name;
        /// <summary>
        /// 生产厂家
        /// </summary>
        public virtual string goods_factory_name
        {
            get { return _goods_factory_name; }
            set { _goods_factory_name = value; OnPropertyChanged(nameof(goods_factory_name)); }
        }

        protected string _goods_unit;
        /// <summary>
        /// 单位
        /// </summary>
        public virtual string goods_unit
        {
            get { return _goods_unit; }
            set { _goods_unit = value; OnPropertyChanged(nameof(goods_unit)); }
        }

        protected int _delivery_qty;
        /// <summary>
        /// 送货数量
        /// </summary>
        public virtual int delivery_qty
        {
            get { return _delivery_qty; }
            set { _delivery_qty = value; OnPropertyChanged(nameof(delivery_qty)); }
        }

        protected int _check_qty;
        /// <summary>
        /// 复核数量
        /// </summary>
        public virtual int check_qty
        {
            get { return _check_qty; }
            set { _check_qty = value; OnPropertyChanged(nameof(check_qty)); }
        }

        protected Int64 _lot_id;
        /// <summary>
        /// 批号ID
        /// </summary>
        public virtual Int64 lot_id
        {
            get { return _lot_id; }
            set { _lot_id = value; OnPropertyChanged(nameof(lot_id)); }
        }

        protected string _lot_no;
        /// <summary>
        /// 批号
        /// </summary>
        public virtual string lot_no
        {
            get { return _lot_no; }
            set { _lot_no = value; OnPropertyChanged(nameof(lot_no)); }
        }

        protected Int64 _batch_id;
        /// <summary>
        /// 批次
        /// </summary>
        public virtual Int64 batch_id
        {
            get { return _batch_id; }
            set { _batch_id = value; OnPropertyChanged(nameof(batch_id)); }
        }

        protected float _price;
        /// <summary>
        /// 单价
        /// </summary>
        public virtual float price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(price)); }
        }

        protected DateTime _pprod_date;
        /// <summary>
        /// 生产日期
        /// </summary>
        public virtual DateTime pprod_date
        {
            get { return _pprod_date; }
            set { _pprod_date = value; OnPropertyChanged(nameof(pprod_date)); }
        }

        protected DateTime _pvalid_date;
        /// <summary>
        /// 有效日期至
        /// </summary>
        public virtual DateTime pvalid_date
        {
            get { return _pvalid_date; }
            set { _pvalid_date = value; OnPropertyChanged(nameof(pvalid_date)); }
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

        protected string _batch_no;
        /// <summary>
        /// 批次编码
        /// </summary>
        public virtual string batch_no
        {
            get { return _batch_no; }
            set { _batch_no = value; OnPropertyChanged(nameof(batch_no)); }
        }
    }
}

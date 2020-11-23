using System;
using System.ComponentModel;

namespace MIIOT.Model
{
    /// <summary>
    /// cart_replenishdtl:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class cart_replenishdtl : BaseRecord
    {
        public cart_replenishdtl()
        { }
        #region Model
        private long _id;
        private long _goods_id;
        private string _goods_no;
        private string _goods_name;
        private string _goods_spec;
        private string _unit;
        private string _factory_name;
        public DateTime validity_date { get; set; }
        private int _replenish_qty;
        private int _old_replenish_qty = 0;
        private int _need_replenish_qty;
        private int _new_replenish_qty = 0;

        public long replenish_id { get; set; }
        /// <summary>
        /// auto_increment
        /// </summary>
        public long id
        {
            set { _id = value; }
            get { return _id; }
        }
        public long goods_id
        {
            set { _goods_id = value; }
            get { return _goods_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goods_no
        {
            set { _goods_no = value; }
            get { return _goods_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goods_name
        {
            set { _goods_name = value; }
            get { return _goods_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goods_spec
        {
            set { _goods_spec = value; }
            get { return _goods_spec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string factory_name
        {
            set { _factory_name = value; }
            get { return _factory_name; }
        }

        public int replenish_qty
        {
            set { _replenish_qty = value; }
            get { return _replenish_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int old_replenish_qty
        {
            set
            {
                _old_replenish_qty = value;
            }
            get { return _old_replenish_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int need_replenish_qty
        {
            set { _need_replenish_qty = value; }
            get { return _need_replenish_qty; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int new_replenish_qty
        {
            set
            {
                _new_replenish_qty = value;
                OnPropertyChanged("new_replenish_qty");
            }
            get
            {
                return _new_replenish_qty;
            }
        }

        #endregion Model
        [Description("External")]
        public bool IsEdit { get; set; }
        private bool qtyErr;

        [Description("External")]
        public bool QtyErr
        {
            get { return qtyErr; }
            set
            {
                qtyErr = value;
                OnPropertyChanged("QtyErr");
            }
        }

      

        private string _replenish_no;
        [Description("External")]
        public string replenish_no
        {
            set { _replenish_no = value; }
            get { return _replenish_no; }
        }


        private string _cart_id;
        [Description("External")]
        public string cart_id
        {
            set { _cart_id = value; }
            get { return _cart_id; }
        }
        private string _cart_name;
        [Description("External")]

        public string cart_name
        {
            get { return _cart_name; }
            set { _cart_name = value; }
        }

        private int _replenish_status;
        [Description("External")]

        public int replenish_status
        {
            get { return _replenish_status; }
            set { _replenish_status = value; }
        }

        private long _out_storehouse_id;
        [Description("External")]

        public long out_storehouse_id
        {
            get { return _out_storehouse_id; }
            set { _out_storehouse_id = value; }
        }
        private string _out_storehouse_name;
        [Description("External")]

        public string out_storehouse_name
        {
            get { return _out_storehouse_name; }
            set { _out_storehouse_name = value; }
        }
        private DateTime _outstore_time;
        [Description("External")]

        public DateTime outstore_time
        {
            get { return _outstore_time; }
            set { _outstore_time = value; }
        }
        private long _replenishuser_id;
        [Description("External")]

        public long replenishuser_id
        {
            get { return _replenishuser_id; }
            set { _replenishuser_id = value; }
        }
        private DateTime _replenish_time;
        [Description("External")]

        public DateTime replenish_time
        {
            get { return _replenish_time; }
            set { _replenish_time = value; }
        }
        private string _replenish_name;
        [Description("External")]

        public string replenish_name
        {
            get { return _replenish_name; }
            set { _replenish_name = value; }
        }
        private long _creman_id;
        [Description("External")]

        public long creman_id
        {
            get { return _creman_id; }
            set { _creman_id = value; }
        }
        private string _creman_name;
        [Description("External")]

        public string creman_name
        {
            get { return _creman_name; }
            set { _creman_name = value; }
        }
        private string _memo;
        [Description("External")]

        public string memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        private int _deleflag = 0;
        [Description("External")]
        public int deleflag
        {
            set { _deleflag = value; }
            get { return _deleflag; }
        }
    }
}


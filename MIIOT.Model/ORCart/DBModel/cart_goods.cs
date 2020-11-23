using MIIOT.Model.ORCart.SPDModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MIIOT.Model
{
    /// <summary>
    /// cart_goods:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class cart_goods : BaseRecord
    {
        public cart_goods()
        { }
        #region Model
        private string _goods_id;
        private string _goods_no;
        private string _goods_name;
        private string _goods_spec;
        private string _factory_name;
        private string _supply_name;
        private string _unit;
        private string _common_name;
        private int? _use_status;
        private int _goods_type;
        /// <summary>
        /// 
        /// </summary>
        public string goods_id
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
        public string factory_name
        {
            set { _factory_name = value; }
            get { return _factory_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string supply_name
        {
            set { _supply_name = value; }
            get { return _supply_name; }
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
        public string common_name
        {
            set { _common_name = value; }
            get { return _common_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? use_status
        {
            set { _use_status = value; }
            get { return _use_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int goods_type
        {
            set { _goods_type = value; }
            get { return _goods_type; }
        }

        [Description("External")]
        public double? usetime { get; set; }

        #endregion Model
        public int ischarge { get; set; }
        public int recyclableflag { get; set; }
        public int singleflag { get; set; }
        private int _qty;
        [Description("External")]
        public int qty
        {
            get { return _qty; }
            set
            {
                _qty = value;
                OnPropertyChanged("qty");
            }
        }
        private decimal _price;

        public decimal price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("price");
            }
        }
        [Description("External")]
        public string creman_name { get; set; }
        [Description("External")]
        public DateTime cretime { get; set; }
        [Description("External")]
        public string batch_no { get; set; }
        [Description("External")]
        public DateTime validity_date { get; set; }
        [Description("External")]
        public int need_qty { get; set; }
        private string _reasonCode;

        [Description("External")]
        public string ReasonCode
        {
            get { return _reasonCode; }
            set
            {
                _reasonCode = value;
                OnPropertyChanged("ReasonCode");
            }
        }
        private ObservableCollection<spd_backreason> _Reasons = new ObservableCollection<spd_backreason>();

        [Description("External")]
        public ObservableCollection<spd_backreason> Reasons
        {
            get { return _Reasons; }
            set
            {
                _Reasons = value;
                OnPropertyChanged("Reasons");
            }
        }
        private int _initType;

        [Description("External")]
        public int InitType
        {
            get { return _initType; }
            set
            {
                _initType = value;
                OnPropertyChanged("InitType");
            }
        }

        [Description("External")]
        public string SoucrceCode { get; set; }
        [Description("External")]
        public string plot_id { get; set; }
        [Description("External")]
        public string plot_no { get; set; }
        [Description("External")]
        public string single_code { get; set; }
        [Description("External")]
        public double uplimit { get; set; }
        [Description("External")]
        public double downlimit { get; set; }
        private int _SPDQty;

        [Description("External")]
        public int SpdQty
        {
            get { return _SPDQty; }
            set
            {
                _SPDQty = value;
                OnPropertyChanged("SpdQty");
            }
        }
    }
    public class StockList : cart_goods
    {
        public double stockqty { get; set; }
        public string barcode { get; set; }
    }
    public class StockTack : cart_goods
    {
        private int _Qty;

        public int Qty
        {
            get { return _Qty; }
            set
            {
                _Qty = value;
                OnPropertyChanged("Qty");
            }
        }

        private int _tackQty;

        public int TackQty
        {
            get { return _tackQty; }
            set
            {
                _tackQty = value;
                OnPropertyChanged("TackQty");
            }
        }
        private string _tackStatus;

        public string TackStatus
        {
            get { return _tackStatus; }
            set
            {
                _tackStatus = value;
                OnPropertyChanged("TackStatus");
            }
        }
        private bool _Tacked = true;

        public bool Tacked
        {
            get { return _Tacked; }
            set
            {
                _Tacked = value;
                OnPropertyChanged("Tacked");
            }
        }

    }
}


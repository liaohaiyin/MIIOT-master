using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/7/18 11:34:40
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart
{
    public class cart_clear : BaseRecord
    {
        public long id { get; set; }
        public string goods_id { get; set; }
        public string goods_no { get; set; }
        public string goods_name { get; set; }
        public string goods_spec { get; set; }
        public string factory_name { get; set; }
        public string unit { get; set; }
        public bool pay_flag { get; set; }
        private int _status;

        public int status
        {
            get { return _status; }
            set
            {
                if (value == 1)
                    IsHis = true;
                else
                    IsHis = false;
                _status = value;
            }
        }

        public string remark { get; set; }
        public int usetime { get; set; }
        public string origin_id { get; set; }
        public string creater { get; set; }
        public DateTime create_time { get; set; } = DateTime.Now;
        public string updater { get; set; }
        public DateTime update_time { get; set; } = DateTime.Now;
        public string plot_id { get; set; }
        public string plot_no { get; set; }
        public string single_code { get; set; }
        public DateTime validity_date { get; set; }

        public bool follow_tag { get; set; }
        public bool recevie_tag { get; set; }
        public string receiveddtl_id { get; set; }
        public int receivedtlstatus { get; set; }//1待记账，2已记账，3已销账
        private int _goods_type;

        public int goods_type
        {
            get { return _goods_type; }
            set
            {
                _goods_type = value;
                OnPropertyChanged("goods_type");
            }
        }


        private decimal _totalprice;

        public decimal totalprice
        {
            get { return _totalprice; }
            set
            {
                _totalprice = value;
                OnPropertyChanged("totalprice");
            }
        }

        private decimal _price;

        public decimal price
        {
            get { return _price; }
            set
            {
                ExtotalPrice = value * qty;
                if (ExPrice == 0)
                    ExPrice = value;
                _price = value;
                OnPropertyChanged("price");
            }
        }
        private decimal _ExPrice;

        [Description("External")]
        public decimal ExPrice
        {
            get { return _ExPrice; }
            set
            {
                _ExPrice = value;
                OnPropertyChanged("ExPrice");
            }
        }
        private decimal _totalPrice;

        [Description("External")]
        public decimal ExtotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                OnPropertyChanged("ExtotalPrice");
            }
        }
        [Description("External")]
        public bool backFlag { get; set; } = true;

        private int _qty;

        public int qty
        {
            get
            {
                return _qty;
            }
            set
            {
                ExtotalPrice = value * price;
                _qty = value;
                OnPropertyChanged("qty");
            }
        }
        private bool _isHis;
        [Description("External")]
        public bool IsHis
        {
            get { return _isHis; }
            set
            {
                _isHis = value;
                OnPropertyChanged("IsHis");
            }
        }
        [Description("External")]
        public List<cart_clear> Children { get; set; } = new List<cart_clear>();

    }
}

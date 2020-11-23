using System;
namespace MIIOT.Model
{
    /// <summary>
    /// cart_stock:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class cart_stock
    {
        public cart_stock()
        { }
        #region Model
        private long _stock_id;
        private string _goods_id;
        private string _stockhouse_no;
        private DateTime? _cretime = DateTime.Now;
        private string _creman_id;
        private string _creman_name;
        private string _repair_status;
        /// <summary>
        /// 
        /// </summary>
        public long stock_id
        {
            set { _stock_id = value; }
            get { return _stock_id; }
        }
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
        public string stockhouse_id
        {
            set { _stockhouse_no = value; }
            get { return _stockhouse_no; }
        }
        public int qty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? cretime
        {
            set { _cretime = value; }
            get { return _cretime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string creman_id
        {
            set { _creman_id = value; }
            get { return _creman_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string creman_name
        {
            set { _creman_name = value; }
            get { return _creman_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string repair_status
        {
            set { _repair_status = value; }
            get { return _repair_status; }
        }
        #endregion Model
        public string plot_id { get; set; }
        public string plot_no { get; set; }
        public DateTime validity_date { get; set; }

    }
    public class cart_stock_err : cart_stock
    {

    }
}


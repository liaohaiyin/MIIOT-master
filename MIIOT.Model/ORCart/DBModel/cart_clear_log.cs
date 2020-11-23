using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.ORCart
{
    public class cart_clear_log
    {
        public long id { get; set; }
        public string source_id { get; set; }
        public string goods_id { get; set; }
        public string opt_type { get; set; }
        public int qty { get; set; }
        public string creater { get; set; }
        public string creater_id { get; set; }
        public DateTime creat_time { get; set; }
        public string pay_tag { get; set; }
        public decimal total_price { get; set; }
        public string single_code { get; set; }

    }
}

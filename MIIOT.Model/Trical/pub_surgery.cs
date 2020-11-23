using MIIOT.Model.ORCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.Trical
{
    public class pub_surgery
    {
        public long organ_id { get; set; }
        public string surgery_code { get; set; }
        public string surgery_name { get; set; }
        public string dept { get; set; }
        public string doctor { get; set; }
        public string case_no { get; set; }
        public string patient { get; set; }
        public string surgery_time { get; set; }

    }
}

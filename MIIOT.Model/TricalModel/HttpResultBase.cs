using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.Trical
{
    public class HttpResultBase
    {
        public int? code { get; set; }
        public string msg { get; set; }
        public DateTime? timestamp { get; set; }
        public object data { get; set; }
       
    }
}

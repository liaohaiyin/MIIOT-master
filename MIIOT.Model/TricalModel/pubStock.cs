using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class Stock
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseSourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseCode { get; set; }
        /// <summary>
        /// lrx的库房
        /// </summary>
        public string storehouseName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseAddr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseMemo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zoneIdArrs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string organIdString { get; set; }
    }
}

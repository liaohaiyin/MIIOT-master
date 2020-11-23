using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class PickDtlsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// 活化部分凝血活酶时间(APTT)试剂
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsSpec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pickQty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string locNo { get; set; }
    }
    public class PickInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string sourceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pickPersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PickDtlsItem> pickDtls { get; set; }
    }
}

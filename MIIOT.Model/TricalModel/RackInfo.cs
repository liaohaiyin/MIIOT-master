using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class RackInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RackDtlListItem> rackDtlList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int storehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rackPersonId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rackPersonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isDelete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int createrId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtModified { get; set; }
    }
    public class RackDtlListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rackId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceDtlId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originDtlId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int storehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int locId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int zoneId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string locNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goodsId { get; set; }
        /// <summary>
        /// (APTT)活化部分凝血活酶时间试剂Forlrx
        /// </summary>
        public string goodsName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodsSpec { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rackQty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isDelete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int createrId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtModified { get; set; }
    }
}

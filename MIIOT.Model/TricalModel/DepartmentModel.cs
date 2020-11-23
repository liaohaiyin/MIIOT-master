using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.TricalModel
{
    public class DepartmentModel
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string officeCode { get; set; }
        /// <summary>
        /// 试剂中心库
        /// </summary>
        public string officeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int officeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int officeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string officeAddr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string head { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string contactNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string officeMemo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int isDelete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createrId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long modifierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmtModified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string office_source_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string origin_id { get; set; }
    }
}

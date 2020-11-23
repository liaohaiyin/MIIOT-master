using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model.Trical
{
    public class user_info
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userName { get; set; }
        public string displayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userStatus { get; set; }
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
        public long gmtCreate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifierId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long gmtModified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roleIdList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Model
{
    public class AccpetInfo
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
        public List<AcceptDtlListItem> acceptDtlList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organ_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int source_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string origin_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int supply_id { get; set; }
        /// <summary>
        /// XXXX医院
        /// </summary>
        public string supply_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long supply_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int storehouse_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int accept_person_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accept_person_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int check_person_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string check_person_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_delete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int creater_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmt_create { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifier_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmt_modified { get; set; }
    }
    public class AcceptDtlListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int organ_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string accept_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int source_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int source_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source_dtl_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string origin_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string origin_dtl_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int goods_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_no { get; set; }
        /// <summary>
        /// (APTT)活化部分凝血活酶时间试剂
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_spec { get; set; }
        /// <summary>
        /// 法国思达高
        /// </summary>
        public string goods_factory_name { get; set; }
        /// <summary>
        /// 盒
        /// </summary>
        public string goods_unit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int delivery_qty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int check_qty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lot_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lot_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string batch_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pprod_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pvalid_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int is_delete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int creater_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmt_create { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int modifier_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gmt_modified { get; set; }
    }

    
}

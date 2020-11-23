using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/31 14:09:47
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.ORCart.SPDModel
{
    public class spd_surgeryinfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string perfstorehouseno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string returnerrmsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string infection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? rollbackflag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string surgeryno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? tablenumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anesmanname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string printtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uorganid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgdatano { get; set; }
        /// <summary>
        /// 宫腔镜下宫腔粘连分离术5-3
        /// </summary>
        public string opsubtitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anesmanno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? hvchecked { get; set; }
        /// <summary>
        /// 27号手术室
        /// </summary>
        public string roomname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? dvchecked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string instruments { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string storehouseid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bedno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apppovedate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string examined { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? customsex { get; set; }
        /// <summary>
        /// 杨建华
        /// </summary>
        public string apppovemanname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string surgerytime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string surgeryid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string posture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string podx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string perfstorehouseid { get; set; }
        /// <summary>
        /// 手术室
        /// </summary>
        public string perfstorehousename { get; set; }
        /// <summary>
        /// 妇科
        /// </summary>
        public string storehousename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgdataid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string customid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string roomid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anestype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? surgerystatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patienttime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string originid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string printflag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? datasource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string printmanname { get; set; }
        /// <summary>
        /// 张素颖
        /// </summary>
        public string customname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string surgeonname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string surgeonno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string editdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paytype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string apppovemanid { get; set; }

        [Description("External")]
        public List<ClearGroup> ClearGroups { get; set; } = new List<ClearGroup>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/7/23 18:26:37
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model.TricalModel
{
    public class ScreenDataModel
    {
        public int _id { get; set; }
        public string Target { get; set; }
        public string CMD { get; set; }
        public string SourceType { get; set; }
        public string dataMsg { get; set; }
        public string dataMD5 { get; set; }
    }
}

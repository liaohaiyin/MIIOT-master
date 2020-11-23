using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/22 23:10:00
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model
{
    public enum StockTypeEnum
    {
        [Description("正常")]
        N = 0,
        [Description("缺货")]
        L = 1,
        [Description("积压")]
        F = 2
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/22 22:49:04
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Model
{
    public enum GoodsTypeEnum
    {
        [Description("低值收费耗材")]
        LOWPAY = 0,
        [Description("低值不收费耗材")]
        LOWFREE = 1,
        [Description("高值耗材")]
        HIGH = 2,
        [Description("高值复用器械")]
        REUSE = 3,
        [Description("耗材包")]
        PKG = 4
    }
}

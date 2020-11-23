using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MIIOT.DiagManager.Helper
{
    public static class PrintHelper
    {
        public static int Init(pub_accept_dtl model_pub_accept_dtl, out string code)
        {
            List<string> lst = new List<string>();
            lst.Add($"名称:{model_pub_accept_dtl.goods_name}");
            lst.Add($"编码:{model_pub_accept_dtl.goods_no}");
            lst.Add($"规格:{model_pub_accept_dtl.goods_spec}");
            lst.Add($"单位:{model_pub_accept_dtl.goods_unit}");
            lst.Add($"效期:{model_pub_accept_dtl.pvalid_date.ToString("yyyy-MM-dd")}");
            lst.Add($"批号:{model_pub_accept_dtl.lot_no}");
            lst.Add($"批次:{model_pub_accept_dtl.batch_no}");
            return Print(lst, out code);
        }

        public static int Print(List<string> data, out string code)
        {
            List<string> lst = new List<string>();
            foreach (var item in data)
            {
                if (StrWidth(item) > 250)
                    lst.AddRange(StrCut(item));
                else
                    lst.Add(item);
            }
            return AppRuntime.printer.Printer(lst, out code);
        }
        private static List<string> StrCut(string Str)
        {
            List<string> Cuts = new List<string>();
            string tempstr = "";
            foreach (var item in Str)
            {
                tempstr += item;
                double wid = StrWidth(tempstr);
                if (wid < 250)
                {
                    continue;
                }
                else
                {
                    Cuts.Add(tempstr);
                    tempstr = "";
                }
            }
            Cuts.Add(tempstr);
            tempstr = "";
            return Cuts;
        }
        private static double StrWidth(string Str)
        {
            return TextRenderer.MeasureText(Str,
              new Font("黑体", 15)).Width;
        }
    }
}

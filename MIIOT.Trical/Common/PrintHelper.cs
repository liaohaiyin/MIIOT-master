using MIIOT.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.DocumentStructures;

namespace MIIOT.Trical.Common
{
    public class PrintHelper
    {

        RFIDPrinter printer = new RFIDPrinter();
        public int Print(List<string> data, out string code)
        {
            List<string> aaa = new List<string>();
            foreach (var item in data)
            {
                if (StrWidth(item) > 250)
                    aaa.AddRange(StrCut(item));
                else
                    aaa.Add(item);
            }
            return printer.Printer(aaa, out code);
        }
        private List<string> StrCut(string Str)
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
        private double StrWidth(string Str)
        {
            return System.Windows.Forms.TextRenderer.MeasureText(Str,
              new System.Drawing.Font("黑体", 15)).Width;
        }
    }
}

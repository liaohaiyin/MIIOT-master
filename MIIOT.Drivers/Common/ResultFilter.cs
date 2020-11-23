using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class ResultFilter
    {
        public ResultFilter()
        {

        }
        private static readonly object obj = new object();
        public RFResults result { get; set; } = new RFResults();
        public string GetResult(string PreResult,int GapTime=500)
        {
            lock (obj)
            {
                if (PreResult != result.Result)
                {
                    result.Result = PreResult;
                    result.time = DateTime.Now;
                    return PreResult;
                }
                else
                {
                    if ((DateTime.Now - result.time).TotalMilliseconds < GapTime)
                    {
                        result.time = DateTime.Now;
                        return string.Empty;
                    }
                    else
                    {
                        result.time = DateTime.Now;
                        return PreResult;
                    }
                }
            }
        }
        public void Clear()
        {
            result = new RFResults();
        }
    }
    public class RFResults
    {
        public string Result { get; set; }
        public DateTime time { get; set; }
    }
}

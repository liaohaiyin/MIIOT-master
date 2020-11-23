using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class WhereBuilderResult
    {
        public string Where { get; }
        public Dictionary<string, object> Param { get; }

        public WhereBuilderResult(string where, Dictionary<string, object> param)
        {
            this.Where = where;
            this.Param = param;
        }
    }
}

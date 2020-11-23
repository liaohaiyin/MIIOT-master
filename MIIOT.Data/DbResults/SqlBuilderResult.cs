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
    public class SqlBuilderResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string Sql { get; }
        /// <summary>
        /// 
        /// </summary>
        public object Param { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public SqlBuilderResult(string sql, object param)
        {
            this.Sql = sql;
            this.Param = param;
        }
    }
}

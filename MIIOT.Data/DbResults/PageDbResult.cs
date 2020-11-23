using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    /// <summary>
    /// 分页查询结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageDbResult<T> : DbResult<List<T>>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
    }
}

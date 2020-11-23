using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    /// <summary>
    /// 查询数据结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryDbResult<T> : DbResult<List<T>>
    {

    }
}

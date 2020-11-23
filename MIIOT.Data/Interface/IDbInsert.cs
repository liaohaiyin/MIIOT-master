using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    public interface IDbInsert<T>
    {
        void Clear();
        SqlBuilderResult Build();
        IDbInsert<T> Values(IEnumerable<T> models);
        IDbInsert<T> Values(T model);
        ExecuteDbResult Execute(int? timeout = null);
    }
}

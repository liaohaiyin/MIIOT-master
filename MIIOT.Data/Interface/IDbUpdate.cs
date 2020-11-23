using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    public interface IDbUpdate<T>
    {
        void Clear();
        SqlBuilderResult Build();
        IDbUpdate<T> SetUpdateProperties(params string[] propertyNames);
        IDbUpdate<T> SetModels(IEnumerable<T> models);
        IDbUpdate<T> SetModel(T model);
        IDbUpdate<T> Set(object obj, Expression<Func<T, bool>> where);
        ExecuteDbResult Execute(int? timeout = null);
    }
}

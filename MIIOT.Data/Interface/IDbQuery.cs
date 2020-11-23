using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    public interface IDbQuery<T>
    {
        void Clear();
        SqlBuilderResult Build();
        IDbQuery<T> Where(Expression<Func<T, bool>> where);
        IDbQuery<T> And(Expression<Func<T, bool>> where);
        IDbQuery<T> Or(Expression<Func<T, bool>> where);
        IDbQuery<T> OrderBy<Field>(Expression<Func<T, Field>> fieldSelector, OrderByType type);
        PageDbResult<T> Page(int page, int size, int? timeout = null);
        ScalarDbResult<int> Count(int? timeout = null);
        QueryDbResult<T> ToList(int? timeout = null);
        QueryFirstDbResult<T> FirstOrDefault(int? timeout = null);
        QueryDbResult<T> Top(int top, int? timeout = null);
    }
}

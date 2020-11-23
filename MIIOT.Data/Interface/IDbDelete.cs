using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    public interface IDbDelete<T>
    {
        void Clear();
        SqlBuilderResult Build();
        IDbDelete<T> Where(Expression<Func<T, bool>> where);
        IDbDelete<T> And(Expression<Func<T, bool>> where);
        IDbDelete<T> Or(Expression<Func<T, bool>> where);
        ExecuteDbResult Execute(int? timeout = null);
    }
}

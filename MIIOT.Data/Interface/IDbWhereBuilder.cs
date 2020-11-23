using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    /// <summary>
    /// where条件生成接口
    /// </summary>
    public interface IDbWhereBuilder<T>
    {
        /// <summary>
        /// 清除已设置的where条件
        /// </summary>
        void Clear();
        /// <summary>
        /// 设置一个where条件
        /// </summary>
        /// <param name="where"></param>
        void SetWhere(Expression<Func<T, bool>> where);
        /// <summary>
        /// 用and增加一个where条件
        /// </summary>
        /// <param name="where"></param>
        void SetAnd(Expression<Func<T, bool>> where);
        /// <summary>
        /// 用or增加一个where条件
        /// </summary>
        /// <param name="where"></param>
        void SetOr(Expression<Func<T, bool>> where);
        /// <summary>
        /// 生成where条件
        /// </summary>
        /// <returns></returns>
        WhereBuilderResult Build();
        /// <summary>
        /// 忽略指定的参数，where条件中不允许使用这些参数名
        /// </summary>
        /// <param name="names"></param>
        void IgnoreParamName(params string[] names);
    }
}

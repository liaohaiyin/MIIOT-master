using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MIIOT.Data.Entity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DbWhereBuilder<T> : IDbWhereBuilder<T>
    {
        #region 字段
        protected Db _db;
        protected EntityInfo _entityInfo;
        protected Dictionary<string, object> _whereParams;
        protected List<string> _ignoreParamNames;
        protected LambdaExpression _where;
        //protected string lambdaParam;
        #endregion

        #region 构造
        public DbWhereBuilder(Db db)
        {
            this._db = db;
            _whereParams = new Dictionary<string, object>();
            _ignoreParamNames = new List<string>();
            this._entityInfo = Entity.Get<T>();
        }
        #endregion

        #region 辅助
        /// <summary>
        /// 缓存值
        /// 返回变量名称
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual string AddValueAndGetName(object value)
        {
            var index = this._whereParams.Count();
            var name = $"p{index}";
            while (this._ignoreParamNames.Find(p => name.Equals(p, StringComparison.OrdinalIgnoreCase)) != null)
            {
                index++;
            }
            this._whereParams[$"p{index}"] = value;
            return this._db.ToParamName(name);
        }
        #endregion

        #region 对外方法
        /// <summary>
        /// 清除where信息
        /// </summary>
        public void Clear()
        {
            this._whereParams.Clear();
            this._where = null;
        }
        /// <summary>
        /// 设置一个where条件
        /// </summary>
        /// <param name="where"></param>
        public virtual void SetWhere(Expression<Func<T, bool>> where)
        {
            this._where = where;
        }
        /// <summary>
        /// 用and增加一个where条件
        /// </summary>
        /// <param name="where"></param>
        public virtual void SetAnd(Expression<Func<T, bool>> where)
        {
            if (where == null)
            {
                return;
            }
            if (this._where == null)
            {
                this._where = where;
            }
            else
            {
                this._where = Expression.Lambda(Expression.AndAlso(this._where.Body, where.Body), this._where.Parameters);
            }
        }
        /// <summary>
        /// 用or增加一个where条件
        /// </summary>
        /// <param name="where"></param>
        public virtual void SetOr(Expression<Func<T, bool>> where)
        {
            if (where == null)
            {
                return;
            }
            if (this._where == null)
            {
                this._where = where;
            }
            else
            {
                this._where = Expression.Lambda(Expression.OrElse(this._where.Body, where.Body), this._where.Parameters);
            }
        }
        /// <summary>
        /// 生成where条件
        /// </summary>
        /// <returns></returns>
        public virtual WhereBuilderResult Build()
        {
            if (this._where == null) return new WhereBuilderResult(string.Empty, null);

            return new WhereBuilderResult($"where {this.ResolveToWhere(this._where)}", this._whereParams);
        }
        /// <summary>
        /// 设置忽略的参数
        /// </summary>
        /// <param name="names"></param>
        public virtual void IgnoreParamName(params string[] names)
        {
            if (names == null || names.Count() == 0) return;
            this._ignoreParamNames.AddRange(names);
        }
        #endregion

        #region Expression解析
        /// <summary>
        /// 解析Expression => Where
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected virtual string ResolveToWhere(Expression exp)
        {
            if (exp == null)
            {
                return string.Empty;
            }

            if (exp is LambdaExpression)
            {
                return ResolveLambdaExpression(exp as LambdaExpression);
            }
            else if (exp is BinaryExpression)
            {
                return ResolveBinaryExpression(exp as BinaryExpression);
            }
            else if (exp is MemberExpression)
            {
                return ResolveMemberExpression(exp as MemberExpression);
            }
            else if (exp is ConstantExpression)
            {
                return ResolveConstantExpression(exp as ConstantExpression);
            }
            else if (exp is MethodCallExpression)
            {
                return ResolveMethodCallExpression(exp as MethodCallExpression);
            }
            else if (exp is NewArrayExpression)
            {
                return ResolveNewArrayExpression(exp as NewArrayExpression);
            }
            else if (exp is UnaryExpression)
            {
                return ResolveUnaryExpression(exp as UnaryExpression);
            }
            throw new Exception($"Unrecognized Expressions:{exp}");
        }
        /// <summary>
        /// lambda表达式处理
        /// </summary>
        /// <param name="lexp"></param>
        /// <returns></returns>
        protected virtual string ResolveLambdaExpression(LambdaExpression lexp)
        {
            //if (string.IsNullOrWhiteSpace(this.lambdaParam))
            //{
            //    this.lambdaParam = lexp.Parameters.FirstOrDefault().Name;
            //}
            return ResolveToWhere(lexp.Body);
        }
        /// <summary>
        /// 二元表达式处理
        /// </summary>
        /// <param name="bexp"></param>
        /// <returns></returns>
        protected virtual string ResolveBinaryExpression(BinaryExpression bexp)
        {
            string left = ResolveToWhere(bexp.Left);
            string oper = GetExpressionOper(bexp.NodeType);
            string right = ResolveToWhere(bexp.Right);
            return bexp.NodeType.In(ExpressionType.OrElse, ExpressionType.AndAlso) ? $"({left}{oper}{right})" : $"{left}{oper}{right}";
        }
        /// <summary>
        /// lambda表达式中的成员访问处理
        /// </summary>
        /// <param name="mexp"></param>
        /// <returns></returns>
        protected virtual string ResolveMemberExpression(MemberExpression mexp)
        {
            var sub = mexp.Expression;
            var type = sub?.Type;
            if (sub is UnaryExpression)
            {
                sub = (sub as UnaryExpression).Operand;
                type = sub.Type;
            }
            if (sub is ParameterExpression && this._entityInfo.EntityType == type)
            {
                return this._db.ToColumnName(mexp.Member.Name);
            }
            var val = GetExpressionValue(mexp as Expression);
            return AddValueAndGetName(val);
            //throw new Exception($"Unrecognized Expressions:{mexp.Expression}");
        }
        /// <summary>
        /// 常量表达式直接返回参数名，缓存参数值
        /// </summary>
        /// <param name="cexp"></param>
        /// <returns></returns>
        protected virtual string ResolveConstantExpression(ConstantExpression cexp)
        {
            var paramName = AddValueAndGetName(cexp.Value);
            return paramName;
        }
        /// <summary>
        /// Sql方法处理
        /// 支持  In  NotIn Like NotLike Between
        /// </summary>
        /// <param name="mcexp"></param>
        /// <returns></returns>
        protected virtual string ResolveMethodCallExpression(MethodCallExpression mcexp)
        {
            var param = ResolveToWhere(mcexp.Arguments[0]);
            var paramName = param;
            switch (mcexp.Method.Name)
            {
                case "In":
                    paramName = AddValueAndGetName(GetExpressionValue(mcexp.Arguments[1]));
                    return $"{param} In {paramName}";
                case "NotIn":
                    paramName = AddValueAndGetName(GetExpressionValue(mcexp.Arguments[1]));
                    return $"{param} Not In {paramName}";
                case "Like":
                    paramName = AddValueAndGetName(GetExpressionValue(mcexp.Arguments[1]));
                    return $"{param} Like {paramName}";
                case "NotLike":
                    paramName = AddValueAndGetName(GetExpressionValue(mcexp.Arguments[1]));
                    return $"{param} Not Like {paramName}";
                case "Between":
                    var p1 = AddValueAndGetName(GetExpressionValue(mcexp.Arguments[1]));
                    var p2 = AddValueAndGetName(GetExpressionValue(mcexp.Arguments[2]));
                    return $"{param} Between {p1} And {p2}";
            }
            throw new Exception($"Unrecognized Method:{mcexp.Object}.{mcexp.Method.Name}");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="naexp"></param>
        /// <returns></returns>
        protected virtual string ResolveNewArrayExpression(NewArrayExpression naexp)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var exp in naexp.Expressions)
            {
                sb.Append(ResolveToWhere(exp));
                sb.Append(",");
            }
            return sb.ToString(0, sb.Length - 1);
        }
        /// <summary>
        /// 一元运算处理
        /// </summary>
        /// <param name="uexp"></param>
        /// <returns></returns>
        protected virtual string ResolveUnaryExpression(UnaryExpression uexp)
        {
            return ResolveToWhere(uexp.Operand);
        }
        /// <summary>
        /// 操作符转换
        /// </summary>
        /// <param name="eType"></param>
        /// <returns></returns>
        protected virtual string GetExpressionOper(ExpressionType eType)
        {
            switch (eType)
            {
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.Or: return "OR";
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.And: return "AND";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.Equal: return "=";
            }
            throw new Exception($"Unrecognized ExpressionType:{eType.ToString()}");
        }

        /// <summary>
        /// 获取lambda表达式中变量的值
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected virtual object GetExpressionValue(Expression exp)
        {
            return Expression.Lambda(exp).Compile().DynamicInvoke();
            //switch (exp.Type.Name)
            //{
            //    case "Int16":
            //    case "UInt16":
            //        return LambdaCompile<Int16>(exp);
            //    case "Int32":
            //    case "UInt32":
            //        return LambdaCompile<Int32>(exp);
            //    case "Int64":
            //    case "UInt64":
            //        return LambdaCompile<Int64>(exp);
            //    case "Decimal":
            //        return LambdaCompile<Decimal>(exp);
            //    case "Double":
            //        return LambdaCompile<Double>(exp);
            //    case "Single":
            //        return LambdaCompile<Single>(exp);
            //    case "DateTime":
            //        return LambdaCompile<DateTime>(exp);
            //    default:
            //        return LambdaCompile<object>(exp);
            //}

        }
        /// <summary>
        /// 获取lambda表达式中变量的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        protected virtual LambT LambdaCompile<LambT>(Expression exp)
        {
            
            var func = Expression.Lambda<Func<LambT>>(exp).Compile();
            return func();
        }


        #endregion

    }
}

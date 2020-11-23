using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MIIOT.Data.Entity
{
    public class DbQuery<T> : IDbQuery<T>
    {
        #region 字段
        protected Db _db;
        protected EntityInfo _entityInfo;
        protected IDbWhereBuilder<T> _whereBuilder;
        protected IDictionary<string, OrderByType> _orderby = new Dictionary<string, OrderByType>();
        protected PageInfo _page = null;
        protected bool _isCount = false;
        protected int _top = -1;
        #endregion

        #region 构造
        public DbQuery(Db db)
        {
            this._db = db;
            Init();
        }
        #endregion

        #region 对外方法
        public virtual void Clear()
        {
            this._whereBuilder.Clear();
            this._isCount = false;
            this._page = null;
            this._orderby.Clear();
            this._top = -1;
        }
        public virtual SqlBuilderResult Build()
        {
            var tableName = _db.ToTableName(this._entityInfo.Name);
            var whereResult = _whereBuilder.Build();
            var sql = string.Empty;
            if (this._isCount)
            {
                sql = $"select count(1) as `Total` from {tableName} {whereResult.Where}";
            }
            else if (this._page != null)
            {
                //page query
                var row = (this._page.Page - 1) * this._page.Size;
                var offset = this._page.Size;
                //sql = $"select * from (select *,ROW_NUMBER() over({this.BuildOrderBy()}) as [RowId] from {tableName} {whereResult.Where}) as [PageTable] where [PageTable].[RowId] between {start} and {end}";
                sql = $"select * from (select t.*,@rownum:=@rownum+1 as `RowId` from (select @rownum:=0) r, {tableName} t { whereResult.Where}) as `PageTable` where `PageTable`.`RowId` limit {row},{offset}";
                sql += $"; select count(1) from {tableName} {whereResult.Where}";
            }
            else if (this._top > 0)
            {
                sql = $"select * from {tableName} limit {this._top} {whereResult.Where} {this.BuildOrderBy()}";
            }
            else
            {
                //query
                sql = $"select * from {tableName} {whereResult.Where} {this.BuildOrderBy()}";
            }
            return new SqlBuilderResult(sql, whereResult.Param);
        }
        public virtual IDbQuery<T> Where(Expression<Func<T, bool>> where)
        {
            this._whereBuilder.SetWhere(where);
            return this;
        }
        public virtual IDbQuery<T> And(Expression<Func<T, bool>> where)
        {
            this._whereBuilder.SetAnd(where);
            return this;
        }
        public virtual IDbQuery<T> Or(Expression<Func<T, bool>> where)
        {
            this._whereBuilder.SetOr(where);
            return this;
        }
        public virtual IDbQuery<T> OrderBy<Field>(Expression<Func<T, Field>> fieldSelector, OrderByType type)
        {
            if (fieldSelector == null)
            {
                return this;
            }
            if (fieldSelector.Body is MemberExpression)
            {
                var me = fieldSelector.Body as MemberExpression;
                var field = this._db.ToColumnName(me.Member.Name);
                _orderby[field] = type;
            }
            return this;
        }
        public virtual PageDbResult<T> Page(int page, int size, int? timeout = null)
        {
            this._page = new PageInfo(page, size);
            var ret = default(SqlBuilderResult);
            try
            {
                ret = this.Build();
            }
            catch (Exception ex)
            {
                return new PageDbResult<T>()
                {
                    Code = DbResultCode.Error,
                    Exception = ex,
                    Message = ex.Message
                };
            }

            return this._db.QueryPage<T>(ret.Sql, ret.Param, timeout);
        }
        public virtual ScalarDbResult<int> Count(int? timeout = null)
        {
            _isCount = true;
            var ret = default(SqlBuilderResult);
            try
            {
                ret = this.Build();
            }
            catch (Exception ex)
            {
                return new ScalarDbResult<int>()
                {
                    Code = DbResultCode.Error,
                    Exception = ex,
                    Message = ex.Message
                };
            }
            return this._db.ExecuteScalar<int>(ret.Sql, ret.Param, timeout);
        }
        public virtual QueryDbResult<T> ToList(int? timeout = null)
        {
            var ret = default(SqlBuilderResult);
            try
            {
                ret = this.Build();
            }
            catch (Exception ex)
            {
                return new QueryDbResult<T>()
                {
                    Code = DbResultCode.Error,
                    Exception = ex,
                    Message = ex.Message
                };
            }
            return this._db.Query<T>(ret.Sql, ret.Param, true, timeout);
        }
        public virtual QueryFirstDbResult<T> FirstOrDefault(int? timeout = null)
        {
            var ret = default(SqlBuilderResult);
            try
            {
                ret = this.Build();
            }
            catch (Exception ex)
            {
                return new QueryFirstDbResult<T>()
                {
                    Code = DbResultCode.Error,
                    Exception = ex,
                    Message = ex.Message
                };
            }
            return this._db.QueryFirstOrDefault<T>(ret.Sql, ret.Param, timeout);
        }
        public virtual QueryDbResult<T> Top(int top, int? timeout = null)
        {
            this._top = top;
            return this.ToList(timeout);
        }
        #endregion

        #region 内部方法
        protected virtual void Init()
        {
            this._entityInfo = Entity.Get<T>();
            this._whereBuilder = this._db.CreateDbWhereBuilder<T>();
        }
        protected virtual string BuildOrderBy()
        {
            if (this._orderby.Count() == 0) return string.Empty;
            var elems = this._orderby.Select(p => $"{p.Key} {p.Value.ToString()}");
            return $"order by {string.Join(",", elems)}";
        }
        #endregion
    }
}

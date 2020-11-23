using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    public class DbDelete<T> : IDbDelete<T>
    {
        protected Db _db;
        protected EntityInfo _entityInfo;
        protected IDbWhereBuilder<T> _whereBuilder;

        public DbDelete(Db db)
        {
            this._db = db;
            Init();
        }

        protected virtual void Init()
        {
            this._whereBuilder = this._db.CreateDbWhereBuilder<T>();
            this._entityInfo = Entity.Get<T>();
        }

        public virtual IDbDelete<T> Where(Expression<Func<T, bool>> where)
        {
            _whereBuilder.SetWhere(where);
            return this;
        }
        public virtual IDbDelete<T> And(Expression<Func<T, bool>> where)
        {
            _whereBuilder.SetAnd(where);
            return this;
        }
        public virtual IDbDelete<T> Or(Expression<Func<T, bool>> where)
        {
            _whereBuilder.SetOr(where);
            return this;
        }

        public virtual SqlBuilderResult Build()
        {
            var tableName = this._db.ToTableName(this._entityInfo.Name);
            var whereResult = this._whereBuilder.Build();
            var sql = $"delete from {tableName} {whereResult.Where}";
            return new SqlBuilderResult(sql, whereResult.Param);
        }

        public virtual void Clear()
        {
            this._whereBuilder.Clear();
        }

        public virtual ExecuteDbResult Execute(int? timeout = null)
        {
            var ret = default(SqlBuilderResult);
            try
            {
                ret = this.Build();
            }
            catch (Exception ex)
            {
                return new ExecuteDbResult()
                {
                    Code = DbResultCode.Error,
                    Exception = ex,
                    Message = ex.Message
                };
            }
            return this._db.Execute(ret.Sql, ret.Param, timeout);
        }

    }
}

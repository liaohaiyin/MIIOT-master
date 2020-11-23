using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    public class DbInsert<T> : IDbInsert<T>
    {
        protected Db _db;
        protected EntityInfo _entityInfo;
        protected List<T> _models = new List<T>();

        public DbInsert(Db db)
        {
            this._db = db;
            Init();
        }
        protected virtual void Init()
        {
            this._entityInfo = Entity.Get<T>();
        }
        public virtual SqlBuilderResult Build()
        {
            var tableName = this._db.ToTableName(this._entityInfo.Name);
            var noIncCols = this._entityInfo.Columns.Where(p => p.IdentityType != IdentityType.Db);
            var fields = noIncCols.Select(p => this._db.ToColumnName(p.Name));
            var pars = noIncCols.Select(p => this._db.ToParamName(p.Name));

            var sql = $"insert into {tableName}({string.Join(",", fields)}) values({string.Join(",", pars)})";

            if (this._models.Count == 0)
                throw new DataException("No data");
            else if (this._models.Count == 1)
                return new SqlBuilderResult(sql, _models.First());
            else
                return new SqlBuilderResult(sql, _models);
        }

        public virtual void Clear()
        {
            this._models.Clear();
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
            if(this._models.Count == 1)
            {
                var execRet = this._db.Execute(ret.Sql, ret.Param, timeout, CommandType.Text, true);
                if(execRet.Code == DbResultCode.Success)
                {
                    var col = this._entityInfo.Columns.FirstOrDefault(p => p.IdentityType == IdentityType.Db);
                    col?.SetValue(this._models[0], execRet.Data);
                }
                return execRet;
            }
            else
            {
                return this._db.Execute(ret.Sql, ret.Param, timeout, CommandType.Text, false);
            }
        }

        public virtual IDbInsert<T> Values(T model)
        {
            this._models.Add(model);
            return this;
        }

        public virtual IDbInsert<T> Values(IEnumerable<T> models)
        {
            this._models.AddRange(models);
            return this;
        }
    }
}

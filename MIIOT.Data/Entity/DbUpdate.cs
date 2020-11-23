using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace MIIOT.Data.Entity
{
    public class DbUpdate<T> : IDbUpdate<T>
    {
        protected Db _db;
        protected EntityInfo _entityInfo;
        protected List<T> _models = new List<T>();
        protected IDbWhereBuilder<T> _whereBuilder;
        protected List<string> _updateProps = new List<string>();

        protected object _setObj = null;
        protected EntityInfo _setObjEntityInfo = null;

        public DbUpdate(Db db)
        {
            this._db = db;
            Init();
        }
        protected virtual void Init()
        {
            this._entityInfo = Entity.Get<T>();
            this._whereBuilder = this._db.CreateDbWhereBuilder<T>();
        }
        public virtual SqlBuilderResult Build()
        {
            if (this._models.Count > 0)
            {
                var primaryKey = this._entityInfo.PrimaryKey;
                if (primaryKey == null) throw new MissingPrimaryKeyException("Notfound PrimaryKey");

                var tableName = this._db.ToTableName(this._entityInfo.Name);
                var updateCols = this._entityInfo.Columns.Where(p => p.Name != primaryKey.Name);
                if (this._updateProps.Count > 0)
                    updateCols = updateCols.Where(p => this._updateProps.Contains(p.Name));
                if (updateCols.Count() == 0)
                    throw new DataException("There are no columns");
                var kv = updateCols.Select(p => $"{this._db.ToColumnName(p.Name)}={this._db.ToParamName(p.Name)}");
                var sql = $"update {tableName} set {string.Join(",", kv)} where {this._db.ToColumnName(primaryKey.Name)}={this._db.ToParamName(primaryKey.Name)}";


                if (this._models.Count == 1)
                    return new SqlBuilderResult(sql, _models.First());
                else
                    return new SqlBuilderResult(sql, _models);
            }
            else if (this._setObj != null)
            {
                var tableName = this._db.ToTableName(this._entityInfo.Name);
                var kv = this._setObjEntityInfo.Columns.Select(p => $"{this._db.ToColumnName(p.Name)}={this._db.ToParamName(p.Name)}");
                var whereResult = this._whereBuilder.Build();
                var sql = $"update {tableName} set {string.Join(",", kv)} {whereResult.Where}";
                foreach (var col in this._setObjEntityInfo.Columns)
                {
                    whereResult.Param.Add(col.Name, col.GetValue(this._setObj));
                }
                return new SqlBuilderResult(sql, whereResult.Param);
            }
            else
            {
                throw new DataException("No update data");
            }
        }

        public virtual void Clear()
        {
            this._models.Clear();
            this._whereBuilder.Clear();
            this._setObj = null;
            this._setObjEntityInfo = null;
            this._updateProps.Clear();
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
                    Message = ex.Message,
                };
            }
            return this._db.Execute(ret.Sql, ret.Param, timeout, CommandType.Text);
        }

        public virtual IDbUpdate<T> SetUpdateProperties(params string[] propertyNames)
        {
            if (propertyNames?.Length > 0)
            {
                this._updateProps.AddRange(propertyNames);
            }
            return this;
        }
        public virtual IDbUpdate<T> SetModel(T model)
        {
            if (this._setObj != null) return this;
            this._models.Add(model);
            return this;
        }

        public virtual IDbUpdate<T> SetModels(IEnumerable<T> models)
        {
            if (this._setObj != null) return this;
            this._models.AddRange(models);
            return this;
        }
        public virtual IDbUpdate<T> Set(object obj, Expression<Func<T, bool>> where)
        {
            if (this._models.Count > 0) return this;
            this._whereBuilder.SetWhere(where);
            this._setObj = obj;
            this._setObjEntityInfo = Entity.Get(this._entityInfo.Name, obj);
            this._whereBuilder.IgnoreParamName(this._setObjEntityInfo.Columns.Select(p => p.Name).ToArray());

            return this;
        }


    }
}

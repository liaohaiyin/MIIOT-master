using MIIOT.Data.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MIIOT.Data
{
    /// <summary>
    /// 数据库基类
    /// （默认实现sqlserver操作）
    /// </summary>
    public abstract class Db : IDisposable
    {
        /// <summary>
        /// 按连接字符串创建对应数据库对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static Db CreateDb(string connectionString)
        {
            //目前只实现sqlserver
            var db = new MySqlDb();
            db.ConnectionString = connectionString;
            return db;
        }


        private IDbConnection _dbConnection;
        private bool disposedValue;


        #region 属性
        /// <summary>
        /// 数据库类型
        /// </summary>
        public abstract DbProvider DbProvider { get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public virtual string ConnectionString { get; set; }

        /// <summary>
        /// 事务
        /// </summary>
        protected virtual IDbTransaction DbTransaction { get; private set; }
        #endregion

        #region 连接

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public virtual IDbConnection CreateDbConnection()
        {
            if (this.DbTransaction != null)
                return this.DbTransaction.Connection;

            if (this._dbConnection == null)
                this._dbConnection = new MySqlConnection(this.ConnectionString);

            if (this._dbConnection.State != ConnectionState.Open)
                this._dbConnection.Open();

            return this._dbConnection;
        }
        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        public virtual bool TryTestConnect(out string msg)
        {
            msg = string.Empty;
            try
            {
                var conn = CreateDbConnection();
                if (conn.State == ConnectionState.Open)
                    return true;
                conn.Open();
                return conn.State == ConnectionState.Open;
            }
            catch(Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        #endregion

        #region 事务
        /// <summary>
        /// 开始事务
        /// </summary>
        public virtual void BeginTransation()
        {
            var conn = this.CreateDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            this.DbTransaction = conn.BeginTransaction();
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public virtual bool TryBeginTransation()
        {
            try
            {
                BeginTransation();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="il">事务锁定行为</param>
        public virtual void BeginTransation(IsolationLevel il)
        {
            var conn = this.CreateDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            this.DbTransaction = conn.BeginTransaction(il);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        public virtual bool TryBeginTransation(IsolationLevel il)
        {
            try
            {
                BeginTransation(il);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public virtual void Commit()
        {
            if (this.DbTransaction == null)
                throw new InvalidOperationException("事务未开始或已关闭");
            this.DbTransaction.Commit();
            this.DbTransaction = null;
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public virtual bool TryCommit()
        {
            try
            {
                Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public virtual void Rollback()
        {
            if (this.DbTransaction == null)
                throw new InvalidOperationException("事务未开始或已关闭");
            this.DbTransaction.Rollback();
            this.DbTransaction = null;
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public virtual bool TryRollback()
        {
            try
            {
                Rollback();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region sql语句执行
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="timeout">超时时间(秒)</param>
        /// <param name="scopeIdentity">是否返回自增列的值（Insert操作）</param>
        /// <returns></returns>
        public virtual ExecuteDbResult Execute(string sql, object param = null, int? timeout = null, CommandType commandType = CommandType.Text, bool scopeIdentity = false)
        {
            var result = new ExecuteDbResult();
            try
            {
                var conn = CreateDbConnection();
                int n = 0;
                if (scopeIdentity)
                {
                    sql = $"{sql.TrimEnd(';')};select(CASE WHEN (CAST(SCOPE_IDENTITY() as int))>0 THEN CAST(SCOPE_IDENTITY() as int) ELSE 0 END );";
                    try
                    {
                        n = conn.Query<int?>(sql, param, this.DbTransaction, false, timeout, commandType)?.FirstOrDefault() ?? 0;
                    }
                    catch
                    {

                    }
                }
                else
                {
                    n = conn.Execute(sql, param, this.DbTransaction, timeout, commandType);
                }
                if(n>0)
                {
                    result.Code = DbResultCode.Success;
                    result.Data = n;
                }
                else
                {
                    result.Code = DbResultCode.Nothing;
                    result.Data = n;
                }
            }
            catch(Exception ex)
            {
                result.Code = DbResultCode.Error;
                result.Data = -1;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询返回单个值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="timeout"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public ScalarDbResult<T> ExecuteScalar<T>(string sql, object param = null, int? timeout = null, CommandType? cmdType = null)
        {
            var result = new ScalarDbResult<T>();
            try
            {
                var conn = CreateDbConnection();
                var t = conn.ExecuteScalar<T>(sql, param, this.DbTransaction, timeout, cmdType);
                result.Code = DbResultCode.Success;
                result.Data = t;
            }
            catch (Exception ex)
            {
                result.Code = DbResultCode.Error;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 执行查询SQL语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="timeout">超时时间(秒)</param>
        /// <returns></returns>
        public virtual QueryDbResult<T> Query<T>(string sql, object param = null, bool buffered = true, int? timeout = null, CommandType commandType = CommandType.Text)
        {
            var result = new QueryDbResult<T>();
            try
            {
                var conn = CreateDbConnection();
                var data = conn.Query<T>(sql, param, this.DbTransaction, buffered, timeout, commandType);
                if (data != null && data.Count() > 0)
                {
                    result.Code = DbResultCode.Success;
                    result.Data = data.ToList();
                }
                else
                {
                    result.Code = DbResultCode.Nothing;
                    result.Data = null;
                }
            }
            catch (Exception ex)
            {
                result.Code = DbResultCode.Error;
                result.Data = null;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 执行查询SQL语句，返回单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="timeout">超时时间(秒)</param>
        /// <returns></returns>
        public virtual QueryFirstDbResult<T> QueryFirstOrDefault<T>(string sql, object param = null, int? timeout = null, CommandType commandType = CommandType.Text)
        {
            var result = new QueryFirstDbResult<T>();
            try
            {
                var conn = CreateDbConnection();
                var data = conn.QueryFirstOrDefault<T>(sql, param, this.DbTransaction, timeout, commandType);
                if (data != null)
                {
                    result.Code = DbResultCode.Success;
                    result.Data = data;
                }
                else
                {
                    result.Code = DbResultCode.Nothing;
                    result.Data = default(T);
                }
            }
            catch (Exception ex)
            {
                result.Code = DbResultCode.Error;
                result.Data = default(T);
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageAndCountSql"></param>
        /// <param name="param"></param>
        /// <param name="timeout"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public PageDbResult<T> QueryPage<T>(string pageAndCountSql, object param = null, int? timeout = null, CommandType? cmdType = null)
        {
            var result = new PageDbResult<T>();
            try
            {
                var conn = CreateDbConnection();
                var mult = conn.QueryMultiple(pageAndCountSql, param, this.DbTransaction, timeout, cmdType);
                var models = mult.Read<T>().ToList();
                var total = mult.Read<int>().Single();
                if (models.Count == 0)
                {
                    result.Code = DbResultCode.Nothing;
                    result.Total = total;
                }
                else
                {
                    result.Code = DbResultCode.Success;
                    result.Data = models;
                    result.Total = total;
                }
            }
            catch (Exception ex)
            {
                result = new PageDbResult<T>()
                {
                    Code = DbResultCode.Error,
                    Exception = ex,
                    Message = ex.Message,
                };
            }
            return result;
        }
        #endregion

        #region sql参数转化
        /// <summary>
        /// 字段名格式化
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string ToColumnName(string name)
        {
            return $"`{name}`";
        }
        /// <summary>
        /// 参数名格式化
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string ToParamName(string name)
        {
            return $"@{name}";
        }
        /// <summary>
        /// 表名格式化
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string ToTableName(string name)
        {
            return $"`{name}`";
        }
        #endregion

        #region Entity
        /// <summary>
        /// 创建where条件生成器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IDbWhereBuilder<T> CreateDbWhereBuilder<T>()
        {
            return new DbWhereBuilder<T>(this);
        }


        /// <summary>
        /// 增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IDbInsert<T> Insert<T>()
        {
            return new DbInsert<T>(this);
        }
        /// <summary>
        /// 删
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IDbDelete<T> Delete<T>()
        {
            return new DbDelete<T>(this);
        }
        /// <summary>
        /// 改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IDbUpdate<T> Update<T>()
        {
            return new DbUpdate<T>(this);
        }
        /// <summary>
        /// 查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IDbQuery<T> Query<T>()
        {
            return new DbQuery<T>(this);
        }
        #endregion

        #region 备份/还原
        /// <summary>
        /// 备份数据库到指定路径
        /// </summary>
        /// <param name="path"></param>
        public void Backup(string path)
        {
            var conn = CreateDbConnection();
            var connStrBuilder = new MySqlConnectionStringBuilder(conn.ConnectionString);
            var cmd = $"use master;Backup Database TO DISK = '{path}';";
            conn.Execute(cmd);
            conn.Close();
            conn.Dispose();
        }
        /// <summary>
        /// 还原指定路径的备份
        /// </summary>
        /// <param name="path"></param>
        public void Restore(string path)
        {
            var conn = CreateDbConnection();

            var bakInfos = conn.Query<dynamic>($"Restore FileListOnly From Disk='{path}'");
            var bakData = bakInfos.FirstOrDefault(p => p.Type == "D").LogicalName;
            var bakLog = bakInfos.FirstOrDefault(p => p.Type == "L").LogicalName;

            var rsInfos = conn.Query<dynamic>($"Select * From sys.database_files");
            var rsData = rsInfos.FirstOrDefault(p => p.type == 0).physical_name;
            var rsLog = rsInfos.FirstOrDefault(p => p.type == 1).physical_name;

            var connStrBuilder = new MySqlConnectionStringBuilder(conn.ConnectionString);
            var cmd = $"use master;"
                + $"Alter Database Set Offline With rollback immediate;"
                + $"Restore Database From Disk = '{path}' With Replace,Move '{bakData}' To '{rsData}',Move '{bakLog}' To '{rsLog}';"
                + $"Alter Database Set Online With Rollback immediate;";
            conn.Execute(cmd);
            conn.Close();
            conn.Dispose();
        }

        #endregion

        #region 释放资源
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                    if(this._dbConnection != null)
                    {
                        this._dbConnection.Close();
                        this._dbConnection.Dispose();
                        this._dbConnection = null;
                    }
                    if (this.DbTransaction != null)
                    {
                        this.DbTransaction.Dispose();
                        this.DbTransaction = null;
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并替代终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        // ~Db()
        // {
        //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.IO;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace MIIOT.Utility
{
    /// <summary>
    /// 基于Dapper的数据操作类封装的工具类
    /// Author:陈夏松
    /// Date：2017/12/11
    /// </summary>
    public class SqlDapperUtil
    {
        private static string dbConnectionStringConfigPath = null;
        private readonly static ConcurrentDictionary<string, bool> dbConnNamesCacheDic = new ConcurrentDictionary<string, bool>();

        private string dbConnectionName = null;
        private string dbConnectionString = null;
        private string dbProviderName = null;
        private IDbConnection dbConnection = null;
        private bool useDbTransaction = false;
        private IDbTransaction dbTransaction = null;


        #region 私有方法

        private IDbConnection GetDbConnection()
        {
            bool needCreateNew = false;
            if (dbConnection == null || string.IsNullOrWhiteSpace(dbConnection.ConnectionString))
            {
                needCreateNew = true;
            }

            if (needCreateNew)
            {
                dbConnectionString =  GetDbConnectionString(dbConnectionName, out dbProviderName);
                var dbProviderFactory = DbProviderFactories.GetFactory(dbProviderName);
                dbConnection = dbProviderFactory.CreateConnection();
                dbConnection.ConnectionString = dbConnectionString;
            }

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return dbConnection;
        }

        private string GetDbConnectionString(string dbConnName, out string dbProviderName)
        {
            //如果指定的连接字符串配置文件路径，则创建缓存依赖，一旦配置文件更改就失效，再重新读取
            string[] connInfos = MemoryCacheUtil.GetOrAddCacheItem(dbConnName, () =>
            {
                var connStrSettings = ConfigUtil.GetConnectionStringSetting(dbConnName);
                string dbProdName = connStrSettings.ProviderName;
                string dbConnStr = connStrSettings.ConnectionString;
                //LogUtil.Info(string.Format("SqlDapperUtil.GetDbConnectionString>读取连接字符串配置节点[{0}]:{1},ProviderName:{2}", dbConnName, dbConnStr, dbProdName), "SqlDapperUtil.GetDbConnectionString");
                return new[] { dbConnStr, "MySql.Data.MySqlClient" };
            }, SqlDapperUtil.DbConnectionStringConfigPath);

            dbProviderName = connInfos[1];
            return connInfos[0];
        }


        private T UseDbConnection<T>(Func<IDbConnection, T> queryOrExecSqlFunc)
        {
            IDbConnection dbConn = null;

            try
            {
                Type modelType = typeof(T);
                var typeMap = Dapper.SqlMapper.GetTypeMap(modelType);
                if (typeMap == null || !(typeMap is ColumnAttributeTypeMapper<T>))
                {
                    Dapper.SqlMapper.SetTypeMap(modelType, new ColumnAttributeTypeMapper<T>());
                }

                dbConn = GetDbConnection();
                if (useDbTransaction && dbTransaction == null)
                {
                    dbTransaction = GetDbTransaction();
                }

                return queryOrExecSqlFunc(dbConn);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbTransaction == null && dbConn != null)
                {
                    CloseDbConnection(dbConn);
                }
            }
        }

        private void CloseDbConnection(IDbConnection dbConn, bool disposed = false)
        {
            if (dbConn != null)
            {
                if (disposed && dbTransaction != null)
                {
                    dbTransaction.Rollback();
                    dbTransaction.Dispose();
                    dbTransaction = null;
                }

                if (dbConn.State != ConnectionState.Closed)
                {
                    dbConn.Close();
                }
                dbConn.Dispose();
                dbConn = null;
            }
        }

        /// <summary>
        /// 获取一个事务对象（如果需要确保多条执行语句的一致性，必需使用事务）
        /// </summary>
        /// <param name="il"></param>
        /// <returns></returns>
        private IDbTransaction GetDbTransaction(IsolationLevel il = IsolationLevel.Unspecified)
        {
            return GetDbConnection().BeginTransaction(il);
        }

        private DynamicParameters ToDynamicParameters(Dictionary<string, object> paramDic)
        {
            return new DynamicParameters(paramDic);
        }

        #endregion

        public static string DbConnectionStringConfigPath
        {
            get
            {
                if (string.IsNullOrEmpty(dbConnectionStringConfigPath))//如果没有指定配置文件，则取默认的配置文件路径作为缓存依赖路径
                {
                    dbConnectionStringConfigPath = Environment.CurrentDirectory; 
                }

                return dbConnectionStringConfigPath;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !File.Exists(value))
                {
                    throw new FileNotFoundException("指定的DB连接字符串配置文件不存在：" + value);
                }

                //如果配置文件改变，则可能导致连接字符串改变，故必需清除所有连接字符串的缓存以便后续重新加载字符串
                if (!string.Equals(dbConnectionStringConfigPath, value, StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var item in dbConnNamesCacheDic)
                    {
                        MemoryCacheUtil.RemoveCacheItem(item.Key);
                    }
                }

                dbConnectionStringConfigPath = value;
            }
        }

        public SqlDapperUtil(string connName)
        {
            dbConnectionName = connName;
            if (!dbConnNamesCacheDic.ContainsKey(connName)) //如果静态缓存中没有，则加入到静态缓存中
            {
                dbConnNamesCacheDic[connName] = true;
            }

        }


        /// <summary>
        /// 使用事务
        /// </summary>
        public void UseDbTransaction()
        {
            useDbTransaction = true;
        }


        /// <summary>
        /// 获取一个值，param可以是SQL参数也可以是匿名对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T GetValue<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return UseDbConnection((dbConn) =>
            {
                return dbConn.ExecuteScalar<T>(sql, param, dbTransaction, commandTimeout, commandType);
            });
        }

        /// <summary>
        /// 获取第一行的所有值，param可以是SQL参数也可以是匿名对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public Dictionary<string, dynamic> GetFirstValues(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return UseDbConnection((dbConn) =>
            {
                Dictionary<string, dynamic> firstValues = new Dictionary<string, dynamic>();
                List<string> indexColNameMappings = new List<string>();
                int rowIndex = 0;
                using (var reader = dbConn.ExecuteReader(sql, param, dbTransaction, commandTimeout, commandType))
                {
                    while (reader.Read())
                    {
                        if ((++rowIndex) > 1) break;
                        if (indexColNameMappings.Count == 0)
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                indexColNameMappings.Add(reader.GetName(i));
                            }
                        }

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            firstValues[indexColNameMappings[i]] = reader.GetValue(i);
                        }
                    }
                    reader.Close();
                }

                return firstValues;

            });
        }

        /// <summary>
        /// 获取一个数据模型实体类，param可以是SQL参数也可以是匿名对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T GetModel<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) where T : class
        {
            return UseDbConnection((dbConn) =>
            {
                return dbConn.QueryFirstOrDefault<T>(sql, param, dbTransaction, commandTimeout, commandType);
            });
        }

        /// <summary>
        /// 获取符合条件的所有数据模型实体类列表，param可以是SQL参数也可以是匿名对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public List<T> GetModelList<T>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) where T : class
        {
            return UseDbConnection((dbConn) =>
            {
                return dbConn.Query<T>(sql, param, dbTransaction, buffered, commandTimeout, commandType).ToList();
            });
        }

        /// <summary>
        /// 获取符合条件的所有数据并根据动态构建Model类委托来创建合适的返回结果（适用于临时性结果且无对应的模型实体类的情况）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buildModelFunc"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public T GetDynamicModel<T>(Func<IEnumerable<dynamic>, T> buildModelFunc, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            var dynamicResult = UseDbConnection((dbConn) =>
            {
                return dbConn.Query(sql, param, dbTransaction, buffered, commandTimeout, commandType);
            });

            return buildModelFunc(dynamicResult);
        }

        /// <summary>
        /// 获取符合条件的所有指定返回结果对象的列表(复合对象【如：1对多，1对1】)，param可以是SQL参数也可以是匿名对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="types"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="splitOn"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>

        public List<T> GetMultModelList<T>(string sql, Type[] types, Func<object[], T> map, object param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return UseDbConnection((dbConn) =>
            {
                return dbConn.Query<T>(sql, types, map, param, dbTransaction, buffered, splitOn, commandTimeout, commandType).ToList();
            });
        }




        /// <summary>
        /// 执行SQL命令（CRUD），param可以是SQL参数也可以是要添加的实体类
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public bool ExecuteCommand(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return UseDbConnection((dbConn) =>
            {
                int result = dbConn.Execute(sql, param, dbTransaction, commandTimeout, commandType);
                return (result > 0);
            });
        }

        /// <summary>
        /// 批量转移数据(利用SqlBulkCopy实现快速大批量插入到指定的目的表及SqlDataAdapter的批量删除)
        /// </summary>
        public bool BatchMoveData(string srcSelectSql, string srcTableName, List<SqlParameter> srcPrimarykeyParams, string destConnName, string destTableName)
        {

            using (SqlDataAdapter srcSqlDataAdapter = new SqlDataAdapter(srcSelectSql, GetDbConnectionString(dbConnectionName, out dbProviderName)))
            {
                DataTable srcTable = new DataTable();
                SqlCommand deleteCommand = null;
                try
                {
                    srcSqlDataAdapter.AcceptChangesDuringFill = true;
                    srcSqlDataAdapter.AcceptChangesDuringUpdate = false;
                    srcSqlDataAdapter.Fill(srcTable);

                    if (srcTable == null || srcTable.Rows.Count <= 0) return true;

                    string notExistsDestSqlWhere = null;
                    string deleteSrcSqlWhere = null;

                    for (int i = 0; i < srcPrimarykeyParams.Count; i++)
                    {
                        string keyColName = srcPrimarykeyParams[i].ParameterName.Replace("@", "");
                        notExistsDestSqlWhere += string.Format(" AND told.{0}=tnew.{0}", keyColName);
                        deleteSrcSqlWhere += string.Format(" AND {0}=@{0}", keyColName);
                    }

                    string dbProviderName2 = null;
                    using (var destConn = new SqlConnection(GetDbConnectionString(destConnName, out dbProviderName2)))
                    {
                        destConn.Open();

                        string tempDestTableName = "#temp_" + destTableName;
                        destConn.Execute(string.Format("select top 0 * into {0} from {1}", tempDestTableName, destTableName));
                        string destInsertCols = null;
                        using (var destSqlBulkCopy = new SqlBulkCopy(destConn))
                        {
                            try
                            {
                                destSqlBulkCopy.BulkCopyTimeout = 120;
                                destSqlBulkCopy.DestinationTableName = tempDestTableName;
                                foreach (DataColumn col in srcTable.Columns)
                                {
                                    destSqlBulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                                    destInsertCols += "," + col.ColumnName;
                                }

                                destSqlBulkCopy.BatchSize = 1000;
                                destSqlBulkCopy.WriteToServer(srcTable);
                            }
                            catch (Exception ex)
                            {
                                //LogUtil.Error("SqlDapperUtil.BatchMoveData.SqlBulkCopy:" + ex.ToString(), "SqlDapperUtil.BatchMoveData");
                            }

                            destInsertCols = destInsertCols.Substring(1);

                            destConn.Execute(string.Format("insert into {1}({0}) select {0} from {2} tnew where not exists(select 1 from {1} told where {3})",
                                             destInsertCols, destTableName, tempDestTableName, notExistsDestSqlWhere.Trim().Substring(3)), null, null, 100);
                        }
                        destConn.Close();
                    }

                    deleteCommand = new SqlCommand(string.Format("DELETE FROM {0} WHERE {1}", srcTableName, deleteSrcSqlWhere.Trim().Substring(3)), srcSqlDataAdapter.SelectCommand.Connection);
                    deleteCommand.Parameters.AddRange(srcPrimarykeyParams.ToArray());
                    deleteCommand.UpdatedRowSource = UpdateRowSource.None;
                    deleteCommand.CommandTimeout = 200;

                    srcSqlDataAdapter.DeleteCommand = deleteCommand;
                    foreach (DataRow row in srcTable.Rows)
                    {
                        row.Delete();
                    }

                    srcSqlDataAdapter.UpdateBatchSize = 1000;
                    srcSqlDataAdapter.Update(srcTable);
                    srcTable.AcceptChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    //LogUtil.Error("SqlDapperUtil.BatchMoveData:" + ex.ToString(), "SqlDapperUtil.BatchMoveData");
                    return false;
                }
                finally
                {
                    if (deleteCommand != null)
                    {
                        deleteCommand.Parameters.Clear();
                    }
                }
            }

        }

        /// <summary>
        /// 批量复制数据（把源DB中根据SQL语句查出的结果批量COPY插入到目的DB的目的表中）
        /// </summary>
        public TResult BatchCopyData<TResult>(string srcSelectSql, string destConnName, string destTableName, IDictionary<string, string> colMappings, Func<IDbConnection, TResult> afterCoppyFunc)
        {

            using (SqlDataAdapter srcSqlDataAdapter = new SqlDataAdapter(srcSelectSql, GetDbConnectionString(dbConnectionName, out dbProviderName)))
            {
                DataTable srcTable = new DataTable();
                TResult copyResult = default(TResult);
                try
                {
                    srcSqlDataAdapter.AcceptChangesDuringFill = true;
                    srcSqlDataAdapter.AcceptChangesDuringUpdate = false;
                    srcSqlDataAdapter.Fill(srcTable);

                    if (srcTable == null || srcTable.Rows.Count <= 0) return copyResult;


                    string dbProviderName2 = null;
                    using (var destConn = new SqlConnection(GetDbConnectionString(destConnName, out dbProviderName2)))
                    {
                        destConn.Open();
                        string tempDestTableName = "#temp_" + destTableName;
                        destConn.Execute(string.Format("select top 0 * into {0} from {1}", tempDestTableName, destTableName));
                        bool bcpResult = false;
                        using (var destSqlBulkCopy = new SqlBulkCopy(destConn))
                        {
                            try
                            {
                                destSqlBulkCopy.BulkCopyTimeout = 120;
                                destSqlBulkCopy.DestinationTableName = tempDestTableName;
                                foreach (var col in colMappings)
                                {
                                    destSqlBulkCopy.ColumnMappings.Add(col.Key, col.Value);
                                }

                                destSqlBulkCopy.BatchSize = 1000;
                                destSqlBulkCopy.WriteToServer(srcTable);
                                bcpResult = true;
                            }
                            catch (Exception ex)
                            {
                                //LogUtil.Error("SqlDapperUtil.BatchMoveData.SqlBulkCopy:" + ex.ToString(), "SqlDapperUtil.BatchMoveData");
                            }
                        }

                        if (bcpResult)
                        {
                            copyResult = afterCoppyFunc(destConn);
                        }

                        destConn.Close();
                    }

                    return copyResult;
                }
                catch (Exception ex)
                {
                    //LogUtil.Error("SqlDapperUtil.BatchCopyData:" + ex.ToString(), "SqlDapperUtil.BatchCopyData");
                    return copyResult;
                }
            }

        }


        /// <summary>
        /// 当使用了事务，则最后需要调用该方法以提交所有操作
        /// </summary>
        /// <param name="dbTransaction"></param>
        public void Commit()
        {
            try
            {
                if (dbTransaction.Connection != null && dbTransaction.Connection.State != ConnectionState.Closed)
                {
                    dbTransaction.Commit();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbTransaction.Connection != null)
                {
                    CloseDbConnection(dbTransaction.Connection);
                }
                dbTransaction.Dispose();
                dbTransaction = null;
                useDbTransaction = false;

                if (dbConnection != null)
                {
                    CloseDbConnection(dbConnection);
                }
            }
        }

        /// <summary>
        /// 当使用了事务，如果报错或需要中断执行，则需要调用该方法执行回滚操作
        /// </summary>
        /// <param name="dbTransaction"></param>
        public void Rollback()
        {
            try
            {
                if (dbTransaction.Connection != null && dbTransaction.Connection.State != ConnectionState.Closed)
                {
                    dbTransaction.Rollback();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dbTransaction.Connection != null)
                {
                    CloseDbConnection(dbTransaction.Connection);
                }

                dbTransaction.Dispose();
                dbTransaction = null;
                useDbTransaction = false;
            }
        }

        ~SqlDapperUtil()
        {
            try
            {
                CloseDbConnection(dbConnection, true);
            }
            catch
            { }
        }

    }
}
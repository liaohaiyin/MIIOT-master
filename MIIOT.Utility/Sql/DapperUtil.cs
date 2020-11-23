using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Utility
{
    public class DapperUtil
    {
        static string mysqlconnectionString = ConfigurationManager.ConnectionStrings["mysqlconn"].ToString();
        public DapperUtil(string connectString)
        {
            mysqlconnectionString = connectString;
        }
        public bool TestConnected()
        {
            try
            {
                using (IDbConnection conn = MySqlConnection())
                {
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("", ex);
            }
            return false;
        }
        public SqlConnection SqlConnection()
        {
            try
            {
                string sqlconnectionString = ConfigurationManager.ConnectionStrings["sqlconnectionString"].ToString();
                var connection = new SqlConnection(sqlconnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Log.Error("SqlConnection", ex);
            }
            return null;
        }
        public MySqlConnection MySqlConnection()
        {
                var connection = new MySqlConnection(mysqlconnectionString);
                connection.Open();
                return connection;
        }
        public string BuildInsertSqlStr<T>(T obj, string method = "insert")
        {
            var objType = obj.GetType();
            if (obj == null || objType == null)
            {
                return "";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append($" {method} into ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            strSql.Append(" (`");

            List<string> strList = new List<string>();
            PropertyInfo[] propertys = objType.GetProperties();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
              typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                //if (info.Name == "id") continue;

                strList.Add(string.Format("{0}", info.Name));
                var para = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                para.Value = info.GetValue(obj);
                parameters.Add(para);
            }
            strSql.Append(string.Join("`,`", strList));
            strSql.Append("`) values (@");
            strSql.Append(string.Join(",@", strList));
            strSql.Append(") ");
            strSql.Append(";");
            return strSql.ToString();
        }
        public int Insert<T>(IList<T> obj)
        {
            string sqlstr = BuildInsertSqlStr(obj[0]);
            if (sqlstr == "") return 0;
            //增
            using (IDbConnection conn = MySqlConnection())
            {
                return conn.Execute(sqlstr, obj);
            }
        }
        public int Insert<T>(T obj)
        {
            string sqlstr = "";
            try
            {
                sqlstr = BuildInsertSqlStr(obj);
                if (sqlstr == "") return 0;
                //增
                using (IDbConnection conn = MySqlConnection())
                {
                    return conn.Execute(sqlstr, obj);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Inser:" + sqlstr, ex);
                return 0;
            }
        }
        public int Replace<T>(IList<T> obj)
        {
            if (obj.Count == 0) return 1;
            string sqlstr = BuildInsertSqlStr(obj[0], "REPLACE");
            if (sqlstr == "") return 0;
            //增
            using (IDbConnection conn = MySqlConnection())
            {
                return conn.Execute(sqlstr, obj);
            }
        }
        public int Replace<T>(T obj)
        {
            string sqlstr = BuildInsertSqlStr(obj, "REPLACE");
            if (sqlstr == "") return 0;
            //增
            using (IDbConnection conn = MySqlConnection())
            {
                return conn.Execute(sqlstr, obj);
            }
        }
        public bool Delete<T>(T obj)
        {
            int result = 0;
            var objType = obj.GetType();
            if (obj == null || objType == null)
            {
                return result > 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE FROM ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            strSql.Append(" WHERE id=@id");
            using (IDbConnection conn = MySqlConnection())
            {
                result = conn.Execute(strSql.ToString(), obj);
            }
            return result > 0;
        }
        public void Delete<T>(string sqlstr, T obj)
        {
            //var objType = obj.GetType();
            //if (obj == null || objType == null)
            //{
            //    return;
            //}
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("DELETE FROM ");
            //strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            //strSql.Append(" WHERE id=@id");
            //strSql.Append(sqlstr);
            using (IDbConnection conn = MySqlConnection())
            {

                // string sqlCommandText = @"DELETE FROM USERS WHERE ID=@ID";
                int result = conn.Execute(sqlstr.ToString(), obj);
            }
        }
        public int Update<T>(T obj)
        {
            int result = 0;
            if (obj == null)
            {
                return result;
            }
            string strSql = UpdateStr<T>(obj);
            using (IDbConnection conn = MySqlConnection())
            {
                result = conn.Execute(strSql, obj);
            }
            return result;
        }
        public int Update<T>(IList<T> obj)
        {
            int result = 0;
            if (obj == null && obj.Count > 0)
            {
                return result;
            }
            string strSql = UpdateStr<T>(obj[0]);
            using (IDbConnection conn = MySqlConnection())
            {
                result = conn.Execute(strSql, obj);
            }
            return result;
        }
        public string UpdateStr<T>(T obj)
        {
            var objType = obj.GetType();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            strSql.Append(" SET ");
            string KeyName = "";
            List<string> strList = new List<string>();
            System.Reflection.PropertyInfo[] propertys = objType.GetProperties();
            foreach (System.Reflection.PropertyInfo info in propertys)
            {
                try
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                  typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null && attr.Description == "External")
                    {
                        continue;
                    }
                    if (attr != null && attr.Description == "OnlyInsert")
                    {
                        continue;
                    }
                    if (attr != null && attr.Description == "Key")
                    {
                        KeyName = $" WHERE `{info.Name}`=@{info.Name}";
                        continue;
                    }
                    if (info.Name == "id") continue;

                    strList.Add($"`{info.Name}`=@{info.Name}");
                }
                catch (Exception ex)
                {


                }
            }
            strSql.Append(string.Join(",", strList));
            if (string.IsNullOrWhiteSpace(KeyName))
                strSql.Append(" WHERE id=@id");
            else
                strSql.Append(KeyName);
            return strSql.ToString();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlstr">"SELECT * from dsc_goods where id=@id"</param>
        /// <param name="obj">new { id = 298 }</param>
        /// <returns></returns>
        public List<T> Query<T>(string sqlstr, object obj)
        {
            using (IDbConnection conn = MySqlConnection())
            {

                try
                {
                    var result = conn.Query<T>(sqlstr, obj).ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    Log.Error("Query", ex);
                }

                return null;
            }

        }

        public bool TransactionAction(List<DapperParams> sqls)
        {
            using (IDbConnection conn = MySqlConnection())
            {
                using (IDbTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in sqls)
                        {
                            conn.Execute(item.sqlStr, item.entity, transaction);
                        }
                        transaction.Commit();//都执行成功时提交
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Log.Error("TransactionAction", ex);
                        return false;
                    }
                }
            }
            return true;
        }

        public void Proc()
        {
            using (IDbConnection conn = MySqlConnection())
            {
                //设置参数 （input为默认参数类型，可以不写的）
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@username", "newuser", DbType.String, ParameterDirection.Input, 50);
                dp.Add("@age", 20, DbType.Int16, ParameterDirection.Input);
                dp.Add("@roleid", 2, DbType.Int16, ParameterDirection.Input);
                dp.Add("@count", 2, DbType.Int16, ParameterDirection.Output);

                //执行存储过程
                var res = conn.Execute("sp_insertUser", dp, null, null, CommandType.StoredProcedure);
                int count = dp.Get<int>("@count");//获取output参数的值
            }
        }
        private MySqlDbType GetDBType(string typestr)
        {
            switch (typestr)
            {
                case "Int32":
                    return MySqlDbType.Int32;
                case "Double":
                    return MySqlDbType.Double;
                case "DateTime":
                    return MySqlDbType.DateTime;
                case "String":
                    return MySqlDbType.VarChar;
                default:
                    return MySqlDbType.Int32;
            }
        }
    }
    public class DapperParams
    {
        public DapperParams(string sql, object obj)
        {
            sqlStr = sql;
            entity = obj;
        }
        public string sqlStr { get; set; }
        public object entity { get; set; }
    }
}

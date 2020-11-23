using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Dapper;

namespace MIIOT.Utility
{
    public class MySqlUitity
    {
        public MySqlUitity()
        {

        }
        private static object obj = new object();
        private static MySqlUitity _instance = null;
        public static MySqlUitity Ins
        {
            get
            {
                lock (obj)
                {
                    if (_instance == null)
                    { _instance = new MySqlUitity(); }
                    return _instance;
                }
            }
        }
        
        Dictionary<string, List<string>> DBNames = new Dictionary<string, List<string>>();
        List<string> Models = new List<string>();

        public DateTime BootTime = new DateTime(1997, 1, 1);
        public int PartGap = 0;
        public string _currDataBase = "";
        public void InitPartDB(DateTime bootTime, int partGap, List<string> models)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    double year = (DateTime.Now - bootTime).TotalDays / 300+1;
                    InitPartDB(bootTime, partGap, (int)year, models);
                    Thread.Sleep(3600*1000);
                }
               
            });
        }
        public void InitPartDB(DateTime bootTime, int partGap, int year, List<string> models)
        {
            try
            {
                BootTime = bootTime;//分表开始时间
                PartGap = partGap;//分表间隔
                
                string Databases = Query<tableNameModel>("SELECT DATABASE() as TableName;")[0].TableName;//取数据库名称
                foreach (var objType in models)
                {
                    List<tableNameModel> tableNames = new List<tableNameModel>();
                    string tbName = objType.ToLower();
                    if (!Models.Contains(tbName))
                        Models.Add(tbName);
                    tableNames.AddRange(Query<tableNameModel>(string.Format(@"SELECT table_name as TableName from ( select table_name from information_schema.tables where table_schema='{1}')
                                                aa WHERE table_name like '%{0}%'", tbName, Databases)));
                    if (!DBNames.ContainsKey(tbName)) {
                        List<string> m = new List<string>();
                        foreach (var item in tableNames)
                        {
                            if (item.TableName.IndexOf('z') >= 0)
                            {
                                if (!m.Contains(item.TableName))
                                    m.Add(item.TableName);
                            }
                        }
                        DBNames.Add(tbName, m);
                    }                                                          
                }
               
                int tableQty = (BootTime.AddYears(year) - BootTime).Days / partGap + 1;
                StringBuilder SqlStr = new StringBuilder();

                foreach (var item in models)
                {
                    List<string> m = new List<string>();
                    if (DBNames.ContainsKey(item)) {
                        m = DBNames[item];
                        Type tarType = item.GetTypeByStr();
                        if (tarType == null) continue;
                        for (int i = 0; i < tableQty; i++)
                        {
                            string name = item;//.ToString().ToLower().Substring(objType.ToString().LastIndexOf('.') + 1);
                            string tableName = string.Format("z_{0}_{1}", name, i.ToString().PadLeft(3, '0'));

                            bool tete = m.Contains(tableName);
                            if (!tete)
                            {
                                SqlStr.AppendFormat(
                                    "CREATE TABLE If Not Exists {1} SELECT * FROM {0} where 1=2;  ALTER TABLE {1}  ADD PRIMARY KEY (id);  ALTER TABLE {1} modify id integer auto_increment ;"
                                    , name, tableName);

                                PropertyInfo[] propertys = tarType.GetProperties();
                                foreach (PropertyInfo info in propertys)
                                {
                                    DescriptionAttribute attr = Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                                    if (attr != null && attr.Description == "INDEX")//根据字段特性加索引
                                    {
                                        SqlStr.AppendFormat("ALTER TABLE {1} add INDEX {0}idx({0}); ", info.Name, tableName);
                                    }
                                }
                                m.Add(tableName);
                                
                            }
                        }
                    }
                    
                }
                if (SqlStr.ToString().Length > 0)
                {
                    ExecuteMultiple(SqlStr.ToString());//create table
                }
            }
            catch (Exception ex)
            {
                Log.Error("InitPartDB Err", ex);
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
        public void ExecuteMultiple(string sql)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                var multiReader = connection.QueryMultiple(sql);
                multiReader.Dispose();
            }
        }
        public Task<int> InsertAsync(object item)
        {
            return Task.Factory.StartNew(() =>
            {
                return Insert(item, item.GetType());
            });
        }
        /// <summary>
        /// 快速新增
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="objType"></param>
        /// <returns></returns>
        public int Insert(object obj, Type objType)
        {
            if (obj == null || objType == null)
            {
                return 0;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ");
            strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
            strSql.Append(" (");

            List<string> strList = new List<string>();
            int i = 0;
            PropertyInfo[] propertys = objType.GetProperties();
            int filedNum = 0;
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
              typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                filedNum++;
            }
            MySqlParameter[] parameters = new MySqlParameter[filedNum];
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
              typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                if (info.Name == "id") continue;
                if (info.Name == "key")
                {

                    strList.Add(string.Format("`{0}`", info.Name));
                }
                else
                    strList.Add(string.Format("{0}", info.Name));
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            strSql.Append(string.Join(",", strList));
            strSql.Append(") values (");
            strList.Clear();
            foreach (var info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
           typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                {
                    continue;
                }
                if (info.Name == "id") continue;
                strList.Add(string.Format("@{0}", info.Name));
            }
            strSql.Append(string.Join(",", strList));
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            int count;
            try
            {
                count = 0;// DbManager.Ins.ExecuteNonquery(strSql.ToString(), parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }
        public int InsertPart<T>(string sql, T obj)
        {
            try
            {
                int i = 0;
                Type t = typeof(T);
                string tableName = t.ToString().ToLower().Substring(t.ToString().LastIndexOf('.') + 1);
                if (Models.Contains(tableName))
                {
                    DateTime Stime = BootTime;
                    DateTime Etime = DateTime.Now;
                    int DataBaseIdx = (int)((DateTime.Now - BootTime).TotalDays / PartGap);

                    string dataBase = DBNames[tableName][DataBaseIdx];
                    string SqlStr = sql.ToLower().Replace(tableName, dataBase);
                    i = Insert(SqlStr, obj);

                }
                else
                {
                    i = Insert(sql, obj);
                }
                return i;
            }
            catch (Exception ex)
            {
                Log.Error("InsertPart:{0}",ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr">insert into Person(Name,Remark) values(@Name,@Remark)</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Insert<T>(string sqlStr, T obj)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                return connection.Execute(sqlStr, obj);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr">insert into Person(Name,Remark) values(@Name,@Remark)</param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public int Insert<T>(string sqlStr, List<T> objs)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                return connection.Execute(sqlStr, objs);
            }
        }
        public int UpdatePart<T>(string sql, T person, DateTime? time = null)
        {
            try
            {
                int i = 0;
                Type t = typeof(T);
                string tableName = sql.ToLower().RegexRegion("update", "set").Trim();
                if (Models.Contains(tableName))
                {
                    if (time != null)
                    {
                        DateTime Now = (DateTime)time;
                        int DataBaseIdx = (int)((Now - BootTime).TotalDays / PartGap);
                        string dataBase = DBNames[tableName][DataBaseIdx];
                        string SqlStr = sql.ToLower().Replace(tableName, dataBase);
                        i = Update(SqlStr, person);
                    }
                    else
                    {
                        int DataBaseIdx = (int)((DateTime.Now - BootTime).TotalDays / PartGap);
                        while (DataBaseIdx >= 0)
                        {
                            string dataBase = DBNames[tableName][DataBaseIdx];
                            string SqlStr = sql.ToLower().Replace(tableName, dataBase);
                            int con = Update(SqlStr, person);
                            if (con > 0)
                                i =i+ con;
                            DataBaseIdx--;
                        }
                    }

                }
                else
                {
                    i = Update(sql, person);
                }
                return i;
            }
            catch (Exception ex)
            {
                Log.Error("UpdatePart:{0}", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr">update Person set name=@name where id=@ID</param>
        /// <param name="person"></param>
        /// <returns></returns>
        public int Update<T>(string sqlStr, T person)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                return connection.Execute(sqlStr, person);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr">update Person set name=@name where id=@ID</param>
        /// <param name="persons"></param>
        /// <returns></returns>
        public int Update<T>(string sqlStr, List<T> persons)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                return connection.Execute(sqlStr, persons);
            }
        }
        public int DeletePart<T>(string sql, object person, DateTime? time = null)
        {
            try
            {
                int i = 0;
                Type t = typeof(T);
                string tableName = t.ToString().ToLower().Substring(t.ToString().LastIndexOf('.') + 1);
                if (Models.Contains(tableName))
                {
                    if (time != null)
                    {
                        DateTime Now = (DateTime)time;
                        int DataBaseIdx = (int)((Now - BootTime).TotalDays / PartGap);
                        string dataBase = DBNames[tableName][DataBaseIdx];
                        string SqlStr = sql.ToLower().Replace(tableName, dataBase);
                        i = Delete(SqlStr, person);
                    }
                    else
                    {
                        int DataBaseIdx = (int)((DateTime.Now - BootTime).TotalDays / PartGap);
                        while (DataBaseIdx >= 0)
                        {
                            string dataBase = DBNames[tableName][DataBaseIdx];
                            string SqlStr = sql.ToLower().Replace(tableName, dataBase);
                            int con = Delete(SqlStr, person);
                            if (con > 0)
                                i = con;
                            DataBaseIdx--;
                        }
                    }
                }
                else
                {
                    i = Delete(sql, person);
                }
                return i;
            }
            catch (Exception ex)
            {
                Log.Error("DeletePart:{0}", ex);
                return 0;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr">delete from Person where id=@ID</param>
        /// <param name="person"></param>
        /// <returns></returns>
        public int Delete(string sqlStr, object person)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                return connection.Execute(sqlStr, person);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlStr">delete from Person where id=@ID</param>
        /// <param name="persons"></param>
        /// <returns></returns>
        public int Delete(string sqlStr, List<object> persons)
        {
            using (IDbConnection connection = DapperService.MySqlConnection())
            {
                return connection.Execute(sqlStr, persons);
            }
        }
        public Task<int> ExecuteNonquery(string sqlStr, MySqlParameter[] parameters)
        {
            return Task.Factory.StartNew(() =>
            {
                return Executenonquery(sqlStr, parameters);
            });
        }
        public Task<int> ExecuteNonquery(string sqlStr)
        {
            return Task.Factory.StartNew(() =>
            {
                MySqlParameter[] parameters = new MySqlParameter[0];
                return Executenonquery(sqlStr, parameters);
            });
        }

        private int Executenonquery(string sqlStr, MySqlParameter[] parameters)
        {
            int count;
            try
            {
                count = 0;// DbManager.Ins.ExecuteNonquery(sqlStr, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }

        //public Task<int> UpdateAsync(object item)
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        return Update(item, item.GetType());
        //    });
        //}
        //public int Update(object obj, Type objType)
        //{
        //    if (obj == null || objType == null)
        //    {
        //        return 0;
        //    }

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("UPDATE  ");
        //    strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));
        //    strSql.Append(" set ");

        //    string strWhere = "";
        //    List<string> strList = new List<string>();
        //    int i = 0;
        //    PropertyInfo[] propertys = objType.GetProperties();
        //    MySqlParameter[] parameters = new MySqlParameter[propertys.Length];
        //    foreach (PropertyInfo info in propertys)
        //    {
        //        if (info.Name == "id" || info.Name == "key")
        //        {

        //            strList.Add(string.Format("`{0}`=@{0}", info.Name));
        //        }
        //        else
        //            strList.Add(string.Format("{0}=@{0}", info.Name));
        //        parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
        //        parameters[i].Value = info.GetValue(obj);
        //        i++;
        //    }
        //    strSql.Append(string.Join(",", strList));
        //    strSql.Append(strWhere);

        //    int count;
        //    try
        //    {
        //        count = DbManager.Ins.ExecuteNonquery(strSql.ToString(), parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return count;
        //}

        //public Task<int> DeleteAsync(object item)
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        return Delete(item, item.GetType());
        //    });
        //}
        //public int Delete(object obj, Type objType)
        //{
        //    if (obj == null || objType == null)
        //    {
        //        return 0;
        //    }

        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("Delete from ");
        //    strSql.Append(objType.ToString().Substring(objType.ToString().LastIndexOf('.') + 1));

        //    string strWhere = "";
        //    PropertyInfo[] propertys = objType.GetProperties();
        //    MySqlParameter[] parameters = new MySqlParameter[1];
        //    foreach (PropertyInfo info in propertys)
        //    {
        //        if (info.Name == "id")
        //        {
        //            strWhere = string.Format(" where {0}=@{0}", info.Name);
        //            parameters[0] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
        //            parameters[0].Value = info.GetValue(obj);
        //        }
        //    }
        //    strSql.Append(strWhere);

        //    int count;
        //    try
        //    {
        //        count = DbManager.Ins.ExecuteNonquery(strSql.ToString(), parameters);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return count;
        //}
        public Task<int> ExecuteProcAsync(object item, string procName)
        {
            return Task.Factory.StartNew(() =>
            {
                return ExecuteProc(item, item.GetType(), procName);
            });
        }
        public int ExecuteProc(object obj, Type objType, string procName)
        {
            if (obj == null || objType == null)
            {
                return 0;
            }


            List<string> strList = new List<string>();
            int i = 0;
            PropertyInfo[] propertys = objType.GetProperties();
            MySqlParameter[] parameters = new MySqlParameter[propertys.Length - 1];
            foreach (PropertyInfo info in propertys)
            {
                if (info.Name == "id") continue;
                if (info.Name == "key")
                {
                    strList.Add(string.Format("`{0}`", info.Name));
                }
                else
                    strList.Add(string.Format("{0}", info.Name));
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            strList.Clear();
            foreach (var info in propertys)
            {
                if (info.Name == "id") continue;
                strList.Add(string.Format("@{0}", info.Name));
            }
            int count;
            try
            {
                count = 0;// DbManager.Ins.ExecuteProcNonQuery(procName, parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
            return count;
        }
        public Task<int> ExecuteProcByRetrunAsync<T>(object item, string procName)
        {
            return Task.Factory.StartNew(() =>
            {
                return ExecuteProcByRetrun<T>(item, procName);
            });
        }
        public int ExecuteProcByRetrun<T>(object obj, string procName)
        {
            if (obj == null)
            {
                return 0;
            }
            int i = 0;
            PropertyInfo[] propertys = obj.GetType().GetProperties();
            int length = 0;
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                                           typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                    continue;
                length++;
            }
            MySqlParameter[] parameters = new MySqlParameter[length];
            foreach (PropertyInfo info in propertys)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                                        typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                    continue;
                parameters[i] = new MySqlParameter(string.Format("@{0}", info.Name), GetDBType(info.PropertyType.Name));
                parameters[i].Value = info.GetValue(obj);
                i++;
            }
            try
            {
                var counta = 0;// DbManager.Ins.ExecuteProcQuery(procName, parameters);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            return 0;
        }
        public Task<bool> ExcuteCommandByTran(List<string> sqlStr)
        {
            return Task.Factory.StartNew(() =>
            {
                List<MySqlCommand> cmds = new List<MySqlCommand>();
                foreach (var item in sqlStr)
                {
                    cmds.Add(new MySqlCommand(item));
                }
                if (cmds.Count == 0) return false;
                return false;// DbManager.Ins.ExcuteCommandByTran(cmds.ToArray());
            });

        }
        public Task<List<T>> QueryAsync<T>(string sql)
            where T : new()
        {
            return Task<List<T>>.Factory.StartNew(() =>
            {
                return Query<T>(sql);
            });
        }
        public List<T> QueryPart<T>(string sql, DateTime? startTime = null, DateTime? endTime = null)
        {
            try
            {
                Type t = typeof(T);
                string tableName = t.ToString().ToLower().Substring(t.ToString().LastIndexOf('.') + 1);
                if (Models.Contains(tableName))
                {
                    List<T> list = new List<T>();
                    DateTime Stime = startTime == null ? BootTime : (DateTime)startTime;
                    DateTime Etime = endTime == null ? DateTime.Now : (DateTime)endTime;
                        //Stime = DateTime.Parse(sql.RegexRegion("time > '", "'"));
                    if (sql.IndexOf("time >'") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time >'", "'"));
                    if (sql.IndexOf("time > '") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time > '", "'"));
                    if (sql.IndexOf("time>'") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time>'", "'"));
                    if (sql.IndexOf("time> '") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time> '", "'"));


                    if (sql.IndexOf("time >='") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time >='", "'"));
                    if (sql.IndexOf("time >= '") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time >= '", "'"));
                    if (sql.IndexOf("time>='") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time>='", "'"));
                    if (sql.IndexOf("time>= '") > 0)
                        Stime = DateTime.Parse(sql.RegexRegion("time>= '", "'"));

                    if (sql.IndexOf("time <'") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time <'", "'"));
                    if (sql.IndexOf("time < '") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time < '", "'"));
                    if (sql.IndexOf("time<'") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time<'", "'"));
                    if (sql.IndexOf("time< ") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time< '", "'"));

                    if (sql.IndexOf("time <='") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time <='", "'"));
                    if (sql.IndexOf("time <= '") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time <= '", "'"));
                    if (sql.IndexOf("time<='") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time<='", "'"));
                    if (sql.IndexOf("time<= ") > 0)
                        Etime = DateTime.Parse(sql.RegexRegion("time<= '", "'"));

                    int StartDataBaseIdx = (int)((Stime - BootTime).TotalDays / PartGap);
                    int EndDataBaseIdx = (int)((Etime - BootTime).TotalDays / PartGap);
                    //不为负数
                    StartDataBaseIdx = StartDataBaseIdx < 0 ? 0 : StartDataBaseIdx;
                    EndDataBaseIdx = EndDataBaseIdx < 0 ? 0 : EndDataBaseIdx;

                    string dataBase = DBNames[tableName][StartDataBaseIdx];
                    string EdataBase = DBNames[tableName][EndDataBaseIdx];
                    for (int i = StartDataBaseIdx; i <= EndDataBaseIdx; i++)
                    {
                        EdataBase = DBNames[tableName][i];
                        string SqlStr = sql.ToLower().Replace(tableName, EdataBase);
                        list.AddRange(Query<T>(SqlStr));
                    }
                    return list;
                }
                else
                    return Query<T>(sql).ToList();
            }
            catch (Exception ex)
            {
                Log.Error("QueryPart:{0}",ex);
                return new List<T>();
            }
        }
        public List<T> Query<T>(string sql)
        {
            using (IDbConnection conn = DapperService.MySqlConnection())
            {
                return conn.Query<T>(sql).ToList();
            }
        }

        private T parasToList<T>(MySqlParameter[] outparameters)
        {
            if (outparameters == null || outparameters.Length <= 0)
                return default(T);
            T _t = Activator.CreateInstance<T>();
            //获取对象所有属性
            PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
            foreach (PropertyInfo info in propertyInfo)
            {
                DescriptionAttribute attr = Attribute.GetCustomAttribute(info,
                                      typeof(DescriptionAttribute), false) as DescriptionAttribute;
                if (attr != null && attr.Description == "External")
                    continue;
                foreach (var item in outparameters)
                {
                    if (item.ParameterName.ToLower().Substring(1).Equals(info.Name.ToLower()))
                    {
                        info.SetValue(_t, item.Value);
                    }
                }

            }
            return _t;
        }
        private IList<T> DataSetToList<T>(DataTable dt)
        {

            IList<T> list = new List<T>();
            //确认参数有效
            if (dt == null || dt.Rows.Count <= 0)
                return list;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();

                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToLower().Equals(info.Name.ToLower()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }

                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }


    }
    public class DapperService
    {

        public static MySqlConnection MySqlConnection()
        {
            string mysqlConnectionStr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            var connection = new MySqlConnection(mysqlConnectionStr);
            connection.Open();
            return connection;
        }

    }
    public class tableNameModel
    {
        public string TableName { get; set; }
    }
}

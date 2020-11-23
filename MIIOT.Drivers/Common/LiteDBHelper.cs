using LiteDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Drivers
{
    public class LiteDBHelper
    {
        private static object obj = new object();

        public static LiteDBHelper _instance = null;

        public static string DBNAME = "DEFAULT";
        public string PATH;
        public string fileName;
        public static LiteDBHelper Ins
        {
            get
            {
                lock (obj)
                {
                    if (_instance == null)
                    {
                        _instance = new LiteDBHelper();
                    }
                    return _instance;
                }
            }
        }
        public LiteDBHelper()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName">litedb数据库名称</param>
        /// <param name="path">数据库储存的位置</param>
        public void InitDB(string dbName, string path)
        {
            DBNAME = dbName;
            PATH = path;
            fileName = PATH + "\\" + DBNAME + ".db";
        }
        public IEnumerable<string> GetAllCollection()
        {
            try
            {
                using (var db = new LiteDatabase(@fileName))
                {
                    var tmeo = db.GetCollectionNames();
                    return db.GetCollectionNames();
                }
            }
            catch (Exception ex)
            {
                Log.Error("LiteDB-Insert-Error:", ex);
                return null;
            }
        }
        public List<T> GetCollection<T>()
        {
            using (var db = new LiteDatabase(@fileName))
            {
                Type type = typeof(T);
                string tableName = type.Name.ToString();
                var customers = db.GetCollection<T>(tableName);

                return customers.FindAll().ToList();
            }
        }
        
        public void GetCollection(string tableName)
        {
            using (var db = new LiteDatabase(@fileName))
            {
                var documents = db.GetCollection(tableName).FindAll();

                var dt = new DataTable(tableName);
                foreach (var doc in documents)
                {
                    var dr = dt.NewRow() as DataRow;
                    if (dr != null)
                    {
                        foreach (var property in doc.RawValue)
                        {
                            if (!property.Value.IsMaxValue && !property.Value.IsMinValue)
                            {
                                if (!dt.Columns.Contains(property.Key))
                                {
                                    dt.Columns.Add(new DataColumn(property.Key, typeof(string)));
                                }
                                switch (property.Value.Type)
                                {
                                    case BsonType.Null:
                                        dr[property.Key] = "[NULL]";
                                        break;
                                    case BsonType.Document:
                                        dr[property.Key] = property.Value.AsDocument.RawValue.ContainsKey("_type")
                                            ? $"[OBJECT: {property.Value.AsDocument.RawValue["_type"]}]"
                                            : "[OBJECT]";
                                        break;
                                    case BsonType.Array:
                                        dr[property.Key] = $"[ARRAY({property.Value.AsArray.Count})]";
                                        break;
                                    case BsonType.Binary:
                                        dr[property.Key] = $"[BINARY({property.Value.AsBinary.Length})]";
                                        break;
                                    case BsonType.DateTime:
                                        dr[property.Key] = property.Value.AsDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                                        break;
                                    case BsonType.String:
                                        dr[property.Key] = property.Value.AsString;
                                        break;
                                    case BsonType.Int32:
                                    case BsonType.Int64:
                                        dr[property.Key] = property.Value.AsInt64.ToString();
                                        break;
                                    case BsonType.Decimal:
                                    case BsonType.Double:
                                        dr[property.Key] = property.Value.AsDecimal.ToString(CultureInfo.InvariantCulture);
                                        break;
                                    default:
                                        dr[property.Key] = property.Value.ToString();
                                        break;
                                }
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }
        }
        public bool Insert<T>(T t)
        {
            int res = 0;
            string tableName = "";
            try
            {
                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                     tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);
                    res = customers.Insert(t);
                    if (res > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "指定的转换无效。")
                    return true;
                Log.Error("LiteDB-Insert-Error:tableName:", ex);
                return false;
            }

        }

        public int Insert<T>(List<T> t)
        {
            int res = 0;
            try
            {

                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                    string tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);

                    res = customers.Insert(t);
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return res;
            }

        }
        public bool Update<T>(T t)
        {
            bool res = false;
            try
            {

                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                    string tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);
                    res = customers.Update(t);
                    return res;
                }
            }
            catch (Exception ex)
            {

                Log.Error("LiteDB-Update-Error:{0}", ex);
                return res;
            }


        }

        public bool Upsert<T>(T t)
        {
            bool res = false;
            try
            {

                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                    string tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);
                    res = customers.Upsert(t);
                    return res;
                }
            }
            catch (Exception ex)
            {

                Log.Error("LiteDB-Update-Error:{0}", ex);
                return res;
            }


        }

        public int Update<T>(List<T> t)
        {
            int res = 0;
            try
            {

                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                    string tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);
                    res = customers.Update(t);
                    return res;

                }
            }
            catch (Exception ex)
            {

                Log.Error("LiteDB-UpdateList-Error:{0}", ex);
                return res;
            }


        }
        public bool Delete<T>(long Id)
        {
            try
            {
                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                    string tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);
                    bool res = customers.Delete(Id);
                    return res;
                }
            }
            catch (Exception ex)
            {

                Log.Error("LiteDB-Delete-Error:{0}", ex);
                return false;
            }
        }

        public int Delete<T>(Query query)
        {
            int res = 0;
            try
            {

                using (var db = new LiteDatabase(@fileName))
                {
                    Type type = typeof(T);
                    string tableName = type.Name.ToString();
                    var customers = db.GetCollection<T>(tableName);
                    res = customers.Delete(query);
                    return res;
                }
            }
            catch (Exception ex)
            {

                Log.Error("LiteDB-DeleteList-Error:{0}", ex);
                return res;
            }

        }

    }
}

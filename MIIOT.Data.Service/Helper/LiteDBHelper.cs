using LiteDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Service.Helper
{
    public static class LiteDBHelper
    {

        public static string DBNAME = "DEFAULT";
        public static string PATH;
        public static string fileName;

        static LiteDBHelper()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbName">litedb数据库名称</param>
        /// <param name="path">数据库储存的位置</param>
        public static void InitDB(string dbName, string path)
        {
            DBNAME = dbName;
            PATH = path;
            fileName = PATH + "\\" + DBNAME + ".db";
        }
        public static IEnumerable<string> GetAllCollection()
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
                return null;
            }
        }
        public static List<T> GetCollection<T>()
        {
            using (var db = new LiteDatabase(@fileName))
            {
                Type type = typeof(T);
                string tableName = type.Name.ToString();
                var customers = db.GetCollection<T>(tableName);

                return customers.FindAll().ToList();
            }
        }
        
        public static void GetCollection(string tableName)
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
        public static bool Insert<T>(T t)
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
                return false;
            }

        }

        public static int Insert<T>(List<T> t)
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
            }

        }
        public static bool Update<T>(T t)
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
                return res;
            }


        }

        public static bool Upsert<T>(T t)
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
                return res;
            }


        }

        public static int Update<T>(List<T> t)
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
                return res;
            }


        }
        public static bool Delete<T>(long Id)
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
                return false;
            }
        }

        public static int Delete<T>(Query query)
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
                return res;
            }
        }

    }
}

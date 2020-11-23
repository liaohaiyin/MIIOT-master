using MIIOT.Data;
using MIIOT.DiagManager.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;

namespace MIIOT.Data.Service
{
    public class DbService
    {
        static DbService()
        {
            _default = new DbService();
            try
            {
                _default.ConnectionString = ConfigurationManager.ConnectionStrings["DbConnStr"].ConnectionString;
            }
            catch
            {

            }
        }

        private static DbService _default;
        /// <summary>
        /// 默认数据库
        /// </summary>
        public static DbService Default
        {
            get
            {
                return _default;
            }
        }

        public string ConnectionString { get; set; }
        /// <summary>
        /// 创建一个db对象
        /// </summary>
        /// <returns></returns>
        public Db CreateDb()
        {
            return Db.CreateDb(ConnectionString);
        }
        /// <summary>
        /// 获取所有数据
        /// 没有数据或者异常都返回空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>()
        {
            //Thread.Sleep(new Random().Next(200, 1000));//测试
            return Query<T>(null).Data ?? new List<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<T> GetPage<T>(Expression<Func<T, bool>> where,
            int pageNo,
            int pageSize,
            out int total)
            where T : IEntity
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            total = 0;
            using (var db = CreateDb())
            {
                var ret = db.Query<T>().Where(where).OrderBy(p => p.ID, OrderByType.Asc).Page(pageNo, pageSize);
                total = ret.Total;
                return ret.Data ?? new List<T>();
            }
        }

        /// <summary>
        /// 查询数据
        /// 没有数据或者异常都返回空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> FindAll<T>(Expression<Func<T, bool>> where)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            return Query<T>(where).Data ?? new List<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public QueryDbResult<T> Query<T>(Expression<Func<T, bool>> where)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Query<T>().Where(where).ToList();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public QueryFirstDbResult<T> QueryFirstOrDefault<T>(Expression<Func<T, bool>> where)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Query<T>().Where(where).FirstOrDefault();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecuteDbResult Insert<T>(T model)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Insert<T>().Values(model).Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public ExecuteDbResult BatchInsert<T>(List<T> models)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Insert<T>().Values(models).Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecuteDbResult Update<T>(T model)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Update<T>().SetModel(model).Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecuteDbResult Update<T>(object model, Expression<Func<T, bool>> where)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Update<T>().Set(model, where).Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ExecuteDbResult BatchUpdate<T>(List<T> models)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Update<T>().SetModels(models).Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExecuteDbResult Delete<T>(T model)
            where T : IEntity
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Delete<T>().Where(p => p.ID == model.ID).Execute();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public ExecuteDbResult Delete<T>(Expression<Func<T, bool>> where)
        {
            //Thread.Sleep(new Random().Next(50, 500));//测试
            using (var db = CreateDb())
            {
                return db.Delete<T>().Where(where).Execute();
            }
        }
    }
}

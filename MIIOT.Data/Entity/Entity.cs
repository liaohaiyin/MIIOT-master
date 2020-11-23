using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class Entity
    {
        private static ConcurrentDictionary<Type, EntityInfo> cache = new ConcurrentDictionary<Type, EntityInfo>();

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EntityInfo Get<T>()
        {
            var type = typeof(T);
            EntityInfo entityInfo = null;
            if (!cache.TryGetValue(type, out entityInfo))
            {
                entityInfo = new EntityInfo(type);
                cache.TryAdd(type, entityInfo);
            }
            return entityInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static EntityInfo Get(string tableName, object obj)
        {
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("tableName");
            if (obj == null) throw new ArgumentNullException("obj");
            
            var type = obj.GetType();
            return new EntityInfo(tableName, type);
        }
    }
}

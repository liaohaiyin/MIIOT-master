using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    /// <summary>
    /// 表实体信息
    /// </summary>
    public class EntityInfo
    {
        /// <summary>
        /// 实体对象的类型
        /// </summary>
        public Type EntityType { get; }
        /// <summary>
        /// 表名/视图名
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 列信息
        /// </summary>
        public ColumnCollection Columns { get; }
        /// <summary>
        /// 主键
        /// </summary>
        public ColumnInfo PrimaryKey { get; }

        /// <summary>
        /// 初始化表实体信息
        /// </summary>
        /// <param name="entity"></param>
        public EntityInfo(Type entityType)
        {
            this.EntityType = entityType;
            var props = entityType.GetProperties();
            var cols = props.Where(p=>p.GetCustomAttribute<NotMappedAttribute>() == null).Select(p => new ColumnInfo(p));
            this.Name = entityType.Name;//用类型名称做表名
            this.Columns = new ColumnCollection(cols);
            var primaryKey = cols.FirstOrDefault(p => p.IsPrimaryKey || p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase));
            if (primaryKey == null)
                primaryKey = cols.FirstOrDefault();
            this.PrimaryKey = primaryKey;
        }

        public EntityInfo(string tableNamme, Type entityType)
        {
            this.EntityType = entityType;
            var props = entityType.GetProperties();
            var cols = props.Select(p => new ColumnInfo(p));
            this.Name = tableNamme;
            this.Columns = new ColumnCollection(cols);
            var primaryKey = cols.FirstOrDefault(p => p.IsPrimaryKey || p.Name.Equals("ID", StringComparison.OrdinalIgnoreCase));
            if (primaryKey == null)
                primaryKey = cols.FirstOrDefault();
            this.PrimaryKey = primaryKey;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    /// <summary>
    /// 列信息
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// 默认主键
        /// </summary>
        public static string DefaultPrimeryKey { get; set; } = "ID";
        /// <summary>
        /// 默认自增
        /// </summary>
        public static string DefaultIdentityColumn{ get; set; } = "ID";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 自增
        /// </summary>
        public IdentityType IdentityType { get; }
        /// <summary>
        /// 主键
        /// </summary>
        public bool IsPrimaryKey { get; }
        /// <summary>
        /// 
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// 用PropertyInfo实例化列信息
        /// </summary>
        /// <param name="prop"></param>
        public ColumnInfo(PropertyInfo prop)
        {
            if (prop == null)
                throw new ArgumentNullException("porp");

            this.PropertyInfo = prop;
            this.Name = prop.Name;
            var identityAttr = prop.GetCustomAttribute<IdentityAttribute>();
            if (identityAttr == null)
            {
                if(this.Name.Equals(DefaultIdentityColumn, StringComparison.OrdinalIgnoreCase))
                    this.IdentityType = IdentityType.Db;
                else
                    this.IdentityType = IdentityType.None;
            }
            else
                this.IdentityType = identityAttr.IdentityType;
            var primaryKeyAttr = prop.GetCustomAttribute<PrimaryKeyAttribute>();
            if(primaryKeyAttr == null)
                this.IsPrimaryKey = this.Name.Equals(DefaultPrimeryKey, StringComparison.OrdinalIgnoreCase);
            else
                this.IsPrimaryKey = true;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="value"></param>
        public void SetValue(object entity, object value)
        {
            this.PropertyInfo.SetValue(entity, value);
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public object GetValue(object entity)
        {
            return this.PropertyInfo.GetValue(entity);
        }
    }

    /// <summary>
    /// 自增类型
    /// </summary>
    public enum IdentityType
    {
        /// <summary>
        /// 非自增字段
        /// </summary>
        None = 0,
        /// <summary>
        /// 数据库管理自增
        /// </summary>
        Db = 1,
        /// <summary>
        /// 程序自定义自增
        /// </summary>
        Customize,
    }

    /// <summary>
    /// 列集合
    /// </summary>
    public class ColumnCollection : IReadOnlyCollection<ColumnInfo>
    {
        /// <summary>
        /// 列集合
        /// </summary>
        private List<ColumnInfo> cols = null;
        /// <summary>
        /// 列数
        /// </summary>
        public int Count => this.cols.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cols"></param>
        public ColumnCollection(IEnumerable<ColumnInfo> cols)
        {
            this.cols = cols.ToList();
        }

        public IEnumerator<ColumnInfo> GetEnumerator()
        {
            return this.cols.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.cols.GetEnumerator();
        }
        /// <summary>
        /// 按列名获取列信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ColumnInfo this[string name]
        {
            get
            {
                return this.cols.Find(p => p.Name == name);
            }
        }
    }
}

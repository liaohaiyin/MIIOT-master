using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    /// <summary>
    /// 自增标识
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class IdentityAttribute : Attribute
    {
        /// <summary>
        /// 自增方式
        /// </summary>
        public IdentityType IdentityType { get; }

        /// <summary>
        /// 默认数据库自增字段
        /// </summary>

        public IdentityAttribute() : this(IdentityType.Db)
        {
            
        }

        /// <summary>
        /// 自定义自增字段
        /// </summary>
        /// <param name="type"></param>
        public IdentityAttribute(IdentityType type)
        {
            this.IdentityType = type;
        }
    }
}

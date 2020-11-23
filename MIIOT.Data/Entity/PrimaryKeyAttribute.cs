using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryKeyAttribute : Attribute
    {
    }
}

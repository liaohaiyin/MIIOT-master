using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    /// <summary>
    /// 属性验证基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class BaseRule : Attribute, IRule
    {
        /// <summary>
        /// 属性描述名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract ModelValidationResult Validate(object value);
    }
}

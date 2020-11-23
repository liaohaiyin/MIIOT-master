using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    /// <summary>
    /// 范围限制
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class RangeAttribute : BaseRule
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public object Min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public object Max { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public RangeAttribute(string name, object min, object max)
        {
            if (min == null) throw new ArgumentNullException("min");
            if (max == null) throw new ArgumentNullException("max");
            this.Min = min;
            this.Max = max;
            this.Name = name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override ModelValidationResult Validate(object value)
        {
            if (value == null)
                return ModelValidationResult.Ok();
            if (Comparer.Default.Compare(value, this.Min) < 0)
                return ModelValidationResult.RangeError(this.Name, this.Min.ToString(), this.Max.ToString());
            if (Comparer.Default.Compare(value, this.Max) > 0)
                return ModelValidationResult.RangeError(this.Name, this.Min.ToString(), this.Max.ToString());

            return ModelValidationResult.Ok();
        }
    }
}

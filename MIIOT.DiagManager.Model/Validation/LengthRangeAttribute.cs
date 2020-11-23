using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    /// <summary>
    /// 长度范围限制
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class LengthRangeAttribute : BaseRule
    {
        /// <summary>
        /// 最小值
        /// </summary>
        public int Min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public int Max { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public LengthRangeAttribute(string name, int min, int max)
        {
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

            if (value.ToString().Length < this.Min)
                return ModelValidationResult.RangeError(this.Name, this.Min.ToString(), this.Max.ToString());
            if (value.ToString().Length > this.Max)
                return ModelValidationResult.RangeError(this.Name, this.Min.ToString(), this.Max.ToString());

            return ModelValidationResult.Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    /// <summary>
    /// 必填项
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class RequiredAttribute : BaseRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public RequiredAttribute(string name)
        {
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
                return ModelValidationResult.RequiredError(this.Name);

            if (value is string && string.IsNullOrWhiteSpace(value.ToString()))
                return ModelValidationResult.RequiredError(this.Name);

            return ModelValidationResult.Ok();
        }
    }
}

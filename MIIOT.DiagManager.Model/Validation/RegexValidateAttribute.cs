using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    /// <summary>
    /// 正则规则验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    class RegexValidateAttribute : BaseRule
    {
        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public RegexValidateAttribute(string name, string regexString)
        {
            this.Name = name;
            this.RegexString = regexString;
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

            try
            {
                if (!Regex.IsMatch(value.ToString(), this.RegexString))
                    return ModelValidationResult.RegexError(this.Name);
            }
            catch
            {

            }
            
            return ModelValidationResult.Ok();
        }
    }
}

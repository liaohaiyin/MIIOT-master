using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    public class ModelValidationResult
    {
        /// <summary>
        /// 验证是否通过
        /// </summary>
        public bool CheckAccess { get; set; }
        /// <summary>
        /// 验证失败的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ModelValidationResult Error(string message)
        {
            return new ModelValidationResult() { CheckAccess = false, Message = message };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ModelValidationResult RequiredError(string name)
        {
            return new ModelValidationResult() { CheckAccess = false, Message = ModelValidation.RequiredErrorFormat(name) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ModelValidationResult LengthRangeError(string name, string min, string max)
        {
            return new ModelValidationResult() { CheckAccess = false, Message = ModelValidation.LengthRangeErrorFormat(name, min, max) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static ModelValidationResult RangeError(string name, string min, string max)
        {
            return new ModelValidationResult() { CheckAccess = false, Message = ModelValidation.RangeErrorFormat(name, min, max) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ModelValidationResult RegexError(string name)
        {
            return new ModelValidationResult() { CheckAccess = false, Message = ModelValidation.RegexErrorFormat(name) };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ModelValidationResult Ok()
        {
            return new ModelValidationResult() { CheckAccess = true, Message = string.Empty };
        }
    }
}

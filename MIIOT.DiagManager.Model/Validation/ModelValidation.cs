using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model.Validation
{
    /// <summary>
    /// 属性有效性验证
    /// </summary>
    public class ModelValidation
    {
        /// <summary>
        /// {name}长度必须在{min}和{max}之间。
        /// </summary>
        public static Func<string, string, string, string> LengthRangeErrorFormat { get; set; } = (name, min, max) => { return "{name}长度必须在{min}和{max}之间。".Replace("{name}", name).Replace("{min}", min).Replace("{max}", max); };


        /// <summary>
        /// {name}必须在{min}和{max}之间。
        /// </summary>
        public static Func<string, string, string, string> RangeErrorFormat { get; set; } = (name, min, max) => { return "{name}必须在{min}和{max}之间。".Replace("{name}", name).Replace("{min}", min).Replace("{max}", max); };

        /// <summary>
        /// {name}是必填项。
        /// </summary>
        public static Func<string, string> RequiredErrorFormat { get; set; } = (name) => { return "{name}是必填项。".Replace("{name}", name); };

        /// <summary>
        /// {name}格式错误。
        /// </summary>
        public static Func<string, string> RegexErrorFormat { get; set; } = (name) => { return "{name}格式错误。".Replace("{name}", name); };
        /// <summary>
        /// 验证属性
        /// </summary>
        /// <param name="model"></param>
        /// <returns>未通过验证的结果</returns>
        public static List<ModelValidationResult> Validate(object model)
        {
            var rets = new List<ModelValidationResult>();
            if (model != null)
            {
                try
                {
                    var props = model.GetType().GetProperties();
                    foreach (var prop in props)
                    {

                        var propAttr = prop.GetCustomAttribute<PropertyValidateAttribute>();
                        if (propAttr != null)
                        {
                            var propRets = Validate(prop.GetValue(model));
                            var errorRets = propRets.FindAll(p => p.CheckAccess == false);
                            if (errorRets.Count > 0)
                            {
                                rets.AddRange(errorRets);
                            }
                            continue;
                        }

                        var attrs = prop.GetCustomAttributes<BaseRule>();
                        if (attrs == null || attrs.Count() == 0) continue;
                        foreach (var attr in attrs)
                        {
                            var ret = attr.Validate(prop.GetValue(model));
                            if (!ret.CheckAccess)
                            {
                                rets.Add(ret);
                                break;
                            }
                        }
                    }
                }
                catch
                {

                }
            }

            return rets;
        }
    }
}

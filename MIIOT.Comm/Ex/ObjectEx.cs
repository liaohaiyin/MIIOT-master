using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Comm
{
    /// <summary>
    /// Object扩展方法
    /// </summary>
    public static class ObjectEx
    {
        /// <summary>
        /// 对象属性和值转换为Dictionary
        /// 若对象为string，则当作json转换为Dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            if (obj is string)
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString());
            }
            else
            {
                var type = obj.GetType();
                var props = type.GetProperties();
                return props.ToDictionary(k => k.Name, v => v.GetValue(obj));
            }
        }
        /// <summary>
        /// 对象 to json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 源对象属性值复制到目标对象中的相同属性
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void PropertyCopyTo(this object source, object target)
        {
            var sProps = source.GetType().GetProperties();
            var tProps = target.GetType().GetProperties();
            foreach (var sProp in sProps)
            {
                var tProp = tProps.FirstOrDefault(p => p.Name == sProp.Name);
                if (tProp == null || tProp.SetMethod == null)
                    continue;

                tProp.SetValue(target, sProp.GetValue(source));
            }
        }

    }
}

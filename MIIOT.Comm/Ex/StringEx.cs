using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Comm.Ex
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static class StringEx
    {
        /// <summary>
        /// 相似度对比
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value">需要对比的字符串</param>
        /// <param name="ignoreCase">是否忽略大小写</param>
        /// <returns></returns>
        public static double SimilarTo(this string str, string value, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                str = str.ToLower();
                value = value.ToLower();
            }
            if (str == value)
            {
                return 1;
            }
            if (str == null || value == null || str == "" || value == "")
            {
                return 0;
            }
            int colNum = str.Length + 1;
            int rowNum = value.Length + 1;
            var s = new int[3] { 0, 0, 0 };//上  左  左上
            var box = new int[colNum, rowNum];
            for (int c = 0; c < colNum; c++)
            {
                box[c, 0] = c;
            }
            for (int r = 0; r < rowNum; r++)
            {
                box[0, r] = r;
            }

            for (int c = 1; c < colNum; c++)
            {
                for (int r = 1; r < rowNum; r++)
                {
                    s[0] = box[c, r - 1];
                    s[1] = box[c - 1, r];
                    s[2] = box[c - 1, r - 1];
                    if (str[c - 1] == value[r - 1])
                    {
                        s[2] = s[2] + 1;
                    }
                    box[c, r] = s.Min();
                }
            }
            var maxLen = colNum > rowNum ? colNum - 1 : rowNum - 1;
            return 1d - ((double)box[colNum - 1, rowNum - 1] / (double)maxLen);
        }


        /// <summary>
        /// json字符串 to 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        /// <summary>
        /// json字符串 to 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool TryToJsonObject<T>(this string value, out T obj)
        {
            try
            {
                obj = ToJsonObject<T>(value);
                return true;
            }
            catch
            {
                obj = default(T);
                return false;
            }
        }
        /// <summary>
        /// json字符串中的属性复制到对象属性
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target"></param>
        public static void PopulateObject(this string value, object target)
        {
            JsonConvert.PopulateObject(value, target);
        }
        /// <summary>
        /// json字符串中的属性复制到对象属性
        /// </summary>
        /// <param name="value"></param>
        /// <param name="target"></param>
        public static bool TryPopulateObject(this string value, object target)
        {
            try
            {
                PopulateObject(value, target);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取json字符串中的某个键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJsonValue<T>(this string json, string key)
        {
            return JsonConvert.DeserializeObject<JObject>(json).Value<T>(key);
        }


        public static T ToEnumFromDescription<T>(this string des)
            where T : Enum
        {
            var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            DescriptionAttribute desAttr = null;
            foreach (var f in fields)
            {
                desAttr = f.GetCustomAttribute<DescriptionAttribute>();
                if (desAttr == null)
                    continue;
                if (desAttr.Description == des)
                    return (T)f.GetValue(null);
            }
            return default(T);
        }
    }
}

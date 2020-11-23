using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MIIOT.Data.Entity
{
    public static class SqlMethodEx
    {
        public static bool In<T>(this T t, params T[] arrs)
        {
            return arrs.Contains<T>(t);
        }
        public static bool In<T>(this T t, IEnumerable<T> arrs)
        {
            return arrs.Contains<T>(t);
        }
        public static bool NotIn<T>(this T t, params T[] arrs)
        {
            return !arrs.Contains<T>(t);
        }
        public static bool Between<T>(this T t, T t1, T t2)
        {
            try
            {
                return Comparer<T>.Default.Compare(t, t1) >= 0 && Comparer<T>.Default.Compare(t2, t) >= 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool Like(this string text, string match)
        {
            return Regex.Match(text, match).Success;
        }
        public static bool NotLike(this string text, string match)
        {
            return !Regex.Match(text, match).Success;
        }
    }
}

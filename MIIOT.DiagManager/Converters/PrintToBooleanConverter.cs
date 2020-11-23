using MIIOT.DiagManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MIIOT.DiagManager.Converters
{
    public class PrintToBooleanConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] == null && value.Length < 2)
                return true;
            var list = value[0] as IEnumerable<pub_accept_dtl>;
            int? printNum = int.Parse(value[1].ToString());
            if (list.Count() <= 0 || printNum == null)
                return true;
            int count = (list.ElementAt(0)?.delivery_qty ?? 0) - (printNum ?? 0);
            return (count == 0 ? false : true);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

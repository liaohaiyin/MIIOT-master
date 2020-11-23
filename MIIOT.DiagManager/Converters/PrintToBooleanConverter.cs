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
            var lstPrint = value[1] as IEnumerable<LabelInfo>;
            if (list.Count() <= 0 || lstPrint.Count() <= 0)
                return true;
            int count = list.Count() - lstPrint.Count();
            return (count == 0 ? false : true);
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

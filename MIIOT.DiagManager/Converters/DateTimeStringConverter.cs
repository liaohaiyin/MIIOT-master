using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MIIOT.DiagManager.Converters
{
    public class DateTimeStringConverter : IValueConverter
    {
        public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value.GetType() != typeof(DateTime) && value.GetType() != typeof(DateTime?)))
                return DependencyProperty.UnsetValue;


            var time = (DateTime)value;
            return time.ToString(DateTimeFormat);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }


    public class DateTimeStringConverter2 : IMultiValueConverter
    {
        public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 3 || values[0].GetType() != typeof(DateTime))
                return string.Empty;


            var time = (DateTime)values[0];

            if (values[1]?.Equals(values[2]) == true)
                return time.ToString(DateTimeFormat);
            return
                string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { };
        }
    }
}

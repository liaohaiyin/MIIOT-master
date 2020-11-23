
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MIIOT.DiagManager.Converters
{
    [ValueConversion(typeof(int), typeof(SolidColorBrush))]
    public class EqualValueConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] == null && value.Length <= 2)
                return new SolidColorBrush(Colors.Black);

            if (value[0].Equals(value[1]))
                return new SolidColorBrush(Colors.Black);
            else
                return new SolidColorBrush(Colors.Red);

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
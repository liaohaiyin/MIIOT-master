using MIIOT.Comm;
using MIIOT.Data.Entity;
using MIIOT.DiagManager.Model;
using MIIOT.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace MIIOT.DiagManager.Converters
{
    public class FilterFieldColorConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] == null && value.Length <= 2)
                return new SolidColorBrush(Colors.Black);
            var list = value[0] as IEnumerable<ViewProfuctInfo>;
            if (list == null)
                return new SolidColorBrush(Colors.Black);
            var item = list.FirstOrDefault(p => p.ProductNo.Equals(value[1]));
            if (item != null)
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MIIOT.Converter
{
  public  class SysParaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "缺省";
            }

            var temp = CacheData.Ins._sysParas.FirstOrDefault(a => a.ParaKey == value.ToString());
            if (temp != null)
                return temp.ParaValue;
            else return "缺省";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

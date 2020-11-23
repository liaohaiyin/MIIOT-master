using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace MIIOT.Trical.Converter
{
    public class StringRepeatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                var builder = new StringBuilder();
                int num;
                if (parameter is string numStr)
                {
                    if (!int.TryParse(numStr, out num))
                    {
                        return strValue;
                    }
                }
                else if (parameter is int intValue)
                {
                    num = intValue;
                }
                else
                {
                    return strValue;
                }
                for (var i = 0; i < num; i++)
                {
                    builder.Append(strValue);
                }
                return builder.ToString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
    public class IndentConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double colunwidth = 10;
            double left = 0.0;


            UIElement element = value as TreeViewItem;
            while (element.GetType() != typeof(TreeView))
            {
                element = (UIElement)VisualTreeHelper.GetParent(element);
                if (element.GetType() == typeof(TreeViewItem))
                    left += colunwidth;
            }
            return new Thickness(left, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

 

    public class VisibleToReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class DateConvertToColor : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            var calendarDayButton = (CalendarDayButton)values;
            var dateTime = (DateTime)calendarDayButton.DataContext;
            if (!calendarDayButton.IsMouseOver && !calendarDayButton.IsSelected && !calendarDayButton.IsBlackedOut && (dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday))
                return new SolidColorBrush(Color.FromArgb(255, 255, 47, 47));
            else
                return new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class OppositeVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == Visibility.Visible.ToString())
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AcceptVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "已验收")
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CountVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != "0")
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "缺省";
            else
            {
                var temp = CacheData.Ins.Stocks.FirstOrDefault(a => a.id.ToString() == value.ToString());
                if (temp != null)
                {
                    return temp.storehouseName;
                }
                else return "缺省";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "缺省";
            else
            {

                long time = long.Parse(value.ToString());
                if (time == 0)
                    return "暂无数据";
                DateTime date = DateTime.Now.UnixToDateTime(time);
                return DateTime.Now.UnixToDateTime(time);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class bool2colorVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new SolidColorBrush(Colors.White);
            }
            if ((bool)value)
                return CacheData.Ins.solid;
            else
                return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class bool2ForecolorVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return CacheData.Ins.ForegroundSolid;
            }
            if ((bool)value)
                return new SolidColorBrush(Colors.White);
            else
                return CacheData.Ins.ForegroundSolid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class bool2AlarmColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return CacheData.Ins.ForegroundSolid;
            }
            if (value is CheckInfo item)
            {
                if (item.CheckQty != item.Qty)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else
                    return CacheData.Ins.ForegroundSolid;
            }
            return CacheData.Ins.ForegroundSolid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class statusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string StatusValue = "缺省";
            if (value == null)
            {
                return "缺省";
            }
            switch (int.Parse(value.ToString()))
            {
                case 0:
                    StatusValue = "初始";
                    break;
                case 1:
                    StatusValue = "待验收";
                    break;
                case 2:
                    StatusValue = "验收确认";
                    break;
                case 3:
                    StatusValue = "复核确认";
                    break;
                case 99:
                    StatusValue = "拒收";
                    break;
                default:
                    break;
            }
            return StatusValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PrintCompleteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                if (value is pubAccept accept)
                {
                    bool PrintComplete = false;
                    foreach (var item in accept.acceptDtlList)
                    {
                        int count = item.deliveryQty - item.RFID.Count;
                        if (count > 0)
                            PrintComplete = true;
                    }
                    return PrintComplete;
                }
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class rfidStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "缺省";
            }
            else
            {
                return value.ToString() == "0" ? "已打印" : "已复核";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class btnDeteleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                if (value is CheckInfo dtl)
                {
                    if (dtl.IsNew && !dtl.IsHandOrder)
                        return true;
                    else if (dtl.IsHandOrder)
                        return true;
                    else return false;
                }
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
    public class LengthToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else
            {
                if (value.ToString().Length>0)
                {
                        return true;
                }

                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
}

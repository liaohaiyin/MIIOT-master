using MIIOT.Model;
using MIIOT.Model.ORCart;
using MIIOT.ORCart.MainView.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace MIIOT.ORCart.Controls
{
    public class EqualConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentNullException(nameof(values), "values can not be null or empty");

            return values.All(x => x + "" == values[0] + "");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class BadgeModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return MaterialDesignThemes.Wpf.ColorZoneMode.Standard;
            if (int.Parse(value.ToString()) == 0)
                return MaterialDesignThemes.Wpf.ColorZoneMode.Standard;
            return MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryMid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SplitOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            switch ((GoodsTypeEnum)value)
            {
                case GoodsTypeEnum.LOWPAY:
                case GoodsTypeEnum.PKG:
                    return Visibility.Visible;

                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class mulSplitOrderConverter : IMultiValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length == 0)
                return Visibility.Collapsed;

            if ((bool)values[1])
                return Visibility.Collapsed;
            switch ((GoodsTypeEnum)values[0])
            {
                case GoodsTypeEnum.LOWPAY:
                case GoodsTypeEnum.PKG:
                    return Visibility.Visible;

                default:
                    return Visibility.Collapsed;
            }
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class QtyEditVConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (value is GridControl grid)
            {
                if (grid.Clears.Count > 0)
                {
                    //if (grid.Clears[0].recevie_tag == true)
                    //    return Visibility.Collapsed;
                    switch ((GoodsTypeEnum)grid.Clears[0].goods_type)
                    {
                        case GoodsTypeEnum.HIGH:
                            return Visibility.Collapsed;

                        case GoodsTypeEnum.LOWFREE:
                        case GoodsTypeEnum.PKG:
                        case GoodsTypeEnum.LOWPAY:
                        default:
                            return Visibility.Visible;
                    }

                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SingleCodeShowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (value is GridControl grid)
            {
                if (grid.Clears.Count > 0)
                {
                    if (grid.Clears[0].status == 1)
                        return Visibility.Collapsed;
                    if(grid.Clears[0].recevie_tag)
                        return Visibility.Collapsed;

                    switch ((GoodsTypeEnum)grid.Clears[0].goods_type)
                    {
                        case GoodsTypeEnum.HIGH:
                        case GoodsTypeEnum.REUSE:
                            return Visibility.Visible;
                        case GoodsTypeEnum.LOWPAY:
                        case GoodsTypeEnum.PKG:
                        case GoodsTypeEnum.LOWFREE:
                        default:
                            return Visibility.Collapsed;
                    }
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class QtyEditHConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (value is GridControl grid)
            {
                if (grid.Clears.Count > 0)
                {
                    if (grid.Clears[0].recevie_tag == true)
                        return Visibility.Collapsed;

                    switch ((GoodsTypeEnum)grid.Clears[0].goods_type)
                    {
                        case GoodsTypeEnum.HIGH:
                            return Visibility.Visible;
                        case GoodsTypeEnum.LOWPAY:
                        case GoodsTypeEnum.REUSE:
                        case GoodsTypeEnum.PKG:
                        case GoodsTypeEnum.LOWFREE:
                        default:
                            return Visibility.Collapsed;
                    }
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CanChangeUnfreeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
                return false;
            cart_clear item = (cart_clear)value;
            if (item == null)
                return false;
            if (item.recevie_tag)
                return false;
            switch ((GoodsTypeEnum)item.goods_type)
            {
                case GoodsTypeEnum.LOWPAY:
                case GoodsTypeEnum.PKG:
                case GoodsTypeEnum.HIGH:
                    return true;
                case GoodsTypeEnum.REUSE:
                case GoodsTypeEnum.LOWFREE:
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class intTOBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            if (value.ToString() == "1")
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ZoreTOTrueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            cart_clear item = (cart_clear)value;
            if (item == null)
                return false;
            //if (item.recevie_tag)
            //    return false;

            if (item.status == 0)
            {
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class goodsinittoVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (value.ToString() == "1")
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class plotVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (value is StockGoods grid)
            {
                if (grid.InitType == 1)
                {
                    return Visibility.Visible;
                }

                //if (grid.StockGoodslist.Count > 0)
                //{
                //    if (grid.StockGoodslist[0].InitType == 1)
                //    {
                //        return Visibility.Visible;
                //    }
                //}
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StockTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "未知";
            int stype = 0;
            int.TryParse(value.ToString(), out stype);

            StockTypeEnum stockType = (StockTypeEnum)stype;
            return stockType.GetEnumDescription();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StockForeTypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "未知";
            int stype = 0;
            int.TryParse(value.ToString(), out stype);

            StockTypeEnum stockType = (StockTypeEnum)stype;
            switch (stockType)
            {
                case StockTypeEnum.N:
                    return new SolidColorBrush(Color.FromArgb(0xff, 0x4c, 0x4c, 0x4c));
                case StockTypeEnum.L:
                case StockTypeEnum.F:
                default:
                    return new SolidColorBrush(Colors.White);
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StockTypebackColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "未知";
            int stype = 0;
            int.TryParse(value.ToString(), out stype);

            StockTypeEnum stockType = (StockTypeEnum)stype;
            switch (stockType)
            {
                case StockTypeEnum.N:
                    return new SolidColorBrush(Colors.Transparent);
                case StockTypeEnum.L:
                    return new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x9e, 0x70));
                case StockTypeEnum.F:
                    return new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xd2, 0x35));
                default:
                    break;
            }
            return new SolidColorBrush(Color.FromArgb(0xff, 0x4c, 0x4c, 0x4c));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GoodsTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "未知";
            int stype = 0;
            int.TryParse(value.ToString(), out stype);

            GoodsTypeEnum stockType = (GoodsTypeEnum)stype;
            return stockType.GetEnumDescription();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ReplenishStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "待补货";
            if (value.ToString() == "1")
            {
                return "完成";
            }
            if (value.ToString() == "2")
            {
                return "手工补货";
            }
            if (value.ToString() == "3")
            {
                return "部分补货";
            }
            return "待补货";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ShowTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            if (value.ToString() == "1")
            {
                return "补货数量：";
            }
            if (value.ToString() == "2")
            {
                return "退库数量：";
            }
            if (value.ToString() == "3")
            {
                return "盘点数量：";
            }
            if (value.ToString() == "4")
            {
                return "清台数量：";
            }
            if (value.ToString() == "5")
            {
                return "申领数量：";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ShowTypeVisibilyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if (value.ToString() == "1")
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CheckBoxIcoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "CheckboxBlank";
            if ((bool)value)
            {
                return "CheckboxMarked";
            }
            return "CheckboxBlankOutline";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MIIOT.UI.Controls
{
    public class NbSlider : Slider
    {
        static NbSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbSlider), new FrameworkPropertyMetadata(typeof(NbSlider)));
        }

        public NbSlider()
        {
            //this.SetResourceReference(NbCheckBox.StyleProperty, nameof(NbCheckBox));
        }


        /// <summary>
        /// 最大值
        /// </summary>
        /// <summary>
        /// 限制数字输入时，允许的最大值
        /// </summary>
        //public double MaxValue
        //{
        //    get
        //    {
        //        return (double)GetValue(MaxValueProperty);
        //    }
        //    set
        //    {
        //        SetValue(MaxValueProperty, value);
        //    }
        //}
        //public static readonly DependencyProperty MaxValueProperty =
        //    DependencyProperty.Register("MaxValue", typeof(double), typeof(NbSlider), new PropertyMetadata(double.MaxValue));
    }
}

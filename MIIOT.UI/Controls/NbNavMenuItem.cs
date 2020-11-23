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
    public class NbNavMenuItem : RadioButton
    {
        static NbNavMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbNavMenuItem), new FrameworkPropertyMetadata(typeof(NbNavMenuItem)));
        }
        public NbNavMenuItem()
        {
            //this.SetResourceReference(NbNavMenuItem.StyleProperty, nameof(NbNavMenuItem));
        }

        /// <summary>
        /// 按钮图标
        /// </summary>
        public Geometry Icon
        {
            get { return (Geometry)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(NbNavMenuItem));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(NbNavMenuItem), new PropertyMetadata(16d));

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(NbNavMenuItem), new PropertyMetadata(16d));

        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(NbNavMenuItem), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xFF, 0XFF, 0XFF)) { Opacity = 0.222 }));

        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(NbNavMenuItem), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xFF, 0XFF, 0XFF)) { Opacity = 0.333 }));



        public Brush CheckedBrush
        {
            get { return (Brush)GetValue(CheckedBrushProperty); }
            set { SetValue(CheckedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBrushProperty =
            DependencyProperty.Register("CheckedBrush", typeof(Brush), typeof(NbNavMenuItem), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xFF, 0X00, 0X00))));




        public Brush SelectedBrush
        {
            get { return (Brush)GetValue(SelectedBrushProperty); }
            set { SetValue(SelectedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register("SelectedBrush", typeof(Brush), typeof(NbNavMenuItem), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0xFF, 0xFF, 0xFF)) { Opacity = 0.111 }));




        /// <summary>
        /// 菜单ID
        /// </summary>
        public long MenuID
        {
            get { return (long)GetValue(MenuIDProperty); }
            set { SetValue(MenuIDProperty, value); }
        }
        public static readonly DependencyProperty MenuIDProperty =
            DependencyProperty.Register("MenuID", typeof(long), typeof(NbNavMenuItem), new PropertyMetadata(0L));

        /// <summary>
        /// Page路径
        /// </summary>
        public string MenuPath
        {
            get { return (string)GetValue(MenuPathProperty); }
            set { SetValue(MenuPathProperty, value); }
        }
        public static readonly DependencyProperty MenuPathProperty =
            DependencyProperty.Register("MenuPath", typeof(string), typeof(NbNavMenuItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 打开Page的参数
        /// </summary>
        public string MenuParam
        {
            get { return (string)GetValue(MenuParamProperty); }
            set { SetValue(MenuParamProperty, value); }
        }
        public static readonly DependencyProperty MenuParamProperty =
            DependencyProperty.Register("MenuParam", typeof(string), typeof(NbNavMenuItem), new PropertyMetadata(string.Empty));
    }
}

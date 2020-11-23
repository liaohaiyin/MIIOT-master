using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MIIOT.UI.Controls
{
    public class NwButton : RepeatButton
    {
        static NwButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NwButton), new FrameworkPropertyMetadata(typeof(NwButton)));
        }

        public NwButton()
        {
            //this.SetResourceReference(NwButton.StyleProperty, nameof(NwButton));
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
            DependencyProperty.Register("Icon", typeof(Geometry), typeof(NwButton));

        /// <summary>
        /// 
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NwButton), new PropertyMetadata(new CornerRadius(0.0)));





        public Brush OverBorderBrush
        {
            get { return (Brush)GetValue(OverBorderBrushProperty); }
            set { SetValue(OverBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OverBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverBorderBrushProperty =
            DependencyProperty.Register("OverBorderBrush", typeof(Brush), typeof(NwButton), new PropertyMetadata(new SolidColorBrush()));



        public Brush PressedBorderBrush
        {
            get { return (Brush)GetValue(PressedBorderBrushProperty); }
            set { SetValue(PressedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PressedBorderBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedBorderBrushProperty =
            DependencyProperty.Register("PressedBorderBrush", typeof(Brush), typeof(NwButton), new PropertyMetadata(new SolidColorBrush()));





        public Brush OverBackground
        {
            get { return (Brush)GetValue(OverBackgroundProperty); }
            set { SetValue(OverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverBackgroundProperty =
            DependencyProperty.Register("OverBackground", typeof(Brush), typeof(NwButton), new PropertyMetadata(new SolidColorBrush()));


        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PressedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedBackgroundProperty =
            DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(NwButton), new PropertyMetadata(new SolidColorBrush()));



        public Brush OverForeground
        {
            get { return (Brush)GetValue(OverForegroundProperty); }
            set { SetValue(OverForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OverForegroundProperty =
            DependencyProperty.Register("OverForeground", typeof(Brush), typeof(NwButton), new PropertyMetadata(new SolidColorBrush()));




        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PressedForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(NwButton), new PropertyMetadata(new SolidColorBrush()));

    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MIIOT.UI.Controls
{
    public class NmTabControl : TabControl
    {
        static NmTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NmTabControl), new FrameworkPropertyMetadata(typeof(NmTabControl)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NmTabItem();
        }
    }
    public class NmTabItem : TabItem    
    {
        static NmTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NmTabItem), new FrameworkPropertyMetadata(typeof(NmTabItem)));
        }
        /// <summary>
        /// 选中背景色
        /// </summary>
        public SolidColorBrush SelectedColor
        {
            get { return (SolidColorBrush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(NmTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 24, 144, 255))));

        /// <summary>
        /// 选中文字颜色
        /// </summary>
        public SolidColorBrush SelectedForeground
        {
            get { return (SolidColorBrush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }
        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register("SelectedForeground", typeof(SolidColorBrush), typeof(NmTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255,255,255))));


        /// <summary>
        /// 鼠标悬浮背景色
        /// </summary>
        public SolidColorBrush HoverColor
        {
            get { return (SolidColorBrush)GetValue(HoverColorProperty); }
            set { SetValue(HoverColorProperty, value); }
        }

        public static readonly DependencyProperty HoverColorProperty =
            DependencyProperty.Register("HoverColor", typeof(SolidColorBrush), typeof(NmTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0, 0, 0, 0))));

        public TabItemType TabItemType
        {
            get { return (TabItemType)GetValue(TabItemTypeProperty); }
            set
            {
                SetValue(TabItemTypeProperty, value);
            }
        }
        public static readonly DependencyProperty TabItemTypeProperty =
            DependencyProperty.Register("TabItemType", typeof(TabItemType), typeof(NmTabItem), new PropertyMetadata(TabItemType.Middle));
    }

    public enum TabItemType
    {
        Left, Middle, Right
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MIIOT.UI.Controls
{
    public class NbTabControl : TabControl
    {
        static NbTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbTabControl), new FrameworkPropertyMetadata(typeof(NbTabControl)));
        }
        public NbTabControl()
        {
            //this.SetResourceReference(NbTabControl.StyleProperty, nameof(NbTabControl));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NbTabItem();
        }
    }


    public class NbTabItem : TabItem
    {
        static NbTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbTabItem), new FrameworkPropertyMetadata(typeof(NbTabItem)));
        }
        public NbTabItem()
        {
            //this.SetResourceReference(NbTabItem.StyleProperty, nameof(NbTabItem));
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
            DependencyProperty.Register("SelectedColor", typeof(SolidColorBrush), typeof(NbTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 124, 125, 133))));

        /// <summary>
        /// 选中文字颜色
        /// </summary>
        public SolidColorBrush SelectedForeground
        {
            get { return (SolidColorBrush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }
        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register("SelectedForeground", typeof(SolidColorBrush), typeof(NbTabItem), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 229, 229, 231))));


        /// <summary>
        /// 显示关闭按钮
        /// </summary>
        public Visibility ShowCloseButton
        {
            get { return (Visibility)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }
        public static readonly DependencyProperty ShowCloseButtonProperty =
            DependencyProperty.Register("ShowCloseButton", typeof(Visibility), typeof(NbTabItem), new PropertyMetadata(Visibility.Collapsed));



        /// <summary>
        /// 关闭TabItem
        /// </summary>
        public static ICommand NbTabItemCloseCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    var tabItem = obj as NbTabItem;
                    var tabControl = FindParentTabControl(tabItem);
                    if (tabControl != null)
                        tabControl.Items.Remove(tabItem);
                });
            }
        }

        /// <summary>
        /// 递归找父级TabControl
        /// </summary>
        /// <param name="reference">依赖对象</param>
        /// <returns>TabControl</returns>
        private static NbTabControl FindParentTabControl(DependencyObject reference)
        {
            DependencyObject dObj = VisualTreeHelper.GetParent(reference);
            if (dObj == null)
                return null;
            if (dObj.GetType() == typeof(NbTabControl))
                return dObj as NbTabControl;
            else
                return FindParentTabControl(dObj);
        }
    }



    public class NbMenuTabControl : TabControl
    {
        static NbMenuTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbMenuTabControl), new FrameworkPropertyMetadata(typeof(NbMenuTabControl)));
        }
        public NbMenuTabControl()
        {
            //this.SetResourceReference(NbMenuTabControl.StyleProperty, nameof(NbMenuTabControl));
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new NbMenuTabItem();
        }
    }


    public class NbMenuTabItem : TabItem
    {
        static NbMenuTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbMenuTabItem), new FrameworkPropertyMetadata(typeof(NbMenuTabItem)));
        }
        public NbMenuTabItem()
        {
            //this.SetResourceReference(NbMenuTabItem.StyleProperty, nameof(NbMenuTabItem));
        }

        
    }
}

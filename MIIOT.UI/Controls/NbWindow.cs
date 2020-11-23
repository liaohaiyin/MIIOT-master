using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

namespace MIIOT.UI.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class NbWindow : Window
    {
        static NbWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbWindow), new FrameworkPropertyMetadata(typeof(NbWindow)));
        }
        public NbWindow()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(NbWindow), new FrameworkPropertyMetadata(typeof(NbWindow)));
            //this.SetResourceReference(NbWindow.StyleProperty, nameof(NbWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, CloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, MaximizeWindow, CanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, MinimizeWindow, CanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, RestoreWindow, CanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, ShowSystemMenu));
        }
        #region Window Commands

        private void CanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void CanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
        }

        private void CloseWindow(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void MaximizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void MinimizeWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void RestoreWindow(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }


        private void ShowSystemMenu(object sender, ExecutedRoutedEventArgs e)
        {
            var element = e.OriginalSource as FrameworkElement;
            if (element == null)
                return;

            var point = this.WindowState == WindowState.Maximized ? new Point(0, element.ActualHeight)
                : new Point(this.Left + this.BorderThickness.Left, element.ActualHeight + this.Top + this.BorderThickness.Top);
            point = element.TransformToAncestor(this).Transform(point);
            SystemCommands.ShowSystemMenu(this, point);
        }

        #endregion

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (SizeToContent == SizeToContent.WidthAndHeight && WindowChrome.GetWindowChrome(this) != null)
            {
                InvalidateMeasure();
            }
        }

        /// <summary>
        /// 标题栏高度
        /// </summary>
        public double CaptionHeight
        {
            get { return (double)GetValue(CaptionHeightProperty); }
            set { SetValue(CaptionHeightProperty, value); }
        }
        public static readonly DependencyProperty CaptionHeightProperty =
            DependencyProperty.Register("CaptionHeight", typeof(double), typeof(NbWindow), new PropertyMetadata(32.0));

        /// <summary>
        /// 窗体圆角
        /// </summary>
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(NbWindow), new PropertyMetadata(0.0));

        /// <summary>
        /// 拖动大小边框宽度
        /// </summary>
        public double ResizeBorderThickness
        {
            get { return (double)GetValue(ResizeBorderThicknessProperty); }
            set { SetValue(ResizeBorderThicknessProperty, value); }
        }
        public static readonly DependencyProperty ResizeBorderThicknessProperty =
            DependencyProperty.Register("ResizeBorderThickness", typeof(double), typeof(NbWindow), new PropertyMetadata(5.0));

        /// <summary>
        /// 边框阴影宽度
        /// </summary>
        public double GlassFrameThickness
        {
            get { return (double)GetValue(GlassFrameThicknessProperty); }
            set { SetValue(GlassFrameThicknessProperty, value); }
        }
        public static readonly DependencyProperty GlassFrameThicknessProperty =
            DependencyProperty.Register("GlassFrameThickness", typeof(double), typeof(NbWindow), new PropertyMetadata(1.0));

    }
}

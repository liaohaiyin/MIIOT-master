using System.Windows;
using System.Windows.Controls;

namespace MIIOT.Trical
{
    /// <summary>
    ///     步骤条单元项
    /// </summary>
    public class StepBarItem : ContentControl
    {
        /// <summary>
        ///     步骤编号
        /// </summary>
        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register(
            "Index", typeof(int), typeof(StepBarItem), new PropertyMetadata(-1));

        /// <summary>
        ///     步骤编号
        /// </summary>
        public int Index
        {
            get => (int) GetValue(IndexProperty);
            internal set => SetValue(IndexProperty, value);
        }

        /// <summary>
        ///     步骤状态
        /// </summary>
        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status", typeof(StepStatus), typeof(StepBarItem), new PropertyMetadata(StepStatus.Waiting));

        /// <summary>
        ///     步骤状态
        /// </summary>
        public StepStatus Status
        {
            get => (StepStatus) GetValue(StatusProperty);
            internal set => SetValue(StatusProperty, value);
        }
    }
    public enum StepStatus
    {
        /// <summary>
        ///     完成
        /// </summary>
        Complete,

        /// <summary>
        ///     进行中
        /// </summary>
        UnderWay,

        /// <summary>
        ///     等待中
        /// </summary>
        Waiting
    }
    public class FunctionEventArgs<T> : RoutedEventArgs
    {
        public FunctionEventArgs(T info)
        {
            Info = info;
        }

        public FunctionEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public T Info { get; set; }
    }
    internal static class ValueBoxes
    {
        internal static object TrueBox = true;

        internal static object FalseBox = false;

        internal static object Double0Box = .0;

        internal static object Double01Box = .1;

        internal static object Double1Box = 1.0;

        internal static object Double10Box = 10.0;

        internal static object Double20Box = 20.0;

        internal static object Double100Box = 100.0;

        internal static object Double200Box = 200.0;

        internal static object Double300Box = 300.0;

        internal static object DoubleNeg1Box = -1.0;

        internal static object Int0Box = 0;

        internal static object Int1Box = 1;

        internal static object Int2Box = 2;

        internal static object Int5Box = 5;

        internal static object Int99Box = 99;

        internal static object BooleanBox(bool value) => value ? TrueBox : FalseBox;
    }
}
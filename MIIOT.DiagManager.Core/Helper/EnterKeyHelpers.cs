using MIIOT.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MIIOT.DiagManager.Core
{
    public static class EnterKeyHelpers
    {
        public static ICommand GetEnterKeyCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(EnterKeyCommandProperty);
        }

        public static void SetEnterKeyCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(EnterKeyCommandProperty, value);
        }

        public static readonly DependencyProperty EnterKeyCommandProperty =
            DependencyProperty.RegisterAttached(
                "EnterKeyCommand",
                typeof(ICommand),
                typeof(EnterKeyHelpers),
                new PropertyMetadata(null, OnEnterKeyCommandChanged));


        public static object GetEnterKeyCommandParam(DependencyObject target)
        {
            return (object)target.GetValue(EnterKeyCommandParamProperty);
        }

        public static void SetEnterKeyCommandParam(DependencyObject target, object value)
        {
            target.SetValue(EnterKeyCommandParamProperty, value);
        }

        public static readonly DependencyProperty EnterKeyCommandParamProperty =
            DependencyProperty.RegisterAttached(
                "EnterKeyCommandParam",
                typeof(object),
                typeof(EnterKeyHelpers),
                new PropertyMetadata(null));

        static void OnEnterKeyCommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ICommand command = (ICommand)e.NewValue;
            Control control = (Control)target;
            control.KeyDown += (s, args) =>
            {
                if (args.Key == Key.Enter)
                {
                    // make sure the textbox binding updates its source first
                    BindingExpression b = control.GetBindingExpression(NbTextBox.TextProperty);
                    if (b != null)
                    {
                        b.UpdateSource();
                    }
                    object commandParameter = GetEnterKeyCommandParam(target);
                    command.Execute(commandParameter);
                }
            };
        }
    }
}

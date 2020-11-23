using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace MIIOT.UI.Controls
{
    public class NbTextBox : TextBox
    {

        static NbTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbTextBox), new FrameworkPropertyMetadata(typeof(NbTextBox)));
        }

        public NbTextBox()
        {
            //this.SetResourceReference(NbTextBox.StyleProperty, nameof(NbTextBox));
        }

        /// <summary>
        /// 
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(NbTextBox), new PropertyMetadata(new CornerRadius(0.0)));

        /// <summary>
        /// 
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(NbTextBox), new PropertyMetadata(string.Empty));

        /// <summary>
        /// 
        /// </summary>
        public InputCheck InputCheck
        {
            get 
            { 
                return (InputCheck)GetValue(InputCheckProperty); 
            }
            set 
            {
                SetValue(InputCheckProperty, value); 
            }
        }
        public static readonly DependencyProperty InputCheckProperty =
            DependencyProperty.Register("InputCheck", typeof(InputCheck), typeof(NbTextBox), 
                new FrameworkPropertyMetadata(InputCheck.Text, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, InputCheckChangedCallback));


        static void InputCheckChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = (NbTextBox)sender;
            var newValue = (InputCheck)e.NewValue;

            if (newValue == InputCheck.Double || newValue == InputCheck.Int)
                textBox.AddHandler(CommandManager.CanExecuteEvent, new RoutedEventHandler(CanPasteCommandExecuted), true);
            else
                textBox.RemoveHandler(CommandManager.CanExecuteEvent, new RoutedEventHandler(CanPasteCommandExecuted));
        }
        static void CanPasteCommandExecuted(object sender, RoutedEventArgs e)
        {
            var ce = (CanExecuteRoutedEventArgs)e;
            if (ce.Command == ApplicationCommands.Paste)
            {
                ce.CanExecute = false;
                var tb = ((NbTextBox)e.Source);
                var oldText = tb.Text;
                var oldSelSt = tb.SelectionStart;
                ce.Command.Execute(null);
                if((tb.InputCheck == InputCheck.Int && !int.TryParse(tb.Text, out int tempInt)) ||
                    (tb.InputCheck == InputCheck.Double && !double.TryParse(tb.Text, out double tempDouble)))
                {
                    tb.Text = oldText;
                    tb.SelectionStart = oldSelSt;
                }
            }
        }


        /// <summary>
        /// 限制数字输入时，允许的最大值
        /// </summary>
        public double MaxValue
        {
            get
            {
                return (double)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
                MaxLength = value.ToString().Length;
            }
        }
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(NbTextBox), new PropertyMetadata(double.MaxValue));
        /// <summary>
        /// 限制数字输入时，允许的最小值
        /// </summary>
        public double MinValue
        {
            get
            {
                return (double)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(NbTextBox), new PropertyMetadata(double.MinValue));






        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (this.InputCheck == InputCheck.Int || this.InputCheck == InputCheck.Double)
            {
                var oldSelStart = this.SelectionStart;
                if(this.Text.Contains(" "))
                {
                    this.Text = this.Text.Replace(" ", "");
                    this.SelectionStart = this.Text.Length;
                }
                var val = 0d;
                if (double.TryParse(this.Text, out val))
                {
                    if (val > this.MaxValue)
                    {
                        this.Text = this.MaxValue.ToString();
                        this.SelectionStart = this.Text.Length;
                    }
                        
                    //else if (val <= this.MinValue)
                    //    this.Text = this.MinValue.ToString();
                }
            }
            base.OnTextChanged(e);
        }


        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            var txt = this.Text.Insert(this.SelectionStart, e.Text);
            if ((this.InputCheck == InputCheck.Int || this.InputCheck == InputCheck.Double))
            {
                if(e.Text == " " || 
                    
                    (this.MinValue >=0 && this.SelectionStart == 0 && e.Text == "-"))
                    e.Handled = true;

                if (e.Handled == false)
                {
                    if (this.InputCheck == InputCheck.Int)
                    {
                        e.Handled = !Regex.IsMatch(txt, @"^(-|\+)?(\d)*$");
                    }
                    else if (this.InputCheck == InputCheck.Double)
                    {
                        e.Handled = !(Regex.IsMatch(txt, @"^(-|\+)?(\d)*$") ||
                            Regex.IsMatch(txt, @"^(-|\+)?(\d)+(\.){0,1}(\d)*$"));
                    }
                }
                
            }

            

            base.OnPreviewTextInput(e);
        }

        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            if (this.InputCheck == InputCheck.Int || this.InputCheck == InputCheck.Double)
            {
                if (!double.TryParse(this.Text, out double val))
                {
                    this.Text = this.MinValue.ToString();
                }
                else
                {
                    if (val > this.MaxValue)
                        this.Text = this.MaxValue.ToString();
                    else if (val <= this.MinValue)
                        this.Text = this.MinValue.ToString();
                }
            }
            base.OnLostFocus(e);
        }
    }

    public enum InputCheck
    {
        Text = 0,
        Int = 1,
        Double = 2
    }




    public static class PasswordBoxBindingHelper
    {



        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(PasswordBoxBindingHelper), new FrameworkPropertyMetadata(""));


        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxBindingHelper), new FrameworkPropertyMetadata(0));





        //BindedPassword
        public static readonly DependencyProperty BindedPasswordProperty =
            DependencyProperty.RegisterAttached("BindedPassword",
            typeof(string), typeof(PasswordBoxBindingHelper),
            new FrameworkPropertyMetadata(string.Empty, OnBindedPasswordChanged));
        public static string GetBindedPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BindedPasswordProperty);
        }
        public static void SetBindedPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BindedPasswordProperty, value);
        }

        private static void OnBindedPasswordChanged(DependencyObject sender,
          DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            if (passwordBox == null)
            {
                return;
            }
            passwordBox.PasswordChanged -= PasswordChanged;

            if (!(bool)GetIsUpdating(passwordBox))
            {
                passwordBox.Password = (string)e.NewValue;
                SetPasswordLength(sender, passwordBox.Password.Length);
            }
            passwordBox.PasswordChanged += PasswordChanged;
        }


        //IsPasswordBindingEnabled
        public static readonly DependencyProperty IsPasswordBindingEnabledProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindingEnabled",
             typeof(bool), typeof(PasswordBoxBindingHelper), new PropertyMetadata(false, IsPasswordBindingEnabled));
        public static void SetIsPasswordBindingEnabled(DependencyObject dp, bool value)
        {
            dp.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        public static bool GetIsPasswordBindingEnabled(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsPasswordBindingEnabledProperty);
        }
        private static void IsPasswordBindingEnabled(DependencyObject sender,
          DependencyPropertyChangedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;

            if (passwordBox == null)
                return;

            if ((bool)e.OldValue)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
            }

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        //IsUpdating
        private static readonly DependencyProperty IsUpdatingProperty =
           DependencyProperty.RegisterAttached("IsUpdating", typeof(bool),
          typeof(PasswordBoxBindingHelper));

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            SetIsUpdating(passwordBox, true);
            SetBindedPassword(passwordBox, passwordBox.Password);
            SetIsUpdating(passwordBox, false);
            SetPasswordLength(passwordBox, passwordBox.Password.Length);
        }
    }

}

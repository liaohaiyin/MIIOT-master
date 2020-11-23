using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MIIOT.Controls
{
    public class TextBoxNumberUpADown : TextBox
    {


        public delegate void DelChange(object sender, bool IsAdd);
        public event DelChange DoChange;
        public int Qty
        {
            get { return (int)GetValue(QtyProperty); }
            set { SetValue(QtyProperty, value); }
        }
        public static readonly DependencyProperty QtyProperty = DependencyProperty.Register("Qty", typeof(int), typeof(TextBoxNumberUpADown));

        public TextBoxNumberUpADown()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.Prev, (s, e) =>
            {
                DoChange?.Invoke(this, false);
            }));
            CommandBindings.Add(new CommandBinding(ControlCommands.Next, (s, e) =>
            {
                DoChange?.Invoke(this, true);
            }));
            //CommandBindings.Add(new CommandBinding(ControlCommands.HourChange, (s, e) =>
            //{
            //    KeyboadHelper.ShowInputPanel();
            //}));
            //this.GotFocus += TextBoxNumberUpADown_GotFocus;
        }

        private void TextBoxNumberUpADown_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            // KeyboadHelper.ShowInputPanel();

        }


    }
}

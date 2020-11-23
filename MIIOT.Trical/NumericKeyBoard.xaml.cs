using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NFMES.UI.Base
{
    /// <summary>
    /// NumericKeyBoard.xaml 的交互逻辑
    /// </summary>
    public partial class NumericKeyBoard : Window
    {
        private static TextBox _SpinEdit;
        private static NumericKeyBoard _NumericKeyBoard;
        public NumericKeyBoard(TextBox spinEdit)
        {
            InitializeComponent();
            _SpinEdit = spinEdit;
        }



        private void Window_Deactivated(object sender, EventArgs e)
        {
            // this.Hide();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                Button btn = e.OriginalSource as Button;
                if (btn.Name == "Back")
                {
                    txtValue.Text = txtValue.Text.Substring(0, txtValue.Text.Length - 1);
                }
                else
                {
                    txtValue.Text += btn.Content;
                }
            }
            catch
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _SpinEdit.Text = (txtValue.Text.Length == 0 ? "0" : txtValue.Text);
            this.Close();
        }
    }
}
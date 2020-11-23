using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.ORCart.View
{
    /// <summary>
    /// LogListView.xaml 的交互逻辑
    /// </summary>
    public partial class LogListView : UserControl
    {
        public LogListView()
        {
            InitializeComponent();


            CommandBindings.Add(new CommandBinding(DataCommands.Switch, (s, e) =>
            {
                if (true)
                {

                }

            }));
        }
        private void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New 命令被触发了，命令源是:" + e.Source.ToString());
        }

        private void cmdDoCommand_Click(object sender, RoutedEventArgs e)
        {
            // 直接调用命令的两种方式
            ApplicationCommands.New.Execute(null, (Button)sender);

        }
    }
    public class DataCommands
    {

        public static RoutedCommand Switch { get; } = new RoutedCommand(nameof(Switch), typeof(DataCommands));


        private static RoutedUICommand requery;
        static DataCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "Ctrl+R"));
            requery = new RoutedUICommand(
              "Requery", "Requery", typeof(DataCommands), inputs);
        }

        public static RoutedUICommand Requery
        {
            get { return requery; }
        }
    }
}

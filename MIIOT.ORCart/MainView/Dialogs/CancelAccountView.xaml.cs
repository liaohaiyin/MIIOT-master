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

namespace MIIOT.ORCart.MainView.Dialogs
{
    /// <summary>
    /// CancelAccountView.xaml 的交互逻辑
    /// </summary>
    public partial class CancelAccountView : UserControl
    {
        public CancelAccountView()
        {
            InitializeComponent();
        }

        private void CheckBoxHeader_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox chk)
            {
                for (int i = 0; i < grid.Items.Count; i++)
                {
                    DataGridRow neddrow = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(i);
                    if (neddrow == null) continue;
                    var cb = grid.Columns[0].GetCellContent(neddrow);
                    CheckBoxUtil.GetVisualChild(cb, chk.IsChecked);
                }
                foreach (var item in grid.Items)
                {
                    //if (item is BaseRecord record)
                    //{
                    //    record.Selected = chk.IsChecked == true;

                    //}
                }
            }
        }
    }
}

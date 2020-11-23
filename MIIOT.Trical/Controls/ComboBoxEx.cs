using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MIIOT.Trical
{
    public class ComboBoxEx : ComboBox
    {
        public ComboBoxEx()
        {
            this.DisplayMemberPath = "storehouseName";
            this.SelectedValuePath = "id";
            this.Loaded += ComboBoxEx_Loaded;
        }

        private void ComboBoxEx_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ItemsSource = CacheData.Ins.Stocks;
        }
    }
}

using MIIOT.Drivers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

namespace MIIOT.View
{
    /// <summary>
    /// DBEditView.xaml 的交互逻辑
    /// </summary>
    public partial class DBEditView : UserControl, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        #endregion
        
        public DBEditView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnRefreshDB_Click(object sender, RoutedEventArgs e)
        {
            listViewDB.Items.Clear();
           var dbList = LiteDBHelper.Ins.GetAllCollection();
            foreach (var item in dbList)
            {
                listViewDB.Items.Add(item);
            }
        }

        private void listViewDB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView)
            {
               var temo= listView.SelectedItem;
                Type type = typen(temo.ToString());
                LiteDBHelper.Ins.GetCollection("SysPara");
            }
        }
        public  Type typen(string typeName)
        {
            Type type = null;
            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            int assemblyArrayLength = assemblyArray.Length;
            for (int i = 0; i < assemblyArrayLength; ++i)
            {
                type = assemblyArray[i].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            for (int i = 0; (i < assemblyArrayLength); ++i)
            {
                Type[] typeArray = assemblyArray[i].GetTypes();
                int typeArrayLength = typeArray.Length;
                for (int j = 0; j < typeArrayLength; ++j)
                {
                    if (typeArray[j].Name.Equals(typeName))
                    {
                        return typeArray[j];
                    }
                }
            }
            return type;
        }
    }
}

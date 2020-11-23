using MIIOT.Control;
using MIIOT.Drivers;
using MIIOT.Drivers.MCU;
using MIIOT.Drivers.Server;
using MIIOT.Model;
using MIIOT.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace MIIOT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
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
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            settingView = new SettingView();
            debugView = new DebugView();
            alarmSetting = new AlarmSetting();
            Items = new[]{
                new ContorllerItem("报警",alarmSetting),
                new ContorllerItem("调试",debugView),
                new ContorllerItem("设置",settingView),
                new ContorllerItem("配置",new DBEditView()),
            };
            this.Loaded += MainWindow_Loaded;
            DriverObserver driver = new DriverManager();
            driver.AddObserver(new Drivers.NotifyEventHandler(debugView.OnConnect));
            driver.AddObserver(new Drivers.NotifyEventHandler(settingView.OnConnect));
            driver.Start();


        }
        AlarmSetting alarmSetting;
        SettingView settingView;
        DebugView debugView;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Server_OnBufferBack(object sender, byte[] Buff)
        {

        }

        private void Server_OnConnect(object sender, bool isConnect)
        {
            if (sender is NetServerManager magr)
            {
                settingView.UpdateStatus(magr.Cabinet, magr.MacType, isConnect);
            }
        }

        private void Server_OnMsgBack(object sender, string Msg)
        {

        }

        public ContorllerItem[] Items { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ItemListBoxSelecte_changed(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            ItemListBox.SelectedIndex = 1;

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            ItemListBox.SelectedIndex = 0;
        }
    }
}

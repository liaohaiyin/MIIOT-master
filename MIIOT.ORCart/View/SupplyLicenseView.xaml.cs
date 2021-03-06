﻿using MIIOT.ORCart.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace MIIOT.ORCart.View
{
    /// <summary>
    /// SupplyLicenseView.xaml 的交互逻辑
    /// </summary>
    public partial class SupplyLicenseView : UserControl, INotifyPropertyChanged
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
     
        public PicModel PicInfo { get; set; }
        public SupplyLicenseView()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += SupplyLicenseView_Loaded;
        }

        private void SupplyLicenseView_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

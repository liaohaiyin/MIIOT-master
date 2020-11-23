using MIIOT.Model;
using MIIOT.Model.Trical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MIIOT.Trical
{
    public class ButtonEx : Button
    {
        public delegate void delChecked(object sender, pubAccept _acceptModel);
        public event delChecked DoChecked;
        SolidColorBrush PrimaryColor;
        SolidColorBrush PrimaryForeColor;
        public ButtonEx()
        {
            PrimaryColor = this.FindResource("PrimaryHueMidBrush") as SolidColorBrush;
            PrimaryForeColor = this.FindResource("PrimaryHueMidForegroundBrush") as SolidColorBrush;
            Height = 90; Width = 200; Margin = new Thickness(15,10,10,10);
            FontSize = 18;
            IsChecked = false;
            BorderThickness = new Thickness(0);
        }
        public pubAccept _acceptModel { get; set; }
        public ApplyBackInfo _ApplyBackInfo { get; set; }
        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (value)
                {
                    Background = PrimaryColor;
                    Foreground = PrimaryForeColor;
                    BorderBrush = PrimaryColor;
                    DoChecked?.Invoke(this,_acceptModel);
                }
                else
                {
                    Background = new SolidColorBrush(Colors.White);
                    Foreground = CacheData.Ins.ForegroundSolid;
                    BorderBrush = new SolidColorBrush(Colors.LightGray);
                }
                isChecked = value;
            }
        }

    }
}

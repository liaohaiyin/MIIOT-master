using MaterialDesignThemes.Wpf;
using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MIIOT.Trical.Controls
{
    public class KeyboardShow : PackIcon
    {
        public KeyboardShow()
        {

         
            this.Loaded += KeyboardShow_Loaded;
            this.MouseLeftButtonDown += KeyBoardShow_MouseLeftButtonDown;
        }

        private void KeyboardShow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //sysPara=LiteDBHelper.Ins.GetCollection<SysPara>().FirstOrDefault(a => a.ParaKey == "KeyboardShow");
            //if (sysPara.ParaValue == "true")
            //{
            //    IsShow = true;
            //    this.Foreground = CacheData.Ins.solid;
            //}
            //else
            //{
            //    IsShow = false;
            //    this.Foreground = new SolidColorBrush(Color.FromRgb(0x8c, 0x8c, 0x8c));
            //}
        }

        private bool _isShow = false;

        public bool IsShow
        {
            get { return _isShow; }
            set { _isShow = value; }
        }
        SysPara sysPara = new SysPara();
        private void KeyBoardShow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (sysPara == null)
            //{
            //    sysPara = new SysPara() { ParaType = "Para", ParaKey = "KeyboardShow", ParaValue = "false" };
            //    LiteDBHelper.Ins.Insert(sysPara);
            //}
            //if (IsShow)
            //{
            //    IsShow = false;
            //    this.Foreground = new SolidColorBrush(Color.FromRgb(0x8c, 0x8c, 0x8c));
            //    sysPara.ParaValue = "false";
            //    LiteDBHelper.Ins.Update(sysPara);
            //}
            //else
            //{
            //    IsShow = true;
            //    this.Foreground = CacheData.Ins.solid;
            //    sysPara.ParaValue = "true";
            //    LiteDBHelper.Ins.Update(sysPara);
            //}
        }
    }

}

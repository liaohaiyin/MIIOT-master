using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MIIOT.UI.Controls
{
    public class NbLabel : Label
    {
        static NbLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbLabel), new FrameworkPropertyMetadata(typeof(NbLabel)));
        }



        public bool RequiredFlag
        {
            get { return (bool)GetValue(RequiredFlagProperty); }
            set { SetValue(RequiredFlagProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RequiredFlag.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequiredFlagProperty =
            DependencyProperty.Register("RequiredFlag", typeof(bool), typeof(NbLabel), new PropertyMetadata(false));


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MIIOT.UI.Controls
{
    public class NbCheckBox : CheckBox
    {
        static NbCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbCheckBox), new FrameworkPropertyMetadata(typeof(NbCheckBox)));
        }

        public NbCheckBox()
        {
            //this.SetResourceReference(NbCheckBox.StyleProperty, nameof(NbCheckBox));
        }
    }
}

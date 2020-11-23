using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.UI.Controls
{
    public class NbMinSysButton : NbSysButton
    {
        static NbMinSysButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbMinSysButton), new FrameworkPropertyMetadata(typeof(NbMinSysButton)));
        }
        public NbMinSysButton()
        {
            //this.SetResourceReference(NbMinSysButton.StyleProperty, nameof(NbMinSysButton));
        }
    }
}

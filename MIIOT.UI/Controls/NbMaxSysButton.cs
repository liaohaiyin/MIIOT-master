using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.UI.Controls
{
    public class NbMaxSysButton : NbSysButton
    {
        static NbMaxSysButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbMaxSysButton), new FrameworkPropertyMetadata(typeof(NbMaxSysButton)));
        }
        public NbMaxSysButton()
        {
            //this.SetResourceReference(NbMaxSysButton.StyleProperty, nameof(NbMaxSysButton));
        }
    }
}

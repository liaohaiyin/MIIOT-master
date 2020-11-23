using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MIIOT.UI.Controls
{
    public class NbCloseSysButton : NbSysButton
    {
        static NbCloseSysButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbCloseSysButton), new FrameworkPropertyMetadata(typeof(NbCloseSysButton)));
        }
        public NbCloseSysButton()
        {
            //this.SetResourceReference(NbCloseSysButton.StyleProperty, nameof(NbCloseSysButton));
        }
    }

    public class NbExitSysButton : NbSysButton
    {
        static NbExitSysButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NbExitSysButton), new FrameworkPropertyMetadata(typeof(NbExitSysButton)));
        }
        public NbExitSysButton()
        {
            //this.SetResourceReference(NbCloseSysButton.StyleProperty, nameof(NbCloseSysButton));
        }
    }
}

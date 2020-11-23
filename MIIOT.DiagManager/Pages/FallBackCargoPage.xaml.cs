using MIIOT.DiagManager.ViewModels;
using MIIOT.UI.Controls;

namespace MIIOT.DiagManager.Pages
{
    /// <summary>
    /// FallBackCargoPage.xaml 的交互逻辑
    /// </summary>
    public partial class FallBackCargoPage : NbPage
    {
        public FallBackCargoPage()
        {
            InitializeComponent();
            this.DataContext = new FallbackCargoPageVm(this);
        }
    }
}

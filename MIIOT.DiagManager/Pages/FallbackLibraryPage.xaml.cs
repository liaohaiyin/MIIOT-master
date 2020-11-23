using MIIOT.DiagManager.ViewModels;
using MIIOT.UI.Controls;

namespace MIIOT.DiagManager.Pages
{
    /// <summary>
    /// FallbackLibraryPage.xaml 的交互逻辑
    /// </summary>
    public partial class FallbackLibraryPage : NbPage
    {
        public FallbackLibraryPage()
        {
            InitializeComponent();
            this.DataContext = new FallbackLibraryPageVm(this);
        }
    }
}

using MIIOT.DiagManager.ViewModels;
using MIIOT.UI.Controls;

namespace MIIOT.DiagManager.Pages
{
    /// <summary>
    /// WorkAcceptInfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class WorkAcceptInfoPage : NbPage
    {
        public WorkAcceptInfoPage()
        {
            InitializeComponent();
            this.DataContext = new WorkAcceptInfoPageVm(this);
        }
    }
}

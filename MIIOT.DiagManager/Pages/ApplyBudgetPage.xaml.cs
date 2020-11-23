using MIIOT.DiagManager.ViewModels;
using MIIOT.UI.Controls;


namespace MIIOT.DiagManager.Pages
{
    /// <summary>
    /// ApplyBudgetPage.xaml 的交互逻辑
    /// </summary>
    public partial class ApplyBudgetPage : NbPage
    {
        public ApplyBudgetPage()
        {
            InitializeComponent();
            this.DataContext = new ApplyBudgetPageVm(this);
        }
    }
}

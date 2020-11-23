using MIIOT.Trical.Common;
using MIIOT.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MIIOT.Trical.View
{
    /// <summary>
    /// SunmiView.xaml 的交互逻辑
    /// </summary>
    public partial class SunmiView : UserControl
    {
        public SunmiView()
        {
            InitializeComponent();
        }
        public void GoodsUpdate()
        {
            SunmiUtil.Ins.QueryGoods();

            List<object> list = new List<object>();
            IdWorker snowID = new IdWorker(2);
            var id = snowID.nextId();
            ESLGoodsModel esl = new ESLGoodsModel()
            {
                name = "抗心磷脂抗体IgM检测试剂盒",
                id = 14875884928256000,
                seq_num = 14875884928256000,
                unit = "盒",
                spec = "8*70ml",
                area = "欧蒙医学诊断(中国)有限公司",
                price = 15,
                bar_code = "12345679",
                qr_code = "12345679",
                level = "20200727003"

            };
            list.Add(esl);

            string jsstr = JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            SunmiUtil.Ins.UpdateGoods(jsstr);
            return;
        }
    }
}

using MIIOT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/20 18:57:46
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.ORCart
{
    public class CheckBoxUtil
    {
        public static void GetVisualChild(DependencyObject parent, bool? isChecked)
        {
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                DependencyObject v = (DependencyObject)VisualTreeHelper.GetChild(parent, i);
                CheckBox child = v as CheckBox;

                if (child == null)
                {
                    GetVisualChild(v, isChecked);
                }
                else
                {
                    child.IsChecked = isChecked;
                   
                    return;
                }
            }
        }
    }
}

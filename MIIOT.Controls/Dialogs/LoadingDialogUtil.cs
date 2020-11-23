using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***************************************************
* 开发人员：陈夏松
* 创建时间：2020/8/20 19:32:38
* 描述说明：
* 更改历史：
***************************************************/
namespace MIIOT.Controls.Dialogs
{
    public class LoadingDialogUtil
    {
        public static void Show(Action OnTaskAction , Action OnAction)
        {
            var result = DialogHost.Show(new LoadingDialog(), "RootDialog", delegate (object senders, DialogOpenedEventArgs args)
            {
                Task.Run(() =>
                {
                    OnTaskAction();
                }).ContinueWith(a=> {
                    OnAction();
                    args.Session.Close(false);
                });
            });

        }
    }
}

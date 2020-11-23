using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Model.Validation;
using MIIOT.DiagManager.Pages;
using MIIOT.UI;
using MIIOT.UI.ControlEx;
using System.Windows.Input;

namespace MIIOT.DiagManager.ViewModels
{
    public class MainWindowVm : BaseVm
    {

        public ICommand ChangePwd
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    var page = new ChangePwdPage();
                    var vm = new ChangePwdPageVm();
                    vm.LoginName = AppRuntime.Context.User.LoginName;
                    page.DataContext = vm;
                    var changePwd = false;
                    page.AfterFormValidateHandle = dataContext =>
                    {
                        if (vm.NewPwd != vm.ConfirmPwd)
                            return ModelValidationResult.Error("新密码和确认密码不相同");

                        var loginName = vm.LoginName;
                        var oldPwd = PasswordHelper.Encrypt(vm.OldPwd);
                        var newPwd = PasswordHelper.Encrypt(vm.NewPwd);

                        if (oldPwd == newPwd)
                            return ModelValidationResult.Error("新密码不能和原密码相同");

                        using (var db = AppRuntime.DbService.CreateDb())
                        {
                            var qRet = db.Query<UserInfo>().Where(p => p.LoginName == loginName).FirstOrDefault();
                            if (qRet.Data == null || qRet.Data.Pwd != oldPwd)
                            {
                                return ModelValidationResult.Error("密码验证错误");
                            }
                            var eRet = db.Update<UserInfo>().Set(new { Pwd = newPwd }, p => p.ID == qRet.Data.ID).Execute();
                            if (eRet.Code != Data.DbResultCode.Success)
                                return ModelValidationResult.Error("密码修改失败");
                        }
                        changePwd = true;
                        return ModelValidationResult.Ok();
                    };
                    NbFormWindow.Show(page, "修改密码");

                    if (changePwd)
                    {
                        NbMessageBox.Ok("密码修改成功需要注销提示");
                        AppRuntime.Logout();
                    }
                });
            }
        }

        public ICommand Logout
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    AppRuntime.Logout();
                });
            }
        }
    }
}

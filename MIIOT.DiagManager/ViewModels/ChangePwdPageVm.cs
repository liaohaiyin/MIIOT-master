using MIIOT.DiagManager.Model.Validation;
using MIIOT.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.ViewModels
{
    public class ChangePwdPageVm : NbPageVm
    {
        public override void Init()
        {

        }

        protected string _loginName;
        [Required("账号")]
        public virtual string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; OnPropertyChanged(nameof(LoginName)); }
        }

        protected string _oldPwd;
        [Required("原密码")]
        public virtual string OldPwd
        {
            get { return _oldPwd; }
            set { _oldPwd = value; OnPropertyChanged(nameof(OldPwd)); }
        }

        protected string _newPwd;
        [Required("新密码")]
        public virtual string NewPwd
        {
            get { return _newPwd; }
            set { _newPwd = value; OnPropertyChanged(nameof(NewPwd)); }
        }

        protected string _confirmPwd;
        [Required("确认密码")]
        public virtual string ConfirmPwd
        {
            get { return _confirmPwd; }
            set { _confirmPwd = value; OnPropertyChanged(nameof(ConfirmPwd)); }
        }

    }
}


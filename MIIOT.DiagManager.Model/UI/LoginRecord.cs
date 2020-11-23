using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 登录记录
    /// </summary>
    public class LoginRecord : BaseVm
    {
        protected string _loginName;
        /// <summary>
        /// 登录用户名
        /// </summary>
        public virtual string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; OnPropertyChanged(nameof(LoginName)); }
        }


        protected string _pwd;
        /// <summary>
        /// 登录密码
        /// </summary>
        public virtual string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; OnPropertyChanged(nameof(Pwd)); }
        }

        protected bool _remember;
        /// <summary>
        /// 记住密码
        /// </summary>
        public virtual bool Remember
        {
            get { return _remember; }
            set { _remember = value; OnPropertyChanged(nameof(Remember)); }
        }
    }
}

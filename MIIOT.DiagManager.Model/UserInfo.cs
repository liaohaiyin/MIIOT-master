using MIIOT.DiagManager.Model.Validation;
using System;

namespace MIIOT.DiagManager.Model
{/// <summary>
 /// 操作员信息
 /// </summary>
    public class UserInfo : BaseEntity
    {
        protected int _roleInfoID;

        public virtual int RoleInfoID
        {
            get { return _roleInfoID; }
            set { _roleInfoID = value; OnPropertyChanged(nameof(RoleInfoID)); }
        }

        protected string _loginName;
        /// <summary>
        /// 登录账号
        /// </summary>
        [Required("登录账号")]
        public virtual string LoginName
        {
            get { return _loginName; }
            set { _loginName = value; OnPropertyChanged(nameof(LoginName)); }
        }


        protected string _pwd;

        /// <summary>
        /// 密码
        /// </summary>
        [Required("登录密码")]
        public virtual string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; OnPropertyChanged(nameof(Pwd)); }
        }

        protected string _name;
        /// <summary>
        /// 操作员名称
        /// </summary>
        [Required("用户名称")]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        protected enumUserStatus _userStatus;
        /// <summary>
        /// 状态
        /// </summary>
        public virtual enumUserStatus UserStatus
        {
            get { return _userStatus; }
            set { _userStatus = value; OnPropertyChanged(nameof(UserStatus)); }
        }

        protected string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = value; OnPropertyChanged(nameof(Remark)); }
        }

        protected bool _deleteFlag;

        public virtual bool DeleteFlag
        {
            get { return _deleteFlag; }
            set { _deleteFlag = value; OnPropertyChanged(nameof(DeleteFlag)); }
        }
    }


    public class UserInfoView : UserInfo
    {
        protected string _roleInfoName;

        public virtual string RoleInfoName
        {
            get { return _roleInfoName; }
            set { _roleInfoName = value; OnPropertyChanged(nameof(RoleInfoName)); }
        }

    }
}

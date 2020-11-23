using MIIOT.Comm;
using MIIOT.DiagManager.Core;
using MIIOT.DiagManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MIIOT.DiagManager.ViewModels
{
    public class LoginVm : BaseVm
    {
        public LoginVm()
        {
            try
            {
                var loginRecs = FileHelper.LoadJsonFile<List<LoginRecord>>(GlobalConst.LoginRecordSavePath) ?? new List<LoginRecord>();
                loginRecs.ForEach(p => p.Pwd = PasswordHelper.Decrypt(p.Pwd));
                this.LoginRecordList = new ObservableCollection<LoginRecord>(loginRecs);
                this.SelectedLoginRecord = LoginRecordList.FirstOrDefault();
                if (this.SelectedLoginRecord != null && this.SelectedLoginRecord.Remember)
                {
                    this.Pwd = this.SelectedLoginRecord.Pwd;
                }
            }
            catch
            {
                this.LoginRecordList = new ObservableCollection<LoginRecord>();
            }
        }



        protected ObservableCollection<LoginRecord> _loginRecordList;

        public virtual ObservableCollection<LoginRecord> LoginRecordList
        {
            get { return _loginRecordList; }
            set { _loginRecordList = value; OnPropertyChanged(nameof(LoginRecordList)); }
        }

        protected LoginRecord _selectedLoginRecord;

        public virtual LoginRecord SelectedLoginRecord
        {
            get { return _selectedLoginRecord; }
            set
            {
                _selectedLoginRecord = value;
                OnPropertyChanged(nameof(SelectedLoginRecord));
                //if(value != null)
                //{
                //    LoginName = value.LoginName;
                //    Pwd = value.Pwd;
                //    RememberPwd = value.Remember;
                //}
                //else
                //{
                //    Pwd = string.Empty;
                //}
            }
        }


        protected string _loginName;

        public virtual string LoginName
        {
            get { return _loginName; }
            set
            {
                _loginName = value;
                OnPropertyChanged(nameof(LoginName));
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var rec = LoginRecordList?.FirstOrDefault(p => p.LoginName?.Equals(value, StringComparison.OrdinalIgnoreCase) == true);
                    if (rec != null)
                    {
                        Pwd = rec.Pwd;
                        RememberPwd = rec.Remember;
                    }
                    else
                    {
                        Pwd = string.Empty;
                        RememberPwd = false;
                    }
                }
            }
        }

        protected string _pwd;

        public virtual string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; OnPropertyChanged(nameof(Pwd)); }
        }

        protected bool _rememberPwd;

        public virtual bool RememberPwd
        {
            get { return _rememberPwd; }
            set { _rememberPwd = value; OnPropertyChanged(nameof(RememberPwd)); }
        }

        public void SaveRecord()
        {
            var rec = this.LoginRecordList.FirstOrDefault(p => p.LoginName.Equals(LoginName, StringComparison.OrdinalIgnoreCase));
            if (rec == null)
            {
                rec = new LoginRecord()
                {
                    LoginName = LoginName,
                    Pwd = "",
                    Remember = RememberPwd
                };
                this.LoginRecordList.Insert(0, rec);
            }
            else
            {
                rec.Remember = RememberPwd;
                this.LoginRecordList.Remove(rec);
                this.LoginRecordList.Insert(0, rec);
            }
            if (rec.Remember)
                rec.Pwd = Pwd;
            else
                rec.Pwd = "";

            try
            {
                var top10 = this.LoginRecordList.ToList().Take(10).ToList();
                top10.ForEach(p => p.Pwd = string.IsNullOrWhiteSpace(p.Pwd) ? string.Empty : PasswordHelper.Encrypt(p.Pwd));
                FileHelper.SaveJsonFile(GlobalConst.LoginRecordSavePath, top10);
            }
            catch
            {

            }
        }
    }
}

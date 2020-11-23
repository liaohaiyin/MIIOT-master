using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Model.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    [Serializable]
    public class RoleInfo : BaseEntity
    {
        protected string _name;
        [Required("角色名称")]
        [DataMember]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        protected string _remark;
        [DataMember]
        public virtual string Remark
        {
            get { return _remark; }
            set { _remark = value; OnPropertyChanged(nameof(Remark)); }
        }


        //private bool isSystemData = false;
        //[DataMember]
        //public bool IsSystemData
        //{
        //    get { return isSystemData; }
        //    set { isSystemData = value; }
        //}
    }
}

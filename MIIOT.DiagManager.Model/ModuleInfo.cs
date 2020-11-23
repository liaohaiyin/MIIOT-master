using MIIOT.DiagManager.Model;
using MIIOT.DiagManager.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class ModuleInfo : BaseEntity
    {
        protected string _name;
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required("模块名称")]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        protected string _assemblyString;
        /// <summary>
        /// 程序集名称
        /// </summary>
        [Required("程序集名称")]
        public virtual string AssemblyString
        {
            get { return _assemblyString; }
            set { _assemblyString = value; OnPropertyChanged(nameof(AssemblyString)); }
        }

        protected enumSystemType _systemType;
        /// <summary>
        /// 模块所属系统
        /// </summary>
        public virtual enumSystemType SystemType
        {
            get { return _systemType; }
            set { _systemType = value; OnPropertyChanged(nameof(SystemType)); }
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
    }
    
}

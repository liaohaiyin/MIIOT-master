using MIIOT.DiagManager.Model.Validation;

namespace MIIOT.DiagManager.Model
{
    public class XamlResourceInfo : BaseEntity
    {
        protected string _name;
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required("资源名称")]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        protected string _path;
        /// <summary>
        /// 程序集名称
        /// </summary>
        [Required("资源路径")]
        public virtual string Path
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged(nameof(Path)); }
        }

        protected enumSystemType _systemType;
        /// <summary>
        /// 资源所属系统
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

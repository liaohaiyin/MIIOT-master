

namespace MIIOT.DiagManager.Model
{
    public class RolePermission : BaseEntity
    {
        protected int _roleInfoID;

        public virtual int RoleInfoID
        {
            get { return _roleInfoID; }
            set { _roleInfoID = value; OnPropertyChanged(nameof(RoleInfoID)); }
        }


        protected int _permissionInfoID;

        public virtual int PermissionInfoID
        {
            get { return _permissionInfoID; }
            set { _permissionInfoID = value; OnPropertyChanged(nameof(PermissionInfoID)); }
        }
    }

    public class RolePermissionView : RolePermission
    {
        protected string _code;

        public virtual string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        }

    }
}

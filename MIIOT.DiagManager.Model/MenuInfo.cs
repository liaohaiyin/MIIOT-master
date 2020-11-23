using System;
using System.Collections.Generic;
using System.Text;

namespace MIIOT.DiagManager.Model
{
    /// <summary>
    /// 管理软件菜单
    /// </summary>
    public class MenuInfo : BaseEntity, ISort
    {

        protected int _parentID;
        /// <summary>
        /// 父级菜单
        /// （-1表示顶级菜单）
        /// </summary>
        public virtual int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; OnPropertyChanged(nameof(ParentID)); }
        }


        protected string _name;
        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        protected string _path;
        /// <summary>
        /// 菜单路径
        /// </summary>
        public virtual string Path
        {
            get { return _path; }
            set { _path = value; OnPropertyChanged(nameof(Path)); }
        }

        protected string _param;
        /// <summary>
        /// 参数
        /// </summary>
        public virtual string Param
        {
            get { return _param; }
            set { _param = value; OnPropertyChanged(nameof(Param)); }
        }



        protected string _icon;
        /// <summary>
        /// 图标
        /// </summary>
        public virtual string Icon
        {
            get { return _icon; }
            set { _icon = value; OnPropertyChanged(nameof(Icon)); }
        }


        protected string _permissionInfoCode;
        /// <summary>
        /// 权限编号
        /// </summary>
        public virtual string PermissionInfoCode
        {
            get { return _permissionInfoCode; }
            set { _permissionInfoCode = value; OnPropertyChanged(nameof(PermissionInfoCode)); }
        }


        protected int _sortNo;
        /// <summary>
        /// 排序序号
        /// </summary>
        public virtual int SortNo
        {
            get { return _sortNo; }
            set { _sortNo = value; OnPropertyChanged(nameof(SortNo)); }
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

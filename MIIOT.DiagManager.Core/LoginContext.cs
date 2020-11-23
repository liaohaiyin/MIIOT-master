using MIIOT.DiagManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIIOT.DiagManager.Core;

namespace MIIOT.DiagManager.Core
{/// <summary>
 /// 系统登录后，加载的数据。
 /// </summary>
    public class LoginContext
    {

        public LoginContext()
        {

        }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 操作员信息
        /// </summary>
        public UserInfo User { get; } = new UserInfo();
        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleInfo Role { get; } = new RoleInfo();
        /// <summary>
        /// 角色的权限信息
        /// </summary>
        public List<RolePermissionView> RolePermissions { get; } = new List<RolePermissionView>();
        /// <summary>
        /// 动态加载的模块
        /// </summary>
        public List<ModuleItem> Modules { get; } = new List<ModuleItem>();
        /// <summary>
        /// 动态加载的xaml资源文件
        /// </summary>
        public List<XamlResourceItem> XamlResources { get; } = new List<XamlResourceItem>();

        /// <summary>
        /// 菜单信息
        /// </summary>
        public List<MenuInfo> ManagerMenus { get; } = new List<MenuInfo>();
    }
}

using MIIOT.DiagManager.Core;
using MIIOT.Comm;
using MIIOT.Data;
using MIIOT.Data.Entity;
using MIIOT.Data.Service;
using MIIOT.DiagManager.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MIIOT.DiagManager.Core
{
    /// <summary>
    /// 程序运行时相关数据和操作方法
    /// </summary>
    public static class AppRuntime
    {
        /// <summary>
        /// 设置为true时，关闭主窗口后将会打开登录窗口。
        /// </summary>
        public static bool IsLogout { get; set; } = false;

        /// <summary>
        /// 登录信息
        /// </summary>
        public static LoginContext Context { get; } = new LoginContext();

        /// <summary>
        /// 数据库相关操作
        /// </summary>
        public static DbService DbService => DbService.Default;

        /// <summary>
        /// 当前打开的页面
        /// </summary>
        public static Page CurrentPage { get; set; }

        /// <summary>
        /// 全局参数vm
        /// </summary>
        public static GlobalVm GlobalVm { get; } = new GlobalVm();

        /// <summary>
        /// 全局打印标签
        /// </summary>
        public static RFIDPrinter printer { get; } = new RFIDPrinter();

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LoginResult Login(string loginName, string password)
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(password))
                return LoginResult.Error("请输入用户名或密码");

            var db = AppRuntime.DbService.CreateDb();
            try
            {
                //操作员
                var ePwd = PasswordHelper.Encrypt(password);
                var dbRet = db.Query<UserInfo>().Where(p => p.LoginName == loginName).FirstOrDefault();
                if (dbRet.Data == null ||
                    dbRet.Data.Pwd != ePwd)
                {
                    return LoginResult.Error("用户名或密码错误");
                }
                if (dbRet.Data.UserStatus == enumUserStatus.Stop)
                {
                    return LoginResult.Error("账户已停用");
                }
                if (dbRet.Data.UserStatus == enumUserStatus.Lock)
                {
                    return LoginResult.Error("账户已锁定");
                }
                dbRet.Data.PropertyCopyTo(AppRuntime.Context.User);
                SystemLogHelper.CurrentUser = AppRuntime.Context.User;
                if (!LoadMenus(db))
                {
                    return LoginResult.Error("系统菜单加载失败");
                }
            }
            catch
            {
                return LoginResult.Error("登录异常");
            }
            finally
            {
                db.Dispose();
            }

            AppRuntime.Context.LoginTime = DateTime.Now;

            return LoginResult.Success();
        }

        /// <summary>
        /// 加载菜单信息
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private static bool LoadMenus(Db db)
        {
            QueryDbResult<MenuInfo> ret = new QueryDbResult<MenuInfo>() { Code = DbResultCode.Nothing };
            ret = db.Query<MenuInfo>().ToList();
            if (AppRuntime.Context.RolePermissions.Count > 0)
            {
                var permissionCodes = AppRuntime.Context.RolePermissions.Select(p => p.Code).ToArray();
                ret = db.Query<MenuInfo>().Where(p => p.PermissionInfoCode.In(permissionCodes)).ToList();
            }

            if (ret.Code == DbResultCode.Error || ret.Data == null)
                return false;

            if (ret.Data != null)
            {
                Context.ManagerMenus.Clear();
                Context.ManagerMenus.AddRange(ret.Data);                
            }
            return true;
        }

        /// <summary>
        /// 加载模块菜单
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private static bool LoadModules(Db db)
        {
            var ret = db.Query<ModuleInfo>().Where(p => p.SystemType == enumSystemType.ParkManager).ToList();
            if (ret.Code == DbResultCode.Error)
                return false;

            if (ret.Data != null)
            {
                AppRuntime.Context.Modules.Clear();
                AppRuntime.Context.Modules.AddRange(ret.Data.Select(p => new ModuleItem(p)));
                foreach (var item in AppRuntime.Context.Modules)
                {
                    item.Load();
                }
            }

            return true;
        }

        /// <summary>
        /// 在ui线程上执行
        /// </summary>
        /// <param name="callback"></param>
        public static void ExecuteOnUI(Action callback)
        {
            Application.Current.Dispatcher.Invoke(callback);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public static void Logout()
        {
            IsLogout = true;
            AppRuntime.Context.ManagerMenus.Clear();
            AppRuntime.Context.RolePermissions.Clear();

            Application.Current.Dispatcher.Invoke(() =>
            {
                Application.Current.MainWindow.Close();
            });
        }
    }

    public class LoginResult
    {
        public static LoginResult Success(string message = "")
        {
            return new LoginResult() { IsSuccess = true, Message = message };
        }
        public static LoginResult Error(string message)
        {
            return new LoginResult() { IsSuccess = false, Message = message };
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

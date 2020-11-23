using MIIOT.DiagManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data.Service.Helper
{
    public class SystemConfigHelper
    {
        #region 常量
        /// <summary>
        /// 系统logo图片配置键
        /// </summary>
        public const string SystemLogoKey = "SystemLogo";

        #endregion

        /// <summary>
        /// 系统logo图片base64
        /// </summary>
        public static string SystemLogo => GetData(SystemLogoKey);

        #region 配置缓存
        /// <summary>
        /// 配置缓存
        /// </summary>
        private static List<SystemConfig> _cache = new List<SystemConfig>();
        /// <summary>
        /// 配置缓存
        /// </summary>
        public static List<SystemConfig> Cache { get => _cache; }
        /// <summary>
        /// 加载系统配置缓存
        /// </summary>
        /// <returns></returns>
        public static bool LoadCache()
        {
            var ret = DbService.Default.Query<SystemConfig>(null);
            if (ret.Code == DbResultCode.Error)
                return false;

            if (ret.Data?.Count > 0)
            {
                _cache.Clear();
                _cache.AddRange(ret.Data);
                _cache.RemoveAll(p => !SystemConfigHelper.IsShowElement(p.CultrueKey));
                OnCacheLoaded(_cache.Select(p => p.Key));
            }
            return true;
        }
        #endregion

        /// <summary>
        /// 获取指定键值的Data数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetData(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;
            try
            {
                var cfg = _cache.Find(p => p.Key == key);
                if (cfg == null)
                    return string.Empty;
                return cfg.Data ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 界面元素是否显示
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsShowElement(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return true;
            else
                return false;
        }

        #region 事件
        /// <summary>
        /// 系统配置缓存加载完成事件
        /// </summary>
        public static event EventHandler<CacheLoadedEventArgs> CacheLoaded;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newKeys"></param>
        public static void OnCacheLoaded(IEnumerable<string> newKeys)
        {
            try
            {
                CacheLoaded?.Invoke(null, new CacheLoadedEventArgs(newKeys));
            }
            catch
            {

            }
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class CacheLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newKeys"></param>
        public CacheLoadedEventArgs(IEnumerable<string> newKeys)
        {
            this.NewKeys = newKeys?.ToArray();
        }

        /// <summary>
        /// 本次加载更新缓存的键值集合
        /// </summary>
        public string[] NewKeys { get; }

    }
}

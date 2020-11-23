using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    /// <summary>
    /// 数据库操作结果
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    public class DbResult<T>
    {
        /// <summary>
        /// 结果编码
        /// </summary>
        public virtual DbResultCode Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// 异常
        /// [无异常=null]
        /// </summary>
        public virtual Exception Exception { get; set; }
        /// <summary>
        /// 结果
        /// （插入单条数据时，Data值=最新自增键）
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// 数据库结果编码
    /// </summary>
    public enum DbResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 无
        /// [没有报错也没有增删改查任何数据]
        /// </summary>
        Nothing = 1,
        /// <summary>
        /// 失败
        /// [数据库报错和程序代码异常]
        /// </summary>
        Error = 2
    }
}

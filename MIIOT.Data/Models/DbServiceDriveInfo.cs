using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    public interface IDbServiceDirectoryInfo
    {
        string Name { get; }

        string Path { get; }

        DirectoryType DirectoryType { get; }

        IDbServiceDirectoryInfo[] GetSubDirectory();
    }


    /// <summary>
    /// 数据库服务器的目录信息
    /// </summary>
    public class DbServiceDirectoryInfo : IDbServiceDirectoryInfo
    {
        /// <summary>
        /// 
        /// </summary>
        protected Db _db;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        protected DbServiceDirectoryInfo(Db db)
        {
            _db = db;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// 目录类型
        /// </summary>
        public DirectoryType DirectoryType { get; internal set; }

        /// <summary>
        /// 获取所有直接子目录信息
        /// </summary>
        /// <returns></returns>
        public IDbServiceDirectoryInfo[] GetSubDirectory()
        {
            var ret = _db.Query<dynamic>("exec master..xp_dirtree @p,1,1", new { @p = this.Path, });
            if (ret.Code == DbResultCode.Error)
                throw (ret.Exception ?? new Exception(ret.Message));
            if (ret.Code == DbResultCode.Nothing)
                return new DbServiceDirectoryInfo[0];

            return ret.Data
                .Select(p => new DbServiceDirectoryInfo(_db) { Name = p.subdirectory, Path = System.IO.Path.Combine(this.Path, p.subdirectory), DirectoryType = (DirectoryType)p.file })
                .ToArray();
        }

        /// <summary>
        /// 获取数据库服务器的磁盘信息
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IDbServiceDirectoryInfo[] GetDevices(Db db)
        {
            if (db == null) throw new ArgumentNullException("db");

            var ret = db.Query<dynamic>("exec master..xp_fixeddrives");
            if (ret.Code == DbResultCode.Error)
                throw (ret.Exception ?? new Exception(ret.Message));
            if (ret.Code == DbResultCode.Nothing)
                return new DbServiceDirectoryInfo[0];

            return ret.Data.Select(p => new DbServiceDirectoryInfo(db) { Name = p.drive, Path = $@"{p.drive}:\", DirectoryType = DirectoryType.Disk }).ToArray();
        }

        
    }

    public enum DirectoryType
    {
        /// <summary>
        /// 磁盘
        /// </summary>
        Disk = -1,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder = 0,
        /// <summary>
        /// 文件
        /// </summary>
        File = 1,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIIOT.Data
{
    /// <summary>
    /// sqlserver
    /// </summary>
    public class SqlServerDb : Db
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public override DbProvider DbProvider => DbProvider.SqlServer;
    }

    /// <summary>
    /// sqlserver
    /// </summary>
    public class MySqlDb : Db
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public override DbProvider DbProvider => DbProvider.MySql;
    }
}

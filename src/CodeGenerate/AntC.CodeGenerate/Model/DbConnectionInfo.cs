using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    public class DbConnectionInfo
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库类型 1:mysql 2:sqlserver 3:oracle
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// 服务主机IP
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 数据库表分组信息
        /// </summary>
        public Dictionary<string, IEnumerable<TableGroupInfo>> TableGroups { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string ToConnectionString()
        {
            var dbName = "information_schema";
            switch (DbType)
            {
                case DbType.Mysql:
                    dbName = "information_schema";
                    break;
                case DbType.SqlServer:
                    dbName = "master";
                    break;
                case DbType.Oracle:
                    // todo 添加不同数据库类型的默认数据库
                    break;
            }

            return $"server={Host};port={Port};database={dbName};User ID={Username};Password={Password};";
        }
    }
}

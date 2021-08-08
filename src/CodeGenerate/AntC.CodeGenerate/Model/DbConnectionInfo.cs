using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库连接
    /// </summary>
    public class DbConnectionInfo
    {
        public DbConnectionInfo()
        {
            TableGroups = new Dictionary<string, List<TableGroupInfo>>();
            SelectTables = new Dictionary<string, List<string>>();
        }

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
        public Dictionary<string, List<TableGroupInfo>> TableGroups { get; set; }

        /// <summary>
        /// 数据库选中的表信息
        /// </summary>
        public Dictionary<string, List<string>> SelectTables { get; set; }

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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return $"server={Host};port={Port};database={dbName};User ID={Username};Password={Password};";
        }

        public string TestConnect()
        {
            IDbConnection conn;
            switch (DbType)
            {
                case DbType.Mysql:
                    conn = new MySqlConnection(ToConnectionString());
                    break;
                case DbType.SqlServer:
                    conn = new SqlConnection(ToConnectionString());
                    break;
                case DbType.Oracle:
                    conn = new OracleConnection(ToConnectionString());
                    break;
                default:
                    return "当前数据库类型不支持";
            }

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return "连接成功";
        }
    }
}

using AntC.CodeGenerate.Core.Model.Db;
using System.Collections.Generic;

namespace AntC.CodeGenerate.Core.Contracts
{
    /// <summary>
    /// 数据库信息提供器
    /// </summary>
    public interface IDbInfoProvider
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string DbConnectionString { get; set; }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        IEnumerable<DatabaseInfo> GetDataBases();

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="withDetails">包含明细数据</param>
        /// <returns></returns>
        IEnumerable<TableInfo> GetTables(string dbName, bool withDetails = false);

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="databaseInfo">数据库信息</param>
        /// <param name="withDetails">包含明细数据</param>
        /// <returns></returns>
        DatabaseInfo GetTables(DatabaseInfo databaseInfo);

        /// <summary>
        /// 获取数据表列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        IEnumerable<ColumnInfo> GetColumns(string tableName);

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName">库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        TableInfo GetTableInfoWithColumns(string dbName, string tableName);

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName">库名称</param>
        /// <param name="tableNames">表名称</param>
        /// <returns></returns>
        IEnumerable<TableInfo> GetTableInfoWithColumns(string dbName, string[] tableNames);

        /// <summary>
        /// 获取数据库类型对应的代码字段类型
        /// </summary>
        /// <returns></returns>
        string GetFiledTypeName(ColumnInfo column);
    }
}

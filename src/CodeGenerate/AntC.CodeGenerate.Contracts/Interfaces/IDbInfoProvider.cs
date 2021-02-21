using System.Collections.Generic;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 数据库信息提供者
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
        IEnumerable<DataBaseInfoModel> GetDataBases();

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        IEnumerable<DbTableInfoModel> GetTables(string dbName);

        /// <summary>
        /// 获取数据表列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        IEnumerable<DbColumnInfoModel> GetColumns(string tableName);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.Interfaces
{
    /// <summary>
    /// 数据库信息提供者
    /// </summary>
    public interface IDbInfoProvider
    {
        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataBaseInfoModel> GetDataBaseList();

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

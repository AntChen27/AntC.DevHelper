using System;
using System.Collections.Generic;
using AntC.CodeGenerate.Core.Contracts;
using AntC.CodeGenerate.Core.Model.Db;

namespace AntC.CodeGenerate.Core.Provider
{
    /// <summary>
    /// 数据库信息提供器基类
    /// </summary>
    /// <seealso cref="AntC.CodeGenerate.Core.Contracts.IDbInfoProvider" />
    public abstract class BaseDbInfoProvider : IDbInfoProvider
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbConnectionString { get; set; }

        /// <summary>
        /// 获取数据库名称
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<DatabaseInfo> GetDataBases();

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName">数据库信息</param>
        /// <param name="withDetails">包含明细数据</param>
        /// <returns></returns>
        public abstract IEnumerable<TableInfo> GetTables(string dbName, bool withDetails = false);

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="databaseInfo">数据库信息</param>
        /// <returns></returns>
        public abstract DatabaseInfo GetTables(DatabaseInfo databaseInfo);

        /// <summary>
        /// 获取数据表列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public abstract IEnumerable<ColumnInfo> GetColumns(string tableName);

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName">库名称</param>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public abstract TableInfo GetTableInfoWithColumns(string dbName, string tableName);

        /// <summary>
        /// 获取数据表信息
        /// </summary>
        /// <param name="dbName">库名称</param>
        /// <param name="tableNames">表名称</param>
        /// <returns></returns>
        public abstract IEnumerable<TableInfo> GetTableInfoWithColumns(string dbName, string[] tableNames);

        /// <summary>
        /// 获取数据库类型对应的代码字段类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual string GetFiledTypeName(ColumnInfo column)
        {
            var filedTypeName = GetDefaultFiledTypeName(column);

            if (!string.IsNullOrWhiteSpace(filedTypeName) &&
                column.Nullable &&
                !"string".Equals(filedTypeName, StringComparison.CurrentCultureIgnoreCase) &&
                !filedTypeName.Trim().EndsWith("?"))
            {
                return $"{filedTypeName}?";
            }

            return filedTypeName;
        }

        /// <summary>
        /// 获取默认数据库字段类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        protected abstract string GetDefaultFiledTypeName(ColumnInfo column);
    }
}

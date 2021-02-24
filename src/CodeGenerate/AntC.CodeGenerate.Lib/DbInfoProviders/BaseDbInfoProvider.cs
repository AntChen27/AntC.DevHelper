using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using System;
using System.Collections.Generic;

namespace AntC.CodeGenerate.DbInfoProviders
{
    public abstract class BaseDbInfoProvider : IDbInfoProvider
    {

        public string DbConnectionString { get; set; }
        public abstract IEnumerable<DbInfoModel> GetDataBases();
        public abstract IEnumerable<DbTableInfoModel> GetTables(string dbName);
        public abstract IEnumerable<DbColumnInfoModel> GetColumns(string tableName);
        public abstract DbTableInfoModel GetTableInfoWithColumns(string dbName, string tableName);
        /// <summary>
        /// 获取数据库类型对应的代码字段类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual string GetFiledTypeName(DbColumnInfoModel column)
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
        protected abstract string GetDefaultFiledTypeName(DbColumnInfoModel column);
    }
}

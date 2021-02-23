using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using System;
using System.Collections.Generic;

namespace AntC.CodeGenerate.DbInfoProviders
{
    public abstract class BaseDbInfoProvider : IDbInfoProvider
    {
        protected List<IPropertyTypeConverter> PropertyTypeConverters { get; set; } = new List<IPropertyTypeConverter>();

        public string DbConnectionString { get; set; }
        public abstract IEnumerable<DbInfoModel> GetDataBases();
        public abstract IEnumerable<DbTableInfoModel> GetTables(string dbName);
        public abstract IEnumerable<DbColumnInfoModel> GetColumns(string tableName);
        public abstract DbTableInfoModel GetTableInfoWithColumns(string dbName, string tableName);

        public void AddPropertyTypeConverter(IPropertyTypeConverter converter)
        {
            PropertyTypeConverters.Add(converter);
        }

        public void AddPropertyTypeConverter(IEnumerable<IPropertyTypeConverter> converters)
        {
            PropertyTypeConverters.AddRange(converters);
        }

        /// <summary>
        /// 获取数据库类型对应的代码字段类型
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public virtual string GetFiledTypeName(DbColumnInfoModel column)
        {
            var filedTypeName = string.Empty;
            bool isPropertyTypeConverted = false;
            for (var i = PropertyTypeConverters.Count - 1; i >= 0; i--)
            {
                if (PropertyTypeConverters[i].CanConvert(column))
                {
                    filedTypeName = PropertyTypeConverters[i].Convert(column);
                    isPropertyTypeConverted = true;
                    break;
                }
            }

            if (!isPropertyTypeConverted)
            {
                filedTypeName = GetDefaultFiledTypeName(column);
            }

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

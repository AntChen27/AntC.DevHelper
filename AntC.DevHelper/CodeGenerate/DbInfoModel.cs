using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace AntC.DevHelper.CodeGenerate
{
    public class DataBaseInfoModel
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; }

        public IEnumerable<DbTableInfoModel> Tables { get; set; }
    }

    public class DbTableInfoModel
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Commont { get; set; }

        public IEnumerable<DbColumnInfoModel> Columns { get; set; }
    }
    public class DbColumnInfoModel
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Commont { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool Key { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 数据类型名称 带数据长度的
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public long DataLength { get; set; }

        public long NumericPrecision { get; set; }

        public long NumericScale { get; set; }

        /// <summary>
        /// 获取数据库类型对应的代码字段类型
        /// </summary>
        /// <returns></returns>
        public string GetFeildTypeName() =>
            DataType.ToLower() switch
            {
                #region Mysql

                "binary" => "byte[]",
                "bit" => "bool",
                "text" => "string",
                "longtext" => "string",
                "char" => "string",
                "varchar" => "string",
                "nvarchar" => "string",
                "time" => nameof(TimeSpan),
                "date" => nameof(DateTime),
                "datetime" => nameof(DateTime),
                "decimal" => "decimal",
                "float" => "float",
                "double" => "double",
                "integer" => "int",
                "int" => "int",
                "smallint" => "int",
                "tinyint" when DataTypeName == "tinyint(1)" => "bool",
                //"tinyint" => "byte",
                "tinyint" => "int",
                "bigint" => "long",

                #endregion
                _ => DataType
            };

        //{
        //    switch (DataType.ToLower())
        //    {
        //        case "decimal":
        //                return "float";
        //        case "float": return "float";
        //        case "double": return "double";
        //        case "intger": return "int";
        //        case "int": return "int";
        //        case "bigint": return "long";
        //        default:
        //            return dbDataType;
        //    }
        //}
    }
}

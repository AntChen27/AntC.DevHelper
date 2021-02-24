using AntC.CodeGenerate.DbInfoProviders;
using AntC.CodeGenerate.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using AntC.CodeGenerate.Mysql.Model;

namespace AntC.CodeGenerate.Mysql
{
    public class MysqlDbInfoProvider : BaseDbInfoProvider
    {
        public override IEnumerable<DbInfoModel> GetDataBases()
        {
            return GetDb()
                .Queryable<MysqlSchemata>()
                .Select(t => Map(t)).ToList();
        }

        public override IEnumerable<DbTableInfoModel> GetTables(string dbName)
        {
            return GetDb()
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .ToList()
                .Select(Map)
                .ToList();
        }

        public override IEnumerable<DbColumnInfoModel> GetColumns(string tableName)
        {
            return GetDb()
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_NAME == tableName)
                .ToList()
                .Select(Map)
                .ToList();
        }

        public override DbTableInfoModel GetTableInfoWithColumns(string dbName, string tableName)
        {
            var db = GetDb();

            var dbTableInfoModel = Map(db
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == dbName && t.TABLE_NAME == tableName)
                .First()
                );

            dbTableInfoModel.DbInfo = Map(db
                .Queryable<MysqlSchemata>()
                .Where(x => x.SCHEMA_NAME == dbName)
                .First());

            dbTableInfoModel.Columns = db
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_SCHEMA == dbName && t.TABLE_NAME == tableName)
                .ToList()
                .Select(Map)
                .ToList();

            foreach (var dbColumnInfoModel in dbTableInfoModel.Columns)
            {
                dbColumnInfoModel.DbTableInfo = dbTableInfoModel;
            }
            dbTableInfoModel.DbInfo.Tables = new List<DbTableInfoModel>()
            {
                dbTableInfoModel
            };

            return dbTableInfoModel;
        }

        protected override string GetDefaultFiledTypeName(DbColumnInfoModel column)
        {
            bool isUnsigned = column.DataTypeName.ToLower().Contains("unsigned");

            var filedTypeName = column.DataType.ToLower() switch
            {
                #region Mysql

                "image" => "byte[]",
                "blob" => "byte[]",
                "mediumblob" => "byte[]",
                "longblob" => "byte[]",
                "binary" => "byte[]",
                "varbinary" => "byte[]",
                "guid" => "Guid",
                "bit" => "bool",
                "bool" => "bool",
                "boolean" => "bool",
                "text" => "string",
                "longtext" => "string",
                "char" => "string",
                "varchar" => "string",
                "nvarchar" => "string",
                "time" => nameof(TimeSpan),
                "timestamp" => nameof(DateTime),
                "date" => nameof(DateTime),
                "datetime" => nameof(DateTime),
                "smalldatetime" => nameof(DateTime),
                "numeric" => "decimal",
                "smallmoney" => "decimal",
                "money" => "decimal",
                "decimal" => "decimal",
                "float" => "float",
                "double" => "double",
                "integer" => "int",
                "int" => "int",
                "smallint" when isUnsigned => "ushort",
                "smallint" => "short",
                "tinyint" when isUnsigned => "byte",
                "tinyint" => "sbyte",
                "bigint" when isUnsigned => "ulong",
                "bigint" => "long",

                #endregion

                _ => column.DataType
            };

            if (column.Nullable && filedTypeName != "string")
            {
                return $"{filedTypeName}?";
            }

            return filedTypeName;
        }
        private DbInfoModel Map(MysqlSchemata t)
        {
            return new DbInfoModel()
            {
                DbName = t.SCHEMA_NAME,
            };
        }

        private DbTableInfoModel Map(MysqlSchemaTable t)
        {
            return new DbTableInfoModel()
            {
                TableName = t.TABLE_NAME,
                Commont = t.TABLE_COMMENT,
            };
        }

        private DbColumnInfoModel Map(MysqlSchemaColumns t)
        {
            return new MysqlDbColumnInfoModel()
            {
                ColumnName = t.COLUMN_NAME,
                Commont = t.COLUMN_COMMENT,
                DataLength = t.CHARACTER_MAXIMUM_LENGTH,
                DataType = t.DATA_TYPE,
                DataTypeName = t.COLUMN_TYPE,
                Nullable = t.IS_NULLABLE == "YES",
                NumericPrecision = t.NUMERIC_PRECISION,
                NumericScale = t.NUMERIC_SCALE,
                Key = t.COLUMN_KEY == "PRI",
                DefaultValue = t.COLUMN_DEFAULT,
                CharacterSetName = t.CHARACTER_SET_NAME,
                CollationName = t.COLLATION_NAME,
            };
        }


        //创建SqlSugarClient 
        public SqlSugarClient GetDb()
        {
            //创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = "Data Source=10.4.1.248;Initial Catalog=information_schema;User ID=root;Password=123456",//连接符字串
                ConnectionString = DbConnectionString,
                DbType = DbType.MySql,
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
            });

            //添加Sql打印事件，开发中可以删掉这个代码
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
            };
            return db;
        }
    }
}

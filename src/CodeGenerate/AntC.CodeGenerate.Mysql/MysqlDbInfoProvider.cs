using AntC.CodeGenerate.Core.Model.Db;
using AntC.CodeGenerate.Core.Provider;
using AntC.CodeGenerate.Mysql.Entities;
using AntC.CodeGenerate.Mysql.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntC.CodeGenerate.Mysql
{
    public class MysqlDbInfoProvider : BaseDbInfoProvider
    {
        public override IEnumerable<DatabaseInfo> GetDataBases()
        {
            return GetDb()
                .Queryable<MysqlSchemata>()
                .ToList()
                .Select(Map)
                .ToList();
        }

        public override IEnumerable<TableInfo> GetTables(string dbName, bool withDetails = false)
        {
            var db = GetDb();
            if (!withDetails)
            {
                return db
                  .Queryable<MysqlSchemaTable>()
                  .Where(t => t.TABLE_SCHEMA == dbName)
                  .ToList()
                  .Select(Map)
                  .ToList();
            }

            var tableInfos = db
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .ToList()
                .Select(Map)
                .ToList();


            var columnInfos = db
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .ToList();

            var dbInfo = new DatabaseInfo() { DbName = dbName, Tables = tableInfos };

            tableInfos.ForEach(x =>
            {
                x.Columns = columnInfos.Where(col => col.TABLE_NAME == x.TableName).Select(Map).ToList();
                x.DatabaseInfo = dbInfo;
            });
            return tableInfos;
        }

        public override DatabaseInfo GetTables(DatabaseInfo databaseInfo)
        {
            var db = GetDb();
            var tableInfos = db
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == databaseInfo.DbName)
                .ToList()
                .Select(Map)
                .ToList();


            var columnInfos = db
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_SCHEMA == databaseInfo.DbName)
                .ToList();

            tableInfos.ForEach(x =>
            {
                x.Columns = columnInfos.Where(col => col.TABLE_NAME == x.TableName)
                    .Select(o =>
                    {
                        var res = Map(o);
                        res.TableInfo = x;
                        return res;
                    }).ToList();
                x.DatabaseInfo = databaseInfo;
            });

            databaseInfo.Tables = tableInfos;
            return databaseInfo;
        }

        public override IEnumerable<ColumnInfo> GetColumns(string tableName)
        {
            return GetDb()
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_NAME == tableName)
                .ToList()
                .Select(Map)
                .ToList();
        }

        public override TableInfo GetTableInfoWithColumns(string dbName, string tableName)
        {
            var db = GetDb();

            var dbTableInfoModel = Map(db
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == dbName && t.TABLE_NAME == tableName)
                .First());

            dbTableInfoModel.DatabaseInfo = Map(db
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
                dbColumnInfoModel.TableInfo = dbTableInfoModel;
            }
            dbTableInfoModel.DatabaseInfo.Tables = new List<TableInfo>()
            {
                dbTableInfoModel
            };

            return dbTableInfoModel;
        }

        public override IEnumerable<TableInfo> GetTableInfoWithColumns(string dbName, string[] tableNames)
        {
            var db = GetDb();

            var tables = db
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .Where(x => tableNames.Contains(x.TABLE_NAME))
                .ToList();

            var databaseInfo = new DatabaseInfo()
            {
                DbName = dbName,
            };

            var columns = db
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .Where(x => tableNames.Contains(x.TABLE_NAME))
                .ToList();

            var results = new List<TableInfo>();
            foreach (var table in tables)
            {
                var t = Map(table);
                t.DatabaseInfo = databaseInfo;
                foreach (var column in columns.Where(x => x.TABLE_NAME == table.TABLE_NAME))
                {
                    var col = Map(column);
                    col.TableInfo = t;
                }
            }

            databaseInfo.Tables = results;

            return results;
        }

        protected override string GetDefaultFiledTypeName(ColumnInfo column)
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

        private DatabaseInfo Map(MysqlSchemata t)
        {
            return new DatabaseInfo()
            {
                DbName = t.SCHEMA_NAME,
            };
        }

        private TableInfo Map(MysqlSchemaTable t)
        {
            return new TableInfo()
            {
                TableName = t.TABLE_NAME,
                Commont = t.TABLE_COMMENT,
            };
        }

        private ColumnInfo Map(MysqlSchemaColumns t)
        {
            return new MysqlColumnInfo()
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
            db.Open();
            return db;
        }
    }
}

using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using SqlSugar;

namespace AntC.CodeGenerate.Mysql
{
    public class MysqlDbInfoProvider : IDbInfoProvider
    {
        public string DbConnectionString { get; set; }

        public IEnumerable<DataBaseInfoModel> GetDataBases()
        {
            return GetDb()
                .Queryable<MysqlSchemata>()
                .Select(t => new DataBaseInfoModel()
                {
                    DataBaseName = t.SCHEMA_NAME,
                }).ToList();
        }

        public IEnumerable<DbTableInfoModel> GetTables(string dbName)
        {
            return GetDb()
                .Queryable<MysqlSchemaTable>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .Select(t => new DbTableInfoModel()
                {
                    TableName = t.TABLE_NAME,
                    Commont = t.TABLE_COMMENT,
                }).ToList();
        }

        public IEnumerable<DbColumnInfoModel> GetColumns(string tableName)
        {
            return GetDb()
                .Queryable<MysqlSchemaColumns>()
                .Where(t => t.TABLE_NAME == tableName)
                .Select(t => new DbColumnInfoModel()
                {
                    ColumnName = t.COLUMN_NAME,
                    Commont = t.COLUMN_COMMENT,
                    DataLength = t.CHARACTER_MAXIMUM_LENGTH,
                    DataType = t.DATA_TYPE,
                    DataTypeName = t.COLUMN_TYPE,
                    Nullable = t.IS_NULLABLE == "YES",
                    NumericPrecision = t.NUMERIC_PRECISION,
                    NumericScale = t.NUMERIC_SCALE,
                    Key = t.COLUMN_KEY == "PRI"
                }).ToList();
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

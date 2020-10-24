using AntC.DevHelper.CodeGenerate.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.MysqlShema
{
    public class MysqlDbInfoProvider : IDbInfoProvider
    {
        DbContext db = new DbContext();
        public IEnumerable<DbColumnInfoModel> GetColumns(string tableName)
        {
            return db.GetInstance()
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

        public IEnumerable<DataBaseInfoModel> GetDataBaseList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DbTableInfoModel> GetTables(string dbName)
        {
            return db.GetInstance()
                .Queryable<MysqlSchemaTables>()
                .Where(t => t.TABLE_SCHEMA == dbName)
                .Select(t => new DbTableInfoModel()
                {
                    TableName = t.TABLE_NAME,
                    Commont = t.TABLE_COMMENT,
                }).ToList();
        }
    }
}

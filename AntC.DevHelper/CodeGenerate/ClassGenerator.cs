using AntC.DevHelper.CodeGenerate.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AntC.DevHelper.CodeGenerate
{
    public class ClassGenerator
    {
        private readonly IDbInfoProvider dbInfoProvider;

        public ClassGenerator(IDbInfoProvider dbInfoProvider)
        {
            this.dbInfoProvider = dbInfoProvider;
        }

        public IEnumerable<DbTableInfoModel> GetDbTableInfoModels(string dbName)
        {
            var tables = dbInfoProvider.GetTables(dbName);
            foreach (var table in tables)
            {
                table.Columns = dbInfoProvider.GetColumns(table.TableName);
            }
            return tables;
        }
    }
}

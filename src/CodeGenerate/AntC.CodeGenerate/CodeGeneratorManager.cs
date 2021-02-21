using System.Collections.Generic;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate
{
    public class CodeGeneratorManager : ICodeGeneratorManager
    {
        public IDbInfoProvider DbInfoProvider { get; set; }
        public ICodeConverter CodeConverter { get; set; }

        public string DbConnectionString
        {
            get => DbInfoProvider.DbConnectionString;
            set => DbInfoProvider.DbConnectionString = value;
        }

        public CodeGeneratorManager(IDbInfoProvider dbInfoProvider)
        {
            DbInfoProvider = dbInfoProvider;
        }

        public IEnumerable<DataBaseInfoModel> GetDataBases()
        {
            return DbInfoProvider.GetDataBases();
        }

        public IEnumerable<DbTableInfoModel> GetTables(string dbName)
        {
            return DbInfoProvider.GetTables(dbName);
        }

        public IEnumerable<DbColumnInfoModel> GetColumns(string tableName)
        {
            return DbInfoProvider.GetColumns(tableName);
        }

        public string Convert(string value, CodeType type = CodeType.ClassName)
        {
            return CodeConverter.Convert(value, type);
        }
    }
}

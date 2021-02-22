using System;
using System.Collections.Generic;
using System.Linq;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate
{
    public class CodeGeneratorManager : ICodeGeneratorManager
    {
        public IDbInfoProvider DbInfoProvider { get; set; }

        public ICodeConverter CodeConverter { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 执行器
        /// </summary>
        private readonly List<ICodeGenerateExecutor> _executors;

        public string DbConnectionString
        {
            get => DbInfoProvider.DbConnectionString;
            set => DbInfoProvider.DbConnectionString = value;
        }

        public CodeGeneratorManager(IDbInfoProvider dbInfoProvider)
        {
            DbInfoProvider = dbInfoProvider;
            _executors = new List<ICodeGenerateExecutor>();
        }

        #region DbInfoProvider

        public IEnumerable<DbInfoModel> GetDataBases() => DbInfoProvider.GetDataBases();

        public IEnumerable<DbTableInfoModel> GetTables(string dbName) => DbInfoProvider.GetTables(dbName);

        public IEnumerable<DbColumnInfoModel> GetColumns(string tableName) => DbInfoProvider.GetColumns(tableName);

        public DbTableInfoModel GetTableInfoWithColumns(string dbName, string tableName) => DbInfoProvider.GetTableInfoWithColumns(dbName, tableName);


        /// <summary>
        /// 获取数据库类型对应的代码字段类型
        /// </summary>
        /// <returns></returns>
        public string GetFiledTypeName(DbColumnInfoModel column) => DbInfoProvider.GetFiledTypeName(column);

        #endregion

        #region CodeConverter

        public string Convert(string value, CodeType type = CodeType.ClassName) => CodeConverter.Convert(value, type);

        #endregion

        /// <summary>
        /// 执行代码创建
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        public void ExecCodeGenerate(CodeGenerateInfo codeGenerateInfo)
        {
            Check.NotNull(codeGenerateInfo, nameof(codeGenerateInfo));
            Check.NotNullOrEmpty<CodeGenerateTableInfo>(codeGenerateInfo.CodeGenerateTableInfos.ToList(), $"{nameof(codeGenerateInfo)}.{nameof(codeGenerateInfo.CodeGenerateTableInfos)}");

            foreach (var tableInfo in codeGenerateInfo.CodeGenerateTableInfos)
            {
                var context = new CodeGenerateContext()
                {
                    DbInfoProvider = this,
                    CodeConverter = CodeConverter,
                    CodeGenerateTableInfo = tableInfo,
                    DbTableInfoModel = GetDbTableInfoModel(tableInfo),
                    OutPutRootPath = codeGenerateInfo.OutPutRootPath,
                };
                foreach (var codeGenerateExecutor in _executors)
                {
                    codeGenerateExecutor.ExecCodeGenerate(context);
                }
            }
        }

        private DbTableInfoModel GetDbTableInfoModel(CodeGenerateTableInfo tableInfo)
        {
            return GetTableInfoWithColumns(tableInfo.DbName, tableInfo.TableName);
        }

        public void AddCodeGenerateExecutor(ICodeGenerateExecutor executor)
        {
            _executors.Add(executor);
        }

        public void AddCodeGenerateExecutor(IEnumerable<ICodeGenerateExecutor> executors)
        {
            _executors.AddRange(executors);
        }
    }
}

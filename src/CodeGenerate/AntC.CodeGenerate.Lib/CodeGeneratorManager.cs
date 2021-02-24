using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
        /// 执行器 -- 库
        /// </summary>
        private readonly List<IDbCodeGenerateExecutor> _dbCodeGenerateExecutors;
        /// <summary>
        /// 执行器 -- 表
        /// </summary>
        private readonly List<ITableCodeGenerateExecutor> _tableCodeGenerateExecutors;

        protected List<IPropertyTypeConverter> _propertyTypeConverters = new List<IPropertyTypeConverter>();

        public string DbConnectionString
        {
            get => DbInfoProvider.DbConnectionString;
            set => DbInfoProvider.DbConnectionString = value;
        }

        public CodeGeneratorManager(IDbInfoProvider dbInfoProvider)
        {
            DbInfoProvider = dbInfoProvider;
            _tableCodeGenerateExecutors = new List<ITableCodeGenerateExecutor>();
            _dbCodeGenerateExecutors = new List<IDbCodeGenerateExecutor>();
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
        public string GetFiledTypeName(DbColumnInfoModel column)
        {
            var filedTypeName = DbInfoProvider.GetFiledTypeName(column);
            if (!string.IsNullOrWhiteSpace(filedTypeName) &&
                column.Nullable &&
                !"string".Equals(filedTypeName, StringComparison.CurrentCultureIgnoreCase) &&
                !filedTypeName.Trim().EndsWith("?"))
            {
                return $"{filedTypeName}?";
            }

            return filedTypeName;
        }

        public string GetFiledTypeName(PropertyModel property)
        {
            var filedTypeName = string.Empty;
            bool isPropertyTypeConverted = false;
            for (var i = _propertyTypeConverters.Count - 1; i >= 0; i--)
            {
                if (_propertyTypeConverters[i].CanConvert(property))
                {
                    filedTypeName = _propertyTypeConverters[i].Convert(property);
                    isPropertyTypeConverted = true;
                    break;
                }
            }

            if (!isPropertyTypeConverted || string.IsNullOrWhiteSpace(filedTypeName))
            {
                filedTypeName = DbInfoProvider.GetFiledTypeName(property.DbColumnInfo);
            }

            if (!string.IsNullOrWhiteSpace(filedTypeName) &&
                property.DbColumnInfo.Nullable &&
                !"string".Equals(filedTypeName, StringComparison.CurrentCultureIgnoreCase) &&
                !filedTypeName.Trim().EndsWith("?"))
            {
                return $"{filedTypeName}?";
            }

            return filedTypeName;
        }

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

            codeGenerateInfo.OutPutRootPath = string.IsNullOrEmpty(codeGenerateInfo.OutPutRootPath)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output")
                : codeGenerateInfo.OutPutRootPath;

            var classModels = new List<ClassModel>();

            ExecTableCodeGenerate(codeGenerateInfo, classModels);
            ExecDbCodeGenerate(codeGenerateInfo, classModels);
        }

        /// <summary>
        /// 执行代码创建 针对每个库进行生成
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        private void ExecDbCodeGenerate(CodeGenerateInfo codeGenerateInfo, [NotNull] ICollection<ClassModel> classModels)
        {
            if (!_dbCodeGenerateExecutors.Any())
            {
                return;
            }

            var context = new CodeGenerateDbContext()
            {
                DbInfoProvider = this,
                CodeConverter = CodeConverter,
                CodeGenerateDbName = codeGenerateInfo.DbName,
                OutPutRootPath = Path.Combine(codeGenerateInfo.OutPutRootPath, codeGenerateInfo.DbName),
            };

            if (classModels == null || (classModels.Count == 0 && codeGenerateInfo.CodeGenerateTableInfos.Any()))
            {
                foreach (var tableInfo in codeGenerateInfo.CodeGenerateTableInfos)
                {
                    classModels.Add(GetClassModel(context, tableInfo.TableName));
                }
            }

            context.ClassInfo = classModels;

            foreach (var codeGenerateExecutor in _dbCodeGenerateExecutors)
            {
                codeGenerateExecutor.ExecCodeGenerate(context);
            }
        }

        /// <summary>
        /// 执行代码创建 针对每张表进行生成
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        private void ExecTableCodeGenerate(CodeGenerateInfo codeGenerateInfo, [NotNull] ICollection<ClassModel> classModels)
        {
            if (!_tableCodeGenerateExecutors.Any())
            {
                return;
            }

            foreach (var tableInfo in codeGenerateInfo.CodeGenerateTableInfos)
            {
                var context = new CodeGenerateTableContext()
                {
                    DbInfoProvider = this,
                    CodeConverter = CodeConverter,
                    CodeGenerateDbName = codeGenerateInfo.DbName,
                    CodeGenerateTableInfo = tableInfo,
                    OutPutRootPath = Path.Combine(codeGenerateInfo.OutPutRootPath, codeGenerateInfo.DbName),
                };
                context.ClassInfo = GetClassModel(context, tableInfo.TableName);
                classModels.Add(context.ClassInfo);
                foreach (var codeGenerateExecutor in _tableCodeGenerateExecutors)
                {
                    codeGenerateExecutor.ExecCodeGenerate(context);
                }
            }
        }

        private ClassModel GetClassModel(CodeGenerateContext context, string tableName)
        {
            var dbTableInfo = GetTableInfoWithColumns(context.CodeGenerateDbName, tableName);
            return new ClassModel()
            {
                DbTableInfo = dbTableInfo,
                NameSpace = context.GetNameSpace(),
                ClassName = context.GetClassName(dbTableInfo),
                ClassFileName = context.GetClassFileName(dbTableInfo),
                Annotation = dbTableInfo.Commont,
                Properties = dbTableInfo.Columns.Select(x =>
                {
                    var propertyModel = new PropertyModel()
                    {
                        DbColumnInfo = x,
                        //PropertyTypeName = GetFiledTypeName(x),
                        Annotation = x.Commont,
                        PropertyName = Convert(x.ColumnName, CodeType.PerportyName),
                    };
                    propertyModel.PropertyTypeName = GetFiledTypeName(propertyModel);
                    return propertyModel;
                }),
            };
        }

        public void AddCodeGenerateExecutor(Assembly assembly)
        {
            var targetTypeDb = typeof(IDbCodeGenerateExecutor);
            var targetType = typeof(ITableCodeGenerateExecutor);
            var instances = assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetType.Name) != null)
                .Select(x => (ITableCodeGenerateExecutor)ServiceProvider.GetService(x));
            _tableCodeGenerateExecutors.AddRange(instances);

            var instanceDbs = assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetTypeDb.Name) != null)
                .Select(x => (IDbCodeGenerateExecutor)ServiceProvider.GetService(x));
            _dbCodeGenerateExecutors.AddRange(instanceDbs);
        }

        public void AddCodeGenerateExecutor(ITableCodeGenerateExecutor executor)
        {
            _tableCodeGenerateExecutors.Add(executor);
        }

        public void AddCodeGenerateExecutor(IEnumerable<ITableCodeGenerateExecutor> executors)
        {
            _tableCodeGenerateExecutors.AddRange(executors);
        }

        public void AddCodeGenerateExecutor(IDbCodeGenerateExecutor executor)
        {
            _dbCodeGenerateExecutors.Add(executor);
        }

        public void AddCodeGenerateExecutor(IEnumerable<IDbCodeGenerateExecutor> executors)
        {
            _dbCodeGenerateExecutors.AddRange(executors);
        }

        #region AddPropertyTypeConverter

        public void AddPropertyTypeConverter(Assembly assembly)
        {
            var targetType = typeof(IPropertyTypeConverter);
            var instances = assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetType.Name) != null)
                .Select(x => (IPropertyTypeConverter)ServiceProvider.GetService(x));
            _propertyTypeConverters.AddRange(instances);
        }

        public void AddPropertyTypeConverter(IPropertyTypeConverter converter)
        {
            _propertyTypeConverters.Add(converter);
        }

        public void AddPropertyTypeConverter(IEnumerable<IPropertyTypeConverter> converters)
        {
            _propertyTypeConverters.AddRange(converters);
        }

        #endregion
    }
}

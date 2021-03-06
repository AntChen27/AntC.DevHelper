using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using Microsoft.Extensions.DependencyInjection;

namespace AntC.CodeGenerate
{
    public class CodeGeneratorManager : ICodeGeneratorManager
    {
        public IDbInfoProvider DbInfoProvider { get; set; }

        public ICodeConverter CodeConverter { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        private Type _codeWriterType;

        /// <summary>
        /// 执行器 -- 库
        /// </summary>
        private readonly List<IDbCodeGenerator> _dbCodeGenerators;
        /// <summary>
        /// 执行器 -- 表
        /// </summary>
        private readonly List<ITableCodeGenerator> _tableCodeGenerators;

        protected List<IPropertyTypeConverter> _propertyTypeConverters = new List<IPropertyTypeConverter>();

        public string DbConnectionString
        {
            get => DbInfoProvider.DbConnectionString;
            set => DbInfoProvider.DbConnectionString = value;
        }

        public CodeGeneratorManager(IDbInfoProvider dbInfoProvider)
        {
            DbInfoProvider = dbInfoProvider;
            _tableCodeGenerators = new List<ITableCodeGenerator>();
            _dbCodeGenerators = new List<IDbCodeGenerator>();
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
            if (this._codeWriterType == null)
            {
                throw new Exception($"please call method {nameof(SetCodeWriterType)}() first.");
            }

            codeGenerateInfo.OutPutRootPath = string.IsNullOrEmpty(codeGenerateInfo.OutPutRootPath)
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output")
                : codeGenerateInfo.OutPutRootPath;

            var classModels = new List<ClassModel>();

            ExecTableCodeGenerate(codeGenerateInfo, classModels);
            ExecDbCodeGenerate(codeGenerateInfo, classModels);
        }

        public void SetCodeWriterType<T>() where T : ICodeWriter
        {
            this._codeWriterType = typeof(T);
        }

        /// <summary>
        /// 执行代码创建 针对每个库进行生成
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        private void ExecDbCodeGenerate(CodeGenerateInfo codeGenerateInfo, [NotNull] ICollection<ClassModel> classModels)
        {
            if (!_dbCodeGenerators.Any())
            {
                return;
            }

            using var context = new DbCodeGenerateContext()
            {
                DbInfoProvider = this,
                CodeGeneratorContainer = this,
                CodeConverter = CodeConverter,
                CodeGenerateDbName = codeGenerateInfo.DbName,
                OutPutRootPath = codeGenerateInfo.OutPutRootPath,
            };

            if (classModels == null || (classModels.Count == 0 && codeGenerateInfo.CodeGenerateTableInfos.Any()))
            {
                foreach (var tableInfo in codeGenerateInfo.CodeGenerateTableInfos)
                {
                    classModels.Add(GetClassModel(context, tableInfo));
                }
            }

            context.ClassInfo = classModels;

            foreach (var codeGenerateExecutor in _dbCodeGenerators)
            {
                using var codeWriter = (ICodeWriter)CreateNewInstance(_codeWriterType);
                context.CodeWriter = codeWriter;
                codeGenerateExecutor.ExecCodeGenerate(context);
            }
        }

        /// <summary>
        /// 执行代码创建 针对每张表进行生成
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        private void ExecTableCodeGenerate(CodeGenerateInfo codeGenerateInfo, [NotNull] ICollection<ClassModel> classModels)
        {
            if (!_tableCodeGenerators.Any())
            {
                return;
            }

            foreach (var tableInfo in codeGenerateInfo.CodeGenerateTableInfos)
            {
                using var context = new TableCodeGenerateContext()
                {
                    DbInfoProvider = this,
                    CodeGeneratorContainer = this,
                    CodeConverter = CodeConverter,
                    CodeGenerateDbName = codeGenerateInfo.DbName,
                    CodeGenerateTableInfo = tableInfo,
                    OutPutRootPath = codeGenerateInfo.OutPutRootPath,
                };
                context.ClassInfo = GetClassModel(context, tableInfo);
                classModels.Add(context.ClassInfo);
                foreach (var codeGenerateExecutor in _tableCodeGenerators)
                {
                    using var codeWriter = (ICodeWriter)CreateNewInstance(_codeWriterType);
                    context.CodeWriter = codeWriter;
                    codeGenerateExecutor.ExecCodeGenerate(context);
                }
            }
        }

        private object CreateNewInstance(Type type)
        {
            return ServiceProvider.GetService(type);
            //var typeConstructor = type.GetConstructors().OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();
            //var parameterInfos = typeConstructor.GetParameters().Select(x => ServiceProvider.GetService(x.ParameterType)).ToArray();
            //return Activator.CreateInstance(type, parameterInfos);
        }

        private ClassModel GetClassModel(CodeGenerateContext context, CodeGenerateTableInfo tableInfo)
        {
            var dbTableInfo = GetTableInfoWithColumns(context.CodeGenerateDbName, tableInfo.TableName);
            return new ClassModel()
            {
                DbTableInfo = dbTableInfo,
                NameSpace = context.GetNameSpace(),
                ClassName = context.GetClassName(dbTableInfo),
                ClassFileName = context.GetClassFileName(dbTableInfo),
                Annotation = dbTableInfo.Commont,
                GroupName = tableInfo.GroupName,
                Properties = dbTableInfo.Columns.Select(x =>
                {
                    var propertyModel = new PropertyModel()
                    {
                        DbColumnInfo = x,
                        //PropertyTypeName = GetFiledTypeName(x),
                        Annotation = x.Commont,
                        PropertyName = Convert(x.ColumnName, CodeType.PropertyName),
                    };
                    propertyModel.PropertyTypeName = GetFiledTypeName(propertyModel);
                    return propertyModel;
                }),
            };
        }

        #region AddCodeGenerator

        public void AddCodeGenerator(Assembly assembly)
        {
            var targetTypeDb = typeof(IDbCodeGenerator);
            var targetType = typeof(ITableCodeGenerator);
            var instances = assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetType.Name) != null)
                .Select(x => (ITableCodeGenerator)ServiceProvider.GetService(x));
            _tableCodeGenerators.AddRange(instances);

            var instanceDbs = assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetTypeDb.Name) != null)
                .Select(x => (IDbCodeGenerator)ServiceProvider.GetService(x));
            _dbCodeGenerators.AddRange(instanceDbs);
        }

        public void AddCodeGenerator(ITableCodeGenerator executor)
        {
            _tableCodeGenerators.Add(executor);
        }

        public void AddCodeGenerator(IEnumerable<ITableCodeGenerator> executors)
        {
            _tableCodeGenerators.AddRange(executors);
        }

        public void AddCodeGenerator(IDbCodeGenerator executor)
        {
            _dbCodeGenerators.Add(executor);
        }

        public void AddCodeGenerator(IEnumerable<IDbCodeGenerator> executors)
        {
            _dbCodeGenerators.AddRange(executors);
        }

        public bool ContainsCodeGenerator(Type type)
        {
            return _dbCodeGenerators.Any(x => x.GetType() == type)
               || _tableCodeGenerators.Any(x => x.GetType() == type);
        }

        #endregion

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

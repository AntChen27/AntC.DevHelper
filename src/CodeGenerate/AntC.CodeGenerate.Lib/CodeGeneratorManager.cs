using System;
using System.Collections.Generic;
using System.IO;
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
        protected List<IPropertyTypeConverter> _propertyTypeConverters = new List<IPropertyTypeConverter>();

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

            if (!isPropertyTypeConverted)
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

            foreach (var tableInfo in codeGenerateInfo.CodeGenerateTableInfos)
            {
                var context = new CodeGenerateContext()
                {
                    DbInfoProvider = this,
                    CodeConverter = CodeConverter,
                    CodeGenerateTableInfo = tableInfo,
                    OutPutRootPath = string.IsNullOrEmpty(codeGenerateInfo.OutPutRootPath) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output") : codeGenerateInfo.OutPutRootPath,
                };
                context.ClassInfo = GetClassModel(context);
                foreach (var codeGenerateExecutor in _executors)
                {
                    codeGenerateExecutor.ExecCodeGenerate(context);
                }
            }
        }

        private ClassModel GetClassModel(CodeGenerateContext context)
        {
            var dbTableInfo = GetTableInfoWithColumns(context.CodeGenerateTableInfo.DbName, context.CodeGenerateTableInfo.TableName);
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

        public void AddCodeGenerateExecutor(ICodeGenerateExecutor executor)
        {
            _executors.Add(executor);
        }

        public void AddCodeGenerateExecutor(IEnumerable<ICodeGenerateExecutor> executors)
        {
            _executors.AddRange(executors);
        }

        public void AddPropertyTypeConverter(IPropertyTypeConverter converter)
        {
            _propertyTypeConverters.Add(converter);
        }

        public void AddPropertyTypeConverter(IEnumerable<IPropertyTypeConverter> converters)
        {
            _propertyTypeConverters.AddRange(converters);
        }
    }
}

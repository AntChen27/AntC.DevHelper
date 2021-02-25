using System;
using System.Collections.Generic;
using System.Reflection;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码创建器管理
    /// </summary>
    public interface ICodeGeneratorManager : IDbInfoProvider, ICodeConverter
    {
        IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 执行代码创建
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        void ExecCodeGenerate(CodeGenerateInfo codeGenerateInfo);

        #region AddCodeGenerator

        /// <summary>
        /// 添加程序集中所有的代码创建器
        /// </summary>
        /// <param name="assembly"></param>
        void AddCodeGenerator(Assembly assembly);

        /// <summary>
        /// 添加代码创建器
        /// </summary>
        /// <param name="executor"></param>
        void AddCodeGenerator(ITableCodeGenerator executor);

        /// <summary>
        /// 添加代码创建器
        /// </summary>
        /// <param name="executors"></param>
        void AddCodeGenerator(IEnumerable<ITableCodeGenerator> executors);
        
        /// <summary>
        /// 添加代码创建器
        /// </summary>
        /// <param name="executor"></param>
        void AddCodeGenerator(IDbCodeGenerator executor);

        /// <summary>
        /// 添加代码创建器
        /// </summary>
        /// <param name="executors"></param>
        void AddCodeGenerator(IEnumerable<IDbCodeGenerator> executors);

        #endregion

        #region AddPropertyTypeConverter

        /// <summary>
        /// 添加程序集中所有的字段类型转换器
        /// </summary>
        /// <param name="assembly"></param>
        void AddPropertyTypeConverter(Assembly assembly);

        /// <summary>
        /// 添加字段类型转换器
        /// </summary>
        /// <param name="converter"></param>
        void AddPropertyTypeConverter(IPropertyTypeConverter converter);

        /// <summary>
        /// 添加字段类型转换器
        /// </summary>
        /// <param name="converters"></param>
        void AddPropertyTypeConverter(IEnumerable<IPropertyTypeConverter> converters);
        
        #endregion
    }
}

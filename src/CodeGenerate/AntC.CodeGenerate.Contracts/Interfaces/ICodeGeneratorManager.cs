using System;
using System.Collections.Generic;
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

        /// <summary>
        /// 添加代码创建器
        /// </summary>
        /// <param name="executor"></param>
        void AddCodeGenerateExecutor(ICodeGenerateExecutor executor);

        /// <summary>
        /// 添加代码创建器
        /// </summary>
        /// <param name="executors"></param>
        void AddCodeGenerateExecutor(IEnumerable<ICodeGenerateExecutor> executors);
    }
}

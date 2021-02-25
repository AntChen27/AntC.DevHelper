using System;
using System.Collections.Generic;
using System.Reflection;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码创建器管理
    /// </summary>
    public interface ICodeGeneratorManager : IDbInfoProvider, ICodeConverter, ICodeGeneratorContainer, ICodeConverterContainer
    {
        IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// 执行代码创建
        /// </summary>
        /// <param name="codeGenerateInfo"></param>
        void ExecCodeGenerate(CodeGenerateInfo codeGenerateInfo);
    }
}

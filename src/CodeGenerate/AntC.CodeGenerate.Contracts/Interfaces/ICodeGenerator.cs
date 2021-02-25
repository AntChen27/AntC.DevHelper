using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    public interface ICodeGenerator<TContext>
    where TContext : CodeGenerateContext
    {
        /// <summary>
        /// 代码创建器信息
        /// </summary>
        GeneratorInfo GeneratorInfo { get; }

        /// <summary>
        /// 代码创建器配置
        /// </summary>
        GeneratorConfig GeneratorConfig { get; }

        /// <summary>
        /// 执行代码生成
        /// </summary>
        /// <param name="context"></param>
        /// <returns>生成的代码内容</returns>
        void ExecCodeGenerate(TContext context);
    }

    /// <summary>
    /// 代码生成执行器 - 针对数据库表的
    /// </summary>
    public interface ITableCodeGenerator : ICodeGenerator<CodeGenerateTableContext>
    {
    }

    /// <summary>
    /// 代码生成执行器 - 针对数据库的
    /// </summary>
    public interface IDbCodeGenerator : ICodeGenerator<CodeGenerateDbContext>
    {
    }
}

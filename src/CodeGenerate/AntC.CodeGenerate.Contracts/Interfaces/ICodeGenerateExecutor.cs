using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    public interface ICodeGenerateExecutor<TContext>
    where TContext : CodeGenerateContext
    {
        /// <summary>
        /// 执行器信息
        /// </summary>
        ExecutorInfo ExecutorInfo { get; }

        /// <summary>
        /// 执行代码生成
        /// </summary>
        /// <param name="context"></param>
        void ExecCodeGenerate(TContext context);
    }

    /// <summary>
    /// 代码生成执行器 - 针对数据库表的
    /// </summary>
    public interface ITableCodeGenerateExecutor : ICodeGenerateExecutor<CodeGenerateTableContext>
    {
    }

    /// <summary>
    /// 代码生成执行器 - 针对数据库的
    /// </summary>
    public interface IDbCodeGenerateExecutor : ICodeGenerateExecutor<CodeGenerateDbContext>
    {
    }
}

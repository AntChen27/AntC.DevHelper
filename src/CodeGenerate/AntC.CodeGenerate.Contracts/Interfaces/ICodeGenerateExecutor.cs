using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码生成执行器
    /// </summary>
    public interface ICodeGenerateExecutor
    {
        /// <summary>
        /// 执行器信息
        /// </summary>
        ExecutorInfo ExecutorInfo { get; }

        /// <summary>
        /// 执行代码生成
        /// </summary>
        /// <param name="context"></param>
        void ExecCodeGenerate(CodeGenerateContext context);
    }
}

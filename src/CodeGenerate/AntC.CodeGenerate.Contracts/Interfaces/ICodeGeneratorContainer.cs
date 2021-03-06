using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码创建器容器
    /// </summary>
    public interface ICodeGeneratorContainer : IReadOnlyCodeGeneratorContainer
    {
        #region CodeGenerator

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
    }

    public interface IReadOnlyCodeGeneratorContainer
    {
        /// <summary>
        /// 是否包含指定的代码创建器
        /// </summary>
        /// <returns></returns>
        bool ContainsCodeGenerator(Type type);
    }
}

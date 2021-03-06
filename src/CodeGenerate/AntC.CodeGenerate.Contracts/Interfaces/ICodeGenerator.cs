using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    public interface ICodeGenerator<in TContext>
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
    public interface ITableCodeGenerator : ICodeGenerator<TableCodeGenerateContext>
    {
    }

    /// <summary>
    /// 代码生成执行器 - 针对数据库的
    /// </summary>
    public interface IDbCodeGenerator : ICodeGenerator<DbCodeGenerateContext>
    {
    }
}

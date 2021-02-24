using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate
{
    /// <summary>
    /// 代码创建器上下文
    /// </summary>
    public class CodeGenerateContext
    {
        /// <summary>
        /// 输出根路径
        /// </summary>
        public string OutPutRootPath { get; set; }

        /// <summary>
        /// 数据库信息查询
        /// </summary>
        public IDbInfoProvider DbInfoProvider { get; set; }

        /// <summary>
        /// 代码命名转换器
        /// </summary>
        public ICodeConverter CodeConverter { get; set; }

        /// <summary>
        /// 要创建的表信息
        /// </summary>
        public CodeGenerateTableInfo CodeGenerateTableInfo { get; set; }

        /// <summary>
        /// 类表信息
        /// </summary>
        public ClassModel ClassInfo { get; set; }
    }
}

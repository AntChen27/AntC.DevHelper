using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码输出器
    /// </summary>
    public interface ICodeWriter : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        void Append(string content);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        void AppendLine(string content = null);
    }

    /// <summary>
    /// 代码文件输出器
    /// </summary>
    public interface ICodeFileWriter : ICodeWriter
    {
        /// <summary>
        /// 保存文件相对路径 例如code/OrderSystem/Order.cs
        /// </summary>
        string FilePath { get; set; }
    }
}

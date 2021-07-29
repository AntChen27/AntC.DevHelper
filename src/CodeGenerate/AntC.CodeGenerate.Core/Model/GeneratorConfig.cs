using System.Text;

namespace AntC.CodeGenerate.Core.Model
{
    /// <summary>
    /// 执行器配置
    /// </summary>
    public class GeneratorConfig
    {
        /// <summary>
        /// 文件保存相对路径
        /// </summary>
        public string FileRelativePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件保存编码
        /// </summary>
        public Encoding FileEncoding { get; set; } = Encoding.UTF8;
    }
}

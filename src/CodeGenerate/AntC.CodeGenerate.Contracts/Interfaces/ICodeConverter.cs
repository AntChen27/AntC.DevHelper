namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码命名转换器
    /// </summary>
    public interface ICodeConverter
    {
        /// <summary>
        /// 转换为对应的命名内容
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="type">转换类型</param>
        /// <returns></returns>
        string Convert(string value, CodeType type = CodeType.ClassName);
    }
}

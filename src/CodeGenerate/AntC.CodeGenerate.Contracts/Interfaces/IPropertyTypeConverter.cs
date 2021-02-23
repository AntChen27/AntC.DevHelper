using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 属性类型转换器
    /// </summary>
    public interface IPropertyTypeConverter
    {
        /// <summary>
        /// 是否可以转换
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        bool CanConvert(DbColumnInfoModel columnInfo);

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        string Convert(DbColumnInfoModel columnInfo);
    }
}

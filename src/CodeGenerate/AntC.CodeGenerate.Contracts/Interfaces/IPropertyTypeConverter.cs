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
        /// <param name="property"></param>
        /// <returns></returns>
        bool CanConvert(PropertyModel property);

        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        string Convert(PropertyModel property);
    }
}

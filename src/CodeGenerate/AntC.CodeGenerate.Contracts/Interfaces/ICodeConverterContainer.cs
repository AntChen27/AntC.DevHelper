using System.Collections.Generic;
using System.Reflection;

namespace AntC.CodeGenerate.Interfaces
{
    /// <summary>
    /// 代码命名转换器 容器
    /// </summary>
    public interface ICodeConverterContainer
    {
        #region AddPropertyTypeConverter

        /// <summary>
        /// 添加程序集中所有的字段类型转换器
        /// </summary>
        /// <param name="assembly"></param>
        void AddPropertyTypeConverter(Assembly assembly);

        /// <summary>
        /// 添加字段类型转换器
        /// </summary>
        /// <param name="converter"></param>
        void AddPropertyTypeConverter(IPropertyTypeConverter converter);

        /// <summary>
        /// 添加字段类型转换器
        /// </summary>
        /// <param name="converters"></param>
        void AddPropertyTypeConverter(IEnumerable<IPropertyTypeConverter> converters);

        #endregion
    }
}

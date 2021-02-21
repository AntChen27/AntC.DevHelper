using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.DevHelper.CodeGenerate
{
    /// <summary>
    /// 代码类型
    /// </summary>
    public enum CodeType
    {
        /// <summary>
        /// 类文件名
        /// </summary>
        ClassFileName,
        /// <summary>
        /// 命名空间
        /// </summary>
        Namespace,
        /// <summary>
        /// 类名
        /// </summary>
        ClassName,
        /// <summary>
        /// 属性名
        /// </summary>
        PerportyName,
        /// <summary>
        /// 字段名
        /// </summary>
        FieldName,
        /// <summary>
        /// 方法名
        /// </summary>
        MethodName,
    }
}

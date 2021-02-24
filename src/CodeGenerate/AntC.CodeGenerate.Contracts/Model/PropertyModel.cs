using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    public class PropertyModel
    {
        public DbColumnInfoModel DbColumnInfo { get; set; }

        /// <summary>
        /// 属性类型名
        /// </summary>
        public string PropertyTypeName { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Annotation { get; set; }
    }
}

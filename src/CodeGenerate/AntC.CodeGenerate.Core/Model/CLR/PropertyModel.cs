using AntC.CodeGenerate.Core.Model.Db;

namespace AntC.CodeGenerate.Core.Model.CLR
{
    public class PropertyModel
    {
        public ColumnInfo DbColumnInfo { get; set; }

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

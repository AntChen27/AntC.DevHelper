using AntC.CodeGenerate.Core.Model.Db;
using System.Collections.Generic;

namespace AntC.CodeGenerate.Core.Model.CLR
{
    public class ClassModel
    {
        public TableInfo TableInfo { get; set; }

        public IEnumerable<PropertyModel> Properties { get; set; }

        /// <summary>
        /// 类注释
        /// </summary>
        public string Annotation { get; set; }

        /// <summary>
        /// 类名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 输出文件名
        /// </summary>
        public string OutPutFileName { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
    }
}

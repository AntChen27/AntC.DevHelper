using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    public class ClassModel
    {
        public DbTableInfoModel DbTableInfo { get; set; }

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
        /// 类文件
        /// </summary>
        public string ClassFileName { get; set; }

        /// <summary>
        /// 命名空间名称
        /// </summary>
        public string NameSpace { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 代码创建器信息
    /// </summary>
    public class CodeGenerateInfo
    {
        /// <summary>
        /// 输出根路径
        /// </summary>
        public string OutPutRootPath { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 要创建的表信息
        /// </summary>
        public IEnumerable<CodeGenerateTableInfo> CodeGenerateTableInfos { get; set; }
    }

    /// <summary>
    /// 代码创建器 表信息
    /// </summary>
    public class CodeGenerateTableInfo
    {
        /// <summary>
        /// 注释
        /// </summary>
        public string Commont { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
    }
}

using System.Collections.Generic;
using System.Diagnostics;

namespace AntC.CodeGenerate.Core.Model.Db
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    [DebuggerDisplay("DbName={DbName}")]
    public class DatabaseInfo
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 表信息
        /// </summary>
        public IEnumerable<TableInfo> Tables { get; set; }

        public override string ToString()
        {
            return DbName;
        }
    }
}

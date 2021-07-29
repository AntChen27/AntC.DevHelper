using System.Collections.Generic;
using System.Diagnostics;

namespace AntC.CodeGenerate.Core.Model.Db
{
    /// <summary>
    /// 数据库表信息
    /// </summary>
    [DebuggerDisplay("TableName={TableName} Commont={Commont} Db={DatabaseInfo}")]
    public class TableInfo
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Commont { get; set; }

        /// <summary>
        /// 数据库信息
        /// </summary>
        public DatabaseInfo DatabaseInfo { get; set; }

        public IEnumerable<ColumnInfo> Columns { get; set; }

        public override string ToString()
        {
            return TableName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库
    /// </summary>
    [DebuggerDisplay("TableName={TableName} Commont={Commont} Db={DbInfo}")]
    public class DbTableInfoModel
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
        public DbInfoModel DbInfo { get; set; }

        public IEnumerable<DbColumnInfoModel> Columns { get; set; }

        public override string ToString()
        {
            return TableName;
        }
    }
}

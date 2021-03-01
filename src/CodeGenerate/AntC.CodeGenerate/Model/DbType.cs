using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// Mysql
        /// </summary>
        [Description("Mysql")]
        Mysql = 1,

        /// <summary>
        /// SqlServer
        /// </summary>
        [Description("SqlServer")]
        SqlServer = 2,

        /// <summary>
        /// Oracle
        /// </summary>
        [Description("Oracle")]
        Oracle = 3,
    }
}

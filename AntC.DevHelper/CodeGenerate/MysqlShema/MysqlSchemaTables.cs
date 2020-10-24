using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.MysqlShema
{
    [SugarTable("tables")]
    public class MysqlSchemaTables
    {
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_COMMENT { get; set; }
    }
}

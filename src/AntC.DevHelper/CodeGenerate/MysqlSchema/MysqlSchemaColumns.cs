using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.MysqlShema
{
    [SugarTable("columns")]
    public class MysqlSchemaColumns
    {
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string COLUMN_NAME { get; set; }
        public string IS_NULLABLE { get; set; }
        public string DATA_TYPE { get; set; }
        public long CHARACTER_MAXIMUM_LENGTH { get; set; }
        public long NUMERIC_PRECISION { get; set; }
        public long NUMERIC_SCALE { get; set; }
        public string COLUMN_TYPE { get; set; }
        public string COLUMN_KEY { get; set; }
        public string EXTRA { get; set; }
        public string COLUMN_COMMENT { get; set; }
    }
}

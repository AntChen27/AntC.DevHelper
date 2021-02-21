using SqlSugar;

namespace AntC.CodeGenerate.Mysql
{
    [SugarTable("tables")]
    internal class MysqlSchemaTable
    {
        public string TABLE_SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }
        public string TABLE_COMMENT { get; set; }
    }
}

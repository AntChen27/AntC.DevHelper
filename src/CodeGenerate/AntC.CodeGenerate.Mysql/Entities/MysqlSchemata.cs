using SqlSugar;

namespace AntC.CodeGenerate.Mysql.Entities
{
    [SugarTable("SCHEMATA")]
    internal class MysqlSchemata
    {
        public string SCHEMA_NAME { get; set; }
    }
}

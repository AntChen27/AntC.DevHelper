using SqlSugar;

namespace AntC.CodeGenerate.Mysql
{
    [SugarTable("SCHEMATA")]
    internal class MysqlSchemata
    {
        public string SCHEMA_NAME { get; set; }
    }
}

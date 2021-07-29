using AntC.CodeGenerate.Core.Model.Db;

namespace AntC.CodeGenerate.Mysql.Model
{
    public class MysqlColumnInfo : ColumnInfo
    {
        /// <summary>
        /// 字符集名称
        /// </summary>
        public string CharacterSetName { get; set; }

        /// <summary>
        /// 字符集排序名称
        /// </summary>
        public string CollationName { get; set; }
    }
}

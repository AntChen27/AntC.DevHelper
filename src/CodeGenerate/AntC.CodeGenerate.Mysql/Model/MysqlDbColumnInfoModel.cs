using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Mysql.Model
{
    public class MysqlDbColumnInfoModel : DbColumnInfoModel
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

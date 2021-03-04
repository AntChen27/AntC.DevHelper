namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库表分组信息
    /// </summary>
    public class TableGroupInfo
    {
        public string TableName { get; set; }
        public string GroupName { get; set; }
        public override string ToString()
        {
            return $"[{GroupName}]{TableName}";
        }
    }
}

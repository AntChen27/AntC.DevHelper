using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Converts;
using Newtonsoft.Json;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库表分组信息
    /// </summary>
    [JsonConverter(typeof(TableGroupInfoConvert))]
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

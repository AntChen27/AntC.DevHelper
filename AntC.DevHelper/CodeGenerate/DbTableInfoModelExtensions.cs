using AntC.DevHelper.CodeGenerate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntC.DevHelper.CodeGenerate
{
    public static class DbTableInfoModelExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbTableInfo"></param>
        /// <returns></returns>
        public static string ToClassContentString(this DbTableInfoModel dbTableInfo, string nameSpace, ICodeConverter codeConverter)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.ComponentModel.DataAnnotations; ");
            sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema; ");
            sb.AppendLine("");
            sb.AppendLine($"namespace {nameSpace}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {(string.IsNullOrWhiteSpace(dbTableInfo.Commont) ? dbTableInfo.TableName : dbTableInfo.Commont)}");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    [Table(\"{dbTableInfo.TableName}\")]");
            sb.AppendLine($"    public partial class {codeConverter.Convert(CodeType.ClassName, dbTableInfo.TableName)}");
            sb.AppendLine("    {");

            if (dbTableInfo.Columns != null && dbTableInfo.Columns.Count() > 0)
            {
                int i = 0;
                foreach (var col in dbTableInfo.Columns)
                {
                    if (i != 0)
                    {
                        sb.AppendLine("        ");
                    }
                    sb.Append(ToClassContentString(col, codeConverter));
                    i++;
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static string ToClassContentString(this DbColumnInfoModel dbColumnInfo, ICodeConverter codeConverter)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {(string.IsNullOrWhiteSpace(dbColumnInfo.Commont) ? dbColumnInfo.ColumnName : dbColumnInfo.Commont)}");
            sb.AppendLine($"        /// <summary>");
            if (dbColumnInfo.Key)
            {
                sb.AppendLine("        [Key]");
            }
            sb.AppendLine($"        [Column(\"{dbColumnInfo.ColumnName}\", TypeName = \"{(dbColumnInfo.DataTypeName)}\")]");
            sb.AppendLine($"        public {dbColumnInfo.GetFeildTypeName()}{(dbColumnInfo.Nullable && !new string[] { "string" }.Contains(dbColumnInfo.GetFeildTypeName()) ? "?" : "")} {codeConverter.Convert(CodeType.PerportyName, dbColumnInfo.ColumnName)} {{ get; set; }}");

            //[Key]
            //[Column("Permit_Id", TypeName = "varchar(36)")]
            //public string PermitId { get; set; }
            //[Required]
            //[Column("Organ_Id", TypeName = "varchar(36)")]
            //public string OrganId { get; set; }

            return sb.ToString();
        }
    }
}

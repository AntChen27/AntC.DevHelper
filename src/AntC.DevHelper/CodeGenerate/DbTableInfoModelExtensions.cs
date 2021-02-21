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
        /// <param name="nameSpace"></param>
        /// <param name="codeConverter"></param>
        /// <returns></returns>
        public static string ToClassContentString(this DbTableInfoModel dbTableInfo, string nameSpace, ICodeConverter codeConverter, OrmFramework orm)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            if (orm.HasFlag(OrmFramework.EntityFramework))
            {
                sb.AppendLine("using System.ComponentModel.DataAnnotations;");
                sb.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
            }
            if (orm.HasFlag(OrmFramework.SqlSugar))
            {
                sb.AppendLine("using SqlSugar;");
            }
            sb.AppendLine("");
            sb.AppendLine($"namespace {nameSpace}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {(string.IsNullOrWhiteSpace(dbTableInfo.Commont) ? dbTableInfo.TableName : dbTableInfo.Commont.Replace("\r\n", "\r\n        /// "))}");
            sb.AppendLine($"    /// </summary>");
            if (orm.HasFlag(OrmFramework.EntityFramework))
            {
                sb.AppendLine($"    [Table(\"{dbTableInfo.TableName}\")]");
            }
            if (orm.HasFlag(OrmFramework.SqlSugar))
            {
                sb.AppendLine($"    [SugarTable(\"{dbTableInfo.TableName}\")]");
            }

            sb.AppendLine($"    public partial class {codeConverter.Convert(dbTableInfo.TableName, CodeType.ClassName)}");
            sb.AppendLine("    {");

            if (dbTableInfo.Columns != null && dbTableInfo.Columns.Any())
            {
                int i = 0;
                foreach (var col in dbTableInfo.Columns)
                {
                    if (i != 0)
                    {
                        sb.AppendLine("        ");
                    }
                    sb.Append(ToClassContentString(col, codeConverter, orm));
                    i++;
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbColumnInfo"></param>
        /// <param name="codeConverter"></param>
        /// <returns></returns>
        public static string ToClassContentString(this DbColumnInfoModel dbColumnInfo, ICodeConverter codeConverter, OrmFramework orm)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {(string.IsNullOrWhiteSpace(dbColumnInfo.Commont) ? dbColumnInfo.ColumnName : dbColumnInfo.Commont.Replace("\r\n", "\r\n        /// "))}");
            sb.AppendLine($"        /// </summary>");

            #region Orm Framework

            if (orm.HasFlag(OrmFramework.EntityFramework))
            {
                if (dbColumnInfo.Key)
                {
                    sb.AppendLine("        [Key]");
                }
                sb.AppendLine($"        [Column(\"{dbColumnInfo.ColumnName}\", TypeName = \"{(dbColumnInfo.DataTypeName)}\")]");
            }
            if (orm.HasFlag(OrmFramework.SqlSugar))
            {
                sb.AppendLine($"        [SugarColumn(ColumnName = \"{dbColumnInfo.ColumnName}\", ColumnDataType = \"{(dbColumnInfo.DataTypeName)}\"{(dbColumnInfo.Key ? ",IsPrimaryKey = true" : "")})]");
            }

            #endregion

            sb.AppendLine($"        public {dbColumnInfo.GetFeildTypeName()}{(dbColumnInfo.Nullable && !new string[] { "string" }.Contains(dbColumnInfo.GetFeildTypeName()) ? "?" : "")} {codeConverter.Convert(dbColumnInfo.ColumnName, CodeType.PerportyName)} {{ get; set; }}");

            return sb.ToString();
        }
    }

    [Flags]
    public enum OrmFramework
    {
        EntityFramework,
        SqlSugar
    }
}

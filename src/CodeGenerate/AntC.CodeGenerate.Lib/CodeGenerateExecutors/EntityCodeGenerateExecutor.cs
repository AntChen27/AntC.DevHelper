using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.CodeGenerateExecutors
{
    /// <summary>
    /// 实体代码生成器
    /// </summary>
    public class EntityCodeGenerateExecutor : BaseCodeGenerateExecutor
    {
        public override void ExecCodeGenerate(CodeGenerateContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");

            sb.AppendLine("");
            sb.AppendLine($"namespace {context.GetNameSpace()}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {GetClassCommont(context)}");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public partial class {context.GetClassName()}");
            sb.AppendLine("    {");

            if (context.DbTableInfoModel.Columns != null && context.DbTableInfoModel.Columns.Any())
            {
                int i = 0;
                foreach (var col in context.DbTableInfoModel.Columns)
                {
                    if (i != 0)
                    {
                        sb.AppendLine("        ");
                    }
                    sb.Append(ToClassContentString(col, context));
                    i++;
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            var result = sb.ToString();

            Output.ToFile(result, $"{context.GetClassFileName()}.cs", context.OutPutRootPath, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbColumnInfo"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private string ToClassContentString(DbColumnInfoModel dbColumnInfo, CodeGenerateContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {GetPropertyCommont(dbColumnInfo)}");
            sb.AppendLine($"        /// </summary>");

            sb.AppendLine($"        public {context.DbInfoProvider.GetFiledTypeName(dbColumnInfo)} {context.CodeConverter.Convert(dbColumnInfo.ColumnName, CodeType.PerportyName)} {{ get; set; }}");

            return sb.ToString();
        }

        public string GetPropertyCommont(DbColumnInfoModel dbColumnInfo)
        {
            return string.IsNullOrWhiteSpace(dbColumnInfo.Commont)
                ? dbColumnInfo.ColumnName
                : dbColumnInfo.Commont.Replace("\r\n", "\r\n        /// ");
        }

        public string GetClassCommont(CodeGenerateContext context)
        {
            var dbTableInfo = context.DbTableInfoModel;
            return (string.IsNullOrWhiteSpace(dbTableInfo.Commont)
                ? dbTableInfo.TableName
                : dbTableInfo.Commont.Replace("\r\n", "\r\n        /// "));
        }
    }
}

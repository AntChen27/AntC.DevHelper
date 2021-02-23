using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Extension;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra
{
    public class EntityCodeGenerateExecutor : BaseCodeGenerateExecutor
    {
        public bool UseAbpProperty { get; set; } = true;

        public override void ExecCodeGenerate(CodeGenerateContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            AppendUsingNamespace(context, sb);
            sb.AppendLine("");
            sb.AppendLine($"namespace {context.GetNameSpace()}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {GetClassCommont(context)}");
            sb.AppendLine($"    /// </summary>");
            sb.Append($"    public partial class {context.GetClassName()}");

            if (UseAbpProperty)
            {
                // 添加继承类
                var superClassName = context.GetAbpEntitySuperClass();
                sb.Append($"{(string.IsNullOrWhiteSpace(superClassName) ? string.Empty : $" : {superClassName}")}");
            }

            sb.AppendLine();
            sb.AppendLine("    {");

            if (context.DbTableInfoModel.Columns != null && context.DbTableInfoModel.Columns.Any())
            {
                var i = 0;
                foreach (var col in context.DbTableInfoModel.Columns)
                {
                    if (!UseAbpProperty || !col.IsAbpProperty())
                    {
                        if (i != 0)
                        {
                            sb.AppendLine("        ");
                        }
                        sb.Append(ToClassContentString(col, context));
                    }
                    i++;
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            var result = sb.ToString();

            Output.ToFile(result, $"{context.GetClassFileName()}.cs", context.OutPutRootPath, Encoding.UTF8);
        }

        private void AppendUsingNamespace(CodeGenerateContext context, StringBuilder builder)
        {
            if (UseAbpProperty)
            {
                builder.AppendLine($"using {context.GetAbpEntitySuperClassNamespace()};");
            }
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
                ? string.Empty
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

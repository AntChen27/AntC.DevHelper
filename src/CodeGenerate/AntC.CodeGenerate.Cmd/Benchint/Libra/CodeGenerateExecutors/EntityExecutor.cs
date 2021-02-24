using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerateExecutors
{
    public class EntityExecutor : BaseTableCodeGenerateExecutor
    {
        public bool UseAbpProperty { get; set; } = true;
        public bool UseAbpEntity { get; set; } = true;

        public override void ExecCodeGenerate(CodeGenerateTableContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            AppendUsingNamespace(context, sb);
            sb.AppendLine("");
            sb.AppendLine($"namespace {context.GetNameSpace()}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {context.ClassInfo.Annotation}");
            sb.AppendLine($"    /// </summary>");
            sb.Append($"    public partial class {context.ClassInfo.ClassName}");

            if (UseAbpEntity)
            {
                // 添加继承类
                var superClassName = context.GetAbpEntitySuperClass();
                sb.Append($"{(string.IsNullOrWhiteSpace(superClassName) ? string.Empty : $" : {superClassName}")}");
            }

            sb.AppendLine();
            sb.AppendLine("    {");

            if (context.ClassInfo.Properties != null && context.ClassInfo.Properties.Any())
            {
                var i = 0;
                foreach (var col in context.ClassInfo.Properties)
                {
                    if ((UseAbpProperty && col.DbColumnInfo.IsAbpProperty()) || (
                            UseAbpEntity && col.DbColumnInfo.Key))
                    {
                        continue;
                    }

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

            Output.ToFile(result, $"Entities\\{context.ClassInfo.ClassFileName}.cs", context.OutPutRootPath, Encoding.UTF8);
        }

        private void AppendUsingNamespace(CodeGenerateTableContext tableContext, StringBuilder builder)
        {
            if (UseAbpProperty)
            {
                builder.AppendLine($"using {tableContext.GetAbpEntitySuperClassNamespace()};");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="tableContext"></param>
        /// <returns></returns>
        private string ToClassContentString(PropertyModel property, CodeGenerateTableContext tableContext)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        /// <summary>");
            sb.AppendLine($"        /// {property.Annotation}");
            sb.AppendLine($"        /// </summary>");

            sb.AppendLine($"        public {property.PropertyTypeName} {property.PropertyName} {{ get; set; }}");

            return sb.ToString();
        }
    }
}

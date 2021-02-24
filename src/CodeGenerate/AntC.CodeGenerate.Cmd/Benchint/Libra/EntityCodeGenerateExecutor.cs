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
        public bool UseAbpEntity { get; set; } = true;

        public override void ExecCodeGenerate(CodeGenerateContext context)
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

            Output.ToFile(result, $"{context.GetClassFileName(context.ClassInfo.DbTableInfo)}.cs", context.OutPutRootPath, Encoding.UTF8);
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
        /// <param name="property"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private string ToClassContentString(PropertyModel property, CodeGenerateContext context)
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

using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Domain
{
    public class EntityGenerator : BaseTableCodeGenerator
    {
        public bool UseAbpProperty { get; set; } = true;
        public bool UseAbpEntity { get; set; } = true;

        public new GeneratorConfig GeneratorConfig = new GeneratorConfig()
        {
            FileRelativePath = "Entities",
            FileEncoding = Encoding.UTF8,
        };

        public override void ExecCodeGenerate(CodeGenerateTableContext context)
        {
            var outPutPath = Path.Combine("Domain", context.ClassInfo.GroupName ?? string.Empty, $"{context.ClassInfo.ClassFileName}.cs");
            SetRelativePath(context, outPutPath);

            context.AppendLine("using System;");
            AppendUsingNamespace(context);
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation}");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public partial class {context.ClassInfo.ClassName}");

            if (UseAbpEntity)
            {
                // 添加继承类
                var superClassName = context.GetAbpEntitySuperClass();
                context.Append($"{(string.IsNullOrWhiteSpace(superClassName) ? string.Empty : $" : {superClassName}")}");
            }

            context.AppendLine();
            context.AppendLine("    {");

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
                        context.AppendLine("        ");
                    }
                    AppendClassContentString(col, context);

                    i++;
                }
            }

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        private void AppendUsingNamespace(CodeGenerateTableContext context)
        {
            if (UseAbpProperty)
            {
                context.AppendLine($"using {context.GetAbpEntitySuperClassNamespace()};");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private void AppendClassContentString(PropertyModel property, CodeGenerateTableContext context)
        {
            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {property.Annotation}");
            context.AppendLine($"        /// </summary>");

            context.AppendLine($"        public {property.PropertyTypeName} {property.PropertyName} {{ get; set; }}");
        }
    }
}

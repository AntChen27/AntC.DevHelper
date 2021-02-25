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
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            AppendUsingNamespace(context, builder);
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.ClassInfo.Annotation}");
            builder.AppendLine($"    /// </summary>");
            builder.Append($"    public partial class {context.ClassInfo.ClassName}");

            if (UseAbpEntity)
            {
                // 添加继承类
                var superClassName = context.GetAbpEntitySuperClass();
                builder.Append($"{(string.IsNullOrWhiteSpace(superClassName) ? string.Empty : $" : {superClassName}")}");
            }

            builder.AppendLine();
            builder.AppendLine("    {");

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
                        builder.AppendLine("        ");
                    }
                    builder.Append(ToClassContentString(col, context));

                    i++;
                }
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            var outPutPath = Path.Combine("Domain", context.ClassInfo.GroupName ?? string.Empty, $"{context.ClassInfo.ClassFileName}.cs");
            Output.ToFile(result, outPutPath, context.OutPutRootPath, Encoding.UTF8);
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
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"        /// <summary>");
            builder.AppendLine($"        /// {property.Annotation}");
            builder.AppendLine($"        /// </summary>");

            builder.AppendLine($"        public {property.PropertyTypeName} {property.PropertyName} {{ get; set; }}");

            return builder.ToString();
        }
    }
}

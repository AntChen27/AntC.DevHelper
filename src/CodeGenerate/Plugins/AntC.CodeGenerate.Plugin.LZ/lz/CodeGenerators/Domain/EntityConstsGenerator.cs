using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.CodeGenerators.Domain
{
    public class EntityConstsGenerator : BaseTableCodeGenerator
    {
        public bool UseAbpProperty { get; set; } = true;
        public bool UseAbpEntity { get; set; } = true;

        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Lz.Consts.Entity",
            Desc = "此模板生成数据库实体"
        };

        protected override GeneratorConfig GetDefaultConfig(TableCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("DomainShared",
                    context.ClassInfo.GroupName ?? string.Empty,
                    $"{context.ClassInfo.ClassFileName}Consts.cs")
            };
        }

        public override void ExecutingCodeGenerate(TableCodeGenerateContext context)
        {
            context.AppendLine("using System;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 常量");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public static class {context.ClassInfo.ClassName}Consts");

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

        /// <summary>
        ///
        /// </summary>
        /// <param name="property"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private void AppendClassContentString(PropertyModel property, TableCodeGenerateContext context)
        {
            if ("string".Equals(property.PropertyTypeName, StringComparison.CurrentCultureIgnoreCase)
                && property.DbColumnInfo.DataLength <= int.MaxValue)
            {
                context.AppendLine($"        /// <summary>");
                context.AppendLine($"        /// {property.Annotation}");
                context.AppendLine($"        /// </summary>");
                context.AppendLine($"        public  const int Max{property.PropertyName}Length  = {property.DbColumnInfo.DataLength}; ");
            }
        }
    }
}
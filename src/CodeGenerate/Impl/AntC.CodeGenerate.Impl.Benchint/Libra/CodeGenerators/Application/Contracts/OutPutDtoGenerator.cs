using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application.Contracts
{
    public class OutPutDtoGenerator : BaseTableCodeGenerator
    {
        public bool UseAbpProperty { get; set; } = true;
        public bool UseAbpDto { get; set; } = true;
        public bool EnableAttribute { get; set; } = true;

        public override void PreExecCodeGenerate(CodeGenerateTableContext context)
        {
            var outPutPath = Path.Combine("Application.Contracts",
                context.ClassInfo.GroupName ?? string.Empty,
                "Dto",
                context.ClassInfo.ClassName,
                $"{context.ClassInfo.ClassName}Dto.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateTableContext context)
        {
            context.AppendLine("using System.ComponentModel.DataAnnotations;");
            context.AppendLine("using Volo.Abp.Application.Dtos;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 数据传输对象");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public class {context.ClassInfo.ClassName}Dto : EntityDto");

            if (context.ClassInfo.Properties != null)
            {
                var key = context.ClassInfo.Properties.FirstOrDefault(x => x.DbColumnInfo.Key);
                if (key != null)
                {
                    context.Append($"<{key.PropertyTypeName}>");
                }
            }

            context.AppendLine();
            // todo 需要根据情况进行实体继承类的判断
            context.AppendLine("    {");
            if (context.ClassInfo.Properties != null && context.ClassInfo.Properties.Any())
            {
                var i = 0;
                foreach (var col in context.ClassInfo.Properties)
                {
                    if ((UseAbpProperty && col.DbColumnInfo.IsAbpProperty()) || (
                            UseAbpDto && col.DbColumnInfo.Key))
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
        /// <param name="tableContext"></param>
        /// <returns></returns>
        private void AppendClassContentString(PropertyModel property, CodeGenerateTableContext context)
        {
            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {property.Annotation}");
            context.AppendLine($"        /// </summary>");

            if (EnableAttribute && !property.DbColumnInfo.Nullable)
            {
                context.AppendLine($"        [Required(ErrorMessage = \"{property.Annotation} 不能为空\")]");
            }

            if (EnableAttribute &&
                "string".Equals(property.PropertyTypeName, StringComparison.CurrentCultureIgnoreCase)
                && property.DbColumnInfo.DataLength <= int.MaxValue)
            {
                context.AppendLine($"        [StringLength({property.DbColumnInfo.DataLength}, ErrorMessage = \"{property.Annotation} 不能超过{property.DbColumnInfo.DataLength}位的长度\")]");
            }

            context.AppendLine($"        public {property.PropertyTypeName} {property.PropertyName} {{ get; set; }}");
        }
    }
}

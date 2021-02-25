using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application.Contracts
{
    public class OutPutDtoGenerator : BaseTableCodeGenerator
    {
        public bool UseAbpProperty { get; set; } = true;
        public bool UseAbpDto { get; set; } = true;
        public bool EnableAttribute { get; set; } = true;

        public override void ExecCodeGenerate(CodeGenerateTableContext context)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System.ComponentModel.DataAnnotations;");
            builder.AppendLine("using Volo.Abp.Application.Dtos;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.ClassInfo.Annotation} 数据传输对象");
            builder.AppendLine($"    /// </summary>");
            builder.AppendLine($"    public class {context.ClassInfo.ClassName}Dto : EntityDto");
            // todo 需要根据情况进行实体继承类的判断
            builder.AppendLine("    {");
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
                        builder.AppendLine("        ");
                    }
                    builder.Append(ToClassContentString(col, context));

                    i++;
                }
            }

            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            var outPutPath = Path.Combine("Application.Contracts", context.ClassInfo.GroupName ?? String.Empty, $"{context.ClassInfo.ClassName}Dto.cs");
            Output.ToFile(result, outPutPath, context.OutPutRootPath, Encoding.UTF8);
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

            if (EnableAttribute && !property.DbColumnInfo.Nullable)
            {
                builder.AppendLine($"        [Required(ErrorMessage = \"{property.Annotation} 不能为空\")]");
            }

            if (EnableAttribute && "string".Equals(property.PropertyTypeName, StringComparison.CurrentCultureIgnoreCase))
            {
                builder.AppendLine($"        [StringLength({property.DbColumnInfo.DataLength}, ErrorMessage = \"{property.Annotation} 不能超过{property.DbColumnInfo.DataLength}位的长度\")]");
            }

            builder.AppendLine($"        public {property.PropertyTypeName} {property.PropertyName} {{ get; set; }}");

            return builder.ToString();
        }
    }
}

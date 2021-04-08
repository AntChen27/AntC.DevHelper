using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.CodeGenerators.Application.Contracts
{
    public class CreateDtoGenerator : BaseTableCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Lz.Dto.Create",
            Desc = "此模板生成基于Abp的 新增 数据传输对象"
        };

        public bool UseAbpProperty { get; set; } = true;
        public bool UseAbpDto { get; set; } = true;
        public bool EnableAttribute { get; set; } = true;

        protected override GeneratorConfig GetDefaultConfig(TableCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("Application.Contracts",
                    context.ClassInfo.GroupName ?? string.Empty,
                    "Dto",
                    context.ClassInfo.ClassName,
                    $"Create{context.ClassInfo.ClassName}Dto.cs")
            };
        }

        public override void ExecutingCodeGenerate(TableCodeGenerateContext context)
        {
            context.AppendLine("using System;");
            context.AppendLine("using System.ComponentModel.DataAnnotations;");
            context.AppendLine("using Volo.Abp.Application.Dtos;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// 新增 {context.ClassInfo.Annotation} 数据传输对象");
            context.AppendLine($"    /// </summary>");
            context.AppendLine($"    public class Create{context.ClassInfo.ClassName}Dto : CreateUpdate{context.ClassInfo.ClassName}Dto");
            // todo 需要根据情况进行实体继承类的判断
            context.AppendLine("    {");
            //if (context.ClassInfo.Properties != null && context.ClassInfo.Properties.Any())
            //{
            //    var i = 0;
            //    foreach (var col in context.ClassInfo.Properties)
            //    {
            //        if ((UseAbpProperty && col.DbColumnInfo.IsAbpProperty()) || (
            //                UseAbpDto && col.DbColumnInfo.Key))
            //        {
            //            continue;
            //        }

            //        if (i != 0)
            //        {
            //            context.AppendLine("        ");
            //        }
            //        AppendClassContentString(col, context);

            //        i++;
            //    }
            //}

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
            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {property.Annotation}");
            context.AppendLine($"        /// </summary>");

            if (EnableAttribute && !property.DbColumnInfo.Nullable)
            {
                //context.AppendLine($"        [Required(ErrorMessage = \"{property.Annotation} 不能为空\")]");
                context.AppendLine($"        [Required()]");
            }

            if (EnableAttribute &&
                "string".Equals(property.PropertyTypeName, StringComparison.CurrentCultureIgnoreCase)
                && property.DbColumnInfo.DataLength <= int.MaxValue)
            {
                //context.AppendLine($"        [StringLength({property.DbColumnInfo.DataLength}, ErrorMessage = \"{property.Annotation} 不能超过{property.DbColumnInfo.DataLength}位的长度\")]");
                context.AppendLine($"        [StringLength({context.ClassInfo.ClassName}Consts.Max{property.PropertyName}Length)]");
            }
            context.AppendLine($"        public {property.PropertyTypeName} {property.PropertyName} {{ get; set; }}");
        }
    }
}
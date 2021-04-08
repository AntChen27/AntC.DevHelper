using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.CodeGenerators.Application.Contracts
{
    public class PagedAndSortedResultRequestDtoGenerator : BaseTableCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Lz.Dto.PagedAndSortedResultRequest",
            Desc = "此模板生成基于Abp的用于接口分页查询的数据传输对象"
        };

        protected override GeneratorConfig GetDefaultConfig(TableCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("Application.Contracts",
                    context.ClassInfo.GroupName ?? string.Empty,
                    "Dto",
                    context.ClassInfo.ClassName,
                    $"{context.ClassInfo.ClassName}PagedAndSortedResultRequestDto.cs")
            };
        }

        public override void ExecutingCodeGenerate(TableCodeGenerateContext context)
        {
            context.AppendLine("using System;");
            context.AppendLine("using Volo.Abp.Application.Dtos;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 分页排序数据传输对象");
            context.AppendLine($"    /// </summary>");
            context.AppendLine($"    public class {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto");
            context.AppendLine("    {");

            context.AppendLine($"        public {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto()");
            context.AppendLine("        {");

            context.AppendLine($"            if (this.Sorting.IsNullOrWhiteSpace())");
            context.AppendLine($"            {{");
            context.AppendLine($"                Sorting = \"{GetSortPropertyName(context.ClassInfo)} Asc\";");
            context.AppendLine($"            }}");

            context.AppendLine("        }");
            context.AppendLine("    }");
            context.AppendLine("}");
        }

        private string GetSortPropertyName(ClassModel classModel)
        {
            var sortNo = classModel.Properties.FirstOrDefault(x => "SortNo".Equals(x.PropertyName, StringComparison.CurrentCultureIgnoreCase));
            if (sortNo != null)
            {
                return sortNo.PropertyName;
            }

            return classModel.Properties.FirstOrDefault(x => x.DbColumnInfo.Key)?.PropertyName;
        }
    }
}

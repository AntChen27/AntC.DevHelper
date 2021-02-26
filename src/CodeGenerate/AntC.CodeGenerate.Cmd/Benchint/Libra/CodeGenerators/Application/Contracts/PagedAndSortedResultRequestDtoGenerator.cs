using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application.Contracts
{
    public class PagedAndSortedResultRequestDtoGenerator : BaseTableCodeGenerator
    {
        public override void ExecCodeGenerate(CodeGenerateTableContext context)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            builder.AppendLine("using Volo.Abp.Application.Dtos;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.ClassInfo.Annotation} 分页排序数据传输对象");
            builder.AppendLine($"    /// </summary>");
            builder.AppendLine($"    public class {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto : PagedAndSortedResultRequestDto");
            builder.AppendLine("    {");

            builder.AppendLine($"        public {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto()");
            builder.AppendLine("        {");

            builder.AppendLine($"            if (this.Sorting.IsNullOrWhiteSpace())");
            builder.AppendLine($"            {{");
            builder.AppendLine($"                Sorting = \"{GetSortPropertyName(context.ClassInfo)} Asc\";");
            builder.AppendLine($"            }}");

            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            var outPutPath = Path.Combine("Application.Contracts",
                context.ClassInfo.GroupName ?? String.Empty,
                "Dto",
                 context.ClassInfo.ClassName,
                $"{context.ClassInfo.ClassName}PagedAndSortedResultRequestDto.cs");
            Output.ToFile(result, outPutPath, context.OutPutRootPath, Encoding.UTF8);
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

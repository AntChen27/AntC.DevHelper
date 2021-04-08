using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.CodeGenerators.Application.Contracts
{
    public class AppServiceInterfaceGenerator : BaseTableCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "lz.Application.Contracts",
            Desc = "此模板生成基于Abp的ICrudAppService应用服务的契约接口"
        };

        protected override GeneratorConfig GetDefaultConfig(TableCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("Application.Contracts",
                    context.ClassInfo.GroupName ?? string.Empty,
                    $"{GetClassName(context)}.cs")
            };
        }

        public override void ExecutingCodeGenerate(TableCodeGenerateContext context)
        {
            var className = GetClassName(context);
            context.AppendLine("using Benchint.Abp.Application.Services;");
            context.AppendLine("using System;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 应用服务契约接口");
            context.AppendLine($"    /// </summary>");

            var key = context.ClassInfo.Properties?.FirstOrDefault(x => x.DbColumnInfo.Key);

            context.AppendLine($"    public interface {className} : ICrudAppService<{context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}Dto,{key?.PropertyTypeName}, PagedAndSortedResultRequestDto, Create{context.ClassInfo.ClassName}Dto, Update{context.ClassInfo.ClassName}Dto>");

            context.AppendLine("    {");

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        public string GetClassName(TableCodeGenerateContext context)
        {
            return $"I{context.ClassInfo.ClassName}AppService";
        }
    }
}
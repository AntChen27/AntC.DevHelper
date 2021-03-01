using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.CodeGenerators.Application.Contracts
{
    public class AppServiceInterfaceGenerator : BaseTableCodeGenerator
    {
        private string _className;
        public override void PreExecCodeGenerate(CodeGenerateTableContext context)
        {
            _className = GetClassName(context);
            var outPutPath = Path.Combine("Application.Contracts",
                context.ClassInfo.GroupName ?? string.Empty,
                $"{_className}.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateTableContext context)
        {
            context.AppendLine("using Benchint.Abp.Application.Services;");
            context.AppendLine("using System;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 应用服务契约接口");
            context.AppendLine($"    /// </summary>");

            var key = context.ClassInfo.Properties?.FirstOrDefault(x => x.DbColumnInfo.Key);

            context.AppendLine($"    public interface {_className} : IBenchintCrudAppService<{context.ClassInfo.ClassName}Dto, {key?.PropertyTypeName}, {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto, CreateUpdate{context.ClassInfo.ClassName}Dto, CreateUpdate{context.ClassInfo.ClassName}Dto>");

            context.AppendLine("    {");

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        public string GetClassName(CodeGenerateTableContext context)
        {
            return $"I{context.ClassInfo.ClassName}AppService";
        }
    }
}

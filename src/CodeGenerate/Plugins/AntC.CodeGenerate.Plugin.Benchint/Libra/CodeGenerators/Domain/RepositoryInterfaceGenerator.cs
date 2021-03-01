using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.CodeGenerators.Domain
{
    public class RepositoryInterfaceGenerator : BaseTableCodeGenerator
    {
        public override void PreExecCodeGenerate(CodeGenerateTableContext context)
        {
            var outPutPath = Path.Combine("Domain",
                context.ClassInfo.GroupName ?? string.Empty,
                "Repositories",
                $"{GetClassName(context)}.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateTableContext context)
        {
            context.AppendLine("using System;");
            context.AppendLine("using Volo.Abp.Domain.Repositories;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 仓储接口");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public interface {GetClassName(context)} : IRepository");

            if (context.ClassInfo.Properties != null)
            {
                var key = context.ClassInfo.Properties.FirstOrDefault(x => x.DbColumnInfo.Key);
                if (key != null)
                {
                    context.Append($"<{context.ClassInfo.ClassName}, {key.PropertyTypeName}>");
                }
            }

            context.AppendLine();
            context.AppendLine("    {");

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        public string GetClassName(CodeGenerateTableContext context)
        {
            return $"I{context.ClassInfo.ClassName}Repository";
        }
    }
}

using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application
{
    public class AppServiceGenerator : BaseTableCodeGenerator
    {
        private string _className;
        private string _baseClassName = "BenchintCrudAppService";

        public override void PreExecCodeGenerate(CodeGenerateTableContext context)
        {
            _className = GetClassName(context);
            var outPutPath = Path.Combine("Application",
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
            context.AppendLine($"    public class {_className} : {_baseClassName}{GetBaseClassGenericParameter(context, key)}, I{context.ClassInfo.ClassName}AppService");
            context.AppendLine("    {");
            context.AppendLine($"        public {_className}(I{context.ClassInfo.ClassName}Repository repository) : base(repository)");
            context.AppendLine("        {");
            context.AppendLine("        }");
            context.AppendLine("    }");
            context.AppendLine("}");
        }

        /// <summary>
        /// 获取基类泛型参数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetBaseClassGenericParameter(CodeGenerateTableContext context, PropertyModel key)
        {
            if ("CrudAppService".Equals(_baseClassName))
            {
                return $"<{context.ClassInfo.ClassName}, {context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}Dto, {key.PropertyTypeName}, {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto, CreateUpdate{context.ClassInfo.ClassName}Dto, CreateUpdate{context.ClassInfo.ClassName}Dto>";
            }

            if ("BenchintCrudAppService".Equals(_baseClassName))
            {
                return $"<{context.ClassInfo.ClassName}, {context.ClassInfo.ClassName}Dto, {key.PropertyTypeName}, {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto, CreateUpdate{context.ClassInfo.ClassName}Dto, CreateUpdate{context.ClassInfo.ClassName}Dto>";
            }

            return string.Empty;
        }

        public string GetClassName(CodeGenerateTableContext context)
        {
            return $"{context.ClassInfo.ClassName}AppService";
        }
    }
}
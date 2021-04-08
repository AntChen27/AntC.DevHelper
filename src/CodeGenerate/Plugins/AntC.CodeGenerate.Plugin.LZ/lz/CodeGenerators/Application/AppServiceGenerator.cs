using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.CodeGenerators.Application
{
    public class AppServiceGenerator : BaseTableCodeGenerator
    {
        private string _baseClassName = "CrudAppService";

        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Lz.Application",
            Desc = "此模板生成基于Abp的CrudAppService应用服务实现类"
        };

        protected override GeneratorConfig GetDefaultConfig(TableCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("Application",
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
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 应用服务实现");
            context.AppendLine($"    /// </summary>");

            var key = context.ClassInfo.Properties?.FirstOrDefault(x => x.DbColumnInfo.Key);
            context.AppendLine($"    public class {className} : {_baseClassName}{GetBaseClassGenericParameter(context, key)}, I{context.ClassInfo.ClassName}AppService");
            context.AppendLine("    {");
            context.AppendLine($"        public {className}(I{context.ClassInfo.ClassName}Repository repository) : base(repository)");
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
        private string GetBaseClassGenericParameter(TableCodeGenerateContext context, PropertyModel key)
        {
            if (key == null)
            {
                return string.Empty;
            }

            if ("CrudAppService".Equals(_baseClassName))
            {
                return $"<{context.ClassInfo.ClassName}, {context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}Dto, {key.PropertyTypeName}, {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto, Create{context.ClassInfo.ClassName}Dto, Update{context.ClassInfo.ClassName}Dto>";
            }

            if ("BenchintCrudAppService".Equals(_baseClassName))
            {
                return $"<{context.ClassInfo.ClassName}, {context.ClassInfo.ClassName}Dto, {key.PropertyTypeName}, {context.ClassInfo.ClassName}PagedAndSortedResultRequestDto, Create{context.ClassInfo.ClassName}Dto, Update{context.ClassInfo.ClassName}Dto>";
            }

            return string.Empty;
        }

        public string GetClassName(TableCodeGenerateContext context)
        {
            return $"{context.ClassInfo.ClassName}AppService";
        }
    }
}
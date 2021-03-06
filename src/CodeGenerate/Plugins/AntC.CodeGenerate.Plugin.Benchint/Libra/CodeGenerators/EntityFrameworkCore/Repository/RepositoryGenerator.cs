using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.CodeGenerators.EntityFrameworkCore.Repository
{
    public class RepositoryGenerator : BaseTableCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Libra.EfCore.Repository",
            Desc = "此模板生成EfCore的仓储实现",
        };

        protected override GeneratorConfig GetDefaultConfig(TableCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("EntityFrameworkCore", "Repositories",
                    context.ClassInfo.GroupName ?? string.Empty,
                    $"{GetClassName(context)}.cs")
            };
        }

        public override void ExecutingCodeGenerate(TableCodeGenerateContext context)
        {
            EfCoreCodeGenerate(context);
        }

        public string GetClassName(TableCodeGenerateContext context)
        {
            return $"{context.ClassInfo.ClassName}Repository";
        }

        public void EfCoreCodeGenerate(TableCodeGenerateContext context)
        {
            context.AppendLine("using System;");
            context.AppendLine("using Volo.Abp.Domain.Repositories.EntityFrameworkCore;");
            context.AppendLine("using Volo.Abp.EntityFrameworkCore;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} 仓储实现");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public class {GetClassName(context)}");

            var key = context.ClassInfo.Properties?.FirstOrDefault(x => x.DbColumnInfo.Key);
            if (key != null)
            {
                context.Append($" : EfCoreRepository<{context.GetClassName(context.CodeGenerateDbName)}Context, {context.ClassInfo.ClassName}, {key.PropertyTypeName}>, I{context.ClassInfo.ClassName}Repository");
            }
            else
            {
                context.Append($" : I{context.ClassInfo.ClassName}Repository");
            }

            context.AppendLine();
            context.AppendLine("    {");
            context.AppendLine($"        public {GetClassName(context)}(IDbContextProvider<{context.GetClassName(context.CodeGenerateDbName)}Context> dbContextProvider) : base(dbContextProvider)");
            context.AppendLine($"        {{");

            context.AppendLine($"        }}");

            context.AppendLine("    }");
            context.AppendLine("}");
        }
    }
}

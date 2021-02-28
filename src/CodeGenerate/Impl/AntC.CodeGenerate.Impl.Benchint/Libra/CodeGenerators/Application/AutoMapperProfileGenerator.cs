using AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application.Contracts;
using AntC.CodeGenerate.CodeGenerateExecutors;
using System;
using System.IO;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application
{
    public class AutoMapperProfileGenerator : BaseTableCodeGenerator
    {
        public override void PreExecCodeGenerate(CodeGenerateTableContext context)
        {
            var outPutPath = Path.Combine("Application",
                context.ClassInfo.GroupName ?? string.Empty,
                "Mapper",
                $"{context.ClassInfo.ClassName}ApplicationAutoMapperProfile.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateTableContext context)
        {
            context.AppendLine("using AutoMapper;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.ClassInfo.Annotation} AutoMapper映射配置");
            context.AppendLine($"    /// </summary>");
            context.AppendLine($"    public class {context.ClassInfo.ClassName}ApplicationAutoMapperProfile : Profile");
            context.AppendLine("    {");
            context.AppendLine($"        public {context.ClassInfo.ClassName}ApplicationAutoMapperProfile()");
            context.AppendLine("        {");

            if (context.CodeGeneratorContainer.ContainsCodeGenerator(typeof(CreateUpdateDtoGenerator)))
            {
                context.AppendLine(
                    $"            CreateMap<CreateUpdate{context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}>();");
                context.AppendLine(
                    $"            CreateMap<{context.ClassInfo.ClassName}, CreateUpdate{context.ClassInfo.ClassName}Dto>();");
            }

            if (context.CodeGeneratorContainer.ContainsCodeGenerator(typeof(OutPutDtoGenerator)))
            {
                context.AppendLine(
                    $"            CreateMap<{context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}>();");
                context.AppendLine(
                    $"            CreateMap<{context.ClassInfo.ClassName}, {context.ClassInfo.ClassName}Dto>();");
            }

            context.AppendLine("        }");
            context.AppendLine("    }");
            context.AppendLine("}");
        }
    }
}

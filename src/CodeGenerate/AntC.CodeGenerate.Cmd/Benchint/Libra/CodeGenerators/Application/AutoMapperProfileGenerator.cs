using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application.Contracts;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.Application
{
    public class AutoMapperProfileGenerator : BaseTableCodeGenerator
    {
        public override void ExecCodeGenerate(CodeGenerateTableContext context)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using AutoMapper;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.ClassInfo.Annotation} AutoMapper映射配置");
            builder.AppendLine($"    /// </summary>");
            builder.AppendLine($"    public class {context.ClassInfo.ClassName}ApplicationAutoMapperProfile : Profile");
            builder.AppendLine("    {");
            builder.AppendLine($"        public {context.ClassInfo.ClassName}ApplicationAutoMapperProfile()");
            builder.AppendLine("        {");

            if (context.CodeGeneratorContainer.ContainsCodeGenerator(typeof(CreateUpdateDtoGenerator)))
            {
                builder.AppendLine(
                    $"            CreateMap<CreateUpdate{context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}>();");
                builder.AppendLine(
                    $"            CreateMap<{context.ClassInfo.ClassName}, CreateUpdate{context.ClassInfo.ClassName}Dto>();");
            }

            if (context.CodeGeneratorContainer.ContainsCodeGenerator(typeof(OutPutDtoGenerator)))
            {
                builder.AppendLine(
                    $"            CreateMap<{context.ClassInfo.ClassName}Dto, {context.ClassInfo.ClassName}>();");
                builder.AppendLine(
                    $"            CreateMap<{context.ClassInfo.ClassName}, {context.ClassInfo.ClassName}Dto>();");
            }

            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            var outPutPath = Path.Combine("Application", context.ClassInfo.GroupName ?? String.Empty, $"{context.ClassInfo.ClassName}ApplicationAutoMapperProfile.cs");
            Output.ToFile(result, outPutPath, context.OutPutRootPath, Encoding.UTF8);
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextModelCreatingRelationExtensionsGenerator : BaseDbCodeGenerator
    {
        private string _className;
        public override void PreExecCodeGenerate(CodeGenerateDbContext context)
        {
            _className = context.GetClassName(context.CodeGenerateDbName);
            if (_className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                _className = _className.Substring(0, _className.Length - 2);
            }

            var outPutPath = Path.Combine("EntityFrameworkCore",
                $"{_className}DbContextModelCreatingRelationExtensions.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateDbContext context)
        {
            var builder = context.CodeWriter;
            builder.AppendLine("using Microsoft.EntityFrameworkCore;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.CodeGenerateDbName} 库关系映射扩展类 - 表间关系");
            builder.AppendLine($"    /// </summary>");
            builder.Append($"    public static class {_className}DbContextModelCreatingRelationExtensions");
            builder.AppendLine();
            builder.AppendLine("    {");

            //AppendEntityOneByOne(context);
            AppendEntityByGroup(context);

            builder.AppendLine("    }");
            builder.AppendLine("}");
        }

        private void AppendEntityOneByOne(CodeGenerateDbContext context)
        {
            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {context.CodeGenerateDbName} 数据库EFCore关系映射 - 表间关系");
            context.AppendLine($"        /// </summary>");
            context.AppendLine($"        /// <param name=\"context\"></param>");
            context.AppendLine($"        public static void Configure{context.GetClassName(context.CodeGenerateDbName)}(this ModelBuilder context)");
            context.AppendLine($"        {{");

            var i = 0;
            foreach (var clsInfo in context.ClassInfo.OrderBy(x => x.ClassName))
            {
                if (i != 0)
                {
                    context.AppendLine("            ");
                }

                AppendEntityMap(context, clsInfo);

                i++;
            }

            context.AppendLine("        }");
        }

        private void AppendEntityByGroup(CodeGenerateDbContext context)
        {
            var groupInfo = context.ClassInfo.GroupBy(x => x.GroupName).ToList();

            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {context.CodeGenerateDbName}  数据库EFCore关系映射 - 表间关系");
            context.AppendLine($"        /// </summary>");
            context.AppendLine($"        /// <param name=\"context\"></param>");
            context.AppendLine($"        public static void Configure{_className}(this ModelBuilder context)");
            context.AppendLine($"        {{");

            foreach (var group in groupInfo)
            {
                context.AppendLine($"            context.Configure{_className}{group.Key}();");
            }

            context.AppendLine("        }");

            foreach (var group in groupInfo)
            {
                context.AppendLine("        ");
                context.AppendLine($"        /// <summary>");
                context.AppendLine($"        /// {context.CodeGenerateDbName} {group.Key}  数据库EFCore关系映射 - 表间关系");
                context.AppendLine($"        /// </summary>");
                context.AppendLine($"        /// <param name=\"context\"></param>");
                context.AppendLine($"        public static void Configure{_className}{group.Key}(this ModelBuilder context)");
                context.AppendLine($"        {{");

                var i = 0;
                foreach (var clsInfo in group.OrderBy(x => x.ClassName))
                {
                    if (i != 0)
                    {
                        context.AppendLine("            ");
                    }
                    AppendEntityMap(context, clsInfo);
                    i++;
                }

                context.AppendLine("        }");
            }
        }

        private void AppendEntityMap(ICodeWriter builder, ClassModel clsInfo)
        {
            builder.AppendLine($"            context.Entity<{clsInfo.ClassName}>(entity =>");
            builder.AppendLine($"            {{");

            if (clsInfo.Properties.Any(x => x.DbColumnInfo.IsAbpProperty()))
            {
                builder.AppendLine($"                entity.ConfigureByConvention();");
            }

            builder.AppendLine($"            }});");
        }

    }
}

using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators
{
    public class EfCoreDbContextModelCreatingRelationExtensionsGenerator : BaseDbCodeGenerator
    {
        public override void ExecCodeGenerate(CodeGenerateDbContext context)
        {
            var className = context.GetClassName(context.CodeGenerateDbName);

            if (className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                className = className.Substring(0, className.Length - 2);
            }

            var builder = new StringBuilder();
            builder.AppendLine("using Microsoft.EntityFrameworkCore;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.CodeGenerateDbName} 库关系映射扩展类 - 表间关系");
            builder.AppendLine($"    /// </summary>");
            builder.Append($"    public static class {className}DbContextModelCreatingRelationExtensions");
            builder.AppendLine();
            builder.AppendLine("    {");

            //AppendEntityOneByOne(builder, context);
            AppendEntityByGroup(builder, context);

            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            var outPutPath = Path.Combine("EfCoreDbContext", $"{className}DbContextModelCreatingRelationExtensions.cs");
            Output.ToFile(result, outPutPath, context.OutPutRootPath, Encoding.UTF8);
        }

        private void AppendEntityOneByOne(StringBuilder builder, CodeGenerateDbContext context)
        {
            builder.AppendLine($"        /// <summary>");
            builder.AppendLine($"        /// {context.CodeGenerateDbName} 数据库EFCore关系映射 - 表间关系");
            builder.AppendLine($"        /// </summary>");
            builder.AppendLine($"        /// <param name=\"builder\"></param>");
            builder.AppendLine($"        public static void Configure{context.GetClassName(context.CodeGenerateDbName)}(this ModelBuilder builder)");
            builder.AppendLine($"        {{");

            var i = 0;
            foreach (var clsInfo in context.ClassInfo.OrderBy(x => x.ClassName))
            {
                if (i != 0)
                {
                    builder.AppendLine("            ");
                }

                AppendEntityMap(builder, clsInfo);

                i++;
            }

            builder.AppendLine("        }");
        }

        private void AppendEntityByGroup(StringBuilder builder, CodeGenerateDbContext context)
        {
            var groupInfo = context.ClassInfo.GroupBy(x => x.GroupName).ToList();
            var className = context.GetClassName(context.CodeGenerateDbName);

            if (className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                className = className.Substring(0, className.Length - 2);
            }

            builder.AppendLine($"        /// <summary>");
            builder.AppendLine($"        /// {context.CodeGenerateDbName}  数据库EFCore关系映射 - 表间关系");
            builder.AppendLine($"        /// </summary>");
            builder.AppendLine($"        /// <param name=\"builder\"></param>");
            builder.AppendLine($"        public static void Configure{className}(this ModelBuilder builder)");
            builder.AppendLine($"        {{");

            foreach (var group in groupInfo)
            {
                builder.AppendLine($"            builder.Configure{className}{group.Key}();");
            }

            builder.AppendLine("        }");

            foreach (var group in groupInfo)
            {
                builder.AppendLine("        ");
                builder.AppendLine($"        /// <summary>");
                builder.AppendLine($"        /// {context.CodeGenerateDbName} {group.Key}  数据库EFCore关系映射 - 表间关系");
                builder.AppendLine($"        /// </summary>");
                builder.AppendLine($"        /// <param name=\"builder\"></param>");
                builder.AppendLine($"        public static void Configure{className}{group.Key}(this ModelBuilder builder)");
                builder.AppendLine($"        {{");

                var i = 0;
                foreach (var clsInfo in group.OrderBy(x => x.ClassName))
                {
                    if (i != 0)
                    {
                        builder.AppendLine("            ");
                    }
                    AppendEntityMap(builder, clsInfo);
                    i++;
                }

                builder.AppendLine("        }");
            }
        }

        private void AppendEntityMap(StringBuilder builder, ClassModel clsInfo)
        {
            builder.AppendLine($"            builder.Entity<{clsInfo.ClassName}>(entity =>");
            builder.AppendLine($"            {{");

            if (clsInfo.Properties.Any(x => x.DbColumnInfo.IsAbpProperty()))
            {
                builder.AppendLine($"                entity.ConfigureByConvention();");
            }

            builder.AppendLine($"            }});");
        }

    }
}

using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextModelCreatingRelationExtensionsGenerator : BaseDbCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Libra.EfCore.Entity.Relation",
            Desc = "此模板生成EfCore的实体关系代码",
        };

        protected override GeneratorConfig GetDefaultConfig(DbCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("EntityFrameworkCore",
                    $"{GetClassName(context)}DbContextModelCreatingRelationExtensions.cs")
            };
        }

        public override void ExecutingCodeGenerate(DbCodeGenerateContext context)
        {
            var className = GetClassName(context);
            var builder = context.CodeWriter;
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

            //AppendEntityOneByOne(context);
            AppendEntityByGroup(context);

            builder.AppendLine("    }");
            builder.AppendLine("}");
        }

        private void AppendEntityOneByOne(DbCodeGenerateContext context)
        {
            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {context.CodeGenerateDbName} 数据库EFCore关系映射 - 表间关系");
            context.AppendLine($"        /// </summary>");
            context.AppendLine($"        /// <param name=\"context\"></param>");
            context.AppendLine($"        public static void Configure{context.GetClassName(context.CodeGenerateDbName)}Relation(this ModelBuilder context)");
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

        private void AppendEntityByGroup(DbCodeGenerateContext context)
        {
            var className = context.GetClassName(context.CodeGenerateDbName);
            var groupInfo = context.ClassInfo.GroupBy(x => x.GroupName).ToList();

            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {context.CodeGenerateDbName}  数据库EFCore关系映射 - 表间关系");
            context.AppendLine($"        /// </summary>");
            context.AppendLine($"        /// <param name=\"context\"></param>");
            context.AppendLine($"        public static void Configure{className}(this ModelBuilder context)");
            context.AppendLine($"        {{");

            foreach (var group in groupInfo)
            {
                context.AppendLine($"            context.Configure{className}{group.Key}Relation();");
            }

            context.AppendLine("        }");

            foreach (var group in groupInfo)
            {
                context.AppendLine("        ");
                context.AppendLine($"        /// <summary>");
                context.AppendLine($"        /// {context.CodeGenerateDbName} {group.Key}  数据库EFCore关系映射 - 表间关系");
                context.AppendLine($"        /// </summary>");
                context.AppendLine($"        /// <param name=\"context\"></param>");
                context.AppendLine($"        public static void Configure{className}{group.Key}Relation(this ModelBuilder context)");
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

        private string GetClassName(DbCodeGenerateContext context)
        {
            var className = context.GetClassName(context.CodeGenerateDbName);
            if (className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                className = className.Substring(0, className.Length - 2);
            }

            return className;
        }
    }
}

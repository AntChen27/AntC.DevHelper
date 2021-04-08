using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextGenerator : BaseDbCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Lz.EfCore.DbContext",
            Desc = "此模板生成EfCore的仓储实现",
        };

        protected override GeneratorConfig GetDefaultConfig(DbCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("EntityFrameworkCore",
                    $"{GetClassName(context)}DbContext.cs")
            };
        }

        public override void ExecutingCodeGenerate(DbCodeGenerateContext context)
        {
            var className = GetClassName(context);
            context.AppendLine("using System;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {className}");
            context.AppendLine($"    /// </summary>");
            context.AppendLine($"    [ConnectionStringName(\"{context.CodeGenerateDbName}\")]");
            context.Append($"    public partial class {className}DbContext : AbpDbContext<{className}DbContext>");

            context.AppendLine();
            context.AppendLine("    {");

            //AppendOneByOne(context);
            AppendByGroup(context);

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        private void AppendOneByOne(DbCodeGenerateContext context)
        {
            if (context.ClassInfo != null && context.ClassInfo.Any())
            {
                var i = 0;
                foreach (var clsInfo in context.ClassInfo)
                {
                    if (i != 0)
                    {
                        context.AppendLine("        ");
                    }
                    AppendDbSet(context, clsInfo);

                    i++;
                }
            }
        }

        private void AppendByGroup(DbCodeGenerateContext context)
        {
            var groupInfo = context.ClassInfo.GroupBy(x => x.GroupName).ToList();

            var groupIndex = 0;
            foreach (var group in groupInfo)
            {
                if (groupIndex != 0)
                {
                    context.AppendLine("        ");
                }

                context.AppendLine($"        #region {group.Key}");
                context.AppendLine("        ");
                var i = 0;
                foreach (var clsInfo in group.OrderBy(x => x.ClassName))
                {
                    if (i != 0)
                    {
                        context.AppendLine("        ");
                    }
                    AppendDbSet(context, clsInfo);
                    i++;
                }
                context.AppendLine("        ");
                context.AppendLine("        #endregion");
                groupIndex++;
            }
        }

        private void AppendDbSet(ICodeWriter writer, ClassModel clsInfo)
        {
            writer.AppendLine($"        /// <summary>");
            writer.AppendLine($"        /// {clsInfo.Annotation}");
            writer.AppendLine($"        /// </summary>");
            writer.AppendLine($"        public virtual DbSet<{clsInfo.ClassName}> {clsInfo.ClassName}s {{ get; set; }}");
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
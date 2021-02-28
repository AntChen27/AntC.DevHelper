using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextGenerator : BaseDbCodeGenerator
    {
        public override void ExecCodeGenerate(CodeGenerateDbContext context)
        {
            var className = context.GetClassName(context.CodeGenerateDbName);
            if (className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                className = className.Substring(0, className.Length - 2);
            }

            var outPutPath = Path.Combine("EntityFrameworkCore", $"{className}DbContext.cs");
            SetRelativePath(context, outPutPath);

            context.AppendLine("using System;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {className}");
            context.AppendLine($"    /// </summary>");
            context.AppendLine($"    [ConnectionStringName(\"{className}\")]");
            context.Append($"    public partial class {className}DbContext : AbpDbContext<{className}DbContext>");

            context.AppendLine();
            context.AppendLine("    {");

            //AppendOneByOne(context);
            AppendByGroup(context);

            context.AppendLine("    }");
            context.AppendLine("}");
        }


        private void AppendOneByOne(CodeGenerateDbContext context)
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

        private void AppendByGroup(CodeGenerateDbContext context)
        {
            var groupInfo = context.ClassInfo.GroupBy(x => x.GroupName).ToList();
            var className = context.GetClassName(context.CodeGenerateDbName);

            if (className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                className = className.Substring(0, className.Length - 2);
            }

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
            writer.AppendLine($"        public virtual DbSet<{clsInfo.ClassName}> {clsInfo.ClassName} {{ get; set; }}");
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
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

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("using System;");
            AppendUsingNamespace(context, builder);
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {className}");
            builder.AppendLine($"    /// </summary>");
            builder.AppendLine($"    [ConnectionStringName(\"{className}\")]");
            builder.Append($"    public partial class {className}DbContext : AbpDbContext<{className}DbContext>");

            builder.AppendLine();
            builder.AppendLine("    {");

            //AppendOneByOne(builder, context);
            AppendByGroup(builder, context);

            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            var outPutPath = Path.Combine("EntityFrameworkCore", $"{className}DbContext.cs");
            Output.ToFile(result, outPutPath, context.OutPutRootPath, Encoding.UTF8);
        }


        private void AppendOneByOne(StringBuilder builder, CodeGenerateDbContext context)
        {
            if (context.ClassInfo != null && context.ClassInfo.Any())
            {
                var i = 0;
                foreach (var clsInfo in context.ClassInfo)
                {
                    if (i != 0)
                    {
                        builder.AppendLine("        ");
                    }
                    AppendDbSet(builder, clsInfo);

                    i++;
                }
            }
        }

        private void AppendByGroup(StringBuilder builder, CodeGenerateDbContext context)
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
                    builder.AppendLine("        ");
                }

                builder.AppendLine($"        #region {group.Key}");
                builder.AppendLine("        ");
                var i = 0;
                foreach (var clsInfo in group.OrderBy(x => x.ClassName))
                {
                    if (i != 0)
                    {
                        builder.AppendLine("        ");
                    }
                    AppendDbSet(builder, clsInfo);
                    i++;
                }
                builder.AppendLine("        ");
                builder.AppendLine("        #endregion");
                groupIndex++;
            }
        }

        private void AppendDbSet(StringBuilder builder, ClassModel clsInfo)
        {
            builder.AppendLine($"        /// <summary>");
            builder.AppendLine($"        /// {clsInfo.Annotation}");
            builder.AppendLine($"        /// </summary>");
            builder.AppendLine($"        public virtual DbSet<{clsInfo.ClassName}> {clsInfo.ClassName} {{ get; set; }}");
        }


        private void AppendUsingNamespace(CodeGenerateDbContext tableContext, StringBuilder builder)
        {

        }
    }
}

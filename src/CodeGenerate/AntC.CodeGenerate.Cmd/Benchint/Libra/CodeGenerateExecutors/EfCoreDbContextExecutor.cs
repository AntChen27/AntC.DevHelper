using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerateExecutors
{
    public class EfCoreDbContextExecutor : BaseDbCodeGenerateExecutor
    {
        public override void ExecCodeGenerate(CodeGenerateDbContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            AppendUsingNamespace(context, sb);
            sb.AppendLine("");
            sb.AppendLine($"namespace {context.GetNameSpace()}");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// ");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    [ConnectionStringName(\"{context.GetClassName(context.CodeGenerateDbName)}\")]");
            sb.Append($"    public partial class {context.GetClassName(context.CodeGenerateDbName)}DbContext : AbpDbContext<{context.GetClassName(context.CodeGenerateDbName)}DbContext>");

            sb.AppendLine();
            sb.AppendLine("    {");

            if (context.ClassInfo != null && context.ClassInfo.Any())
            {
                var i = 0;
                foreach (var clsInfo in context.ClassInfo)
                {
                    if (i != 0)
                    {
                        sb.AppendLine("        ");
                    }
                    sb.AppendLine($"        /// <summary>");
                    sb.AppendLine($"        /// {clsInfo.Annotation}");
                    sb.AppendLine($"        /// </summary>");
                    sb.AppendLine($"        public virtual DbSet<{clsInfo.ClassName}> {clsInfo.ClassName} {{ get; set; }}");

                    i++;
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            var result = sb.ToString();

            Output.ToFile(result, $"EfCoreDbContext\\{context.GetClassFileName(context.CodeGenerateDbName)}DbContext.cs", context.OutPutRootPath, Encoding.UTF8);
        }

        private void AppendUsingNamespace(CodeGenerateDbContext tableContext, StringBuilder builder)
        {

        }
    }
}

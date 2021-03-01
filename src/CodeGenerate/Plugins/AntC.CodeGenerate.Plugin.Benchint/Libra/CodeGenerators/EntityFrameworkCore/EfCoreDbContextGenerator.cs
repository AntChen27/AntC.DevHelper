using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextGenerator : BaseDbCodeGenerator
    {
        private string _className;

        public override void PreExecCodeGenerate(CodeGenerateDbContext context)
        {
            _className = context.GetClassName(context.CodeGenerateDbName);
            if (_className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                _className = _className.Substring(0, _className.Length - 2);
            }

            var outPutPath = Path.Combine("EntityFrameworkCore", $"{_className}DbContext.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateDbContext context)
        {
            context.AppendLine("using System;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {_className}");
            context.AppendLine($"    /// </summary>");
            context.AppendLine($"    [ConnectionStringName(\"{context.CodeGenerateDbName}\")]");
            context.Append($"    public partial class {_className}DbContext : AbpDbContext<{_className}DbContext>");

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

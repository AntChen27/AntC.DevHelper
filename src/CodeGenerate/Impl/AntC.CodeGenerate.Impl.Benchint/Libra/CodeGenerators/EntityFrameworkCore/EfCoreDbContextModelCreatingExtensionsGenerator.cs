using System;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextModelCreatingExtensionsGenerator : BaseDbCodeGenerator
    {
        private string _className;

        public override void PreExecCodeGenerate(CodeGenerateDbContext context)
        {
            _className = context.GetClassName(context.CodeGenerateDbName);
            if (_className.EndsWith("db", StringComparison.CurrentCultureIgnoreCase))
            {
                _className = _className.Substring(0, _className.Length - 2);
            }

            var outPutPath = Path.Combine("EntityFrameworkCore", $"{_className}DbContextModelCreatingExtensions.cs");
            SetRelativePath(context, outPutPath);
        }

        public override void ExecutingCodeGenerate(CodeGenerateDbContext context)
        {
            context.AppendLine("using Microsoft.EntityFrameworkCore;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.CodeGenerateDbName} 库关系映射扩展类");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public static class {_className}DbContextModelCreatingExtensions");
            context.AppendLine();
            context.AppendLine("    {");

            //AppendEntityOneByOne(context);
            AppendEntityByGroup(context);

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        private void AppendEntityOneByOne(CodeGenerateDbContext context)
        {
            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {context.CodeGenerateDbName} 数据库EFCore关系映射");
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
            context.AppendLine($"        /// {context.CodeGenerateDbName}  数据库EFCore关系映射");
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
                context.AppendLine($"        /// {context.CodeGenerateDbName} {group.Key} 数据库EFCore关系映射");
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

        private void AppendEntityMap(ICodeWriter writer, ClassModel clsInfo)
        {
            writer.AppendLine($"            context.Entity<{clsInfo.ClassName}>(entity =>");
            writer.AppendLine($"            {{");

            writer.AppendLine($"                entity.ToTable(\"{clsInfo.DbTableInfo.TableName}\");");
            writer.AppendLine($"                ");
            AppendEntityCommont(writer, clsInfo);

            var i = 0;
            foreach (var property in clsInfo.Properties)
            {
                if (property.DbColumnInfo.IsAbpProperty())
                {
                    continue;
                }
                if (i != 0)
                {
                    writer.AppendLine($"                ");
                }
                AppendEntityField(writer, property);
                i++;
            }

            writer.AppendLine($"            }});");
        }

        private void AppendEntityCommont(ICodeWriter writer, ClassModel clsInfo)
        {
            if (string.IsNullOrEmpty(clsInfo.DbTableInfo.Commont))
            {
                return;
            }

            writer.AppendLine($"                entity.HasComment(\"{clsInfo.DbTableInfo.Commont}\");");
            writer.AppendLine($"                ");
        }

        private void AppendEntityIndex(ICodeWriter writer, ClassModel clsInfo)
        {
            // todo 添加索引写入
        }

        private void AppendEntityField(ICodeWriter builder, PropertyModel property)
        {
            if (property.DbColumnInfo == null)
            {
                return;
            }


            builder.Append($"                entity.Property(e => e.{property.PropertyName})");
            if (!property.DbColumnInfo.Nullable)
            {
                builder.AppendLine();
                builder.Append($"                    .IsRequired()");
            }
            builder.AppendLine();
            builder.Append($"                    .HasColumnName(\"{property.DbColumnInfo.ColumnName}\")");
            builder.AppendLine();
            builder.Append($"                    .HasColumnType(\"{property.DbColumnInfo.DataTypeName}\")");

            if (!string.IsNullOrEmpty(property.DbColumnInfo.DefaultValue))
            {
                builder.AppendLine();
                builder.Append($"                    .HasDefaultValueSql(\"'{property.DbColumnInfo.DefaultValue}'\")");
            }
            if (!string.IsNullOrEmpty(property.DbColumnInfo.Commont))
            {
                builder.AppendLine();
                builder.Append($"                    .HasComment(\"{property.DbColumnInfo.Commont}\")");
            }
            if (property.DbColumnInfo is MysqlDbColumnInfoModel mysqlDbColumnInfo)
            {
                if (!string.IsNullOrEmpty(mysqlDbColumnInfo.CharacterSetName))
                {
                    builder.AppendLine();
                    builder.Append($"                    .HasCharSet(\"{mysqlDbColumnInfo.CharacterSetName}\")");
                }

                if (!string.IsNullOrEmpty(mysqlDbColumnInfo.CollationName))
                {
                    builder.AppendLine();
                    builder.Append($"                    .HasCollation(\"{mysqlDbColumnInfo.CollationName}\")");
                }
            }

            builder.AppendLine(";");
        }
    }
}

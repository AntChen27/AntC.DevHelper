using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.CodeGenerateExecutors
{
    public class EfCoreDbContextModelCreatingExtensionsExecutor : BaseDbCodeGenerateExecutor
    {
        public override void ExecCodeGenerate(CodeGenerateDbContext context)
        {
            var className = context.GetClassName(context.CodeGenerateDbName);

            var builder = new StringBuilder();
            builder.AppendLine("using Microsoft.EntityFrameworkCore;");
            builder.AppendLine("");
            builder.AppendLine($"namespace {context.GetNameSpace()}");
            builder.AppendLine("{");
            builder.AppendLine($"    /// <summary>");
            builder.AppendLine($"    /// {context.CodeGenerateDbName} 库关系映射扩展类");
            builder.AppendLine($"    /// </summary>");
            builder.Append($"    public static class {className}DbContextModelCreatingExtensions");
            builder.AppendLine();
            builder.AppendLine("    {");

            builder.AppendLine($"        /// <summary>");
            builder.AppendLine($"        /// {context.CodeGenerateDbName} 数据库EFCore关系映射");
            builder.AppendLine($"        /// </summary>");
            builder.AppendLine($"        /// <param name=\"builder\"></param>");
            builder.AppendLine($"        public static void Configure{className}(this ModelBuilder builder)");
            builder.AppendLine($"        {{");

            if (context.ClassInfo != null && context.ClassInfo.Any())
            {
                var i = 0;
                foreach (var clsInfo in context.ClassInfo)
                {
                    if (i != 0)
                    {
                        builder.AppendLine("            ");
                    }

                    AppendEntityMap(builder, clsInfo);

                    i++;
                }
            }

            builder.AppendLine("        }");
            builder.AppendLine("    }");
            builder.AppendLine("}");

            var result = builder.ToString();

            Output.ToFile(result,
                $"EfCoreDbContext\\{context.GetClassFileName(context.CodeGenerateDbName)}DbContextModelCreatingExtensions.cs",
                context.OutPutRootPath, Encoding.UTF8);
        }

        private void AppendEntityMap(StringBuilder builder, ClassModel clsInfo)
        {
            builder.AppendLine($"            builder.Entity<{clsInfo.ClassName}>(entity =>");
            builder.AppendLine($"            {{");

            builder.AppendLine($"                entity.ToTable(\"{clsInfo.DbTableInfo.TableName}\");");
            builder.AppendLine($"                ");
            AppendEntityCommont(builder, clsInfo);

            var i = 0;
            foreach (var property in clsInfo.Properties)
            {
                AppendEntityField(builder, property);
                if (i < clsInfo.Properties.Count() - 1)
                {
                    builder.AppendLine($"                ");
                }

                i++;
            }

            builder.AppendLine($"            }});");
        }

        private void AppendEntityCommont(StringBuilder builder, ClassModel clsInfo)
        {
            if (string.IsNullOrEmpty(clsInfo.DbTableInfo.Commont))
            {
                return;
            }

            builder.AppendLine($"                entity.HasComment(\"{clsInfo.DbTableInfo.Commont}\");");
            builder.AppendLine($"                ");
        }

        private void AppendEntityIndex(StringBuilder builder, ClassModel clsInfo)
        {
            // todo 添加索引写入
        }

        private void AppendEntityField(StringBuilder builder, PropertyModel property)
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

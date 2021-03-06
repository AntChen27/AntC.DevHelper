using System;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql.Model;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.CodeGenerators.EntityFrameworkCore
{
    public class EfCoreDbContextModelCreatingExtensionsGenerator : BaseDbCodeGenerator
    {
        public override GeneratorInfo GeneratorInfo => new GeneratorInfo()
        {
            Name = "Libra.EfCore.Entity.Mapping",
            Desc = "此模板生成EfCore的实体映射关系代码",
        };

        protected override GeneratorConfig GetDefaultConfig(DbCodeGenerateContext context)
        {
            return new GeneratorConfig()
            {
                FileRelativePath = Path.Combine("EntityFrameworkCore", $"{GetClassName(context)}DbContextModelCreatingExtensions.cs")
            };
        }

        public override void ExecutingCodeGenerate(DbCodeGenerateContext context)
        {
            var className = GetClassName(context);
            context.AppendLine("using Microsoft.EntityFrameworkCore;");
            context.AppendLine("");
            context.AppendLine($"namespace {context.GetNameSpace()}");
            context.AppendLine("{");
            context.AppendLine($"    /// <summary>");
            context.AppendLine($"    /// {context.CodeGenerateDbName} 库关系映射扩展类");
            context.AppendLine($"    /// </summary>");
            context.Append($"    public static class {className}DbContextModelCreatingExtensions");
            context.AppendLine();
            context.AppendLine("    {");

            //AppendEntityOneByOne(context);
            AppendEntityByGroup(context);

            context.AppendLine("    }");
            context.AppendLine("}");
        }

        private void AppendEntityOneByOne(DbCodeGenerateContext context)
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

        private void AppendEntityByGroup(DbCodeGenerateContext context)
        {
            var className = context.GetClassName(context.CodeGenerateDbName);
            var groupInfo = context.ClassInfo.GroupBy(x => x.GroupName).ToList();

            context.AppendLine($"        /// <summary>");
            context.AppendLine($"        /// {context.CodeGenerateDbName}  数据库EFCore关系映射");
            context.AppendLine($"        /// </summary>");
            context.AppendLine($"        /// <param name=\"context\"></param>");
            context.AppendLine($"        public static void Configure{className}(this ModelBuilder context)");
            context.AppendLine($"        {{");

            foreach (var group in groupInfo)
            {
                context.AppendLine($"            context.Configure{className}{group.Key}();");
            }

            context.AppendLine("        }");

            foreach (var group in groupInfo)
            {
                context.AppendLine("        ");
                context.AppendLine($"        /// <summary>");
                context.AppendLine($"        /// {context.CodeGenerateDbName} {group.Key} 数据库EFCore关系映射");
                context.AppendLine($"        /// </summary>");
                context.AppendLine($"        /// <param name=\"context\"></param>");
                context.AppendLine($"        public static void Configure{className}{group.Key}(this ModelBuilder context)");
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

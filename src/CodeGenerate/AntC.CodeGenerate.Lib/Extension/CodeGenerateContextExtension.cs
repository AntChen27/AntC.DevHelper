using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate
{
    public static class CodeGenerateContextExtension
    {
        /// <summary>
        /// 获取命名空间
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetNameSpace(this CodeGenerateContext context)
        {
            return context.CodeConverter.Convert(context.CodeGenerateDbName, CodeType.Namespace);
        }

        /// <summary>
        /// 获取类名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClassName(this CodeGenerateContext context, DbTableInfoModel tableInfo)
        {
            return context.CodeConverter.Convert(tableInfo.TableName, CodeType.ClassName);
        }

        /// <summary>
        /// 获取类名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClassName(this CodeGenerateContext context, string name)
        {
            return context.CodeConverter.Convert(name, CodeType.ClassName);
        }

        /// <summary>
        /// 获取类文件名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClassFileName(this CodeGenerateContext context, DbTableInfoModel tableInfo)
        {
            return context.CodeConverter.Convert(tableInfo.TableName, CodeType.ClassFileName);
        }

        /// <summary>
        /// 获取类文件名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClassFileName(this CodeGenerateContext context, string name)
        {
            return context.CodeConverter.Convert(name, CodeType.ClassFileName);
        }

        /// <summary>
        /// 获取主键类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetKeyTypeName(this TableCodeGenerateContext context)
        {
            var propertyModel = context.ClassInfo.Properties.FirstOrDefault(x => x.DbColumnInfo.Key);
            return propertyModel != null ? propertyModel.PropertyTypeName : string.Empty;
        }

        /// <summary>
        /// 获取Abp框架的Entity父类类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAbpEntitySuperClass(this TableCodeGenerateContext context)
        {
            var superClassName = string.Empty;
            var keyTypeNameWithGeneric = context.GetKeyTypeName();

            if (!string.IsNullOrEmpty(keyTypeNameWithGeneric))
            {
                keyTypeNameWithGeneric = $"<{keyTypeNameWithGeneric}>";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpExtraPropertiesProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpConcurrencyStampProperty())
            )
            {
                superClassName = $"AggregateRoot{keyTypeNameWithGeneric}";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpCreationTimeProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpCreatorIdProperty())
            )
            {
                superClassName = $"CreationAuditedAggregateRoot{keyTypeNameWithGeneric}";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpLastModificationTimeProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpLastModifierIdProperty())
            )
            {
                superClassName = $"AuditedAggregateRoot{keyTypeNameWithGeneric}";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpIsDeletedProperty())
            && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpDeletionTimeProperty())
            && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpDeleterIdProperty())
            )
            {
                superClassName = $"FullAuditedAggregateRoot{keyTypeNameWithGeneric}";
            }

            return superClassName;
        }

        /// <summary>
        /// 获取Abp框架的Entity父类类型
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetAbpEntitySuperClassNamespace(this TableCodeGenerateContext context)
        {
            var @namespace = string.Empty;

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpExtraPropertiesProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpConcurrencyStampProperty())
            )
            {
                @namespace = $"Volo.Abp.Domain.Entities";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpCreationTimeProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpCreatorIdProperty())
            )
            {
                @namespace = $"Volo.Abp.Domain.Entities.Auditing";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpLastModificationTimeProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpLastModifierIdProperty())
            )
            {
                @namespace = $"Volo.Abp.Domain.Entities.Auditing";
            }

            if (context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpIsDeletedProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpDeletionTimeProperty())
                && context.ClassInfo.DbTableInfo.Columns.Any(x => x.IsAbpDeleterIdProperty())
            )
            {
                @namespace = $"Volo.Abp.Domain.Entities.Auditing";
            }

            return @namespace;
        }
    }
}

using AntC.CodeGenerate.Core.Model.Db;
using System;

namespace AntC.CodeGenerate.Core.Extension
{
    public static class DbColumnInfoModelExtension
    {
        /// <summary>
        /// 是否为Abp属性
        /// </summary>
        /// <param name="columnInfo">The column information.</param>
        public static bool IsAbpProperty(this ColumnInfo columnInfo)
        {
            return columnInfo.IsAbpExtraPropertiesProperty() ||
                   columnInfo.IsAbpConcurrencyStampProperty() ||
                   columnInfo.IsAbpLastModificationTimeProperty() ||
                   columnInfo.IsAbpLastModifierIdProperty() ||
                   columnInfo.IsAbpCreationTimeProperty() ||
                   columnInfo.IsAbpCreatorIdProperty() ||
                   columnInfo.IsAbpDeletionTimeProperty() ||
                   columnInfo.IsAbpDeleterIdProperty() ||
                   columnInfo.IsAbpIsDeletedProperty()
                ;
        }

        public static bool IsAbpExtraPropertiesProperty(this ColumnInfo columnInfo)
        {
            return "ExtraProperties".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpConcurrencyStampProperty(this ColumnInfo columnInfo)
        {
            return "ConcurrencyStamp".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpCreationTimeProperty(this ColumnInfo columnInfo)
        {
            return "CreationTime".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpCreatorIdProperty(this ColumnInfo columnInfo)
        {
            return "CreatorId".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpLastModificationTimeProperty(this ColumnInfo columnInfo)
        {
            return "LastModificationTime".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpLastModifierIdProperty(this ColumnInfo columnInfo)
        {
            return "LastModifierId".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpIsDeletedProperty(this ColumnInfo columnInfo)
        {
            return "IsDeleted".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpDeleterIdProperty(this ColumnInfo columnInfo)
        {
            return "DeleterId".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpDeletionTimeProperty(this ColumnInfo columnInfo)
        {
            return "DeletionTime".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

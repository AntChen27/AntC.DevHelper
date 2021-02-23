using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Extension
{
    public static class DbColumnInfoModelExtension
    {
        public static bool IsAbpProperty(this DbColumnInfoModel columnInfo)
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

        public static bool IsAbpExtraPropertiesProperty(this DbColumnInfoModel columnInfo)
        {
            return "ExtraProperties".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpConcurrencyStampProperty(this DbColumnInfoModel columnInfo)
        {
            return "ConcurrencyStamp".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpCreationTimeProperty(this DbColumnInfoModel columnInfo)
        {
            return "CreationTime".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpCreatorIdProperty(this DbColumnInfoModel columnInfo)
        {
            return "CreatorId".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpLastModificationTimeProperty(this DbColumnInfoModel columnInfo)
        {
            return "LastModificationTime".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpLastModifierIdProperty(this DbColumnInfoModel columnInfo)
        {
            return "LastModifierId".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpIsDeletedProperty(this DbColumnInfoModel columnInfo)
        {
            return "IsDeleted".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpDeleterIdProperty(this DbColumnInfoModel columnInfo)
        {
            return "DeleterId".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAbpDeletionTimeProperty(this DbColumnInfoModel columnInfo)
        {
            return "DeletionTime".Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}

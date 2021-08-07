using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Core.Contracts;
using AntC.CodeGenerate.Core.Enum;
using AntC.CodeGenerate.Core.Model.CLR;
using AntC.CodeGenerate.Core.Model.Db;

namespace AntC.CodeGenerate.Core.Extension
{
    public static class DbInfoProviderExtension
    {
        public static ClassModel GetClassModel(this IDbInfoProvider provider, string dbName, string tableName)
        {
            var tableInfo = provider.GetTableInfoWithColumns(dbName, tableName);

            var classModel = new ClassModel()
            {
                TableInfo = tableInfo,
                ClassName = CodeHelper.DefaultConverter.Convert(tableName, CodeType.ClassName),
                OutPutFileName = CodeHelper.DefaultConverter.Convert(tableName, CodeType.ClassFileName),
                Annotation = tableInfo.Commont,
                //GroupName = tableInfo.GroupName,
                Properties = tableInfo.Columns.Select(x =>
                {
                    var propertyModel = new PropertyModel()
                    {
                        DbColumnInfo = x,
                        //PropertyTypeName = GetFiledTypeName(x),
                        Annotation = x.Commont,
                        PropertyName = CodeHelper.DefaultConverter.Convert(x.ColumnName, CodeType.PropertyName),
                    };
                    propertyModel.PropertyTypeName = provider.GetFiledTypeName(x);
                    return propertyModel;
                }),
            };
            return classModel;
        }

        public static ClassModel ToClassModel(this IDbInfoProvider provider, TableInfo tableInfo)
        {
            var classModel = new ClassModel()
            {
                TableInfo = tableInfo,
                ClassName = CodeHelper.DefaultConverter.Convert(tableInfo.TableName, CodeType.ClassName),
                OutPutFileName = CodeHelper.DefaultConverter.Convert(tableInfo.TableName, CodeType.ClassFileName),
                Annotation = tableInfo.Commont,
                //GroupName = tableInfo.GroupName,
                Properties = tableInfo.Columns.Select(x =>
                {
                    var propertyModel = new PropertyModel()
                    {
                        DbColumnInfo = x,
                        PropertyTypeName = provider.GetFiledTypeName(x),
                        Annotation = x.Commont,
                        PropertyName = CodeHelper.DefaultConverter.Convert(x.ColumnName, CodeType.PropertyName),
                    };
                    return propertyModel;
                }),
            };
            return classModel;
        }
    }
}

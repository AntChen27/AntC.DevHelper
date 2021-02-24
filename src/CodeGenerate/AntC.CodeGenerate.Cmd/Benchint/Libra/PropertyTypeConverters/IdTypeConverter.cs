using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using System;
using System.Linq;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.PropertyTypeConverters
{
    public class IdTypeConverter : IPropertyTypeConverter
    {
        public bool CanConvert(PropertyModel property)
        {
            return property.DbColumnInfo.DbTableInfo.Columns.Count(x => x.Key) == 1 && property.DbColumnInfo.Key;
        }

        public string Convert(PropertyModel property)
        {
            if (property.DbColumnInfo.Commont.Contains("guid", StringComparison.CurrentCultureIgnoreCase))
            {
                return nameof(Guid);
            }

            return property.PropertyTypeName;
        }
    }
}

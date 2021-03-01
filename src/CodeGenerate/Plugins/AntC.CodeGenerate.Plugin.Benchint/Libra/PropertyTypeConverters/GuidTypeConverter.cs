using System;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.PropertyTypeConverters
{
    public class GuidTypeConverter : IPropertyTypeConverter
    {
        public bool CanConvert(PropertyModel property)
        {
            return property.DbColumnInfo.Commont.Contains("guid", StringComparison.CurrentCultureIgnoreCase);
        }

        public string Convert(PropertyModel property)
        {
            return nameof(Guid);
        }
    }
}

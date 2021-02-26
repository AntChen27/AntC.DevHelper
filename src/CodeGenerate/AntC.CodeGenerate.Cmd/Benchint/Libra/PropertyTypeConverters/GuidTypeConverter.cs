using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using System;
using System.Linq;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.PropertyTypeConverters
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

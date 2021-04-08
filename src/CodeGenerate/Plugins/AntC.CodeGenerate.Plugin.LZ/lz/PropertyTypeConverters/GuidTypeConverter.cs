using System;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.PropertyTypeConverters
{
    public class GuidTypeConverter : IPropertyTypeConverter
    {
        public bool CanConvert(PropertyModel property)
        {
            return property.DbColumnInfo.Commont.Contains("编号", StringComparison.CurrentCultureIgnoreCase);
        }

        public string Convert(PropertyModel property)
        {
            return nameof(Guid);
        }
    }
}
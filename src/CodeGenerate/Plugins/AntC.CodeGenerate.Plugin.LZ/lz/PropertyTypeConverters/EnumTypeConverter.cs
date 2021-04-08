using System;
using System.Collections.Generic;
using System.Linq;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.LZ.lz.PropertyTypeConverters
{
    public class EnumTypeConverter : IPropertyTypeConverter
    {
        private Dictionary<string, string> _enumMapping = new Dictionary<string, string>()
        {
            {"matchstatus","MatchStatus"}
        };

        public bool CanConvert(PropertyModel property)
        {
            if (_enumMapping.Keys.Any(x => x.Equals(property.DbColumnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }

            if (property.DbColumnInfo.DataType.Contains("tinyint"))
            {
                return true;
            }

            return false;
        }

        public string Convert(PropertyModel property)
        {
            if (property.DbColumnInfo.DataType.Contains("tinyint"))
            {
                return "int";
            }
            return _enumMapping[property.DbColumnInfo.ColumnName.ToLower()];
        }
    }
}
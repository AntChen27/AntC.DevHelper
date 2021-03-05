using System;
using System.Collections.Generic;
using System.Linq;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Plugin.Benchint.Libra.PropertyTypeConverters
{
    public class EnumTypeConverter : IPropertyTypeConverter
    {
        private Dictionary<string, string> _enumMapping = new Dictionary<string, string>()
        {
            {"kpi_calc_cate","KpiCalcCate"},
            {"stat_way","KpiStatWay"},
            {"run_stat","KpiStatRunStatus"},
            {"stat_time_dimension","KpiStatTimeDimension"},
            {"enable","EnableStatus"},
            {"db_type","KpiDbType"},
            {"data_source","KpiStatDataSource"},
        };

        public bool CanConvert(PropertyModel property)
        {
            if (_enumMapping.Keys.Any(x => x.Equals(property.DbColumnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }

            if (property.DbColumnInfo.DataType.Contains("tinyint") && property.PropertyName == "sbyte")
            {
                return true;
            }
            return false;
        }

        public string Convert(PropertyModel property)
        {
            if (property.DbColumnInfo.DataType.Contains("tinyint") && property.PropertyName == "sbyte")
            {
                return "int";
            }
            return _enumMapping[property.DbColumnInfo.ColumnName.ToLower()];
        }
    }
}

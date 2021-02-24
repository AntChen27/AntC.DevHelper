using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.Cmd.Benchint.Libra.PropertyTypeConverters
{
    public class EnumTypeConverter : IPropertyTypeConverter
    {
        private Dictionary<string, string> _enumMapping = new Dictionary<string, string>()
        {
            {"kpi_calc_cate","KpiCalcCate"},
            {"stat_way","KpiStatWay"},
            {"run_stat","KpiRunStat"},
            {"stat_time_dimension","KpiStatTimeDimension"},
            {"enable","EnableStatus"},
            {"db_type","KpiDbType"},
            {"data_source","DataSource"},
        };

        public bool CanConvert(PropertyModel property)
        {
            if (_enumMapping.Keys.Any(x => x.Equals(property.DbColumnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }
            return false;
        }

        public string Convert(PropertyModel property)
        {
            return _enumMapping[property.DbColumnInfo.ColumnName.ToLower()];
        }
    }
}

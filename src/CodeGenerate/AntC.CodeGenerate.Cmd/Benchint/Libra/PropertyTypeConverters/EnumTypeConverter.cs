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
            {"stat_way","StatWay"},
            {"run_stat","RunStat"},
            {"stat_time_dimension","StatTimeDimension"},
            {"enable","EnableStatus"},
            {"db_type","DbType"},
        };

        public bool CanConvert(DbColumnInfoModel columnInfo)
        {
            if (_enumMapping.Keys.Any(x => x.Equals(columnInfo.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return true;
            }
            return false;
        }

        public string Convert(DbColumnInfoModel columnInfo)
        {
            return _enumMapping[columnInfo.ColumnName.ToLower()];
        }
    }
}

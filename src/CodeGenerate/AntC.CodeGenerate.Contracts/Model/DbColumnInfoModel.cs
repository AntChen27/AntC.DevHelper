using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    public class DbColumnInfoModel
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Commont { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool Nullable { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool Key { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 数据类型名称 带数据长度的
        /// </summary>
        public string DataTypeName { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public long DataLength { get; set; }

        /// <summary>
        /// decimal 精度整数部分长度
        /// </summary>
        public long NumericPrecision { get; set; }

        /// <summary>
        /// decimal 精度小数部分长度
        /// </summary>
        public long NumericScale { get; set; }

        /// <summary>
        /// 数据表信息
        /// </summary>
        public DbTableInfoModel DbTableInfo { get; set; }
    }
}

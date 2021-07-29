namespace AntC.CodeGenerate.Core.Model.Db
{
    public class ColumnInfo
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
        /// 数字精度
        /// </summary>
        public long NumericPrecision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public long NumericScale { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 数据表信息
        /// </summary>
        public TableInfo TableInfo { get; set; }
    }
}

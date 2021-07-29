using System;
using SqlSugar;

namespace AntC.CodeGenerate.Mysql.Entities
{
    [SugarTable("tables")]
    internal class MysqlSchemaTable
    {
        /// <summary>
        /// 数据表登记目录
        /// </summary>
        public string TABLE_CATALOG { get; set; }
        /// <summary>
        /// 数据表所属的数据库名
        /// </summary>
        public string TABLE_SCHEMA { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TABLE_NAME { get; set; }
        /// <summary>
        /// 表类型[SYSTEM VIEW|BASE TABLE|VIEW]
        /// </summary>
        public string TABLE_TYPE { get; set; }
        /// <summary>
        /// 使用的数据库引擎[MyISAM|CSV|InnoDB]
        /// </summary>
        public string ENGINE { get; set; }
        /// <summary>
        /// 版本，默认值10
        /// </summary>
        public ulong? VERSION { get; set; }
        /// <summary>
        /// 行格式[Compact|Dynamic|Fixed]
        /// </summary>
        public string ROW_FORMAT { get; set; }
        /// <summary>
        /// 表里所存多少行数据
        /// </summary>
        public ulong? TABLE_ROWS { get; set; }
        /// <summary>
        /// 平均行长度
        /// </summary>
        public ulong? AVG_ROW_LENGTH { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public ulong? DATA_LENGTH { get; set; }
        /// <summary>
        /// 最大数据长度
        /// </summary>
        public ulong? MAX_DATA_LENGTH { get; set; }
        /// <summary>
        /// 索引长度
        /// </summary>
        public ulong? INDEX_LENGTH { get; set; }
        /// <summary>
        /// 空间碎片
        /// </summary>
        public ulong? DATA_FREE { get; set; }
        /// <summary>
        /// 做自增主键的自动增量当前值
        /// </summary>
        public ulong? AUTO_INCREMENT { get; set; }
        /// <summary>
        /// 表的创建时间
        /// </summary>
        public DateTime? CREATE_TIME { get; set; }
        /// <summary>
        /// 表的更新时间
        /// </summary>
        public DateTime? UPDATE_TIME { get; set; }
        /// <summary>
        /// 表的检查时间
        /// </summary>
        public DateTime? CHECK_TIME { get; set; }
        /// <summary>
        /// 表的字符校验编码集
        /// </summary>
        public string TABLE_COLLATION { get; set; }
        /// <summary>
        /// 校验和
        /// </summary>
        public ulong CHECKSUM { get; set; }
        /// <summary>
        /// 创建选项
        /// </summary>
        public string CREATE_OPTIONS { get; set; }
        /// <summary>
        /// 表的注释、备注
        /// </summary>
        public string TABLE_COMMENT { get; set; }
    }
}

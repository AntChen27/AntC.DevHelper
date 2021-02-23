﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.CodeGenerate.Model
{
    /// <summary>
    /// 数据库信息
    /// </summary>
    public class DbInfoModel
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// 表信息
        /// </summary>
        public IEnumerable<DbTableInfoModel> Tables { get; set; }
    }
}
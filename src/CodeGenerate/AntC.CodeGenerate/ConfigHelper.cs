using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Converts;
using AntC.CodeGenerate.Model;
using Newtonsoft.Json;

namespace AntC.CodeGenerate
{
    /// <summary>
    /// 配置读取与保存
    /// </summary>
    public class ConfigHelper
    {
        private static Encoding _defaultEncoding = Encoding.UTF8;

        private static string _dbConnectionsConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dbConnections.json");

        public static List<DbConnectionInfo> DbConnectionInfos { get; private set; }

        public static DbConnectionInfo CurrentDbConnectionInfo { get; set; }

        public static event Action<ConfigEntity> OnSave;

        public static ConfigEntity Config { get; set; }

        public static IEnumerable<DbConnectionInfo> Load()
        {
            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.Open, FileAccess.Read);
            using TextReader textReader = new StreamReader(stream, _defaultEncoding);
            var contentStr = textReader.ReadToEnd();
            Config = JsonConvert.DeserializeObject<ConfigEntity>(contentStr, new TableGroupInfoConvert());
            //DbConnectionInfos = JsonConvert.DeserializeObject<List<DbConnectionInfo>>(contentStr, new TableGroupInfoConvert());
            DbConnectionInfos = Config.DbConnInfos;
            if (CurrentDbConnectionInfo != null)
            {
                CurrentDbConnectionInfo = DbConnectionInfos.FirstOrDefault(x => CurrentDbConnectionInfo == null || CurrentDbConnectionInfo.Name == x.Name);
            }
            return DbConnectionInfos;
        }

        public static IEnumerable<DbConnectionInfo> SaveAndLoad()
        {
            Save();
            return Load();
        }

        public static void Save()
        {
            if (File.Exists(_dbConnectionsConfigPath))
            {
                File.Delete(_dbConnectionsConfigPath);
            }

            OnSave?.Invoke(Config);

            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.OpenOrCreate, FileAccess.Write);
            using TextWriter textWriter = new StreamWriter(stream, _defaultEncoding);
            var contentStr = JsonConvert.SerializeObject(Config, new TableGroupInfoConvert());
            textWriter.Write(contentStr);
        }

        public class ConfigEntity
        {
            /// <summary>
            /// 输出文件夹
            /// </summary>
            public string OutputFolder { get; set; }

            /// <summary>
            /// 完成代码生成后是否打开文件夹
            /// </summary>
            public bool IsOnFinishedOpenFolder { get; set; }

            /// <summary>
            /// 是否在运行前清空输出文件夹
            /// </summary>
            public bool IsClearFolderBeforeRunning { get; set; }

            /// <summary>
            /// 数据库连接信息
            /// </summary>
            public List<DbConnectionInfo> DbConnInfos { get; set; }
        }
    }
}

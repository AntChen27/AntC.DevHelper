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

        public static IEnumerable<DbConnectionInfo> Load()
        {
            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.Open, FileAccess.Read);
            using TextReader textReader = new StreamReader(stream, _defaultEncoding);
            var contentStr = textReader.ReadToEnd();
            DbConnectionInfos = JsonConvert.DeserializeObject<List<DbConnectionInfo>>(contentStr, new TableGroupInfoConvert());
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

            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.OpenOrCreate, FileAccess.Write);
            using TextWriter textWriter = new StreamWriter(stream, _defaultEncoding);
            var contentStr = JsonConvert.SerializeObject(DbConnectionInfos, new TableGroupInfoConvert());
            textWriter.Write(contentStr);
        }
    }
}

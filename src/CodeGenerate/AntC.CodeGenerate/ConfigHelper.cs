using System;
using System.Collections.Generic;
using System.IO;
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

        public static IEnumerable<DbConnectionInfo> LoadConnectionConfigs()
        {
            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.Open, FileAccess.Read);
            using TextReader textReader = new StreamReader(stream, _defaultEncoding);
            var contentStr = textReader.ReadToEnd();
            var dbConnectionConfigs = JsonConvert.DeserializeObject<List<DbConnectionInfo>>(contentStr, new TableGroupInfoConvert());
            return dbConnectionConfigs;
        }

        public static void SaveConnectionConfigs(IEnumerable<DbConnectionInfo> connectionInfos)
        {
            if (File.Exists(_dbConnectionsConfigPath))
            {
                File.Delete(_dbConnectionsConfigPath);
            }

            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.OpenOrCreate, FileAccess.Write);
            using TextWriter textWriter = new StreamWriter(stream, _defaultEncoding);
            var contentStr = JsonConvert.SerializeObject(connectionInfos, new TableGroupInfoConvert());
            textWriter.Write(contentStr);
        }
    }
}

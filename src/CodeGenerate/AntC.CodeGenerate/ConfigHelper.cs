using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            var dbConnectionConfigs = JsonConvert.DeserializeObject<List<DbConnectionInfo>>(contentStr);
            return dbConnectionConfigs;
        }

        public static void SaveConnectionConfigs(IEnumerable<DbConnectionInfo> connectionInfos)
        {
            using FileStream stream = new FileStream(_dbConnectionsConfigPath, FileMode.Open, FileAccess.Write);
            using TextWriter textWriter = new StreamWriter(stream, _defaultEncoding);
            var contentStr = JsonConvert.SerializeObject(connectionInfos);
            textWriter.Write(contentStr);
        }
    }
}

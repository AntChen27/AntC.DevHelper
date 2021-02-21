using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntC.DevHelper
{
    public class Output
    {
        public static string ToFile(string content, string fileName, string rootPath = null, Encoding encoding = null)
        {
            if (rootPath == null)
            {
                rootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output");
            }

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            string filepath = Path.Combine(rootPath, fileName);
            if (!Directory.Exists(Path.GetDirectoryName(filepath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            }

            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }

            byte[] data = encoding.GetBytes(content);
            using FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);

            fs.Write(data);

            return filepath;
        }
    }
}

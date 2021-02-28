using AntC.CodeGenerate.Interfaces;
using System;
using System.IO;
using System.Text;

namespace AntC.CodeGenerate.CodeWriters
{
    public class CodeFileWriter : ICodeFileWriter
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (value != null && _filePath != value)
                {
                    _filePath = value;
                    CreateDirectoryAndRemoveFile();
                }
            }
        }

        private StringBuilder _builder;
        private long _filePosition = 0;
        private long _bufferLength = 1024 * 8;

        private static int createTimes = 0;

        public CodeFileWriter()
        {
            _builder = new StringBuilder();
            createTimes++;
            Console.WriteLine($"创建CodeFileWriter {createTimes}次");
        }

        public void Append(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            _builder.Append(content);
            FlushOnBufferFull();
        }

        public void AppendLine(string content = null)
        {
            _builder.AppendLine(content);
            FlushOnBufferFull();
        }

        private void CreateDirectoryAndRemoveFile()
        {
            if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            }

            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        private void FlushOnBufferFull()
        {
            if (_builder.Length >= _bufferLength)
            {
                Flush();
            }
        }

        private void Flush()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                return;
            }
            using var stream = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.Position = _filePosition;
            var content = _builder.ToString();
            var data = Encoding.GetBytes(content);
            stream.Write(data);
            stream.Flush();
            _builder.Clear();
            //Console.WriteLine($"输出路径：{FilePath}");
            //Console.WriteLine($"输出内容：{content}");
            _filePosition = stream.Position;
        }

        public void Dispose()
        {
            Flush();
        }
    }
}

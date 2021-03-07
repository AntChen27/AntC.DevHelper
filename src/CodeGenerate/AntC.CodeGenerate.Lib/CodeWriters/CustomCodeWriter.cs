using System;
using System.Text;
using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeWriters
{
    public class CustomCodeWriter : ICodeWriter
    {
        public event Action<string> OnAppendContent;
        
        private readonly StringBuilder _builder;

        public long BufferLength { get; set; } = 1024 * 8;

        public CustomCodeWriter()
        {
            _builder = new StringBuilder();
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

        private void FlushOnBufferFull()
        {
            if (_builder.Length >= BufferLength)
            {
                Flush();
            }
        }

        private void Flush()
        {
            OnAppendContent?.Invoke(_builder.ToString());
            _builder.Clear();
        }

        public void Dispose()
        {
            Flush();
        }
    }
}

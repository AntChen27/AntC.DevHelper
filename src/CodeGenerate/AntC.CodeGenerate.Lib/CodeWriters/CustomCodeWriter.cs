using System;
using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeWriters
{
    public class CustomCodeWriter : ICodeWriter
    {
        public event Action<string> OnAppendContent;

        public void Dispose()
        {
        }

        public void Append(string content)
        {
            OnAppendContent(content);
        }

        public void AppendLine(string content = null)
        {
            Append(content);
            Append(Environment.NewLine);
        }
    }
}

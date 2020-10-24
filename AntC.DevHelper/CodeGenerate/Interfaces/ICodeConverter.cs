using System;
using System.Collections.Generic;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.Interfaces
{
    public interface ICodeConverter
    {
        string Convert(CodeType type, string value);
    }
}

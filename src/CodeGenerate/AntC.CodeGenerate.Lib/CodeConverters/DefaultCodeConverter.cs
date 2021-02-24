using System.Linq;
using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.CodeConverters
{
    public class DefaultCodeConverter : ICodeConverter
    {
        private const string SplitChar = "_.";

        public virtual string Convert(string value, CodeType type = CodeType.ClassName)
        {
            if (type == CodeType.ClassFileName
                || type == CodeType.Namespace
                || type == CodeType.ClassName
                || type == CodeType.PerportyName
                || type == CodeType.MethodName)
            {
                return FirstCharUpper(value);
            }
            if (type == CodeType.FieldName)
            {
                return FirstCharLower(value);
            }
            return value;
        }

        private string FirstCharUpper(string value)
        {
            return string.Join("", value.Split(SplitChar.ToCharArray())
                .Select(t => t.ToUpper().Substring(0, 1) + t.Substring(1)));
        }

        private string FirstCharLower(string value)
        {
            return string.Join("", value.Split(SplitChar.ToCharArray())
                .Select(t => "_" + t.ToLower().Substring(0, 1) + t.Substring(1)));
        }
    }
}

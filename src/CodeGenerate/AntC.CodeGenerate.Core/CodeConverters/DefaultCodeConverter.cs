using System.Linq;
using AntC.CodeGenerate.Core.Contracts;
using AntC.CodeGenerate.Core.Enum;

namespace AntC.CodeGenerate.Core.CodeConverters
{
    public class DefaultCodeConverter : ICodeConverter
    {
        private const string SplitChar = "_.";

        public virtual string Convert(string value, CodeType type = CodeType.ClassName)
        {
            if (type == CodeType.ClassFileName
                || type == CodeType.Namespace
                || type == CodeType.ClassName
                || type == CodeType.PropertyName
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
                .Select(t => string.IsNullOrEmpty(t) ? t : (t.Length > 1 ? (t.ToUpper().Substring(0, 1) + t.Substring(1)) : (t.ToUpper()))));
        }

        private string FirstCharLower(string value)
        {
            return string.Join("", value.Split(SplitChar.ToCharArray())
                .Select(t => string.IsNullOrEmpty(t) ? t : (t.Length > 1 ? (t.ToLower().Substring(0, 1) + t.Substring(1)) : (t.ToLower()))));
        }
    }
}

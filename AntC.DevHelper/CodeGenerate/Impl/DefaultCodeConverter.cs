using AntC.DevHelper.CodeGenerate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntC.DevHelper.CodeGenerate.Impl
{
    public class DefaultCodeConverter : ICodeConverter
    {
        //private readonly LittleCamelCaseCodeConverter _littleCamelCaseCodeConverter = new LittleCamelCaseCodeConverter();
        //private readonly BigCamelCaseCodeConverter _bigCamelCaseCodeConverter = new BigCamelCaseCodeConverter();

        public string Convert(CodeType type, string value)
        {
            if (type == CodeType.Namespace
                || type == CodeType.ClassName
                || type == CodeType.PerportyName
                || type == CodeType.MethodName)
            {
                //return _bigCamelCaseCodeConverter.Convert(type, value);
                return FirstCharUpper(value);
            }
            if (type == CodeType.FieldName)
            {
                //return _littleCamelCaseCodeConverter.Convert(type, value);
                return FirstCharLower(value);
            }

            return value;
        }

        private string FirstCharUpper(string value)
        {
            return string.Join("", value.Split("_")
                .Select(t => t.ToUpper().Substring(0, 1) + t.Substring(1)));
        }

        private string FirstCharLower(string value)
        {
            return string.Join("", value.Split("_")
                .Select(t => "_" + t.ToLower().Substring(0, 1) + t.Substring(1)));
        }

    }
}

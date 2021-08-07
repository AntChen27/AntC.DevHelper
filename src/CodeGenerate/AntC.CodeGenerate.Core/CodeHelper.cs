using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Core.CodeConverters;
using AntC.CodeGenerate.Core.Contracts;

namespace AntC.CodeGenerate.Core
{
    public class CodeHelper
    {
        public static ICodeConverter DefaultConverter = new DefaultCodeConverter();
        public static ICodeConverter BigCamelCaseCodeConverter = new BigCamelCaseCodeConverter();
        public static ICodeConverter LittleCamelCaseCodeConverter = new LittleCamelCaseCodeConverter();
        public static ICodeConverter PascalCodeConverter = new PascalCodeConverter();

        public static string FirstCharToUpper(string value,params char[] splitChars)
        {
            return string.Join("", value.Split(splitChars)
                .Select(t => t.ToUpper().Substring(0, 1) + t.Substring(1)));
        }

        public static string FirstCharToLower(string value,params char[] splitChars)
        {
            return string.Join("", value.Split(splitChars)
                .Select(t => t.ToLower().Substring(0, 1) + t.Substring(1)));
        }
    }
}

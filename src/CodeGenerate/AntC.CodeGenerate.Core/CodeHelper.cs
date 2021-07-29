using System;
using System.Collections.Generic;
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate
{
    public static class CodeGenerateContextExtension
    {
        public static string GetNameSpace(this CodeGenerateContext context)
        {
            return context.CodeConverter.Convert(context.CodeGenerateTableInfo.TableName, CodeType.Namespace);
        }

        public static string GetClassName(this CodeGenerateContext context)
        {
            return context.CodeConverter.Convert(context.DbTableInfoModel.TableName, CodeType.ClassName);
        }

        public static string GetClassFileName(this CodeGenerateContext context)
        {
            return context.CodeConverter.Convert(context.DbTableInfoModel.TableName, CodeType.ClassFileName);
        }
    }
}

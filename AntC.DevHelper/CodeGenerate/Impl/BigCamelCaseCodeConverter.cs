using AntC.DevHelper.CodeGenerate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AntC.DevHelper.CodeGenerate.Impl
{
    /// <summary>
    /// 大驼峰命名代码转换器
    /// </summary>
    public class BigCamelCaseCodeConverter : ICodeConverter
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        private const char charSplit = '_';

        public string Convert(CodeType type, string value)
        {
            if (type == CodeType.Namespace
                || type == CodeType.ClassName
                || type == CodeType.MethodName
                || type == CodeType.PerportyName)
            {
                return PieceString(value);
            }

            return value;
        }

        /// <summary>
        /// 跳过的字符串
        /// </summary>
        private List<string> listPass = new List<string>();

        /// <summary>
        /// 小写转换为驼峰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string ConvertString(string tb1)
        {
            Match mt = Regex.Match(tb1, @"_(\w*)*");
            while (mt.Success)
            {
                var item = mt.Value;
                if (!string.IsNullOrWhiteSpace(item))
                {
                    if (listPass.Contains(item.ToLower()))
                    {
                        continue;
                    }
                }
                while (item.IndexOf('_') >= 0)
                {
                    string newUpper = item.Substring(item.IndexOf(charSplit), 2);
                    item = item.Replace(newUpper, newUpper.Trim(charSplit).ToUpper());
                    tb1 = tb1.Replace(newUpper, newUpper.Trim(charSplit).ToUpper());
                }
                mt = mt.NextMatch();
            }

            return tb1;
        }

        /// <summary>
        /// 字符拼装
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string PieceString(string str)
        {
            StringBuilder strRes = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(str))
            {
                List<string> strList = str.Split(charSplit).ToList();
                foreach (var item in strList)
                {
                    if (listPass.Contains(item))
                    {
                        strRes.Append(item);
                    }
                    else
                    {
                        strRes.Append(ConvertString(charSplit + item.ToLower()));
                    }
                }
            }
            return strRes.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace AntC.CodeGenerate.Model
{
    public static class TemplateManager
    {
        public static string TemplateRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates");

        public static Dictionary<string, string> TemplateDic = new Dictionary<string, string>();

        public static void Init()
        {
            var templateServiceConfiguration = new TemplateServiceConfiguration();
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core");
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core.Enum");
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core.Model.CLR");
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core.Model.Db");
            //这里可以类引用解决器
            templateServiceConfiguration.ReferenceResolver = new MyReferenceResolver();
            Engine.Razor = RazorEngineService.Create(templateServiceConfiguration);
        }

        public static void LoadTemplates(bool clearCache = false)
        {
            if (clearCache)
            {
                TemplateDic.Clear();
            }
            LoadTemplates(new DirectoryInfo(TemplateRootPath));
        }

        private static void LoadTemplates(DirectoryInfo directoryInfo)
        {
            if (!directoryInfo.Exists)
            {
                return;
            }

            var fileInfos = directoryInfo.GetFiles("*.cshtml");

            foreach (var fileInfo in fileInfos)
            {
                LoadTemplate(fileInfo.FullName.Replace(TemplateRootPath, ""));
            }

            foreach (var directory in directoryInfo.GetDirectories())
            {
                LoadTemplates(directory);
            }
        }

        public static void LoadTemplate(string relativePath)
        {
            relativePath = relativePath.TrimStart('\\');
            //打开并且读取模板
            string template = File.ReadAllText(Path.Combine(TemplateRootPath, relativePath));
            //添加模板
            Engine.Razor.AddTemplate(relativePath, template);
            //编译模板
            Engine.Razor.Compile(relativePath, null);

            if (!TemplateDic.ContainsKey(relativePath))
            {
                TemplateDic.Add(relativePath, "");
            }
            else
            {
                TemplateDic[relativePath] = "";
            }
        }

        public static string Run<T>(string templatePath, T model)
        {
            return Engine.Razor.Run(templatePath, null, model);
        }
        public static string Run<T>(int idx, T model)
        {
            return Engine.Razor.Run(TemplateDic.Keys.ToList()[idx], null, model);
        }
    }
}

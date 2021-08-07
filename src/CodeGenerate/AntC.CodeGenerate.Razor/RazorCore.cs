using AntC.CodeGenerate.Core.Contracts;
using AntC.CodeGenerate.Core.Model.Db;
using AntC.CodeGenerate.Mysql;
using RazorEngine.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AntC.CodeGenerate.Core.Extension;

namespace AntC.CodeGenerate.Razor
{
    using RazorEngine;
    using RazorEngine.Templating; // For extension methods.

    /// <summary>
    /// 参考 https://blog.csdn.net/Tomato2313/article/details/109844812
    /// </summary>
    public class RazorCore
    {
        public static void Run2()
        {
            var templateServiceConfiguration = new TemplateServiceConfiguration();
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Model");
            //这里可以类引用解决器
            templateServiceConfiguration.ReferenceResolver = new MyReferenceResolver();
            Engine.Razor = RazorEngineService.Create(templateServiceConfiguration);

            //打开并且读取模板
            string template = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", "Entity.cshtml"));
            //添加模板
            Engine.Razor.AddTemplate("templateKey", template);
            //编译模板
            Engine.Razor.Compile("templateKey", null);

            var result = Engine.Razor.Run("templateKey", null, new TableInfo());

        }

        public static void Run1()
        {
            string template = "@using System\r\n@using AntC.CodeGenerate.Cmd\r\n  @{var tt = Model.TableName;\r\n Model.SetOutputFileName(tt+\"_Table\");\r\n Model.OutputFileName=tt+\"_Table1\";}\r\n Hello @Model.Name, welcome to RazorEngine!";

            //var result = Engine.Razor.RunCompile(template, "templateKey", typeof(CodeHelper), mode);
        }

        public static void Run()
        {
            var dbConnectionString = "server=10.3.1.233;port=3306;database=information_schema;User ID=root;Password=123456;";

            IDbInfoProvider dbInfoProvider = new MysqlDbInfoProvider()
            {
                DbConnectionString = dbConnectionString
            };
            var dbinfo = new DatabaseInfo() { DbName = "libra.kpidb" };
            //dbinfo = dbInfoProvider.GetTables(dbinfo);

            //var templateServiceConfiguration = new TemplateServiceConfiguration();
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core");
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core.Enum");
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core.Model.CLR");
            //templateServiceConfiguration.Namespaces.Add("AntC.CodeGenerate.Core.Model.Db");
            ////这里可以类引用解决器
            //templateServiceConfiguration.ReferenceResolver = new MyReferenceResolver();
            //Engine.Razor = RazorEngineService.Create(templateServiceConfiguration);

            ////打开并且读取模板
            //string template = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "templates", "Entity.cshtml"));
            ////添加模板
            //Engine.Razor.AddTemplate("Entity.cshtml", template);
            ////编译模板
            //Engine.Razor.Compile("Entity.cshtml", null);



            //var tableInfo = dbinfo.Tables.FirstOrDefault(x => x.TableName == "kpi_define");

            var classModel = dbInfoProvider.GetClassModel("libra.kpidb.debug", "kpi_define");
            //var result = Engine.Razor.Run("Entity.cshtml", null, classModel);

            TemplateManager.Init();
            TemplateManager.LoadTemplates();

            var result = TemplateManager.Run(0, classModel);
        }
    }
}

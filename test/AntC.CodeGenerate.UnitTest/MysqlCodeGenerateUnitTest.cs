using AntC.CodeGenerate.CodeConverters;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Mysql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Model;

namespace AntC.CodeGenerate.UnitTest
{
    [TestClass]
    public class MysqlCodeGenerateUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            //string dbConnectionString = "server=10.4.1.248;port=3310;database=information_schema;User ID=root;Password=123456;";
            string dbConnectionString = "server=localhost;port=3306;database=information_schema;User ID=root;Password=123456;";
            string dbName = "libra.kpidb";
            //string[] tableNames = new string[] { "kpi_define" };
            string[] tableNames = new string[] { "kpi_define_base", "kpi_define_ext" };

            var serviceProvider = Init();
            var codeGeneratorManager = InitGeneratorManager(serviceProvider, dbConnectionString);

            //codeGeneratorManager.AddCodeGenerator(serviceProvider.GetService<EntityCodeGenerateExecutor>());

            codeGeneratorManager.ExecCodeGenerate(new CodeGenerateInfo()
            {
                //OutPutRootPath = AppDomain.CurrentDomain.BaseDirectory,
                CodeGenerateTableInfos = tableNames.Select(x => new CodeGenerateTableInfo() { DbName = dbName, TableName = x })
            });
        }

        private IServiceProvider Init()
        {
            IServiceCollection services = new ServiceCollection();
            //×¢Èë
            //services.AddTransient<EntityCodeGenerateExecutor, EntityCodeGenerateExecutor>();
            services.AddTransient<MysqlDbInfoProvider, MysqlDbInfoProvider>();
            services.AddTransient<ICodeConverter, DefaultCodeConverter>();

            //¹¹½¨ÈÝÆ÷
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        private ICodeGeneratorManager InitGeneratorManager(IServiceProvider serviceProvider, string dbConnectionString)
        {
            var codeGeneratorManager = new CodeGeneratorManager(serviceProvider.GetService<MysqlDbInfoProvider>())
            {
                ServiceProvider = serviceProvider,
                CodeConverter = serviceProvider.GetService<ICodeConverter>(),
                DbConnectionString = dbConnectionString,
            };
            return codeGeneratorManager;
        }
    }
}

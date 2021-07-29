using AntC.CodeGenerate.Mysql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using AntC.CodeGenerate.CodeGenerateExecutors;
using AntC.CodeGenerate.Core.CodeConverters;
using AntC.CodeGenerate.Core.Contracts;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace AntC.CodeGenerate.UnitTest
{
    [TestClass]
    public class MysqlCodeGenerateUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dbConnectionString = "server=10.4.1.248;port=3310;database=information_schema;User ID=root;Password=123456;";
            //var dbConnectionString = "server=localhost;port=3306;database=information_schema;User ID=root;Password=123456;";
            var dbName = "libra.kpidb";
            var tableNames = new List<string>() { "kpi_define_base", "kpi_define_ext" };

            var serviceProvider = Init();

            var codeGeneratorManager = InitGeneratorManager(serviceProvider, dbConnectionString);

            codeGeneratorManager.AddCodeGenerator(typeof(Program).Assembly);
            codeGeneratorManager.AddPropertyTypeConverter(typeof(Program).Assembly);

            tableNames = codeGeneratorManager.GetTables(dbName).Select(x => x.TableName).ToList();
            tableNames.Remove("seed");

            var codeGenerateInfo = new CodeGenerateInfo()
            {
                //OutPutRootPath = AppDomain.CurrentDomain.BaseDirectory,
                DbName = dbName,
                CodeGenerateTableInfos = tableNames.Select(x => new CodeGenerateTableInfo()
                {
                    TableName = x,
                    GroupName = ""
                })
            };
            codeGeneratorManager.ExecCodeGenerate(codeGenerateInfo);
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

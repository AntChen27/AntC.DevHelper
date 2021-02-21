using System;
using System.Linq;
using AntC.CodeGenerate.CodeConverters;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Mysql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AntC.CodeGenerate.UnitTest
{
    [TestClass]
    public class MysqlCodeGenrateUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string dbConnectionString = "server=10.4.1.248;port=3310;database=information_schema;User ID=root;Password=123456;";
            var serviceProvider = Init();
            var codeGeneratorManager = InitGeneratorManager(serviceProvider, dbConnectionString);

            var dataBaseInfoModels = codeGeneratorManager.GetDataBases();
            var dbTableInfoModels = codeGeneratorManager.GetTables(dataBaseInfoModels.FirstOrDefault(x => x.DataBaseName.Contains("kpi"))?.DataBaseName ?? "libra.kpidb");
            var dbColumnInfoModels = codeGeneratorManager.GetColumns(dbTableInfoModels.FirstOrDefault(x => x.TableName == "kpi_define")
                ?.TableName);
        }

        private IServiceProvider Init()
        {
            IServiceCollection services = new ServiceCollection();
            //×¢Èë
            services.AddTransient<ICodeGeneratorManager>(provider =>
                new CodeGeneratorManager(provider.GetService<MysqlDbInfoProvider>())
                {
                    CodeConverter = provider.GetService<ICodeConverter>()
                });

            //services.AddTransient<IDbInfoProvider, MysqlDbInfoProvider>();
            services.AddTransient<MysqlDbInfoProvider, MysqlDbInfoProvider>();
            services.AddTransient<ICodeConverter, DefaultCodeConverter>();

            //¹¹½¨ÈÝÆ÷
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        private ICodeGeneratorManager InitGeneratorManager(IServiceProvider serviceProvider, string dbConnectionString)
        {
            var codeGeneratorManager = serviceProvider.GetService<ICodeGeneratorManager>();
            codeGeneratorManager.DbConnectionString = dbConnectionString;
            return codeGeneratorManager;
        }
    }
}

using AntC.CodeGenerate.CodeConverters;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntC.CodeGenerate.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbConnectionString = "server=10.4.1.248;port=3310;database=information_schema;User ID=root;Password=123456;";
            //var dbConnectionString = "server=localhost;port=3306;database=information_schema;User ID=root;Password=123456;";
            var dbName = "libra.kpidb";
            var tableNames = new List<string>() { "kpi_define_base", "kpi_define_ext" };

            var serviceProvider = Init(services =>
            {
                services
                    .AddTransient<Benchint.Libra.EntityCodeGenerateExecutor,
                       Benchint.Libra.EntityCodeGenerateExecutor>();
            });
            var codeGeneratorManager = InitGeneratorManager(serviceProvider, dbConnectionString);
            codeGeneratorManager.AddCodeGenerateExecutor(serviceProvider.GetService<Benchint.Libra.EntityCodeGenerateExecutor>());

            tableNames = codeGeneratorManager.GetTables(dbName).Select(x => x.TableName).ToList();

            codeGeneratorManager.ExecCodeGenerate(new CodeGenerateInfo()
            {
                //OutPutRootPath = AppDomain.CurrentDomain.BaseDirectory,
                CodeGenerateTableInfos = tableNames.Select(x => new CodeGenerateTableInfo() { DbName = dbName, TableName = x })
            });
        }

        private static IServiceProvider Init(Action<IServiceCollection> action = null)
        {
            IServiceCollection services = new ServiceCollection();
            //注入
            //services.AddTransient<EntityCodeGenerateExecutor, EntityCodeGenerateExecutor>();
            services.AddTransient<MysqlDbInfoProvider, MysqlDbInfoProvider>();
            services.AddTransient<ICodeConverter, DefaultCodeConverter>();

            action?.Invoke(services);

            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        private static ICodeGeneratorManager InitGeneratorManager(IServiceProvider serviceProvider, string dbConnectionString)
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

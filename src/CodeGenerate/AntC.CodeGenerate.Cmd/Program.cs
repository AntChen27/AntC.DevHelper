using AntC.CodeGenerate.CodeConverters;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Model;
using AntC.CodeGenerate.Mysql;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AntC.CodeGenerate.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            //var dbConnectionString = "server=10.4.1.248;port=3310;database=information_schema;User ID=root;Password=123456;";
            var dbConnectionString = "server=localhost;port=3306;database=information_schema;User ID=root;Password=123456;";
            var dbName = "libra.kpidb";
            var tableNames = new List<string>() { "kpi_define_base", "kpi_define_ext" };

            var serviceProvider = Init(services =>
            {
                services
                    .AddTransient<Benchint.Libra.EntityCodeGenerateExecutor,
                       Benchint.Libra.EntityCodeGenerateExecutor>();
                services
                    .AddTransient<Benchint.Libra.PropertyTypeConverters.EnumTypeConverter,
                        Benchint.Libra.PropertyTypeConverters.EnumTypeConverter>();
            });
            var codeGeneratorManager = InitGeneratorManager(serviceProvider, dbConnectionString);
            codeGeneratorManager.AddCodeGenerateExecutor(serviceProvider.GetService<Benchint.Libra.EntityCodeGenerateExecutor>());
            codeGeneratorManager.AddPropertyTypeConverter(serviceProvider.GetService<Benchint.Libra.PropertyTypeConverters.EnumTypeConverter>());

            tableNames = codeGeneratorManager.GetTables(dbName).Select(x => x.TableName).ToList();

            codeGeneratorManager.ExecCodeGenerate(new CodeGenerateInfo()
            {
                //OutPutRootPath = AppDomain.CurrentDomain.BaseDirectory,
                CodeGenerateTableInfos = tableNames.Select(x => new CodeGenerateTableInfo() { DbName = dbName, TableName = x })
            });

            var totalMemory = GC.GetTotalMemory(true);

            Console.WriteLine($"完成代码创建,共使用内存{GetMemorySizeWithUnit(totalMemory)}...");
        }

        private static string GetMemorySizeWithUnit(decimal byteSize, int jz = 0)
        {
            if (byteSize / 1024 > 1024)
            {
                return GetMemorySizeWithUnit(byteSize / 1024, jz + 1);
            }

            var unit = jz switch
            {
                0 => "B",
                1 => "KB",
                2 => "MB",
                3 => "GB",
                4 => "TB",
                5 => "PB",
            };
            return $"{Math.Round(byteSize / 1024, 2)}{unit}";
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

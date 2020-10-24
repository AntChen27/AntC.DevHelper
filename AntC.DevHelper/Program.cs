
using AntC.DevHelper.CodeGenerate;
using AntC.DevHelper.CodeGenerate.Impl;
using AntC.DevHelper.CodeGenerate.Interfaces;
using AntC.DevHelper.CodeGenerate.MysqlShema;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AntC.DevHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            //注入
            services.AddTransient<ClassGenerator, ClassGenerator>();
            services.AddTransient<IDbInfoProvider, MysqlDbInfoProvider>();
            services.AddTransient<ICodeConverter, DefaultCodeConverter>();

            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();


            //解析
            var classGenerator = serviceProvider.GetService<ClassGenerator>();
            var dbTableInfoModels = classGenerator.GetDbTableInfoModels("libra.kpidb");

            foreach (var dbTableInfoModel in dbTableInfoModels)
            {
                var str = dbTableInfoModel.ToClassContentString("Benchint.Libra.KpiStatService", serviceProvider.GetService<ICodeConverter>());
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }
    }
}

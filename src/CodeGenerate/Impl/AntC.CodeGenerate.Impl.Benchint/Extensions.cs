using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AntC.CodeGenerate.Impl.Benchint
{
    public static class Extensions
    {
        public static void UseBenchintCodeGenerateImpl(this ICodeGeneratorManager codeGeneratorManager)
        {
            codeGeneratorManager.AddCodeGenerator(typeof(Extensions).Assembly);
            codeGeneratorManager.AddPropertyTypeConverter(typeof(Extensions).Assembly);
        }

        /// <summary>
        /// 注入 代码生成执行器<see cref="ITableCodeGenerator"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection UseBenchintCodeGenerateImpl(this IServiceCollection services)
        {
            services.UseCodeGenerateExecutor(typeof(Extensions).Assembly);
            services.UsePropertyTypeConverter(typeof(Extensions).Assembly);
            return services;
        }

    }
}

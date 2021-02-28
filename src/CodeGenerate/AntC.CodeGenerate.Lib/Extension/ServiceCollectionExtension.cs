using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AntC.CodeGenerate.Interfaces;

namespace AntC.CodeGenerate.Extension
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// 注入 属性类型转换器<see cref="IPropertyTypeConverter"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection UsePropertyTypeConverter(this IServiceCollection services, Assembly assembly)
        {
            var targetType = typeof(IPropertyTypeConverter);
            assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetType.Name) != null)
                .ToList()
                .ForEach(x => { services.AddTransient(x); });
            return services;
        }

        /// <summary>
        /// 注入 代码生成执行器<see cref="ITableCodeGenerator"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection UseCodeGenerateExecutor(this IServiceCollection services, Assembly assembly)
        {
            var targetTypeDb = typeof(IDbCodeGenerator);
            var targetType = typeof(ITableCodeGenerator);
            assembly
               .GetTypes()
               .Where(x => x.GetInterface(targetType.Name) != null || x.GetInterface(targetTypeDb.Name) != null)
               .ToList()
               .ForEach(x => { services.AddTransient(x); });

            return services;
        }

        /// <summary>
        /// 注入 代码输出器<see cref="ICodeWriter"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection UseCodeWriter(this IServiceCollection services, Assembly assembly)
        {
            var targetType = typeof(ICodeWriter);
            assembly
                .GetTypes()
                .Where(x => x.GetInterface(targetType.Name) != null)
                .ToList()
                .ForEach(x => { services.AddTransient(x); });

            return services;
        }
    }
}

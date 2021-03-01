using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AntC.CodeGenerate
{
    public abstract class AbstractPlugin : IPlugin
    {
        public IServiceCollection LoadPlugin(IServiceCollection services)
        {
            var assembly = GetType().Assembly;
            services.UseCodeGenerateExecutor(assembly);
            services.UsePropertyTypeConverter(assembly);
            return services;
        }

        public IEnumerable<Type> GetCodeGenerators()
        {
            var targetTypeDb = typeof(IDbCodeGenerator);
            var targetType = typeof(ITableCodeGenerator);
            return GetType().Assembly
                 .GetTypes()
                 .Where(x => x.GetInterface(targetType.Name) != null || x.GetInterface(targetTypeDb.Name) != null)
                 .ToList();
        }
    }
}

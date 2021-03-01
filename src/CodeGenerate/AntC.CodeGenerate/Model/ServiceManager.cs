using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.CodeConverters;
using AntC.CodeGenerate.CodeWriters;
using AntC.CodeGenerate.Extension;
using AntC.CodeGenerate.Interfaces;
using AntC.CodeGenerate.Mysql;
using Microsoft.Extensions.DependencyInjection;

namespace AntC.CodeGenerate.Model
{
    public class ServiceManager
    {
        private static readonly IServiceCollection services = new ServiceCollection();

        public static IServiceProvider ServiceProvider { get => _serviceProvider; }
        private static IServiceProvider _serviceProvider;

        static ServiceManager()
        {
            Init();
        }

        private static void Init(Action<IServiceCollection> action = null)
        {
            //注入
            services.AddTransient<MysqlDbInfoProvider, MysqlDbInfoProvider>();
            services.AddTransient<ICodeConverter, DefaultCodeConverter>();

            services.UseCodeWriter(typeof(CodeFileWriter).Assembly);

            action?.Invoke(services);

            //构建容器
            _serviceProvider = services.BuildServiceProvider();
        }

        public static void AddService(Action<IServiceCollection> action)
        {
            action?.Invoke(services);

            //构建容器
            _serviceProvider = services.BuildServiceProvider();
        }

        public static ICodeGeneratorManager CreateGeneratorManager(DbConnectionInfo dbConnection)
        {
            Type dbProviderType = dbConnection.DbType switch
            {
                DbType.Mysql => typeof(MysqlDbInfoProvider),
                _ => throw new Exception($"不支持的数据库类型{dbConnection.DbType}")
            };
            var codeGeneratorManager = new CodeGeneratorManager((IDbInfoProvider)ServiceProvider.GetService(dbProviderType))
            {
                ServiceProvider = ServiceProvider,
                CodeConverter = ServiceProvider.GetService<ICodeConverter>(),
                DbConnectionString = dbConnection.ToConnectionString(),
            };
            return codeGeneratorManager;
        }
    }
}

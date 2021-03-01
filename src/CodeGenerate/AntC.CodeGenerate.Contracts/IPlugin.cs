using System;
using System.Collections.Generic;
using AntC.CodeGenerate.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AntC.CodeGenerate
{
    /// <summary>
    /// 代码创建器-插件
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// 加载插件
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection LoadPlugin(IServiceCollection services);

        IEnumerable<Type> GetCodeGenerators();
    }
}

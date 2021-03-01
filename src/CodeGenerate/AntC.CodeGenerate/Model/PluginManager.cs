using System;
using System.Collections.Generic;
using System.Text;
using AntC.CodeGenerate.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace AntC.CodeGenerate.Model
{
    public class PluginManager
    {
        private static List<IPlugin> plugins = new List<IPlugin>();

        public static void AddPlugin(IPlugin plugin)
        {
            plugins.Add(plugin);
            ServiceManager.AddService((service) =>
            {
                plugin.LoadPlugin(service);
            });
        }
    }
}

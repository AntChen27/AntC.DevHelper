using System.Collections.Generic;

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

        public static IEnumerable<IPlugin> GetPlugins()
        {
            return plugins;
        }
    }
}

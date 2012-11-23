namespace Sxta.Core.Plugins
{
    using System;
    using System.Reflection;
    using System.Collections.Specialized;
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    public interface IPluginManager
    {
        bool SetPluginDirectories(string[] PluginDirectories, bool ignoreDefaultCoreDirectory);

        void ScanForPlugins();

        StringCollection InsertPlugins(StringCollection PluginFiles);

        /// <summary>
        /// Returns a collection of all loaded plugins.
        /// </summary>
        PluginCollection Plugins
        {
            get;
        }

        /// <summary>
        /// Add a <see cref="Plugin"/> object to the tree, inserting all it's extensions.
        /// </summary>
        void InsertPlugin(IPlugin plugin);
    }
}

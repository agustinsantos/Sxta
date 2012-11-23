namespace Sxta.Core.Plugins
{
    using System;
    using System.Reflection;
    using System.Collections.Specialized;
    using System.IO;

    // Import log4net classes.
    using log4net;

    /// <summary>
    /// 
    /// </summary>
    public class PluginManager : IPluginManager
    {
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// 
        /// NOTE that using System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        /// is equivalent to typeof(LoggingExample) but is more portable
        /// i.e. you can copy the code directly into another class without
        /// needing to edit the code.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        readonly static string defaultCoreDirectory = "Plugins";

        static bool ignoreDefaultCoreDirectory = false;
        static string[] pluginDirectories = null;
        PluginCollection plugins = new PluginCollection();

        // ModuleManager singleton 
        static PluginManager instance = new PluginManager();

        /// <summary>
        /// Gets the default PluginManager
        /// </summary>
        public static PluginManager Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Don't create PluginManager objects, only have ONE per application.
        /// </summary>
        private PluginManager()
        {
        }


        public bool SetPluginDirectories(string[] PluginDirectories, bool ignoreDefaultCoreDir)
        {
            if (PluginDirectories == null || PluginDirectories.Length < 1)
            {
                // something went wrong
                return false;
            }
            pluginDirectories = PluginDirectories;
            ignoreDefaultCoreDirectory = ignoreDefaultCoreDir;
            return true;
        }

        public void ScanForPlugins()
        {
            StringCollection PluginFiles = new StringCollection();
            StringCollection retryList = null;

            if (ignoreDefaultCoreDirectory == false)
            {
                PluginFiles = SearchDirectory(defaultCoreDirectory, "*.plugin", PluginFiles, false, true);
                retryList = InsertPlugins(PluginFiles);
            }
            else
                retryList = new StringCollection();

            if (pluginDirectories != null)
            {
                foreach (string path in pluginDirectories)
                {
                    PluginFiles = SearchDirectory(path, "*.plugin", PluginFiles, false, true);
                    StringCollection partialRetryList = InsertPlugins(PluginFiles);
                    if (partialRetryList.Count != 0)
                    {
                        string[] retryListArray = new string[partialRetryList.Count];
                        partialRetryList.CopyTo(retryListArray, 0);
                        retryList.AddRange(retryListArray);
                    }
                }
            }

            while (retryList.Count > 0)
            {
                StringCollection newRetryList = InsertPlugins(retryList);

                // break if no add-in could be inserted.
                if (newRetryList.Count == retryList.Count)
                {
                    break;
                }

                retryList = newRetryList;
            }

            if (retryList.Count > 0)
            {
                throw new ApplicationException("At least one Plugin uses an undefined pluggable module: " + retryList[0]);
            }
        }

        /// <summary>
        /// Finds all files which are valid to the mask <code>filemask</code> in the path
        /// <code>directory</code> and all subdirectories (if searchSubdirectories
        /// is true. The found files are added to the StringCollection 
        /// <code>collection</code>.
        /// If <code>ignoreHidden</code> is true, hidden files and folders are ignored.
        /// </summary>
        private StringCollection SearchDirectory(string directory, string filemask, StringCollection collection,
                                                 bool searchSubdirectories, bool ignoreHidden)
        {
            try
            {
                string[] file = Directory.GetFiles(directory, filemask);
                foreach (string f in file)
                {
                    if (ignoreHidden && (File.GetAttributes(f) & FileAttributes.Hidden) == FileAttributes.Hidden)
                        continue;
                    collection.Add(f);
                }

                if (searchSubdirectories)
                {
                    string[] dir = Directory.GetDirectories(directory);
                    foreach (string d in dir)
                    {
                        if (ignoreHidden && (File.GetAttributes(d) & FileAttributes.Hidden) == FileAttributes.Hidden)
                            continue;
                        SearchDirectory(d, filemask, collection, searchSubdirectories, ignoreHidden);
                    }
                }
            }
            catch (Exception)
            {
                if (log.IsErrorEnabled)
                    log.Error("Can't access directory " + directory);
            }

            return collection;
        }

        public StringCollection InsertPlugins(StringCollection PluginFiles)
        {
            StringCollection retryList = new StringCollection();
            foreach (string pluginFile in PluginFiles)
            {
                IPlugin plugin = new Plugin();
                try
                {
                    plugin.Initialize(pluginFile);
                    InsertPlugin(plugin);
                }
                catch (Exception e)
                {
                    retryList.Add(pluginFile);
                    throw new PluginInitializeException(pluginFile, e);
                }
            }
            return retryList;
        }


        /// <summary>
        /// Returns a collection of all loaded plugins.
        /// </summary>
        public PluginCollection Plugins
        {
            get
            {
                return plugins;
            }
        }

        /// <summary>
        /// Add a <see cref="Plugin"/> object to the tree, inserting all it's extensions.
        /// </summary>
        public void InsertPlugin(IPlugin plugin)
        {
            plugins.Add(plugin);
        }
    }
}

namespace Sxta.Core.Plugins
{
    using System;
    using System.Reflection;
    using System.Collections.Generic;

    /// <summary>
    /// The <code>IPlugin</code> class handles the extensibility of the PluginTree by loading
    /// nodes to insert.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// returns the filename of the xml definition in which
        /// this IPlugin is defined.
        /// </summary>
        string FileName
        {
            get;
        }

        /// <summary>
        /// returns the Name of the IPlugin
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// returns the Author of the IPlugin
        /// </summary>
        string Author
        {
            get;
        }

        /// <summary>
        /// returns a copyright string of the IPlugin
        /// </summary>
        string Copyright
        {
            get;
        }

        /// <summary>
        /// returns a url of the homepage of the plugin
        /// or the author.
        /// </summary>
        string Url
        {
            get;
        }

        /// <summary>
        /// returns a brief description of what the plugin
        /// does.
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// returns the version of the plugin.
        /// </summary>
        string Version
        {
            get;
        }

        /// <summary>
        /// returns a hashtable with the runtime libraries
        /// where the key is the assembly name and the value
        /// is the assembly object.
        /// </summary>
        IDictionary<string, Assembly> RuntimeLibraries
        {
            get;
        }

        /// <summary>
        /// returns a arraylist with all extensions defined by
        /// this IPlugin.
        /// </summary>
        IList<Extension> Extensions
        {
            get;
        }


        /// <summary>
        /// Initializes this IPlugin. It loads the configuration definition in file
        /// fileName.
        /// </summary>
        void Initialize(string fileName);

        /// <summary>
        /// Creates an object which is related to this IPlugin.
        /// </summary>
        /// <exception cref="TypeNotFoundException">
        /// If className could not be created.
        /// </exception>
        object CreateObject(string className);

    }
}

namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections;
    
    /// <summary>
    /// This interface represents a tree node
    /// </summary>
    public interface IModulePathNode
    {
        String Path
        {
            get;
            set;
        }

        /// <value>
        /// A hash table containing the child nodes. Where the key is the
        /// node name and the value is a <see cref="IModulePathNode"/> object.
        /// </value>
        Hashtable ChildNodes
        {
            get;
        }

        /// <summary>
        /// Adds a module instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        void AddModuleInstance(IModule instance);

        /// <value>
        /// A module defined in this node, or <code>null</code> if no module
        /// was defined.
        /// </value>
        IModule Module
        {
            get;
            set;
        }

        /// <summary>
        /// Shortcut to the method <see cref="Resolve"/>
        /// </summary>
        IModule this[String id]
        {
            get;
        }

        /// <summary>
        /// Registers a subcontainer. The components exposed
        /// by this container will be accessible from subcontainers.
        /// </summary>
        /// <param name="childContainer"></param>
        void AddModulePathNode(IModulePathNode childNode);

        /// <summary>
        /// Gets or sets the parent container if this instance
        /// is a sub container.
        /// </summary>
        IModulePathNode Parent
        {
            get;
            set;
        }
    }
}

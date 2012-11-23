using System;
using System.Collections;
namespace Sxta.Core.Plugins
{
	/// <summary>
	/// This class represents a module path node
	/// </summary>
	public class DefaultModulePathNode : IModulePathNode
	{
        Hashtable childNodes = new Hashtable();
        IModule module = null;
        IModulePathNode parent = null;
        string path = null;

        internal DefaultModulePathNode(string pathName, IModulePathNode parentNode)
        {
            path = pathName;
            parent = parentNode;
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }

        }

		/// <value>
		/// A hash table containing the child nodes. Where the key is the
		/// node name and the value is a <see cref="IModulePathNode"/> object.
		/// </value>
        public Hashtable ChildNodes
        {
            get
            {
                return childNodes;
            }
        }

        /// <summary>
        /// Adds a module instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        public void AddModuleInstance(IModule instance)
        {
        }

        /// <value>
        /// A module defined in this node, or <code>null</code> if no module
        /// was defined.
        /// </value>
        public IModule Module
        {
            get
            {
                return module;
            }
            set
            {
                module = value;
            }
        }


        /// <summary>
        /// Shortcut to the method <see cref="Resolve"/>
        /// </summary>
        public IModule this[String id]
        {
            get
            {
                IModulePathNode node = (IModulePathNode)childNodes[id];
                if (node != null)
                    return node.Module;
                else
                    return null;
            }
        }

        /// <summary>
        /// Registers a subcontainer. The components exposed
        /// by this container will be accessible from subcontainers.
        /// </summary>
        /// <param name="childContainer"></param>
        public void AddModulePathNode(IModulePathNode childNode)
        {
            childNode.Parent = this;

        }

        /// <summary>
        /// Gets or sets the parent container if this instance
        /// is a sub container.
        /// </summary>
        public IModulePathNode Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }
	}
}

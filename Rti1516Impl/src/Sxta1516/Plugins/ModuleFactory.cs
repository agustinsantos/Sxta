namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections;
    using System.Xml;

	/// <summary>
	/// Creates a new <code>IModule</code> object.
	/// </summary>
	public class ModuleFactory
	{
		Hashtable moduleHashtable = new Hashtable();
		
		/// <remarks>
		/// Adds a new builder to this factory. After the builder is added
		/// Modules from the builder type can be created by the factory
		/// </remarks>
		/// <exception cref="DuplicateModuleException">
		/// Is thrown when a Module builder with the same <code>ModuleName</code>
		/// was already inserted
		/// </exception>
		public void AddModuleBuilder(ModuleBuilder builder)
		{
			if (moduleHashtable[builder.ModuleName] != null) {
				throw new DuplicateModuleException(builder.ModuleName);
			}
			moduleHashtable[builder.ModuleName] = builder;
		}
		
		/// <remarks>
		/// Creates a new <code>IModule</code> object using  <code>ModuleNode</code>
		/// as a mark of which builder to take for creation.
		/// </remarks>
		public IModule CreateModule(Sxta.Core.Plugins.IPlugin plugin, XmlNode moduleNode)
		{

            //TODO TODO TODO. 
            if (moduleNode.Attributes.Count >= 2 && moduleNode.Attributes["id"] != null && moduleNode.Attributes["class"] != null)
            {
                IModule module = (IModule)plugin.CreateObject(moduleNode.Attributes["class"].Value);

                if (module == null)
                {
                    ModuleBuilder builder = moduleHashtable[moduleNode.Name] as ModuleBuilder;
                    if (builder != null)
                    {
                        module = builder.BuildModule(plugin, moduleNode.Attributes["class"].Value);
                    }

                }
                return module;
            }

            throw new ModuleNotFoundException(String.Format("no Module builder found for <{0}>", moduleNode.Name));
		}
	}
}

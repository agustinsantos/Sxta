namespace Sxta.Core.Plugins
{
	using System;
	using System.Collections;
    using System.Reflection;

	/// <summary>
	/// This class does basic Module handling for you.
	/// </summary>
	public interface IModuleManager
	{
        /// <summary>
        /// Adds a module instance.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        void AddModuleInstance(String key, IModule instance);
		
        void AddModuleFactory(ModuleFactory factory);

        /// <summary>
        /// Adds modules with some key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        void AddModules(string path, IModule[] Modules);
        
        /// <summary>
        /// This method loads an assembly and gets all 
        /// it's defined modules
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>the assembly loaded</returns>
        Assembly LoadModulesInAssembly(string fileName);

		/// <remarks>
		/// This method initializes the Modules according to the provided key.
		/// </remarks>
		void InitializeModules(string key);
        
        /// <remarks>
        /// Calls InitializeModule on all Modules.
        /// </remarks>
        void InitializeAllModules();

        /// <remarks>
        /// This method initializes the Modules according to the provided key.
        /// </remarks>
        void UnloadModules(string key);
        
        /// <remarks>
		/// Calls UnloadModule on all Modules.
		/// </remarks>
		void UnloadAllModules();
		
		
		/// <remarks>
		/// Requestes a specific Module, may return null if this Module is not found.
		/// </remarks>
		IModule GetModule(string key);
	}
}

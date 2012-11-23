namespace Sxta.Core.Plugins
{
    using System;
    using System.Reflection;

    // Import log4net classes.
    using log4net;

	/// <summary>
	/// This builder builds a new module
	/// </summary>
	public class ModuleBuilder
	{
        /// <summary>
        /// Define a static logger variable so that it references the
        ///	Logger instance.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		Assembly assembly;
		string className;
		string moduleName;
		
		/// <summary>
		/// Initializes a new ModuleBuilder instance with beeing
		/// className the name of the condition class and assembly the
		/// assembly in which the class is defined.
		/// </summary>
        public ModuleBuilder(string className, Assembly assembly)
		{
			this.assembly  = assembly;
			this.className = className;
			
			// get Module name from attribute
			ModuleNameAttribute ModuleNameAttribute = (ModuleNameAttribute)Attribute.GetCustomAttribute(assembly.GetType(ClassName), typeof(ModuleNameAttribute));
			moduleName = ModuleNameAttribute.Name;
		}
		
		/// <summary>
		/// Returns the className the name of the condition class;
		/// </summary>
		public string ClassName {
			get {
				return className;
			}
		}
		
		/// <summary>
		/// Returns the name of the Module (it is used to determine which xml element
		/// represents which Module.
		/// </summary>
		public string ModuleName {
			get {
				return moduleName;
			}
		}
		
		/// <summary>
		/// Returns a newly build <code>IModule</code> object.
		/// </summary>
        public IModule BuildModule(IPlugin plugin, string classToBuild)
		{
            IModule module;
			try {
				// create instance (ignore case)
                module = (IModule)assembly.CreateInstance(classToBuild, true);
			} catch 
            {
				module = null;
			}
			return module;
		}
		
	}
}

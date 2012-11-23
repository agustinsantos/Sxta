namespace Sxta.Core.Plugins
{
    using System;
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using Sxta.Core.Plugins;

    /// <summary>
    /// This class does basic Module handling for you.
    /// </summary>
    public class ModuleManager : IModuleManager, IModulePath
    {
        List<IModule> moduleList = new List<IModule>();
        Hashtable modulesHashtable = new Hashtable();
        ArrayList moduleProviderList = new ArrayList();
        ModuleFactory moduleFactory = new ModuleFactory();

        static IModulePathNode pathRoot = new DefaultModulePathNode("/", null);

        // ModuleManager singleton 
        static ModuleManager defaultModuleManager = new ModuleManager();

        /// <summary>
        /// Gets the default ModuleManager
        /// </summary>
        public static ModuleManager Instance
        {
            get { return defaultModuleManager; }
        }

        /// <summary>
        /// Don't create ModuleManager objects, only have ONE per application.
        /// </summary>
        private ModuleManager()
        {
            //AddModuleInstance("/System/Kernel/Services/FileService", new FileUtilityService());
        }

        /// <summary>
        /// Gets the root Node.
        /// </summary>
        public IModulePathNode Root
        {
            get { return pathRoot; }
        }

        /// <summary>
        /// Gets the ModuleFactory.
        /// </summary>
        public ModuleFactory ModuleFactory
        {
            get { return moduleFactory; }
        }

        /// <summary>
        /// Gets a reference to the internal Module list.
        /// </summary>
        public List<IModule> ModulesEnumerator
        {
            get { return moduleList; }
        }


        /// <summary>
        /// Adds a module instance.
        /// </summary>
        /// <param name="path">the path</param>
        /// <param name="instance">The IModule to be inserted</param>
        public void AddModuleInstance(String path, IModule instance)
        {
            IModulePathNode node = CreatePath(path);
            node.Module = instance;
            moduleList.Add(instance);
        }


        public void AddModuleFactory(ModuleFactory factory)
        {
        }

        /// <summary>
        /// Adds modules with some key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="instance"></param>
        public void AddModules(string path, IModule[] modules)
        {
            foreach (IModule module in modules)
            {
                AddModuleInstance(path, module);
            }
        }


        /// <summary>
        /// This method loads an assembly and gets all 
        /// it's defined modules
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>the assembly loaded</returns>
        public Assembly LoadModulesInAssembly(string fileName)
        {
            return null;
        }

        /// <remarks>
        /// This method initializes the Modules according to the provided key.
        /// </remarks>
        public void InitializeModules(string modulesPath)
        {
            // add plugin tree Modules
            IModulePathNode node = ResolvePath(modulesPath);
            if (node == null)
                return;
            Hashtable modules = node.ChildNodes;

            // initialize all Modules
            foreach (IModulePathNode moduleNode in modules.Values)
            {
                DateTime now = DateTime.Now;
                if (moduleNode.Module != null)
                    moduleNode.Module.InitializeModule();
            }
        }

        /// <remarks>
        /// Calls InitializeModule on all Modules.
        /// </remarks>
        public void InitializeAllModules()
        {
            foreach (IModule module in moduleList)
            {
                module.InitializeModule();
            }
        }

        /// <remarks>
        /// This method initializes the Modules according to the provided key.
        /// </remarks>
        public void UnloadModules(string key)
        {
        }

        /// <remarks>
        /// Calls UnloadModule on all Modules.
        /// </remarks>
        public void UnloadAllModules()
        {
            foreach (IModule module in moduleList)
            {
                module.UnloadModule();
            }
        }


        /// <remarks>
        /// Requestes a specific Module, may return null if this Module is not found.
        /// </remarks>
        public IModule GetModule(string path)
        {
            IModulePathNode node = ResolvePath(path);
            if (node != null)
                return node.Module;
            else
                return null;
        }

        public void AddModuleProvider(string className, Assembly assembly)
        {
            ModuleBuilder provider = new ModuleBuilder(className, assembly);
            moduleProviderList.Add(provider);
        }

        bool IsInstanceOfType(Type type, IModule Module)
        {
            Type ModuleType = Module.GetType();

            foreach (Type iface in ModuleType.GetInterfaces())
            {
                if (iface == type)
                {
                    return true;
                }
            }

            while (ModuleType != typeof(System.Object))
            {
                if (type == ModuleType)
                {
                    return true;
                }
                ModuleType = ModuleType.BaseType;
            }
            return false;
        }

        /// <remarks>
        /// Requestes a specific Module, may return null if this Module is not found.
        /// </remarks>
        public IModule GetModule(Type ModuleType)
        {
            IModule s = (IModule)modulesHashtable[ModuleType];
            if (s != null)
            {
                return s;
            }

            foreach (IModule Module in moduleList)
            {
                if (IsInstanceOfType(ModuleType, Module))
                {
                    modulesHashtable[ModuleType] = Module;
                    return Module;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns a IModulePathNode instance by the path
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IModulePathNode ResolvePath(String path)
        {
            if (path == null || path.Length == 0)
            {
                return null;
            }

            string[] splittedPath = path.Split(new char[] { '/' });
            IModulePathNode curPath = pathRoot;
            int i = 0;

            while (i < splittedPath.Length)
            {
                if (splittedPath[i].Length != 0)
                {
                    IModulePathNode nextPath = (IModulePathNode)curPath.ChildNodes[splittedPath[i]];
                    if (nextPath == null)
                    {
                        return null;
                    }
                    curPath = nextPath;
                }
                ++i;
            }

            return curPath;
        }

        /// <summary>
        /// Shortcut to the method <see cref="ResolvePath"/>
        /// </summary>
        public IModule this[String path]
        {
            get
            {
                return GetModule(path);
            }
        }

        public IModulePathNode CreatePath(string path)
        {
            if (path == null || path.Length == 0)
            {
                return pathRoot;
            }

            string[] splittedPath = path.Split(new char[] { '/' });
            IModulePathNode curPath = pathRoot;
            int i = 0;

            while (i < splittedPath.Length)
            {
                if (splittedPath[i].Length != 0)
                {
                    IModulePathNode nextPath = (IModulePathNode)curPath.ChildNodes[splittedPath[i]];
                    if (nextPath == null)
                    {
                        curPath.ChildNodes[splittedPath[i]] = nextPath = new DefaultModulePathNode(splittedPath[i], curPath);
                    }
                    curPath = nextPath;
                }
                ++i;
            }

            return curPath;
        }

        Hashtable registeredAssemblies = new Hashtable();

        /// <summary>
        /// This method loads an assembly and gets all 
        /// it's defined modules
        /// </summary>
        public Assembly LoadAssembly(string fileName)
        {
            bool fileExists = false;
            if (File.Exists(fileName))
            {
                fileExists = true;
                fileName = Path.GetFullPath(fileName);
            }

            Assembly assembly = (Assembly)registeredAssemblies[fileName];

            if (assembly == null)
            {
                Assembly asm = null;
                if (fileExists)
                {
                    asm = Assembly.LoadFrom(fileName);
                }
                if (asm == null)
                {
                    asm = Assembly.Load(fileName);
                }
#if TODO
                if (asm == null)
                {
                    asm = Assembly.LoadWithPartialName(fileName);
                }
#endif
                registeredAssemblies[fileName] = assembly = asm;
                LoadModulesAndFactories(assembly);
            }

            return assembly;
        }
        /// <summary>
        /// This method does load all modules and factories in the given assembly.
        /// </summary>
        void LoadModulesAndFactories(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (!type.IsAbstract)
                {
                    if (type.IsSubclassOf(typeof(AbstractModule)) && Attribute.GetCustomAttribute(type, typeof(ModuleNameAttribute)) != null)
                    {
                        moduleFactory.AddModuleBuilder(new ModuleBuilder(type.FullName, assembly));
                    }
                }
            }
        }
    }
}

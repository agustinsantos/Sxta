namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections.Generic;

    public class AbstractModule : IModule
    {
        string id = null;
        string name = null;
        string myClass = null;

        /// <summary>
        /// returns the name of the xml node of this Module.
        /// </summary>
        public string Name
        {
            get
            {
                if (name == null)
                {
                    ModuleNameAttribute moduleName = (ModuleNameAttribute)Attribute.GetCustomAttribute(GetType(), typeof(ModuleNameAttribute));
                    if (moduleName != null)
                    {
                        name = moduleName.Name;
                    }
                    else
                        name = "";
                }
                return name;
            }
        }

        /// <summary>
        /// Returns the uniqe ID of the Module.
        /// </summary>
        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Returns the class attribute of the module
        /// (this is optional, but for most modules useful, therefore
        /// it is in the base class).
        /// </summary>
        public string Class
        {
            get
            {
                return myClass;
            }
            set
            {
                myClass = value;
            }
        }

        public virtual void InitializeModule()
        {
            initialized = true;
            OnInitialize(new ModuleEventArgs(this));
        }

        public bool Initialized
        {
            get { return initialized; }
            set { initialized = value; }
        }

        public virtual void ReplaceModule(IModule newModule)
        {
            OnReplacement(new ModuleReplacementEventArgs(this, newModule));
        }

        public virtual void UnloadModule()
        {
            OnUnload(new ModuleEventArgs(this));
        }

        protected virtual void OnInitialize(ModuleEventArgs e)
        {
            if (Initialize != null)
            {
                Initialize(this, e);
            }
        }

        protected virtual void OnReplacement(ModuleReplacementEventArgs e)
        {
            if (Replacement != null)
            {
                Replacement(this, e);
            }
        }

        protected virtual void OnUnload(ModuleEventArgs e)
        {
            if (Unload != null)
            {
                Unload(this, e);
            }
        }

        public event ModuleEventHandler Initialize;
        public event ModuleReplacementEventHandler Replacement;
        public event ModuleEventHandler Unload;

        /// <summary> The initialization state of this Module.</summary>
        private bool initialized;
    }
}

namespace Sxta.Core.Plugins
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This interface must be implemented by all modules.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// This method is called after the Modules are loaded.
        /// </summary>
        void InitializeModule();

        /// <summary>
        /// This method is called before the Module is unloaded.
        /// </summary>
        void UnloadModule();

        /// <summary>
        /// This method is called after the Module are replaced.
        /// </summary>
        void ReplaceModule(IModule newModule);

        /// <summary>
        /// returns the name of the xml node of this Module. (it is the same
        /// for each Module type)
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// returns the ID of this Module object.
        /// </summary>
        string ID
        {
            get;
        }

        /// <summary>
        /// returns the Class which is used in the action corresponding to
        /// this module (may return null, if no action for this module is
        /// given)
        /// </summary>
        string Class
        {
            get;
        }

        event ModuleEventHandler Initialize;
        event ModuleEventHandler Unload;
        event ModuleReplacementEventHandler Replacement;
    }
}

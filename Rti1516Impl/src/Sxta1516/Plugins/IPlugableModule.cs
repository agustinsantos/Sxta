namespace Sxta.Core.Plugins
{
    using System;
    
    /// <summary>
    /// This interface must be implemented by all modules.
    /// </summary>
    public interface IPluggableModule : IModule
    {
        /// <summary>
        /// This method is called after the Modules are registed.
        /// </summary>
        void RegisterModule();

        /// <summary>
        /// This method is called before the Module is deregisted.
        /// </summary>
        void DeregisterModule();

        event ModuleEventHandler Register;
        event ModuleEventHandler Deregister;
    }
}

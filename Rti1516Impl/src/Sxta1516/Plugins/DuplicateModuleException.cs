namespace Sxta.Core.Plugins
{
    using System;

    /// <summary>
    /// Is thrown when the system finds a duplicate module.
    /// </summary>
    public class DuplicateModuleException : Exception
    {
        /// <summary>
        /// Constructs a new <see cref="DuplicateModuleException"/> instance.
        /// </summary>
        public DuplicateModuleException(string serv)
            : base("there already exists a module with name : " + serv)
        {
        }
    }
}

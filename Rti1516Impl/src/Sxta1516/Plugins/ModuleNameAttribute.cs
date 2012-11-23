namespace Sxta.Core.Plugins
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Indicates that class represents a Module.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ModuleNameAttribute : Attribute
    {
        string name;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ModuleNameAttribute(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Returns the name of the Module.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}

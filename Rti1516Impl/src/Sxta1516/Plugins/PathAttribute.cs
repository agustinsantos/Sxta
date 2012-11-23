namespace Sxta.Core.Plugins
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Field, Inherited = true)]
    public class PathAttribute : Attribute
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public PathAttribute()
        {
        }
    }
}

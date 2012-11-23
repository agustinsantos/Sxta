namespace Sxta.Rti1516.Reflection
{
    using System;

    /// <summary>
    /// That class represents a HLAReflectionAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class HLAReflectionAttribute : Attribute
    {

        static public bool useAOP = true;
        protected bool useExternalConfig = false;

        protected HLAreflection baseInfo = new HLAreflection();

        public string Name
        {
            get { return baseInfo.Name; }
            set { baseInfo.Name = value; }
        }

        public string NameNotes
        {
            get { return baseInfo.NameNotes; }
            set { baseInfo.NameNotes = value; }
        }

        /// <summary>
        /// Indicates whether the attribute must be constructs using external configuration data
        /// </summary>
        public bool UseExternalConfig
        {
            get
            {
                return useExternalConfig;
            }
            set
            {
                useExternalConfig = value;
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAReflectionAttribute()
        {
        }
    }
}

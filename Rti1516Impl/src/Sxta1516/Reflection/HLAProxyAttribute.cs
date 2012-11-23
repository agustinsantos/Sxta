namespace Sxta.Rti1516.Reflection
{
    using System;

	/// <summary>
	/// That class  stores information about a Proxy object. 
    /// It is used in the activation process of a proxy object.
	/// </summary>
    public class HLAProxyAttribute
    {
        /// <summary> The object class handle.</summary>
        private long objectClassHandle;

        /// <summary> The object instance handle.</summary>
        private long objectInstanceHandle;

        /// <summary> The object name.</summary>
        private System.String objectName;

        /// <summary>
        /// Indicates the object class handle
        /// </summary>
        public long ObjectClassHandle
        {
            get
            {
                return objectClassHandle;
            }
            set
            {
                objectClassHandle = value;
            }
        }

        /// <summary>
        /// Indicates the object class handle
        /// </summary>
        public string ObjectName
        {
            get
            {
                return objectName;
            }
            set
            {
                objectName = value;
            }
        }

        /// <summary>
        /// Indicates the object instance handle
        /// </summary>
        public long ObjectInstanceHandle
        {
            get
            {
                return objectInstanceHandle;
            }
            set
            {
                objectInstanceHandle = value;
            }
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public HLAProxyAttribute()
        {
        }
    }
}

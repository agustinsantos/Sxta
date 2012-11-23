namespace Sxta.Rti1516.Reflection
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.XrtiHandles;

    /// <summary> 
    /// Describes an object instance.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    public class ObjectInstanceDescriptor
    {
        // PATCH ANGEL
        private long federationExecutionHandle;
        virtual public long FederationExecutionHandle
        {
            get { return federationExecutionHandle; }
        }
        // END PATCH

        /// <summary> 
        /// Returns the name of this object instance.
        /// </summary>
        /// <returns> the name of this object instance
        /// </returns>
        virtual public System.String Name
        {
            get { return name; }
        }

        /// <summary> 
        /// Returns the handle of this object instance.
        /// </summary>
        /// <returns> the handle of this object instance
        /// </returns>
        virtual public IObjectInstanceHandle Handle
        {
            get { return handle; }
        }

        /// <summary> 
        /// Returns the handle of the object instance class.
        /// </summary>
        /// <returns> the handle of the object instance class
        /// </returns>
        virtual public IObjectClassHandle ClassHandle
        {
            get { return classHandle; }
        }

        /// <summary> 
        /// Returns the set of attribute handles representing the
        /// attributes owned by the federate.
        /// </summary>
        /// <returns> the set of attribute handles representing the
        /// attributes owned by the federate
        /// </returns>
        virtual public IAttributeHandleSet OwnedAttributes
        {
            get { return ownedAttributes; }
        }

        /// <summary> The name of the object instance.</summary>
        private System.String name;

        /// <summary> The handle of the object instance.</summary>
        private IObjectInstanceHandle handle;

        /// <summary> The handle of the object instance class.</summary>
        private IObjectClassHandle classHandle;

        /// <summary> The handles of the attributes owned by the federate.</summary>
        private IAttributeHandleSet ownedAttributes;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pName">the name of the object instance
        /// </param>
        /// <param name="pHandle">the handle of the object instance
        /// </param>
        /// <param name="pClassHandle">the handle of the object instance class
        /// </param>
        public ObjectInstanceDescriptor(System.String pName, IObjectInstanceHandle pHandle, IObjectClassHandle pClassHandle, long pFederationHandle)
        {
            name = pName;
            handle = pHandle;
            classHandle = pClassHandle;
            federationExecutionHandle = pFederationHandle;

            ownedAttributes = new XRTIAttributeHandleSet();
        }
    }
}
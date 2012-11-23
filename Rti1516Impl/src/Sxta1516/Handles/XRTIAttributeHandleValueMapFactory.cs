namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> 
    /// Factory for <code>IAttributeHandleValueMap</code> instances.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIAttributeHandleValueMapFactory : IAttributeHandleValueMapFactory
    {
        /// <summary> Creates a new <code>IAttributeHandleValueMap</code> with the
        /// specified initial capacity.
        /// 
        /// </summary>
        /// <param name="capacity">the initial capacity of the map
        /// </param>
        /// <returns> the newly created <code>AttributeHandleMap</code>
        /// </returns>
        public virtual IAttributeHandleValueMap Create(int capacity)
        {
            return new XRTIAttributeHandleValueMap(capacity);
        }
    }
}
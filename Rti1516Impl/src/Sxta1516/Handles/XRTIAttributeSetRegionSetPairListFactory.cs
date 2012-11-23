namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> 
    /// Factory for <code>IAttributeSetRegionSetPairList</code> instances.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIAttributeSetRegionSetPairListFactory : IAttributeSetRegionSetPairListFactory
    {
        /// <summary> Creates and returns a new <code>IAttributeSetRegionSetPairList</code>
        /// instance with the specified initial capacity.
        /// 
        /// </summary>
        /// <param name="capacity">the initial capacity of the list
        /// </param>
        /// <returns> the newly created list
        /// </returns>
        public virtual IAttributeSetRegionSetPairList Create(int capacity)
        {
            return new XRTIAttributeSetRegionSetPairList(capacity);
        }
    }
}
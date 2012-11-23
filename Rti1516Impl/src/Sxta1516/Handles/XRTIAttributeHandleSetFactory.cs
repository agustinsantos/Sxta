namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> A factory for <code>IAttributeHandleSet</code>s.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIAttributeHandleSetFactory : AttributeHandleSetFactory
    {
        /// <summary> Creates and returns a new <code>IAttributeHandleSet</code>.
        /// 
        /// </summary>
        /// <returns> the newly created <code>IAttributeHandleSet</code>
        /// </returns>
        public virtual IAttributeHandleSet Create()
        {
            return new XRTIAttributeHandleSet();
        }
    }
}
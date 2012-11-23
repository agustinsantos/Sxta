namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> A factory for <code>IDimensionHandleSet</code> instances.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIDimensionHandleSetFactory : IDimensionHandleSetFactory
    {
        /// <summary> Creates and returns a new <code>IDimensionHandleSet</code>.
        /// 
        /// </summary>
        /// <returns> the newly created <code>IDimensionHandleSet</code>
        /// </returns>
        public virtual IDimensionHandleSet Create()
        {
            return new XRTIDimensionHandleSet();
        }
    }
}
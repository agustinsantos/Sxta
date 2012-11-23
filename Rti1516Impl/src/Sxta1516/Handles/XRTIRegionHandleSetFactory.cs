namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;

    /// <summary> 
    /// A factory for <code>IRegionHandleSet</code>s.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIRegionHandleSetFactory : IRegionHandleSetFactory
    {
        /// <summary>
        ///  Creates and returns a new <code>IRegionHandleSet</code>.
        /// </summary>
        /// <returns> the newly created <code>IRegionHandleSet</code>
        /// </returns>
        public virtual IRegionHandleSet Create()
        {
            return new XRTIRegionHandleSet();
        }
    }
}
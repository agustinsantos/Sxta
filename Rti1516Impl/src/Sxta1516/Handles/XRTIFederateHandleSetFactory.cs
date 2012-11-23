namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    /// <summary> A factory for <code>IFederateHandleSet</code> instances.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIFederateHandleSetFactory : IFederateHandleSetFactory
    {
        /// <summary> Creates and returns a new <code>IFederateHandleSet</code>.
        /// 
        /// </summary>
        /// <returns> the newly created <code>IFederateHandleSet</code>
        /// </returns>
        public virtual IFederateHandleSet Create()
        {
            return new XRTIFederateHandleSet();
        }
    }
}
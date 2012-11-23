namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;

    /// <summary> 
    /// A factory for <code>IParameterHandleValueMap</code> instances.
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIParameterHandleValueMapFactory : IParameterHandleValueMapFactory
    {
        /// <summary> Creates a new <code>IParameterHandleValueMap</code> instance with 
        /// the specified initial capacity.
        /// 
        /// </summary>
        /// <param name="capacity">the initial map capacity
        /// </param>
        /// <returns> the newly created <code>IParameterHandleValueMap</code>
        /// </returns>
        public virtual IParameterHandleValueMap Create(int capacity)
        {
            return new XRTIParameterHandleValueMap(capacity);
        }
    }
}
namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;

    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> 
    /// The factory is used only (outside of the RTI) to Create <code>IAttributeHandle</code>s
    /// received as attribute or parameter values.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIAttributeHandleFactory : IAttributeHandleFactory
    {
        /// <summary>
        ///  Decodes an attribute handle, returning a new instance of
        /// <code>IAttributeHandle</code>.
        /// </summary>
        /// <param name="bufferStream">the encoded attribute handle
        /// </param>
        /// <param name="offset">the offset of the handle data in the bufferStream
        /// </param>
        /// <returns> an instance of <code>IAttributeHandle</code> corresponding
        /// to the encoded handle
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the attribute handle could not be decoded
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of the execution
        /// </exception>
        public virtual IAttributeHandle Decode(byte[] buffer, int offset)
        {
            return new XRTIAttributeHandle(EncodingHelpers.DecodeLong(buffer, offset));
        }
    }
}
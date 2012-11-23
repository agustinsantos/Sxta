namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> 
    /// This factory is used only (outside of the RTI) to Create
    /// <code>IFederateHandle</code> objects corresponding to federate
    /// handles received as attribute or parameter values.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIFederateHandleFactory : IFederateHandleFactory
    {
        /// <summary> 
        /// Decodes a federate handle within the specified bufferStream and
        /// returns a corresponding <code>IFederateHandle</code> instance.
        /// </summary>
        /// <param name="bufferStream">the bufferStream from which to Decode the federate handle
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which the encoded
        /// federate handle resides
        /// </param>
        /// <returns> the new <code>IFederateHandle</code> instance corresponding
        /// to the encoded handle
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the federate handle could not be decoded
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of the execution
        /// </exception>
        public virtual IFederateHandle Decode(byte[] buffer, int offset)
        {
            byte[] buf = new byte[8];

            Array.Copy(buffer, offset, buf, 0, 8);

            return (XRTIFederateHandle)(EncodingHelpers.DecodeLong(buf));
        }
    }
}
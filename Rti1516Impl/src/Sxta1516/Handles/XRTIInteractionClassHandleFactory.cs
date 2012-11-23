namespace Sxta.Rti1516.XrtiHandles
{

    using System;

    using Hla.Rti1516;

    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> This factory is used only (outside of the RTI) to Create 
    /// <code>IInteractionClassHandle</code>s corresponding to interaction
    /// class handles received as attribute or parameter values.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIInteractionClassHandleFactory : IInteractionClassHandleFactory
    {
        /// <summary> Decodes an interaction class handle contained within the specified
        /// bufferStream, returning a corresponding instance of 
        /// <code>IInteractionClassHandle</code>.
        /// 
        /// </summary>
        /// <param name="bufferStream">the bufferStream that contains the encoded handle
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which the handle is stored
        /// </param>
        /// <returns> an instance of <code>IInteractionClassHandle</code> corresponding
        /// to the encoded handle
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the interaction class handle could not be decoded
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member of the
        /// execution
        /// </exception>
        public virtual IInteractionClassHandle Decode(byte[] buffer, int offset)
        {
            byte[] buf = new byte[8];

            Array.Copy(buffer, offset, buf, 0, 8);

            return new XRTIInteractionClassHandle(EncodingHelpers.DecodeLong(buf));
        }
    }
}
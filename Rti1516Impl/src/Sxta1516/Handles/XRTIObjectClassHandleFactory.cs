namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;
    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> A factory for <code>IObjectClassHandle</code>s.  This factory is used only
    /// (outside of the run-time infrastructure) to Create <code>IObjectClassHandle</code>s
    /// received as attribute or parameter values.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIObjectClassHandleFactory : IObjectClassHandleFactory
    {
        /// <summary> Decodes an object class handle stored within the specified bufferStream.
        /// 
        /// </summary>
        /// <param name="bufferStream">the bufferStream that contains the encoded handle
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which the encoded
        /// handle is located
        /// </param>
        /// <returns> an <code>IObjectClassHandle</code> representing the decoded
        /// handle
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the handle could not be decoded
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not a member
        /// of the execution
        /// </exception>
        public virtual IObjectClassHandle Decode(byte[] buffer, int offset)
        {
            byte[] buf = new byte[8];

            Array.Copy(buffer, offset, buf, 0, 8);

            return new XRTIObjectClassHandle(EncodingHelpers.DecodeLong(buf));
        }
    }
}
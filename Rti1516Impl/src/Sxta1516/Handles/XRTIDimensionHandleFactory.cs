namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;

    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> This factory is used only (outside of the RTI) to Create
    /// <code>IDimensionHandle</code>s received as attribute or parameter
    /// values.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIDimensionHandleFactory : IDimensionHandleFactory
    {
        /// <summary> Decodes a dimension handle within the specified bufferStream.
        /// 
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded handle
        /// </param>
        /// <param name="offset">the location of the handle within the bufferStream
        /// </param>
        /// <returns> the <code>IDimensionHandle</code> instance 
        /// corresponding to the decoded handle
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the dimension handle could not
        /// be decoded
        /// </exception>
        /// <exception cref="FederateNotExecutionMember"> if the federate is not
        /// a member of the execution
        /// </exception>
        public virtual IDimensionHandle Decode(byte[] buffer, int offset)
        {
            byte[] buf = new byte[8];

            Array.Copy(buffer, offset, buf, 0, 8);

            return new XRTIDimensionHandle(EncodingHelpers.DecodeLong(buf));
        }
    }
}
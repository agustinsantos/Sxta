using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Time
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    /// <summary> 
    /// A factory for <code>OpaqueLogicalTime</code>s.
    /// </summary>
    /// <author> 
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class OpaqueLogicalTimeFactory : ILogicalTimeFactory
    {
        /// <summary> 
        /// Decodes a logical time stored within the specified bufferStream,
        /// returning a <code>ILogicalTime</code> object corresponding to
        /// the decoded parameterValue.
        /// </summary>
        /// <param name="bufferStream">the bufferStream that contains the encoded parameterValue
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which the
        /// encoded parameterValue is stored
        /// </param>
        /// <returns> a new <code>ILogicalTime</code> representing the
        /// decoded parameterValue
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the parameterValue could not be decoded
        /// </exception>
        public virtual ILogicalTime Decode(byte[] buffer, int offset)
        {
            int length = buffer.Length - offset;
            byte[] val = new byte[length];

            Array.Copy(buffer, offset, val, 0, length);

            return new OpaqueLogicalTime(val, length);
        }

        /// <summary> 
        /// Creates and returns an instance of the initial logical time.
        /// </summary>
        /// <returns> an instance of the initial logical time
        /// </returns>
        public virtual ILogicalTime MakeInitial()
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary> 
        /// Creates and returns an instance of the final logical time.
        /// </summary>
        /// <returns> an instance of the final logical time
        /// </returns>
        public virtual ILogicalTime MakeFinal()
        {
            throw new NotImplementedException("Not yet implemented");
        }
    }
}

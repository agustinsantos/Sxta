using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Time
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    /// <summary> 
    /// A factory for <code>OpaqueLogicalTimeInterval</code>s.
    /// </summary>
    /// <author> 
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class OpaqueLogicalTimeIntervalFactory : ILogicalTimeIntervalFactory
    {
        /// <summary> 
        /// Decodes a logical time interval stored within the specified
        /// bufferStream, returning a corresponding new <code>ILogicalTimeInterval</code>.
        /// </summary>
        /// <param name="bufferStream">the bufferStream containing the encoded interval
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which the encoded
        /// interval is stored
        /// </param>
        /// <returns> a new <code>ILogicalTimeInterval</code> corresponding to the
        /// encoded interval
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the time interval could not be decoded
        /// </exception>
        public virtual ILogicalTimeInterval Decode(byte[] buffer, int offset)
        {
            int length = buffer.Length - offset;
            byte[] val = new byte[length];

            Array.Copy(buffer, offset, val, 0, length);

            return new OpaqueLogicalTimeInterval(val, length);
        }

        /// <summary> 
        /// Creates and returns a zero-length logical time interval.
        /// </summary>
        /// <returns> a new zero-length <code>ILogicalTimeInterval</code>
        /// </returns>
        public virtual ILogicalTimeInterval MakeZero()
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary> 
        /// Creates and returns an epsilon-length logical time interval.
        /// </summary>
        /// <returns> a new epsilon-length <code>ILogicalTimeInterval</code>
        /// </returns>
        public virtual ILogicalTimeInterval MakeEpsilon()
        {
            throw new NotImplementedException("Not yet implemented");
        }
    }
}

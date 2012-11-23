namespace Sxta.Rti1516.Time
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    /// <summary> 
    /// A factory for <code>DoubleValuedLogicalTimeInterval</code>s.
    /// </summary>
    /// <author> 
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class DoubleValuedLogicalTimeIntervalFactory : ILogicalTimeIntervalFactory
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
            return new DoubleValuedLogicalTimeInterval(EncodingHelpers.DecodeDouble(buffer, offset));
        }

        /// <summary> 
        /// Creates and returns a zero-length logical time interval.
        /// </summary>
        /// <returns> a new zero-length <code>ILogicalTimeInterval</code>
        /// </returns>
        public virtual ILogicalTimeInterval MakeZero()
        {
            return new DoubleValuedLogicalTimeInterval(0);
        }

        /// <summary> 
        /// Creates and returns an epsilon-length logical time interval.
        /// </summary>
        /// <returns> a new epsilon-length <code>ILogicalTimeInterval</code>
        /// </returns>
        public virtual ILogicalTimeInterval MakeEpsilon()
        {
            return new DoubleValuedLogicalTimeInterval(0.1);
        }
    }
}
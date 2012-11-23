namespace Sxta.Rti1516.Time
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;
    
    /// <summary> 
    /// A factory for <code>DoubleValuedLogicalTime</code>s.
    /// </summary>
    /// <author> 
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class DoubleValuedLogicalTimeFactory : ILogicalTimeFactory
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
            return new DoubleValuedLogicalTime(EncodingHelpers.DecodeDouble(buffer, offset));
        }

        /// <summary> 
        /// Creates and returns an instance of the initial logical time.
        /// </summary>
        /// <returns> an instance of the initial logical time
        /// </returns>
        public virtual ILogicalTime MakeInitial()
        {
            return new DoubleValuedLogicalTime(0);
        }

        /// <summary> 
        /// Creates and returns an instance of the final logical time.
        /// </summary>
        /// <returns> an instance of the final logical time
        /// </returns>
        public virtual ILogicalTime MakeFinal()
        {
            return new DoubleValuedLogicalTime(System.Double.MaxValue);
        }
    }
}
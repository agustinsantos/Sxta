using System;
using System.Collections.Generic;
using System.Text;

namespace Sxta.Rti1516.Time
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    /// <summary>
    ///  The default logical time interval.
    /// </summary>
    /// <author> 
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class OpaqueLogicalTimeInterval : ILogicalTimeInterval
    {
        /// <summary> The parameterValue of this logical time interval.</summary>
        private byte[] val;
        private int length;

        /// <summary>
        /// Creates a new <code>OpaqueLogicalTimeInterval</code>.
        /// </summary>
        /// <param name="pValue">the parameterValue of the logical time interval
        /// </param>
        public OpaqueLogicalTimeInterval(byte[] pValue, int pLength)
        {
            val = pValue;
            length = pLength;
        }

        /// <summary> 
        /// Checks whether this logical time interval has a zero length.
        /// </summary>
        /// <returns> <code>true</code> if this logical time interval has a
        /// zero length, <code>false</code> otherwise
        /// </returns>
        public virtual bool IsZero()
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary> 
        /// Checks whether this logical time interval has an epsilon length.
        /// </summary>
        /// <returns> <code>true</code> if this logical time interval has an
        /// epsilon length, <code>false</code> otherwise
        /// </returns>
        public virtual bool IsEpsilon()
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary> 
        /// Subtracts the specified logical time interval from this one,
        /// returning a new <code>ILogicalTimeInterval</code> representing the
        /// result.
        /// </summary>
        /// <param name="subtrahend">the logical time interval to Subtract from this one
        /// </param>
        /// <returns> a new <code>ILogicalTimeInterval</code> representing the
        /// result of the operation
        /// </returns>
        public virtual ILogicalTimeInterval Subtract(ILogicalTimeInterval subtrahend)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary> 
        /// Compares this logical time interval to another.
        /// </summary>
        /// <param name="other">the <code>ILogicalTimeInterval</code> to compare this to
        /// </param>
        /// <returns> <code>+1</code> if this logical time interval is longer than
        /// the other, <code>-1</code> if it is shorter, or <code>0</code> if the
        /// two intervals are the same length
        /// </returns>
        public virtual int CompareTo(System.Object other)
        {
            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary> 
        /// Checks this logical time interval for equality with another.
        /// </summary>
        /// <param name="other">the <code>ILogicalTimeInterval</code> to compare this to
        /// </param>
        /// <returns> <code>true</code> if the two intervals are equal, <code>false</code>
        /// otherwise
        /// </returns>
        public override bool Equals(System.Object other)
        {
            try
            {
                return val.Equals(((OpaqueLogicalTimeInterval)other).val);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns a hash code corresponding to this logical time interval.
        /// </summary>
        /// <returns> a hash code corresponding to this logical time interval
        /// </returns>
        public override int GetHashCode()
        {
            return val.GetHashCode();
        }

        /// <summary> 
        /// Returns a string representation of this logical time interval.
        /// </summary>
        /// <returns> a string representation of this logical time interval
        /// </returns>
        public override System.String ToString()
        {
            return val.ToString();
        }

        /// <summary> Returns the encoded length of this logical time interval.
        /// 
        /// </summary>
        /// <returns> the encoded length of this logical time interval (in bytes)
        /// </returns>
        public virtual int EncodedLength()
        {
            return length;
        }

        /// <summary> 
        /// Encodes this logical time interval, placing the result into the
        /// specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream in which to place the encoded interval
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which to store
        /// the encoded interval
        /// </param>
        public virtual void Encode(byte[] buffer, int offset)
        {
            Array.Copy(val, 0, buffer, offset, length);
        }
    }
}

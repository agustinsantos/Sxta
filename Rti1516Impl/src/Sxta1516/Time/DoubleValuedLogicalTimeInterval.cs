namespace Sxta.Rti1516.Time
{
    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    /// <summary>
    ///  An immutable double-valued logical time interval.
    /// </summary>
    /// <author> 
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class DoubleValuedLogicalTimeInterval : ILogicalTimeInterval
    {
        /// <summary> 
        /// Returns the parameterValue of this time interval.
        /// </summary>
        /// <returns> the parameterValue of this time interval
        /// </returns>
        virtual public double Value
        {
            get
            {
                return val;
            }

        }

        /// <summary> The parameterValue of this logical time interval.</summary>
        private double val;
        private const int length = sizeof(double);

        /// <summary>
        /// Creates a new <code>XRTILogicalTimeInterval</code>.
        /// </summary>
        /// <param name="pValue">the parameterValue of the logical time interval
        /// </param>
        public DoubleValuedLogicalTimeInterval(double pValue)
        {
            val = pValue;
        }

        /// <summary> 
        /// Checks whether this logical time interval has a zero length.
        /// </summary>
        /// <returns> <code>true</code> if this logical time interval has a
        /// zero length, <code>false</code> otherwise
        /// </returns>
        public virtual bool IsZero()
        {
            return (val == 0);
        }

        /// <summary> 
        /// Checks whether this logical time interval has an epsilon length.
        /// </summary>
        /// <returns> <code>true</code> if this logical time interval has an
        /// epsilon length, <code>false</code> otherwise
        /// </returns>
        public virtual bool IsEpsilon()
        {
            return (val == 1);
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
            return new DoubleValuedLogicalTimeInterval(val - ((DoubleValuedLogicalTimeInterval)subtrahend).val);
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
            double otherValue = ((DoubleValuedLogicalTimeInterval)other).val;

            if (val > otherValue)
            {
                return +1;
            }
            else if (val < otherValue)
            {
                return -1;
            }
            else
            {
                return 0;
            }
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
                return (val == ((DoubleValuedLogicalTimeInterval)other).val);
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
            return (int)val;
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
            byte[] encodedValue = EncodingHelpers.EncodeDouble(val);

            Array.Copy(encodedValue, 0, buffer, offset, encodedValue.Length);
        }
    }
}
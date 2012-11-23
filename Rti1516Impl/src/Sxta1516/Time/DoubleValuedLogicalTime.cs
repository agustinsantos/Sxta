namespace Sxta.Rti1516.Time
{

    using System;
    using Hla.Rti1516;
    using Sxta.Rti1516.Serializers.XrtiEncoding;

    /// <summary> 
    /// An immutable double-valued logical time parameterValue.
    /// </summary>
    /// <author>
    /// Ángel Silva
    /// </author>
    [Serializable]
    public class DoubleValuedLogicalTime : ILogicalTime
    {
        /// <summary> The parameterValue of this logical time.</summary>
        private System.Double val = 0;
        private const int length = sizeof(double);


        /// <summary> 
        /// Creates a new <code>XRTILogicalTime</code>.
        /// </summary>
        /// <param name="pValue">the parameterValue of the logical time
        /// </param>
        public DoubleValuedLogicalTime(System.Double pValue)
        {
            val = pValue;
        }

        /// <summary> 
        /// Checks whether this represents an initial time.
        /// </summary>
        /// <returns> <code>true</code> if this represents an initial time,
        /// <code>false</code> otherwise
        /// </returns>
        public virtual bool IsInitial()
        {
            return (val == 0);
        }

        /// <summary> 
        /// Checks whether this represents a final time.
        /// </summary>
        /// <returns> <code>true</code> if this represents a final time,
        /// <code>false</code> otherwise
        /// </returns>
        public virtual bool IsFinal()
        {
            return (val == System.Double.MaxValue);
        }

        /// <summary> 
        /// Adds the specified time interval to this logical time, returning
        /// the result as a new <code>ILogicalTime</code>.
        /// </summary>
        /// <param name="val">the time interval to Add to this logical time
        /// </param>
        /// <returns> a new <code>ILogicalTime</code> that represents this logical time
        /// plus the specified time interval
        /// </returns>
        /// <exception cref=""> IllegalTimeArithmetic if the operation cannot be performed
        /// </exception>
        public virtual ILogicalTime Add(ILogicalTimeInterval pVal)
        {
            double intervalValue = ((DoubleValuedLogicalTimeInterval)pVal).Value, newValue = val + intervalValue;

            if (newValue < 0)
            {
                throw new IllegalTimeArithmetic("attempted to Create logical time greater than maximum");
            }
            else
            {
                return new DoubleValuedLogicalTime(newValue);
            }
        }

        /// <summary> 
        /// Subtracts the specified time interval from this logical time, returning
        /// the result as a new <code>ILogicalTime</code>.
        /// </summary>
        /// <param name="val">the time interval to Subtract from this logical time
        /// </param>
        /// <returns> a new <code>ILogicalTime</code> that represents this logical time
        /// minus the specified time interval
        /// </returns>
        /// <exception cref=""> IllegalTimeArithmetic if the operation cannot be performed
        /// </exception>
        public virtual ILogicalTime Subtract(ILogicalTimeInterval pVal)
        {
            double intervalValue = ((DoubleValuedLogicalTimeInterval)pVal).Value, newValue = val - intervalValue;

            if (newValue < 0)
            {
                throw new IllegalTimeArithmetic("attempted to Create logical time less than zero");
            }
            else
            {
                return new DoubleValuedLogicalTime(newValue);
            }
        }

        /// <summary> 
        /// Computes and returns the time interval between this logical time
        /// and another one.
        /// </summary>
        /// <param name="val">the other logical time
        /// </param>
        /// <returns> the logical time interval between this logical time and
        /// the other logical time
        /// </returns>
        public virtual ILogicalTimeInterval Distance(ILogicalTime pVal)
        {
            return new DoubleValuedLogicalTimeInterval(System.Math.Abs(val - ((DoubleValuedLogicalTime)pVal).val));
        }

        /// <summary> 
        /// Compares this logical time to another.
        /// </summary>
        /// <param name="other">the logical time to compare this to
        /// </param>
        /// <returns> <code>+1</code> if this logical time is greater than the
        /// other one, <code>-1</code> if this logical time is less than the
        /// other one, <code>0</code> if the two logical times are equal
        /// </returns>
        public virtual int CompareTo(System.Object other)
        {
            System.Double otherValue = ((DoubleValuedLogicalTime)other).val;

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
        /// Checks this logical time for equality with another.
        /// </summary>
        /// <param name="other">the other logical time to compare this to
        /// </param>
        /// <returns> <code>true</code> if the other object represents the
        /// same logical time as this one, <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object other)
        {
            try
            {
                return (val == ((DoubleValuedLogicalTime)other).val);
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns a hash code corresponding to this logical time.
        /// </summary>
        /// <returns> a hash code corresponding to this logical time
        /// </returns>
        public override int GetHashCode()
        {
            return (int)val;
        }

        /// <summary> 
        /// Returns a string representation of this logical time.
        /// </summary>
        /// <returns> a string representation of this logical time
        /// </returns>
        public override System.String ToString()
        {
            return val.ToString();
        }

        /// <summary> 
        /// Returns the encoded length of this logical time.
        /// </summary>
        /// <returns> the encoded length of this logical time (in bytes)
        /// </returns>
        public virtual int EncodedLength()
        {
            return length;
        }

        /// <summary> 
        /// Encodes this logical time, placing the result into the specified
        /// bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream in which to place the result
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which to store the
        /// encoded parameterValue
        /// </param>
        public virtual void Encode(byte[] buffer, int offset)
        {
            byte[] encodedValue = EncodingHelpers.EncodeDouble(val);

            Array.Copy(encodedValue, 0, buffer, offset, encodedValue.Length);
        }
    }
}

namespace Hla.Rti1516
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class defines the predefined types of transportation.
    /// An implementation is allowed to define new types of transportation.
    /// None of the predefined types of transportation shall be eliminated.
    /// </summary>
    [Serializable]
    public class TransportationType
    {
        private const int lowestValue = 1;

        /// <summary> The next value to assign for a new transportation type.</summary>
        protected static int nextToAssign = lowestValue; //begins at lowest

        /// <summary> The reliable transportation type.</summary>
        public static readonly TransportationType HLA_RELIABLE = new TransportationType("HLAreliable");

        /// <summary> The best-effort transportation type.</summary>
        public static readonly TransportationType HLA_BEST_EFFORT = new TransportationType("HLAbestEffort");

        /// <summary> The value of the instance.</summary>
        private int transportationValue;

        private string transportationName = "";

        private static Dictionary<string, int> transportsMap;


        /// <summary> 
        /// Decodes a transportation type stored within the specified buffer and
        /// returns a corresponding <code>TransportationType</code> instance.
        /// </summary>
        /// <param name="buffer">the buffer in which the encoded value is stored
        /// </param>
        /// <param name="offset">the offset within the buffer at which the encoded
        /// value is located
        /// </param>
        /// <returns> an <code>TransportationType</code> corresponding to the decoded
        /// value
        /// </returns>
        /// <exception cref="CouldNotDecode"> if the transportation type could not be decoded
        /// </exception>
        public static TransportationType Decode(byte[] buffer, int offset)
        {
            int transportationValue = buffer[offset];

            if (transportationValue == HLA_RELIABLE.transportationValue)
            {
                return HLA_RELIABLE;
            }
            else if (transportationValue == HLA_BEST_EFFORT.transportationValue)
            {
                return HLA_BEST_EFFORT;
            }
            else
            {
                throw new CouldNotDecode("invalid transportation type (neither reliable nor best-effort)");
            }
        }

        /// <summary>
        ///  Copy constructor.
        /// </summary>
        /// <param name="otherTransportationType">the transportation type to copy
        /// </param>
        public TransportationType(TransportationType otherTransportationType)
        {
            transportationValue = otherTransportationType.transportationValue;
        }

        /// <summary> 
        /// Private constructor.
        /// </summary>
        /// <param name="pValue">the integer value corresponding to this transportation type
        /// </param>
        private TransportationType(int pValue)
        {
            transportationValue = pValue;
            if (transportationValue < lowestValue || transportationValue >= nextToAssign)
                throw new RTIinternalError("TransportationType: illegal value " + transportationValue);
        }

        /// <summary> 
        /// Protected constructor.
        /// </summary>
        public TransportationType(string name)
        {
            if (transportsMap == null)
                transportsMap = new Dictionary<string, int>();
            if (transportsMap.ContainsKey(name))
            {
                this.transportationName = name;
                this.transportationValue = transportsMap[name];
            }
            else
            {
                transportationValue = nextToAssign++;
                transportationName = name;
                transportsMap.Add(transportationName, transportationValue);
            }
        }

        /// <summary> 
        /// Compares this transportation type for equality with another.
        /// </summary>
        /// <param name="otherTransportationType">the other transportation type
        /// </param>
        /// <returns> <code>true</code> if the two transportation types are equal,
        /// <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherTransportationType)
        {
            try
            {
                return (transportationValue == ((TransportationType)otherTransportationType).transportationValue);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns a hash code corresponding to this transportation type.
        /// </summary>
        /// <returns> a hash code corresponding to this transportation type
        /// </returns>
        public override int GetHashCode()
        {
            return transportationValue;
        }

        /// <summary> 
        /// Returns the encoded length of this transportation type.
        /// </summary>
        /// <returns> the encoded length of this transportation type (in bytes)
        /// </returns>
        public virtual int EncodedLength()
        {
            return 1;
        }

        /// <summary> 
        /// Encodes this transportation type, placing the result into the specified buffer.
        /// </summary>
        /// <param name="buffer">the buffer in which to place the encoded value
        /// </param>
        /// <param name="offset">the offset within the buffer at which to store the encoded
        /// value
        /// </param>
        public virtual void Encode(byte[] buffer, int offset)
        {
            buffer[offset] = (byte)transportationValue;
        }

        /// <summary> 
        /// Returns a string representation of this transportation type.
        /// </summary>
        /// <returns> a string representation of this transportation type
        /// </returns>
        public override System.String ToString()
        {
            return "TransportationType(" + transportationName + ")";
        }
    }
}
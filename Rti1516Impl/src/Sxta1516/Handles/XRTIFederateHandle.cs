namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;
    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;
    using Sxta.Rti1516.Management;

    /// <summary> 
    /// The federate identifier.
    /// Type-safe handle for a federate.  Generally these are created by the
    /// RTI and passed to the user.
    /// The federate is stored as a full UUID as described in the standard http://www.uddi.org/
    /// but externaly is represented only by the least significant 64 bits (long), so some conflicts 
    /// could be happen
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIFederateHandle : IFederateHandle
    {

        /// <summary> 
        /// Returns this handle's unique identifier.
        /// </summary>
        /// <returns> this handle's unique identifier
        /// </returns>
        internal long Identifier
        {
            get
            {
                return leastSig;
            }
        }

        /// <summary> 
        /// The encoded buffer.
        /// </summary>
        byte[] encodedBuf = new byte[16];

        /// <summary> 
        /// The most significant 64 bits.
        /// </summary>
        private long mostSig;

        /// <summary> 
        /// The least significant 64 bits.
        /// </summary>
        private long leastSig;

        /// <summary> 
        /// Constructor.
        /// </summary>
        public XRTIFederateHandle()
        {
            Guid uuid = System.Guid.NewGuid();
            //TODO Check if this array depends on Big-little endian
            encodedBuf = uuid.ToByteArray();
            this.mostSig = BitConverter.ToInt64(encodedBuf, 0);
            this.leastSig = BitConverter.ToInt64(encodedBuf, 8);
        }

        /// <summary> 
        /// Constructor.
        /// </summary>
        private XRTIFederateHandle(long most, long least)
        {
            byte[] mostBuf = BitConverter.GetBytes(most);
            byte[] leastBuf = BitConverter.GetBytes(least);
            Array.Copy(mostBuf, 0, encodedBuf, 0, 8);
            Array.Copy(leastBuf, 0, encodedBuf, 8, 8);

            this.mostSig = most;
            this.leastSig = least;
        }

        /// <summary> 
        /// Tests this federate handle for equality with another.
        /// </summary>
        /// <param name="otherFederateHandle">the other federate handle to compare this to
        /// </param>
        /// <returns> <code>true</code> if this refers to the same federate as other
        /// handle, <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherFederateHandle)
        {
            try
            {
                return (Identifier == ((XRTIFederateHandle)otherFederateHandle).Identifier);
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns the hash code corresponding to this federate handle.
        /// </summary>
        /// <returns> the hash code corresponding to this federate handle
        /// </returns>
        public override int GetHashCode()
        {
            return (int)Identifier;
        }

        /// <summary> 
        /// Returns the encoded length of this federate handle.
        /// </summary>
        /// <returns> the encoded length of this federate handle (in bytes)
        /// </returns>
        public int EncodedLength()
        {
            return sizeof(long);
        }

        /// <summary> 
        /// Encodes this federate handle, placing the result into the specified
        /// byte array.
        /// </summary>
        /// <param name="bufferStream">the bufferStream to hold the encoded federate handle
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which to store the
        /// encoded handle
        /// </param>
        public void Encode(byte[] buffer, int offset)
        {
            Array.Copy(encodedBuf, 8, buffer, offset, EncodedLength());
        }

        /// <summary> 
        /// Returns a string representation of this federate handle.
        /// 
        /// Returns a 36-character string of six fields separated by hyphens,
        /// with each field represented in lowercase hexadecimal with the same
        /// number of digits as in the field. The order of fields is: time_low,
        /// time_mid, version and time_hi treated as a single field, variant and
        /// clock_seq treated as a single field, and node.
        /// </summary>
        /// <returns> a string representation of this federate handle
        /// </returns>
        public override System.String ToString()
        {
            return "urn:sxta:federate-" + this.Identifier.ToString("X");
        }

        public static explicit operator long(XRTIFederateHandle handle)
        {
            return handle.leastSig;
        }

        public static explicit operator XRTIFederateHandle(long val)
        {
            XRTIFederateHandle tmp = new XRTIFederateHandle(0, val);
            return tmp;
        }

        // PATCH ANGEL
        // TODO: Esto provoca una dependencia de la libreria Management que deberia ser
        // conocida posteriormente. Mirad si podemos quitar la dependencia.
        public static explicit operator HLAfederateHandle(XRTIFederateHandle handle)
        {
            HLAfederateHandle newHandle = new HLAfederateHandle();

            // Selects the four-first bits of leastSig
            int value = BitConverter.ToInt32(handle.encodedBuf, 8);
            newHandle.data = value;

            return newHandle;
        }
        // END PATCH ANGEL

    }
}
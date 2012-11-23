namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;
    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> 
    /// Type-safe handle for an object class.  Generally these are created by the
    /// run-time infrastructure and passed to the user.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public struct XRTIObjectClassHandle : IObjectClassHandle
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
                return identifier;
            }

        }

        /// <summary> The object class identifier.</summary>
        private long identifier;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pIdentifier">the object class identifier
        /// </param>
        /// PATCH ANGEL: Este método era internal
        public XRTIObjectClassHandle(long pIdentifier)
        {
            identifier = pIdentifier;
        }

        /// <summary> 
        /// Checks this object class handle for equality with another.
        /// </summary>
        /// <param name="otherObjectClassHandle">the other object class handle
        /// </param>
        /// <returns> <code>true</code> if the object class handles refer
        /// to the same object class, <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherObjectClassHandle)
        {
            try
            {
                return (identifier == ((XRTIObjectClassHandle)otherObjectClassHandle).identifier);
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns a hash code corresponding to this object
        /// class handle.
        /// </summary>
        /// <returns> a hash code corresponding to this object class handle
        /// </returns>
        public override int GetHashCode()
        {
            return (int)identifier;
        }

        /// <summary> 
        /// Returns the encoded length of this object class handle.
        /// </summary>
        /// <returns> the encoded length of this object class handle (in bytes)
        /// </returns>
        public int EncodedLength()
        {
            return 8;
        }

        /// <summary> 
        /// Encodes this object handle, placing the result into the specified
        /// bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream to contain the encoded parameterValue
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which to store
        /// the encoded parameterValue
        /// </param>
        public void Encode(byte[] buffer, int offset)
        {
            byte[] buf = EncodingHelpers.EncodeLong(identifier);

            Array.Copy(buf, 0, buffer, offset, 8);
        }

        /// <summary> 
        /// Returns a string representation of this object class handle.
        /// </summary>
        /// <returns> a string representation of this object class handle
        /// </returns>
        public override System.String ToString()
        {
            return "#ObjectClassHandle:" + identifier;
        }
    }
}
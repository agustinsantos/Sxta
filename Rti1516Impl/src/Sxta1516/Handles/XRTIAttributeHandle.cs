namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;

    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> 
    /// Type-safe handle for an attribute.  Generally these are created by the
    /// RTI and passed to the user.
    /// </summary>
    /// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public struct XRTIAttributeHandle : IAttributeHandle
    {
        /// <summary> 
        /// Returns this handle's unique identifier.
        /// </summary>
        /// <returns> this handle's unique identifier
        /// </returns>
        public long Identifier
        {
            get
            {
                return identifier;
            }
        }

        /// <summary> The attribute identifier.</summary>
        internal long identifier;


        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pIdentifier">the attribute identifier
        /// </param>
        /// PATCH ANGEL: Este constructor era internal
        public XRTIAttributeHandle(long pIdentifier)
        {
            identifier = pIdentifier;
        }

        /// <summary> 
        /// Checks this attribute handle for equality with another.
        /// </summary>
        /// <param name="otherAttributeHandle">the other attribute handle to compare this
        /// to
        /// </param>
        /// <returns> <code>true</code> if this refers to the same attribute as the 
        /// other handle, <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherAttributeHandle)
        {
            try
            {
                return (identifier == ((XRTIAttributeHandle)otherAttributeHandle).identifier);
            }
            catch
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns the hash code of this attribute handle.
        /// </summary>
        /// <returns> the hash code corresponding to this attribute handle
        /// </returns>
        public override int GetHashCode()
        {
            return (int)identifier;
        }

        /// <summary> 
        /// Returns the encoded length of this attribute handle.
        /// </summary>
        /// <returns> the encoded length of this attribute handle, in bytes
        /// </returns>
        public int EncodedLength()
        {
            return 8;
        }

        /// <summary> 
        /// Encodes this attribute handle, placing the result into the specified
        /// array.
        /// </summary>
        /// <param name="bufferStream">the bufferStream in which to place the encoded handle
        /// </param>
        /// <param name="offset">the bufferStream offset at which to store the data
        /// </param>
        public void Encode(byte[] buffer, int offset)
        {
            byte[] buf = EncodingHelpers.EncodeLong(identifier);

            Array.Copy(buf, 0, buffer, offset, 8);
        }

        /// <summary> 
        /// Returns a string representation of this attribute handle.
        /// </summary>
        /// <returns> a string representation of this attribute handle
        /// </returns>
        public override System.String ToString()
        {
            return "#AttributeHandle:" + identifier;
        }
    }
}
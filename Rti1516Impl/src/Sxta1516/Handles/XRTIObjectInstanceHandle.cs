namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;
    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> 
    /// Type-safe handle for an object instance.  Generally these are created by the
    /// run-time infrastructure and passed to the user.
    /// </summary>
    /// <author> 
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>
    [Serializable]
    public class XRTIObjectInstanceHandle : IObjectInstanceHandle
    {
        /// <summary> Returns this handle's unique identifier.
        /// 
        /// </summary>
        /// <returns> this handle's unique identifier
        /// </returns>
        virtual protected internal long Identifier
        {
            get
            {
                return identifier;
            }

        }

        /// <summary> The object instance identifier.</summary>
        private long identifier;

        /// <summary> 
        /// Constructor.
        /// </summary>
        /// <param name="pIdentifier">the object instance identifier
        /// </param>
        public XRTIObjectInstanceHandle(long pIdentifier)  // protected internal
        {
            identifier = pIdentifier;
        }

        /// <summary> 
        /// Checks this object instance handle for equality with another.
        /// </summary>
        /// <param name="otherObjectInstanceHandle">the other object instance handle
        /// </param>
        /// <returns> <code>true</code> if the two handles refer to the same
        /// object instance, <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherObjectInstanceHandle)
        {
            try
            {
                return (identifier == ((XRTIObjectInstanceHandle)otherObjectInstanceHandle).identifier);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        /// <summary> 
        /// Computes and returns a hash code corresponding to this object
        /// instance handle.
        /// </summary>
        /// <returns> a hash code corresponding to this object instance handle
        /// </returns>
        public override int GetHashCode()
        {
            return (int)identifier;
        }

        /// <summary> 
        /// Returns the encoded length of this object instance handle.
        /// </summary>
        /// <returns> the encoded length of this object instance handle
        /// </returns>
        public virtual int EncodedLength()
        {
            return 8;
        }

        /// <summary> 
        /// Encodes this object instance handle, placing the result into the
        /// specified bufferStream.
        /// </summary>
        /// <param name="bufferStream">the bufferStream in which to place the encoded parameterValue
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which to store the
        /// parameterValue
        /// </param>
        public virtual void Encode(byte[] buffer, int offset)
        {
            byte[] buf = EncodingHelpers.EncodeLong(identifier);

            Array.Copy(buf, 0, buffer, offset, 8);
        }

        /// <summary> 
        /// Returns a string representation of this object instance handle.
        /// </summary>
        /// <returns> a string representaiton of this object instance handle
        /// </returns>
        public override System.String ToString()
        {
            return "#ObjectInstanceHandle:" + identifier.ToString("X");
        }
    }
}
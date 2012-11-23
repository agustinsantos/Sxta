namespace Sxta.Rti1516.XrtiHandles
{

    using System;

    using Hla.Rti1516;
    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

    /// <summary> Type-safe handle for an interaction class.  Generally these are created by the
    /// run-time infrastructure and passed to the user.
    /// 
    /// </summary>
    /// <author> Agustin Santos. Based on code originally written by Andrzej Kapolka
    /// </author>

    [Serializable]
    public class XRTIInteractionClassHandle : IInteractionClassHandle
    {
        public static XRTIInteractionClassHandle NullInteractionClassHandle = new XRTIInteractionClassHandle(-1);

        /// <summary> 
        /// Returns this handle's unique identifier.
        /// </summary>
        /// <returns> this handle's unique identifier
        /// </returns>
        virtual public long Identifier
        {
            get
            {
                return identifier;
            }

        }
        /// <summary> The interaction class identifier.</summary>
        private long identifier;


        /// <summary> Constructor.
        /// 
        /// </summary>
        /// <param name="pIdentifier">the interaction class identifier
        /// </param>
        /// TODO ANGEL: Debe ser protected internal
        public XRTIInteractionClassHandle(long pIdentifier)
        {
            identifier = pIdentifier;
        }

        /// <summary> Checks this interaction class handle for equality with another.
        /// 
        /// </summary>
        /// <param name="otherInteractionClassHandle">the other interaction class handle to compare
        /// this to
        /// </param>
        /// <returns> <code>true</code> if this refers to the same interaction class as other handle,
        /// <code>false</code> otherwise
        /// </returns>
        public override bool Equals(System.Object otherInteractionClassHandle)
        {
            try
            {
                return (identifier == ((XRTIInteractionClassHandle)otherInteractionClassHandle).identifier);
            }
            catch (System.InvalidCastException)
            {
                return false;
            }
        }

        /// <summary> Computes and returns a hash code corresponding to this interaction class.
        /// 
        /// </summary>
        /// <returns> a hash code corresponding to this interaction class
        /// </returns>
        public override int GetHashCode()
        {
            return (int)identifier;
        }

        /// <summary> Returns the encoded length of this interaction class handle.
        /// 
        /// </summary>
        /// <returns> the encoded length of this interaction class handle (in bytes)
        /// </returns>
        public virtual int EncodedLength()
        {
            return 8;
        }

        /// <summary> Encodes this interaction class handle, placing the result into the specified
        /// bufferStream.
        /// 
        /// </summary>
        /// <param name="bufferStream">the bufferStream to contain the encoded parameterValue
        /// </param>
        /// <param name="offset">the offset within the bufferStream at which to store the parameterValue
        /// </param>
        public virtual void Encode(byte[] buffer, int offset)
        {
            byte[] buf = EncodingHelpers.EncodeLong(identifier);

            Array.Copy(buf, 0, buffer, offset, 8);
        }

        /// <summary> Returns a string representation of this interaction class handle.
        /// 
        /// </summary>
        /// <returns> a string representation of this interaction class handle
        /// </returns>
        public override System.String ToString()
        {
            return "#InteractionClassHandle:" + identifier;
        }
    }
}
namespace Sxta.Rti1516.XrtiHandles
{
    using System;

    using Hla.Rti1516;

    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

	/// <summary> 
	/// Type-safe handle for a dimension.  Generally these are created by the
	/// RTI and passed to the user.
	/// </summary>
	/// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
	/// </author>
	[Serializable]
	public struct XRTIDimensionHandle : IDimensionHandle
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

		/// <summary> The dimension identifier.</summary>
		private long identifier;
		
		
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="pIdentifier">the dimension identifier
		/// </param>
		internal XRTIDimensionHandle(long pIdentifier)
		{
			identifier = pIdentifier;
		}
		
		/// <summary> 
		/// Checks this dimension handle for equality with another.
		/// </summary>
		/// <param name="otherDimensionHandle">the dimension handle to compare this to
		/// </param>
		/// <returns> <code>true</code> if this refers to the same dimension as the
		/// other handle, <code>false</code> otherwise
		/// </returns>
		public override bool Equals(System.Object otherDimensionHandle)
		{
			try
			{
				return (identifier == ((XRTIDimensionHandle) otherDimensionHandle).identifier);
			}
			catch (System.InvalidCastException)
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this dimension handle.
		/// </summary>
		/// <returns> the hash code corresponding to this dimension handle
		/// </returns>
		public override int GetHashCode()
		{
			return (int) identifier;
		}
		
		/// <summary>
		///  Returns the encoded length of this dimension handle.
		/// </summary>
		/// <returns> the encoded length of this dimension handle (in bytes)
		/// </returns>
		public int EncodedLength()
		{
			return 8;
		}
		
		/// <summary> 
		/// Encodes this dimension handle, placing the result into the
		/// specified bufferStream.
		/// </summary>
		/// <param name="bufferStream">the bufferStream to contain the encoded dimension handle
		/// </param>
		/// <param name="offset">the offset within the bufferStream at which to place the
		/// encoded dimension handle
		/// </param>
		public void Encode(byte[] buffer, int offset)
		{
			byte[] buf = EncodingHelpers.EncodeLong(identifier);
			
			Array.Copy(buf, 0, buffer, offset, 8);
		}
		
		/// <summary> 
		/// Returns a string representation of this dimension handle.
		/// </summary>
		/// <returns> a string representation of this dimension handle
		/// </returns>
		public override System.String ToString()
		{
			return "#DimensionHandle:" + identifier;
		}
	}
}
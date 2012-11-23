namespace Sxta.Rti1516.XrtiHandles
{
    using System;
    using Hla.Rti1516;
    using EncodingHelpers = Sxta.Rti1516.Serializers.XrtiEncoding.EncodingHelpers;

	/// <summary> 
	/// Type-safe handle for a parameter.  Generally these are created by the
	/// run-time infrastructure and passed to the user.
	/// </summary>
	/// <author>
    /// Agustin Santos. Based on code originally written by Andrzej Kapolka
	/// </author>
	[Serializable]
	public struct XRTIParameterHandle : IParameterHandle
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

		/// <summary> The parameter identifier.</summary>
		private long identifier;
		
		
		/// <summary> 
		/// Constructor.
		/// </summary>
		/// <param name="pIdentifier">the parameter identifier
		/// </param>
		internal XRTIParameterHandle(long pIdentifier)
		{
			identifier = pIdentifier;
		}
		
		/// <summary> 
		/// Checks this parameter handle for equality with another.
		/// </summary>
		/// <param name="otherParameterHandle">the other parameter handle
		/// </param>
		/// <returns> <code>true</code> if the two handles represent the
		/// same parameter, <code>false</code> otherwise
		/// </returns>
		public override bool Equals(System.Object otherParameterHandle)
		{
			try
			{
				return (identifier == ((XRTIParameterHandle) otherParameterHandle).identifier);
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this parameter
		/// handle.
		/// </summary>
		/// <returns> a hash code corresponding to this parameter handle
		/// </returns>
		public override int GetHashCode()
		{
			return (int) identifier;
		}
		
		/// <summary> 
		/// Returns the encoded length of this parameter handle.
		/// </summary>
		/// <returns> the encoded length of this parameter handle (in bytes)
		/// </returns>
		public int EncodedLength()
		{
			return 8;
		}
		
		/// <summary> 
		/// Encodes this parameter handle and places the result in the specified
		/// bufferStream.
		/// </summary>
		/// <param name="bufferStream">the bufferStream to contain the encoded handle
		/// </param>
		/// <param name="offset">the offset within the bufferStream at which to store the
		/// encoded handle
		/// </param>
		public void  Encode(byte[] buffer, int offset)
		{
			byte[] buf = EncodingHelpers.EncodeLong(identifier);
			
			Array.Copy(buf, 0, buffer, offset, 8);
		}
		
		/// <summary> 
		/// Returns a string representation of this parameter handle.
		/// </summary>
		/// <returns> a string representation of this parameter handle
		/// </returns>
		public override System.String ToString()
		{
			return "parameter handle #" + identifier + "#";
		}
	}
}
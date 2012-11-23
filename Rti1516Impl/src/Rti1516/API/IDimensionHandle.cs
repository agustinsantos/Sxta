namespace Hla.Rti1516
{
	using System;

	/// <summary>
	///  Type-safe handle for a dimension.  Generally these are created by the
	/// RTI and passed to the user.
	/// </summary>
	public interface IDimensionHandle
	{
		/// <summary>
		///  Checks this dimension handle for equality with another.
		/// </summary>
		/// <param name="otherDimensionHandle">the dimension handle to compare this to
		/// </param>
		/// <returns> <code>true</code> if this refers to the same dimension as the
		/// other handle, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherDimensionHandle);
		
		/// <summary> Computes and returns a hash code corresponding to this dimension handle.
		/// 
		/// </summary>
		/// <returns> the hash code corresponding to this dimension handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns the encoded length of this dimension handle.
		/// </summary>
		/// <returns> the encoded length of this dimension handle (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this dimension handle, placing the result into the
		/// specified buffer.
		/// </summary>
		/// <param name="buffer">the buffer to contain the encoded dimension handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which to place the
		/// encoded dimension handle
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this dimension handle.
		/// </summary>
		/// <returns> a string representation of this dimension handle
		/// </returns>
		System.String ToString();
	}
}
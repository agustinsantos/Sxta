namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Type-safe handle for an attribute.  Generally these are created by the
	/// RTI and passed to the user.
	/// </summary>
	public interface IAttributeHandle
	{
		/// <summary> 
		/// Checks this attribute handle for equality with another.
		/// </summary>
		/// <param name="otherAttributeHandle">the other attribute handle to compare this
		/// to
		/// </param>
		/// <returns> <code>true</code> if this refers to the same attribute as the 
		/// other handle, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherAttributeHandle);
		
		/// <summary> 
		/// Computes and returns the hash code of this attribute handle.
		/// </summary>
		/// <returns> the hash code corresponding to this attribute handle
		/// </returns>
		int GetHashCode();
		
		/// <summary>
		///  Returns the encoded length of this attribute handle.
		/// </summary>
		/// <returns> the encoded length of this attribute handle, in bytes
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this attribute handle, placing the result into the specified
		/// array.
		/// </summary>
		/// <param name="buffer">the buffer in which to place the encoded handle
		/// </param>
		/// <param name="offset">the buffer offset at which to store the data
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this attribute handle.
		/// </summary>
		/// <returns> a string representation of this attribute handle
		/// </returns>
		System.String ToString();
	}
}
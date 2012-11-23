namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Type-safe handle for a parameter.  Generally these are created by the
	/// run-time infrastructure and passed to the user.
	/// </summary>
	public interface IParameterHandle
	{
		/// <summary> 
		/// Checks this parameter handle for equality with another.
		/// </summary>
		/// <param name="otherParameterHandle">the other parameter handle
		/// </param>
		/// <returns> <code>true</code> if the two handles represent the
		/// same parameter, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherParameterHandle);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this parameter
		/// handle.
		/// </summary>
		/// <returns> a hash code corresponding to this parameter handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns the encoded length of this parameter handle.
		/// </summary>
		/// <returns> the encoded length of this parameter handle (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this parameter handle and places the result in the specified
		/// buffer.
		/// </summary>
		/// <param name="buffer">the buffer to contain the encoded handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store the
		/// encoded handle
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this parameter handle.
		/// </summary>
		/// <returns> a string representation of this parameter handle
		/// </returns>
		System.String ToString();
	}
}
namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Type-safe handle for an object instance.  Generally these are created by the
	/// run-time infrastructure and passed to the user.
	/// </summary>
	public interface IObjectInstanceHandle
	{
		/// <summary> 
		/// Checks this object instance handle for equality with another.
		/// </summary>
		/// <param name="otherObjectInstanceHandle">the other object instance handle
		/// </param>
		/// <returns> <code>true</code> if the two handles refer to the same
		/// object instance, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherObjectInstanceHandle);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this object
		/// instance handle.
		/// </summary>
		/// <returns> a hash code corresponding to this object instance handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns the encoded length of this object instance handle.
		/// </summary>
		/// <returns> the encoded length of this object instance handle
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this object instance handle, placing the result into the
		/// specified buffer.
		/// </summary>
		/// <param name="buffer">the buffer in which to place the encoded value
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store the
		/// value
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this object instance handle.
		/// </summary>
		/// <returns> a string representaiton of this object instance handle
		/// </returns>
		System.String ToString();
	}
}
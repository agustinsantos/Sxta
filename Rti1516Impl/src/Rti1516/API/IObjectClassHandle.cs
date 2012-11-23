namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Type-safe handle for an object class.  Generally these are created by the
	/// run-time infrastructure and passed to the user.
	/// </summary>
	public interface IObjectClassHandle
	{
		/// <summary> 
		/// Checks this object class handle for equality with another.
		/// </summary>
		/// <param name="otherObjectClassHandle">the other object class handle
		/// </param>
		/// <returns> <code>true</code> if the object class handles refer
		/// to the same object class, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherObjectClassHandle);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this object
		/// class handle.
		/// </summary>
		/// <returns> a hash code corresponding to this object class handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns the encoded length of this object class handle.
		/// </summary>
		/// <returns> the encoded length of this object class handle (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this object handle, placing the result into the specified
		/// buffer.
		/// </summary>
		/// <param name="buffer">the buffer to contain the encoded value
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store
		/// the encoded value
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this object class handle.
		/// </summary>
		/// <returns> a string representation of this object class handle
		/// </returns>
		System.String ToString();
	}
}
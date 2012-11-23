using System;
namespace Hla.Rti1516
{
	
	/// <summary> 
	/// Type-safe handle for an interaction class.  Generally these are created by the
	/// run-time infrastructure and passed to the user.
	/// </summary>
	public interface IInteractionClassHandle
	{
		/// <summary> 
		/// Checks this interaction class handle for equality with another.
		/// </summary>
		/// <param name="otherInteractionClassHandle">the other interaction class handle to compare
		/// this to
		/// </param>
		/// <returns> <code>true</code> if this refers to the same interaction class as other handle,
		/// <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherInteractionClassHandle);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this interaction class.
		/// </summary>
		/// <returns> a hash code corresponding to this interaction class
		/// </returns>
		int GetHashCode();
		
		/// <summary>
		///  Returns the encoded length of this interaction class handle.
		/// </summary>
		/// <returns> the encoded length of this interaction class handle (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this interaction class handle, placing the result into the specified
		/// buffer.
		/// </summary>
		/// <param name="buffer">the buffer to contain the encoded value
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store the value
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this interaction class handle.
		/// </summary>
		/// <returns> a string representation of this interaction class handle
		/// </returns>
		System.String ToString();
	}
}
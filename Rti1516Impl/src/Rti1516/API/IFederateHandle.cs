namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// Type-safe handle for a federate.  Generally these are created by the
	/// RTI and passed to the user.
	/// </summary>
	public interface IFederateHandle
	{
		/// <summary> 
		/// Tests this federate handle for equality with another.
		/// </summary>
		/// <param name="otherFederateHandle">the other federate handle to compare this to
		/// </param>
		/// <returns> <code>true</code> if this refers to the same federate as other
		/// handle, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object otherFederateHandle);
		
		/// <summary> 
		/// Computes and returns the hash code corresponding to this federate handle.
		/// </summary>
		/// <returns> the hash code corresponding to this federate handle
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns the encoded length of this federate handle.
		/// </summary>
		/// <returns> the encoded length of this federate handle (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this federate handle, placing the result into the specified
		/// byte array.
		/// </summary>
		/// <param name="buffer">the buffer to hold the encoded federate handle
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store the
		/// encoded handle
		/// </param>
		void  Encode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Returns a string representation of this federate handle.
		/// </summary>
		/// <returns> a string representation of this federate handle
		/// </returns>
		System.String ToString();
	}
}
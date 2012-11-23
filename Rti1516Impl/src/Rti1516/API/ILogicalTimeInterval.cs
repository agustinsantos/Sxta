namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An immutable logical time interval.
	/// </summary>
	public interface ILogicalTimeInterval : System.IComparable
	{
		/// <summary> 
		/// Checks whether this logical time interval has a zero length.
		/// </summary>
		/// <returns> <code>true</code> if this logical time interval has a
		/// zero length, <code>false</code> otherwise
		/// </returns>
		bool IsZero();
		
		/// <summary> 
		/// Checks whether this logical time interval has an epsilon length.
		/// </summary>
		/// <returns> <code>true</code> if this logical time interval has an
		/// epsilon length, <code>false</code> otherwise
		/// </returns>
		bool IsEpsilon();
		
		/// <summary> 
		/// Subtracts the specified logical time interval from this one,
		/// returning a new <code>ILogicalTimeInterval</code> representing the
		/// result.
		/// </summary>
		/// <param name="subtrahend">the logical time interval to Subtract from this one
		/// </param>
		/// <returns> a new <code>ILogicalTimeInterval</code> representing the
		/// result of the operation
		/// </returns>
		ILogicalTimeInterval Subtract(ILogicalTimeInterval subtrahend);

		/// <summary>
		///  Checks this logical time interval for equality with another.
		/// </summary>
		/// <param name="other">the <code>ILogicalTimeInterval</code> to compare this to
		/// </param>
		/// <returns> <code>true</code> if the two intervals are equal, <code>false</code>
		/// otherwise
		/// </returns>
		bool Equals(System.Object other);
		
		/// <summary> 
		/// Computes and returns a hash code corresponding to this logical time interval.
		/// </summary>
		/// <returns> a hash code corresponding to this logical time interval
		/// </returns>
		int GetHashCode();
		
		/// <summary> 
		/// Returns a string representation of this logical time interval.
		/// </summary>
		/// <returns> a string representation of this logical time interval
		/// </returns>
		System.String ToString();
		
		/// <summary> 
		/// Returns the encoded length of this logical time interval.
		/// </summary>
		/// <returns> the encoded length of this logical time interval (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this logical time interval, placing the result into the
		/// specified buffer.
		/// </summary>
		/// <param name="buffer">the buffer in which to place the encoded interval
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store
		/// the encoded interval
		/// </param>
		void  Encode(byte[] buffer, int offset);
	}
}
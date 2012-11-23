namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// An immutable logical time value.
	/// </summary>
	public interface ILogicalTime : System.IComparable
	{
		/// <summary> 
		/// Checks whether this represents an initial time.
		/// </summary>
		/// <returns> <code>true</code> if this represents an initial time,
		/// <code>false</code> otherwise
		/// </returns>
		bool IsInitial();
		
		/// <summary> 
		/// Checks whether this represents a final time.
		/// </summary>
		/// <returns> <code>true</code> if this represents a final time,
		/// <code>false</code> otherwise
		/// </returns>
		bool IsFinal();
		
		/// <summary> 
		/// Adds the specified time interval to this logical time, returning
		/// the result as a new <code>ILogicalTime</code>.
		/// </summary>
		/// <param name="val">the time interval to Add to this logical time
		/// </param>
		/// <returns> a new <code>ILogicalTime</code> that represents this logical time
		/// plus the specified time interval
		/// </returns>
		/// <exception cref="IllegalTimeArithmetic">  if the operation cannot be performed
		/// </exception>
		ILogicalTime Add(ILogicalTimeInterval val);
		
		/// <summary> 
		/// Subtracts the specified time interval from this logical time, returning
		/// the result as a new <code>ILogicalTime</code>.
		/// </summary>
		/// <param name="val">the time interval to Subtract from this logical time
		/// </param>
		/// <returns> a new <code>ILogicalTime</code> that represents this logical time
		/// minus the specified time interval
		/// </returns>
		/// <exception cref="IllegalTimeArithmetic">  if the operation cannot be performed
		/// </exception>
		ILogicalTime Subtract(ILogicalTimeInterval val);
		
		/// <summary> 
		/// Computes and returns the time interval between this logical time
		/// and another one.
		/// </summary>
		/// <param name="val">the other logical time
		/// </param>
		/// <returns> the logical time interval between this logical time and
		/// the other logical time
		/// </returns>
		ILogicalTimeInterval Distance(ILogicalTime val);
		
		/// <summary>
		///  Checks this logical time for equality with another.
		/// </summary>
		/// <param name="other">the other logical time to compare this to
		/// </param>
		/// <returns> <code>true</code> if the other object represents the
		/// same logical time as this one, <code>false</code> otherwise
		/// </returns>
		bool Equals(System.Object other);
		
		/// <summary>
		///  Computes and returns a hash code corresponding to this logical time.
		/// </summary>
		/// <returns> a hash code corresponding to this logical time
		/// </returns>
		int GetHashCode();
		
		/// <summary>
		///  Returns a string representation of this logical time.
		/// </summary>
		/// <returns> a string representation of this logical time
		/// </returns>
		System.String ToString();
		
		/// <summary> 
		/// Returns the encoded length of this logical time.
		/// </summary>
		/// <returns> the encoded length of this logical time (in bytes)
		/// </returns>
		int EncodedLength();
		
		/// <summary> 
		/// Encodes this logical time, placing the result into the specified
		/// buffer.
		/// </summary>
		/// <param name="buffer">the buffer in which to place the result
		/// </param>
		/// <param name="offset">the offset within the buffer at which to store the
		/// encoded value
		/// </param>
		void  Encode(byte[] buffer, int offset);
	}
}
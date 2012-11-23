namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>ILogicalTimeInterval</code>s.
	/// </summary>
	public interface ILogicalTimeIntervalFactory
	{
		/// <summary> 
		/// Decodes a logical time interval stored within the specified
		/// buffer, returning a corresponding new <code>ILogicalTimeInterval</code>.
		/// </summary>
		/// <param name="buffer">the buffer containing the encoded interval
		/// </param>
		/// <param name="offset">the offset within the buffer at which the encoded
		/// interval is stored
		/// </param>
		/// <returns> a new <code>ILogicalTimeInterval</code> corresponding to the
		/// encoded interval
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the time interval could not be decoded
		/// </exception>
		ILogicalTimeInterval Decode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Creates and returns a zero-length logical time interval.
		/// </summary>
		/// <returns> a new zero-length <code>ILogicalTimeInterval</code>
		/// </returns>
		ILogicalTimeInterval MakeZero();
		
		/// <summary> 
		/// Creates and returns an epsilon-length logical time interval.
		/// </summary>
		/// <returns> a new epsilon-length <code>ILogicalTimeInterval</code>
		/// </returns>
		ILogicalTimeInterval MakeEpsilon();
	}
}
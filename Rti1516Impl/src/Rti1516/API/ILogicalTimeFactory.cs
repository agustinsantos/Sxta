namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// A factory for <code>ILogicalTime</code>s.
	/// </summary>
	public interface ILogicalTimeFactory
	{
		/// <summary> 
		/// Decodes a logical time stored within the specified buffer,
		/// returning a <code>ILogicalTime</code> object corresponding to
		/// the decoded value.
		/// </summary>
		/// <param name="buffer">the buffer that contains the encoded value
		/// </param>
		/// <param name="offset">the offset within the buffer at which the
		/// encoded value is stored
		/// </param>
		/// <returns> a new <code>ILogicalTime</code> representing the
		/// decoded value
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the value could not be decoded
		/// </exception>
		ILogicalTime Decode(byte[] buffer, int offset);
		
		/// <summary> 
		/// Creates and returns an instance of the initial logical time.
		/// </summary>
		/// <returns> an instance of the initial logical time
		/// </returns>
		ILogicalTime MakeInitial();
		
		/// <summary> 
		/// Creates and returns an instance of the final logical time.
		/// </summary>
		/// <returns> an instance of the final logical time
		/// </returns>
		ILogicalTime MakeFinal();
	}
}
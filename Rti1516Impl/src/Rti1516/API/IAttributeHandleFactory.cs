namespace Hla.Rti1516
{
	using System;

	/// <summary> 
	/// The factory is used only (outside of the RTI) to Create <code>IAttributeHandle</code>s
	/// received as attribute or parameter values.
	/// </summary>
	public interface IAttributeHandleFactory
	{
		/// <summary> 
		/// Decodes an attribute handle, returning a new instance of
		/// <code>IAttributeHandle</code>.
		/// </summary>
		/// <param name="buffer">the encoded attribute handle
		/// </param>
		/// <param name="offset">the offset of the handle data in the buffer
		/// </param>
		/// <returns> an instance of <code>IAttributeHandle</code> corresponding
		/// to the encoded handle
		/// </returns>
		/// <exception cref="CouldNotDecode"> if the attribute handle could not be decoded
		/// </exception>
		/// <exception cref="FederateNotExecutionMember"> if the federate is not a member
		/// of the execution
		/// </exception>
		IAttributeHandle Decode(byte[] buffer, int offset);
	}
}